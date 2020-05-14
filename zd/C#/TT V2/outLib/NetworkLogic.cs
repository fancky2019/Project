using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace AuthCommon
{

    public class ZDMsg
    {
        public const string SUCCEED_ACK = "0";
        public const string SUCCEED_ACK_MSG = "0,OK";
        public const string SYSTEM_ERR_MSG = "System error, please check log!";


        public const int UNKNOW = 0;
        public const int REQUEST_MSG = 1;
        public const int RESPONSE_MSG = 2;

        public const int msgPos = NetworkLogic.DATA_OFFSET + NetworkLogic.CMD_LENGTH;
        public int msgCmd = 0;
        public int msgSize = 0;
        public byte[] rawData = null;

        public string ackCode = null;
        public string ackDesc = null;
        public int msgDirection = UNKNOW;

        public void parseRaw(byte[] raw)
        {
            // Parse network message format
            msgSize = BitConverter.ToInt32(raw, 0);
            msgCmd = BitConverter.ToInt32(raw, NetworkLogic.DATA_OFFSET);

            if (msgCmd % 2 == 0)
                msgDirection = RESPONSE_MSG;
            else
                msgDirection = REQUEST_MSG;

            rawData = raw;

            // Parse message of specific comunicate entity
            parseData(raw, msgPos, msgSize - NetworkLogic.CMD_LENGTH);
        }

        /// <summary>
        /// Defualt implemenation of parsing of specific comunicate entity
        /// </summary>
        /// <param name="rawData"></param>
        /// <param name="begin"></param>
        /// <param name="size"></param>
        protected virtual void parseData(byte[] rawData, int begin, int size)
        {
            if (msgDirection == RESPONSE_MSG)
            {
                parseResponseCd(rawData, begin, size);
            }
        }

        protected int parseResponseCd(byte[] rawData, int begin, int size)
        {
            int codeEndIndex = begin;
            byte endChar = (byte)';';
            for (; codeEndIndex < size + begin; codeEndIndex++)
            {
                if (rawData[codeEndIndex] == endChar)
                {
                    codeEndIndex++;
                    break;
                }
            }

            string result = System.Text.ASCIIEncoding.ASCII.GetString(rawData, begin, codeEndIndex - begin);
            string[] values = result.Split(',');
            ackCode = values[0];
            if (values.Length > 1)
                ackDesc = values[1];

            return codeEndIndex;
        }
    }

    public class UserStringMsg : ZDMsg
    {
        public string strData = null;

        private RijndaelManaged rijAlg = null;

        public UserStringMsg(){}

        public UserStringMsg(RijndaelManaged rijAlg)
        {
            this.rijAlg = rijAlg;
        }

        protected override void parseData(byte[] rawData, int begin, int size)
        {
            string msgContent = null;
            if (rijAlg != null)
            {
                msgContent = RijndaelImpl.DecryptStringFromBytes(rawData, begin, size, rijAlg);
                strData = msgContent;
                //Todo: if (msgDirection == RESPONSE_MSG)
                //???????????????????????????????????????????????????
            }
            else
            {
                msgContent = System.Text.ASCIIEncoding.ASCII.GetString(rawData, begin, size);

                int beginPos = begin;
                if (msgDirection == RESPONSE_MSG)
                    beginPos = parseResponseCd(rawData, begin, size);

                strData = System.Text.ASCIIEncoding.ASCII.GetString(rawData, beginPos, size - beginPos);
            }
        }
    }

    public class BytesArrayMsg : ZDMsg
    {
        public List<byte[]> data = null;

        private RijndaelManaged rijAlg = null;

        public BytesArrayMsg(){}

        public BytesArrayMsg(RijndaelManaged rijAlg)
        {
            this.rijAlg = rijAlg;
        }

        protected override void parseData(byte[] rawData, int begin, int size)
        {
            data = new List<byte[]>();

            int byteIndex = ZDMsg.msgPos;

            if (msgDirection == RESPONSE_MSG)
                byteIndex = parseResponseCd(rawData, begin, size);

            int byteArrCount = BitConverter.ToInt32(rawData, byteIndex);
            byteIndex += 4; // count of raw byte array

            for (int i = 0; i < byteArrCount; i++)
            {
                int dataLen = BitConverter.ToInt32(rawData, byteIndex);
                byteIndex += 4; // size of each raw byte array

                byte[] rawByte = new byte[dataLen];
                Array.Copy(rawData, byteIndex, rawByte, 0, dataLen);
                data.Add(rawByte);
                byteIndex += dataLen;
            }
        }
    }

    public class NetworkLogic
    {
        public const int DATA_OFFSET = 4;
        public const int CMD_LENGTH = 4;
        private static byte[] dataBuffer = new byte[2 * 1024];


        public static void sendMsg(Socket socket, int networkCmd, string data)
        {
            int msgSize = CMD_LENGTH + data.Length;
            // Message size field
            byte[] byteOfMsgSize = BitConverter.GetBytes(msgSize);
            Array.Copy(byteOfMsgSize, dataBuffer, DATA_OFFSET);

            // Command field
            byte[] byteCommand = BitConverter.GetBytes(networkCmd);
            Array.Copy(byteCommand, 0, dataBuffer, DATA_OFFSET, CMD_LENGTH);

            ASCIIEncoding.ASCII.GetBytes(data, 0, data.Length, dataBuffer, ZDMsg.msgPos);
            syncSend(socket, dataBuffer, 0, ZDMsg.msgPos + data.Length);
        }

        public static void sendMsg(Socket socket, int networkCmd, List<byte[]> dataList)
        {
            int msgSize = CMD_LENGTH;
            msgSize += 4; // for count of list

            for (int i = 0; i < dataList.Count; i++)
                msgSize = msgSize + 4 + dataList[i].Length;

            // Message size field
            byte[] byteOfMsgSize = BitConverter.GetBytes(msgSize);
            Array.Copy(byteOfMsgSize, dataBuffer, DATA_OFFSET);

            // Command field
            byte[] byteCommand = BitConverter.GetBytes(networkCmd);
            Array.Copy(byteCommand, 0, dataBuffer, DATA_OFFSET, CMD_LENGTH);

            // List size field
            byte[] byteOfListSize = BitConverter.GetBytes(dataList.Count);
            Array.Copy(byteOfListSize, 0, dataBuffer, ZDMsg.msgPos, 4);

            syncSend(socket, dataBuffer, 0, ZDMsg.msgPos + 4);

            for (int i = 0; i < dataList.Count; i++)
            {
                byte[] byteOfDataSize = BitConverter.GetBytes(dataList[i].Length);
                syncSend(socket, byteOfDataSize, 0, 4);
                syncSend(socket, dataList[i], 0, dataList[i].Length);
            }

        }

        /// <summary>
        /// Note the data maybe include unused bytes at the end
        /// </summary>
        /// <param name="networkCmd"></param>
        /// <param name="data"></param>
        public static void sendMsg(Socket socket, int networkCmd, byte[] data)
        {
            int msgSize = CMD_LENGTH + data.Length;
            // Message size field
            byte[] byteOfMsgSize = BitConverter.GetBytes(msgSize);
            Array.Copy(byteOfMsgSize, dataBuffer, DATA_OFFSET);

            // Command field
            byte[] byteCommand = BitConverter.GetBytes(networkCmd);
            Array.Copy(byteCommand, 0, dataBuffer, DATA_OFFSET, CMD_LENGTH);

            syncSend(socket, dataBuffer, 0, ZDMsg.msgPos);
            syncSend(socket, data, 0, data.Length);

        }


        /// <summary>
        /// Note the data maybe include unused bytes at the end
        /// </summary>
        /// <param name="networkCmd"></param>
        /// <param name="data"></param>
        public static ZDMsg readOneMsg(Socket socket)
        {
            ZDMsg msg = new ZDMsg();
            byte[] raw = syncReceive(socket);
            msg.parseRaw(raw);
            return msg;
        }


        /// <summary>
        /// Note the data maybe include unused bytes at the end
        /// </summary>
        /// <param name="networkCmd"></param>
        /// <param name="data"></param>
        public static void readOneMsg(Socket socket,ZDMsg zdMsg)
        {
            byte[] raw = syncReceive(socket);
            zdMsg.parseRaw(raw);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="socket"></param>
        ///// <param name="data">include size info</param>
        //public static void syncSend(Socket socket, byte[] data)
        //{
        //    int size = BitConverter.ToInt32(data, 0) + DATA_OFFSET;
        //    int dataSent = 0;
        //    while (dataSent < size)
        //    {
        //        int count = socket.Send(data, dataSent, size - dataSent, SocketFlags.None);
        //        dataSent += count;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="data">does not include size info</param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        public static void syncSend(Socket socket, byte[] data, int offset, int size)
        {
            int dataSent = offset;
            size += offset;
            while (dataSent < size)
            {
                int count = socket.Send(data, dataSent, size - dataSent, SocketFlags.None);
                dataSent += count;
            }
        }

        private static byte[] readBuffer = new byte[1024];
        private static Parser networkParser = new NewMsgProctoclParser();

        public static byte[] syncReceive(Socket socket)
        {
            try
            {
                while (true)
                {
                    if (socket.Poll(1000000, SelectMode.SelectRead)) // one-second timeout
                    {
                        int bytesRead = socket.Receive(readBuffer);
                        if (0 == bytesRead)
                            throw new SocketException(System.Convert.ToInt32(SocketError.ConnectionReset));
                        //parser_.AddToStream(System.Text.Encoding.UTF8.GetString(readBuffer_, 0, bytesRead));
                        networkParser.AddToParser(readBuffer, 0, bytesRead);
                    }
                    else
                    {
                        //throw new QuickFIXException("Initiator timed out while reading socket");
                    }

                    byte[] rawMsg = null;
                    if (networkParser.getRawMsg(out rawMsg))
                        return rawMsg;
                }
            }
            catch (System.ObjectDisposedException e)
            {
                // this exception means socket_ is already closed when poll() is called
            }
            
            return null;
        }


        public static void sendDataByEncrtyped(RecvStateObject orso, string strData)
        {
            byte[] dataContent = orso.safeGurad.aesUtil.AESEncrypt(strData);

            StringBuilder sb = DataPool.getStringBuilder();
            sb.Append("{(len=").Append(dataContent.Length).Append(")");

            byte[] header = System.Text.ASCIIEncoding.ASCII.GetBytes(sb.ToString());
            DataPool.recycleStringBuilder(sb);

            lock (orso.workSocket)
            {
                NetworkLogic.syncSend(orso.workSocket, header, 0, header.Length);
                NetworkLogic.syncSend(orso.workSocket, dataContent, 0, dataContent.Length);

                //char of right brace }
                header[0] = 125;
                NetworkLogic.syncSend(orso.workSocket, header, 0, 1);
            }
        }

        public static void sendEncryptedData(Socket socket, byte[] dataContent)
        {

            StringBuilder sb = DataPool.getStringBuilder();
            sb.Append("{(len=").Append(dataContent.Length).Append(")");

            byte[] header = System.Text.ASCIIEncoding.ASCII.GetBytes(sb.ToString());
            DataPool.recycleStringBuilder(sb);

            lock (socket)
            {
                NetworkLogic.syncSend(socket, header, 0, header.Length);
                NetworkLogic.syncSend(socket, dataContent, 0, dataContent.Length);

                //char of right brace }
                header[0] = 125;
                NetworkLogic.syncSend(socket, header, 0, 1);
            }
        }

        public static byte[] ObjectStringToBytes2(string inPut)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("{(len=")
                .Append(Encoding.UTF8.GetByteCount(inPut))
                .Append(")")
                .Append(inPut)
                .Append("}");

            byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return bytes;
        }

        public static void syncSendOnCompress(Socket socket, byte[] data, int offset, int size)
        {
            byte[] cb = null;
            MemoryStream ms = new MemoryStream();
            ms.WriteByte(1);
            using (DeflateStream zip = new System.IO.Compression.DeflateStream(ms, CompressionMode.Compress))
            {
                zip.Write(data, offset, size);
                zip.Close();
                cb = ms.ToArray();
            }
            ms.Close();

            sendEncryptedData(socket, cb);
        }
    }

    public interface Parser
    {
        void AddToParser(byte[] data, int offset, int count);
        bool getRawMsg(out byte[] rawMsg);
    }

    public class NewMsgProctoclParser : Parser
    {
        private byte[] buffer_ = new byte[1024 * 5];
        private int msgBeginindex = 0;
        private int spaceAvailabeIndex = 0;

        public void AddToParser(byte[] data, int offset, int count)
        {
            if (buffer_.Length - spaceAvailabeIndex < count)
            {
                int lengthOfPartMsg = spaceAvailabeIndex - msgBeginindex;
                if (buffer_.Length >= lengthOfPartMsg + count)
                {
                    // Shift partial data of current message
                    Array.Copy(buffer_, msgBeginindex, buffer_, 0, lengthOfPartMsg);
                    Array.Copy(data, 0, buffer_, lengthOfPartMsg, count);
                    msgBeginindex = 0;
                    spaceAvailabeIndex = lengthOfPartMsg + count;
                }
                else
                {
                    // Todo: exception or expand buffer_ size
                    throw new Exception("Buffer of Parser is not enough!");
                }
            }
            else
            {
                Array.Copy(data, 0, buffer_, spaceAvailabeIndex, count);
                spaceAvailabeIndex += count;
            }
        }

        public bool getRawMsg(out byte[] rawMsg)
        {
            rawMsg = null;

            // Message lenth part is not enougth
            int dataLength = spaceAvailabeIndex - msgBeginindex;
            if (dataLength < NetworkLogic.DATA_OFFSET) return false;

            // Message data part is not enougth
            int size = BitConverter.ToInt32(buffer_, msgBeginindex);
            int msgLength = size + NetworkLogic.DATA_OFFSET;
            if (dataLength < size + NetworkLogic.DATA_OFFSET) return false;

            rawMsg = new byte[msgLength];
            Array.Copy(buffer_, msgBeginindex, rawMsg, 0, msgLength);
            msgBeginindex += msgLength;
            return true;
        }
    }


    //State object for reading client data asynchronously
    public class ReceiveStateObject
    {

        // Client  socket.
        public Socket workSocket = null;
        // Receive buffer.
        public byte[] innerBuffer = new byte[16];
        public byte[] buffer = null;

        public int bytesReceive = 0;
        public int msgSize = 0;
        public bool isForBodyLength = true;

        public object implProcessState = null;
    }

    public abstract class ClientWaiterNew2
    {
        private Socket listenSocket = null;
        private ManualResetEvent allDone = null;

        private Thread waiterThread = null;
        private volatile bool stopFlag = true;
        private volatile bool testFlag = false;

        public ClientWaiterNew2()
        {
        }

        public ClientWaiterNew2(string hostIP, string hostPort)
        {
            allDone = new ManualResetEvent(false);

            int ftServerPort = Convert.ToInt32(hostPort);
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(hostIP), ftServerPort);
            listenSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(ipe);

            waiterThread = new Thread(new ThreadStart(startListen));
        }


        /// <summary>
        /// When set as test mode, clien does not need to subscribe/unscribe
        /// </summary>
        /// <param name="flag"></param>
        public void setTestMode(bool flag)
        {
            testFlag = flag;
        }

        private void startListen()
        {
            listenSocket.Listen(100);

            while (!stopFlag)
            {
                allDone.Reset();

                // Start an asynchronous socket to listen for connections.
                Console.WriteLine("Waiting for a connection...");
                listenSocket.BeginAccept(new AsyncCallback(AcceptCallback), listenSocket);

                // Wait until a connection is made before continuing.
                allDone.WaitOne();
            }
        }

        public void start()
        {
            stopFlag = false;
            waiterThread.Start();
        }

        public void stop()
        {
            stopFlag = true;
            allDone.Set();
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            Socket listenSocket = (Socket)ar.AsyncState;
            Socket socket = listenSocket.EndAccept(ar);

            // Signal the main thread to continue.
            allDone.Set();

            Console.WriteLine(socket.RemoteEndPoint.ToString() + " connected!");

            if (testFlag)
            {
                //dispatcher.addMarketClient(socket, contracts);
                int a = 0;
            }
            else
            {
                // Create the state object.
                ReceiveStateObject rso = new ReceiveStateObject();
                rso.workSocket = socket;
                rso.bytesReceive = 0;
                rso.msgSize = NetworkLogic.DATA_OFFSET;
                rso.isForBodyLength = true;
                onSocketCreated(rso);

                SocketError socketErr;
                socket.BeginReceive(rso.innerBuffer, 0, rso.msgSize, SocketFlags.None, out socketErr, new AsyncCallback(ReadCallback), rso);
            }
        }

        public virtual void onSocketCreated(ReceiveStateObject rso)
        {
            Socket socket = rso.workSocket;
            IPEndPoint endPoint = (IPEndPoint)socket.RemoteEndPoint;
            //rso.remoteIPPort = endPoint.Address.ToString() + ":" + endPoint.Port;
        }

        public virtual void onSocketClosed(ReceiveStateObject rso)
        {
            // Do nothing
        }

        public abstract void handleClientMsg(Socket socket, byte[] msg, ReceiveStateObject rso);
        //public abstract void handleClientMsg(Socket socket, byte[] msg);

        /// <summary>
        /// Data packet format from client is:
        /// [Data(4bytes)][Command(1byte)][content]
        /// Command: 1 - connect
        ///          2 - disconnect
        ///          3 - get all available contracts
        ///          4 - subscribe contracts
        ///          5 - unsbuscribe contracts
        /// </summary>
        /// <param name="ar"></param>
        public void ReadCallback(IAsyncResult ar)
        {
            ReceiveStateObject rso = null;
            try
            {
                rso = (ReceiveStateObject)ar.AsyncState;
                Socket socket = rso.workSocket;
                // Read data from the remote device.
                SocketError socketErr;
                int currentReceive = socket.EndReceive(ar, out socketErr);
                if (currentReceive == 0)
                {
                    onSocketClosed(rso);
                    //if (networkLogger != null)
                    //    networkLogger.log(ZDLogger.LVL_CRITCAL, "Client closed socket!");
                    return;
                }

                rso.bytesReceive += currentReceive;

                if (rso.bytesReceive < rso.msgSize)
                {
                    if (rso.isForBodyLength)
                        socket.BeginReceive(rso.innerBuffer, rso.bytesReceive, rso.msgSize - rso.bytesReceive, SocketFlags.None, out socketErr, new AsyncCallback(ReadCallback), rso);
                    else
                        socket.BeginReceive(rso.buffer, NetworkLogic.DATA_OFFSET + rso.bytesReceive, rso.msgSize - rso.bytesReceive, SocketFlags.None, out socketErr, new AsyncCallback(ReadCallback), rso);
                }
                else
                {
                    // For getting body length
                    if (rso.isForBodyLength)
                    {
                        int msgLength = BitConverter.ToInt32(rso.innerBuffer, 0);
                        byte[] buffer = DataPool.getByteData(msgLength + NetworkLogic.DATA_OFFSET);
                        Array.Copy(rso.innerBuffer, buffer, NetworkLogic.DATA_OFFSET);

                        rso.buffer = buffer;
                        rso.msgSize = msgLength;
                        rso.bytesReceive = 0;
                        rso.isForBodyLength = false;

                        socket.BeginReceive(buffer, NetworkLogic.DATA_OFFSET, msgLength, SocketFlags.None, out socketErr, new AsyncCallback(ReadCallback), rso);
                    }
                    // For getting whole message
                    else
                    {
                        handleClientMsg(rso.workSocket, rso.buffer, rso);
                        DataPool.recycleByteData(rso.buffer);

                        rso.isForBodyLength = true;
                        rso.msgSize = NetworkLogic.DATA_OFFSET;
                        rso.bytesReceive = 0;
                        socket.BeginReceive(rso.innerBuffer, 0, rso.msgSize, SocketFlags.None, out socketErr, new AsyncCallback(ReadCallback), rso);
                    }
                }
            }
            catch (SocketException se)
            {
                try
                {
                    //SendWorker sendWorker = dispatcher.getSendWorker(rso.workSocket);
                    //if (sendWorker != null)
                    //    sendWorker.socketBrokenSiganlFromRecevieThread();

                    DataPool.recycleByteData(rso.buffer);
                    Console.WriteLine("Socket with {0} is closed!", rso.workSocket.RemoteEndPoint.ToString());
                    rso.workSocket.Close();
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception e)
            {
            }
        }

    }

    public abstract class ClientWaiterNew
    {
        private Socket listenSocket = null;
        private ManualResetEvent allDone = null;

        private Thread waiterThread = null;
        private volatile bool stopFlag = true;
        private ZDLogger networkLogger = null;
        private ZDLogger errorLogger = null;

        public ClientWaiterNew()
        {
        }

        public ClientWaiterNew(string hostIP, string hostPort, ZDLogger networkLogger, ZDLogger errorLogger)
        {
            this.networkLogger = networkLogger;
            this.errorLogger = errorLogger;
            allDone = new ManualResetEvent(false);

            int ftServerPort = Convert.ToInt32(hostPort);
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(hostIP), ftServerPort);
            listenSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(ipe);

            waiterThread = new Thread(new ThreadStart(startListen));
        }

        private void startListen()
        {
            listenSocket.Listen(100);

            while (!stopFlag)
            {
                allDone.Reset();

                // Start an asynchronous socket to listen for connections.
                Console.WriteLine("Waiting for a connection...");
                listenSocket.BeginAccept(new AsyncCallback(AcceptCallback), listenSocket);

                // Wait until a connection is made before continuing.
                allDone.WaitOne();
            }
        }

        public void start()
        {
            stopFlag = false;
            waiterThread.Start();
        }

        public void stop()
        {
            stopFlag = true;
            allDone.Set();
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            Socket listenSocket = (Socket)ar.AsyncState;
            Socket socket = listenSocket.EndAccept(ar);

            // Signal the main thread to continue.
            allDone.Set();

            string logMsg = socket.RemoteEndPoint.ToString() + " connected!";
            Console.WriteLine(logMsg);
            if (networkLogger != null)
                networkLogger.log(ZDLogger.LVL_CRITCAL, logMsg);

            // Create the state object.
            RecvStateObject orso = new RecvStateObject();
            orso.workSocket = socket;
            onSocketCreated(orso);
            // Give child class chance to assign an Parser implementation and if does not assign, assgin one default
            if (orso.parser == null)
                orso.parser = new NewMsgProctoclParser();

            socket.BeginReceive(orso.buffer, 0, orso.buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), orso);
        }

        public virtual void onSocketCreated(RecvStateObject orso)
        {
            Socket socket = orso.workSocket;
            IPEndPoint endPoint = (IPEndPoint)socket.RemoteEndPoint;
            orso.remoteIPPort = endPoint.Address.ToString() + ":" + endPoint.Port;
        }

        public virtual void onSocketClosed(RecvStateObject orso)
        {
            // Do nothing
        }

        public abstract void handleClientMsg(Socket socket, byte[] msg, RecvStateObject rso);

        /// <summary>
        /// Data packet format from client is:
        /// [Data(4bytes)][Command(1byte)][content]
        /// Command: 1 - connect
        ///          2 - disconnect
        ///          3 - get all available contracts
        ///          4 - subscribe contracts
        ///          5 - unsbuscribe contracts
        /// </summary>
        /// <param name="ar"></param>
        public void ReadCallback(IAsyncResult ar)
        {
            RecvStateObject orso = (RecvStateObject)ar.AsyncState;
            Socket socket = orso.workSocket;

            try
            {
                // Read data from the remote device.
                SocketError socketErr;
                int currentReceive = socket.EndReceive(ar, out socketErr);
                if (currentReceive == 0)
                {
                    onSocketClosed(orso);
                    if (errorLogger != null)
                        errorLogger.log(ZDLogger.LVL_CRITCAL, "Client closed peer socket!");

                    try
                    {
                        socket.Close();
                    }
                    catch (Exception ex)
                    {
                        errorLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
                    }

                    return;
                }

                if (networkLogger != null)
                    networkLogger.log(ZDLogger.LVL_CRITCAL, orso.remoteIPPort + ": "+ System.Text.ASCIIEncoding.ASCII.GetString(orso.buffer, 0, currentReceive));

                orso.parser.AddToParser(orso.buffer, 0, currentReceive);
                byte[] raw;
                while (orso.parser.getRawMsg(out raw))
                {
                    string msg = System.Text.ASCIIEncoding.ASCII.GetString(raw, NetInfoParser.GOOD_PARSER_OFFSET, raw.Length - NetInfoParser.GOOD_PARSER_OFFSET);
                    //handleClientMsg(socket, msg, orso);
                }
                socket.BeginReceive(orso.buffer, 0, orso.buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), orso);
            }
            catch (SocketException se)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, se.ToString());

                try
                {
                    string msg = string.Format("Socket with {0} is closed!", orso.workSocket.RemoteEndPoint.ToString());
                    errorLogger.log(ZDLogger.LVL_ERROR, msg);
                }
                catch (Exception ex)
                {
                    errorLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
                }

                onSocketClosed(orso);

                try
                {
                    orso.workSocket.Close();
                }
                catch (Exception ex)
                {
                    errorLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
                }
            }
            catch (Exception e)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, e.ToString());
                socket.BeginReceive(orso.buffer, 0, orso.buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), orso);
            }
        }
    }


    public enum SessionWorkState
    {
        WAIT_SYMMETRIC_KEY,
        SYMMETRIC_KEY_READY
    }

    public class SafeGuard
    {
        public SessionWorkState sessionWorkSate = SessionWorkState.WAIT_SYMMETRIC_KEY;
        public AESUtil aesUtil = null;
        
        public void clearResource()
        {
            if (aesUtil != null)
            {
                aesUtil.Dispose();
                aesUtil = null;
            }
        }

    }


    public abstract class ClientWaiter
    {
        private Socket listenSocket = null;
        private ManualResetEvent allDone = null;

        private Thread waiterThread = null;
        public volatile bool stopFlag = true;
        public ZDLogger networkLogger = null;
        public ZDLogger errorLogger = null;

        private ObjectPool<byte[]> pinBufferPool = null;

        private const string heartbeatMsg = "TEST0001@@@@@@@@&";

        public RSAUtil rsaUtil = null;

        private string hostIP = null;
        private string hostPort = null;

        public bool hasCompressData = false;

        public ClientWaiter()
        {
        }

        public ClientWaiter(string hostIP, string hostPort, ZDLogger networkLogger, ZDLogger errorLogger)
        {
            this.networkLogger = networkLogger;
            this.errorLogger = errorLogger;

            this.hostIP = hostIP;
            this.hostPort = hostPort;

            allDone = new ManualResetEvent(false);

            // Added on 20171009 -begin
            pinBufferPool = new ObjectPool<byte[]>(() => new byte[1024]);
            for (int i = 0; i < 6000; i++)
            {
                pinBufferPool.PutObject(new byte[1024]);
            }
            // Added on 20171009 -end

            waiterThread = new Thread(new ThreadStart(startListen));
        }

        public ClientWaiter(string hostIP, string hostPort, ZDLogger networkLogger, ZDLogger errorLogger, int socketCnt, int pinBufferSize = 1024)
        {
            this.networkLogger = networkLogger;
            this.errorLogger = errorLogger;

            this.hostIP = hostIP;
            this.hostPort = hostPort;

            allDone = new ManualResetEvent(false);



            pinBufferPool = new ObjectPool<byte[]>(() => new byte[pinBufferSize]);
            for (int i = 0; i < socketCnt * 2; i++)
            {
                pinBufferPool.PutObject(new byte[pinBufferSize]);
            }


            int ftServerPort = Convert.ToInt32(hostPort);
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(hostIP), ftServerPort);
            listenSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(ipe);

            waiterThread = new Thread(new ThreadStart(startListen));
        }



        public void setRSAKeyFile(string keyFileName)
        {
            try
            {
                rsaUtil = new RSAUtil(keyFileName);
            }
            catch (Exception ex)
            {
                if(errorLogger != null)
                    errorLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
            }
        }

        private void startListen()
        {

            try
            {
                if(listenSocket == null)
                {
                    int ftServerPort = Convert.ToInt32(hostPort);
                    IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(hostIP), ftServerPort);
                    listenSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    listenSocket.Bind(ipe);
                }

                listenSocket.Listen(100);

                while (!stopFlag)
                {
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.
                    Console.WriteLine("Waiting for a connection...");
                    listenSocket.BeginAccept(new AsyncCallback(AcceptCallback), listenSocket);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }

            }catch(Exception ex)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
            }
        }

        public void stopListening()
        {
            errorLogger.log(ZDLogger.LVL_CRITCAL, ">>>>stopListening() is called");

            stopFlag = true;

            try
            {
                listenSocket.Close();
            }
            catch (Exception ex)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
            }

            listenSocket = null;
        }
        
        public void restartListening()
        {
            errorLogger.log(ZDLogger.LVL_CRITCAL, ">>>>restartListening() is called");

            stopFlag = false;
            waiterThread = new Thread(new ThreadStart(startListen));
            waiterThread.Start();
        }

        public void start()
        {
            stopFlag = false;
            waiterThread.Start();
        }

        public void stop()
        {
            stopFlag = true;
            allDone.Set();
            try
            {
                if (listenSocket != null)
                    listenSocket.Close();
            }
            catch (Exception e)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, "step1: " + e.ToString());
            }

            //rsaUtil
            try
            {
                if (rsaUtil != null)
                    rsaUtil.Dispose();
            }
            catch (Exception e)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, "step2: " + e.ToString());

            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                Socket listenSocket = (Socket)ar.AsyncState;
                Socket socket = listenSocket.EndAccept(ar);

                //// Only for test, not for production -begin
                //socket.SendBufferSize = 80 * 1024;
                //socket.ReceiveBufferSize = 80 * 1024;
                //// Only for test, not for production -end

                socket.NoDelay = true;
                //Added on 20180126 -begin
                socket.SendTimeout = 5000;
                //Added on 20180126 -end

                //added on 20180104 -begin
                byte[] inOptionValues = new byte[12];
                BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);//是否启用Keep-Alive
                BitConverter.GetBytes((uint)3000).CopyTo(inOptionValues, 4);//多长时间开始第一次探测
                BitConverter.GetBytes((uint)3000).CopyTo(inOptionValues, 8);//探测时间间隔
                socket.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
                //added on 20180104 -end

                // Signal the main thread to continue.
                allDone.Set();

                string logMsg = socket.RemoteEndPoint.ToString() + " connected!";
                Console.WriteLine(logMsg);
                if (networkLogger != null)
                    networkLogger.log(ZDLogger.LVL_CRITCAL, logMsg);

                // Create the state object.
                RecvStateObject orso = new RecvStateObject();

                // Added on 20171009 -begin
                orso.buffer = pinBufferPool.GetObject();
                orso.sendBuffer = pinBufferPool.GetObject();
                // Added on 20171009 -end

                orso.workSocket = socket;
                onSocketCreated(orso);

                //Added on 20171228 -begin
                if (!orso.isCloseSocketSetted)
                {
                //Added on 20171228 -end

                    // Give child class chance to assign an Parser implementation and if does not assign, assgin one default
                    if (orso.parser == null)
                    {
                        //orso.parser = new NetInfoParser();
                        NetInfoParserV2 parser = new NetInfoParserV2();
                        parser.setErrLogger(networkLogger);
                        orso.parser = parser;
                    }

                    socket.BeginReceive(orso.buffer, 0, orso.buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), orso);
                }
            }
            catch (ObjectDisposedException ode)
            {
                errorLogger.log(ZDLogger.LVL_CRITCAL, "Server stopped listening socket!");
            }
            catch (Exception e)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, e.ToString());
            }
        }

        /// <summary>
        /// Server implementation want to close sokcet connection intentionly
        /// </summary>
        /// <param name="orso"></param>
        public void closeSocketConnection(RecvStateObject orso)
        {
            try
            {
                // in fact is onSocketClosing()
                onSocketClosed(orso);
                errorLogger.log(ZDLogger.LVL_CRITCAL, "closeSocketConnection#1: close socket actively on demand: " + orso.remoteIPPort);

            }
            catch (Exception iex)
            {
                errorLogger.log(ZDLogger.LVL_CRITCAL, "closeSocketConnection#2: " + iex.ToString());
            }


            try
            {
                orso.workSocket.Shutdown(SocketShutdown.Both);
                orso.workSocket.Close();
            }
            catch (Exception iex)
            {
                errorLogger.log(ZDLogger.LVL_CRITCAL, "closeSocketConnection#3: " + iex.ToString());
            }

            orso.isCloseSocketSetted = true;
        }

        public virtual void onSocketCreated(RecvStateObject orso)
        {
            Socket socket = orso.workSocket;
            IPEndPoint endPoint = (IPEndPoint)socket.RemoteEndPoint;
            orso.remoteIPPort = endPoint.Address.ToString() + ":" + endPoint.Port;
        }

        public virtual void onSocketClosed(RecvStateObject orso)
        {
            // Do nothing
        }

        public abstract void handleClientMsg(Socket socket, string msg, RecvStateObject rso);
        //public abstract void handleClientMsg(Socket socket, byte[] msg);

        /// <summary>
        /// Data packet format from client is:
        /// [Data(4bytes)][Command(1byte)][content]
        /// Command: 1 - connect
        ///          2 - disconnect
        ///          3 - get all available contracts
        ///          4 - subscribe contracts
        ///          5 - unsbuscribe contracts
        /// </summary>
        /// <param name="ar"></param>
        public void ReadCallback(IAsyncResult ar)
        {
            RecvStateObject orso = (RecvStateObject)ar.AsyncState;
            Socket socket = orso.workSocket;

            try
            {
                // Read data from the remote device.
                SocketError socketErr;
                int currentReceive = socket.EndReceive(ar, out socketErr);
                if (currentReceive == 0)
                {

                    #region handling of socket receving 0
                    if (errorLogger != null)
                        errorLogger.log(ZDLogger.LVL_CRITCAL, orso.remoteIPPort + " ReadCallback(), recv 0, recv0Count:" + orso.recv0Count);

                    // Added on 20181120 --begin
                    bool breakFlag = false;

                    //Add on 20190426 -begin
                    if (orso.recv0Count == 0)
                    {
                        orso.recv0Timestamp = DateTime.Now;
                    }
                    else
                    {
                        //if (DateTime.Now.Subtract(orso.recv0Timestamp).TotalSeconds < 1 && orso.recv0Count > 5 || orso.recv0Count > 10)
                        // YW-148 对应调整
                        if (orso.recv0Count > 10)
                            breakFlag = true;
                    }

                    orso.recv0Count++;
                    //Add on 20190426 -end


                    // Removed on 2020-02-26 -begin
                    // YW-148 对应调整
                    /*
                    try
                    {
                        byte[] heartbeatData = null;
                        if (orso.isEncryptedSession)
                        {
                            heartbeatData = orso.safeGurad.aesUtil.AESEncrypt(heartbeatMsg);
                            NetworkLogic.sendEncryptedData(socket, heartbeatData);
                        }
                        else
                        {
                            heartbeatData = NetworkLogic.ObjectStringToBytes2(heartbeatMsg);
                            NetworkLogic.syncSend(socket, heartbeatData, 0, heartbeatData.Length);
                        }

                        //avoid heartbeat flood
                        Thread.Sleep(300);

                    }
                    catch (SocketException se)
                    {
                        breakFlag = true;
                    }
                    catch (Exception exx)
                    {
                        if (errorLogger != null)
                        {
                            errorLogger.log(ZDLogger.LVL_CRITCAL, orso.remoteIPPort + " ReadCallback()#80, recv 0");
                            errorLogger.log(ZDLogger.LVL_CRITCAL, exx.ToString());
                        }
                    }
                    */

                    Thread.Sleep(200);
                    // Removed on 2020-02-26 -end

                    if (breakFlag)
                    {
                        // Added on 20181120 --end


                        try
                        {
                            if (orso.buffer != null)
                            {
                                byte[] temp = orso.buffer;
                                pinBufferPool.PutObject(temp);

                                orso.buffer = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#21," + ex.ToString());
                        }

                        try
                        {
                            if (orso.sendBuffer != null)
                            {
                                byte[] temp = orso.sendBuffer;
                                pinBufferPool.PutObject(temp);

                                orso.sendBuffer = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#22," + ex.ToString());
                        }

                        //Added on 20171228 -begin
                        if (orso.isCloseSocketSetted)
                        {
                            return;
                        }
                        //Added on 20171228 -end

                        try
                        {
                            onSocketClosed(orso);
                        }
                        catch (Exception ex)
                        {
                            errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#23," + ex.ToString());
                        }

                        //Changed on 20171215 -begin
                        // moved from the begining of this block to here, allow user can access socket object to record something
                        try
                        {
                            socket.Shutdown(SocketShutdown.Both);
                            socket.Close();
                        }
                        catch (Exception ex)
                        {
                            errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#20," + ex.ToString());
                        }
                        //Changed on 20171215 -end


                        try
                        {
                            orso.safeGurad.clearResource();
                        }
                        catch (Exception ex)
                        {
                            errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#24," + ex.ToString());
                        }
                        
                    }
                    else
                    {
                        socket.BeginReceive(orso.buffer, 0, orso.buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), orso);
                    }

                    return;
                    #endregion
                }

                // changed on 20171023 -begin
                // mantis:1085
                string strData = null;
                // no matter is encrypted session or not, first piece of network data is clear text
                // so, even the encrypted session, first piece of data comes into following condition
                if (!orso.isEncryptedSession)
                {
                    //*
                    strData = System.Text.UTF8Encoding.UTF8.GetString(orso.buffer, 0, currentReceive);
                    if (networkLogger != null)
                        networkLogger.log(ZDLogger.LVL_TRACE, orso.remoteIPPort + " " + strData);
                    //*/
                }

                if (orso.lastReceiveTick == 0)
                {
                    #region first piece of network data checking
                    

                    if (strData!= null && !strData.StartsWith("{("))
                    {
                        try
                        {
                            if (orso.buffer != null)
                            {
                                byte[] temp = orso.buffer;
                                pinBufferPool.PutObject(temp);

                                orso.buffer = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#31," + ex.ToString());
                        }

                        try
                        {
                            if (orso.sendBuffer != null)
                            {
                                byte[] temp = orso.sendBuffer;
                                pinBufferPool.PutObject(temp);

                                orso.sendBuffer = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#32," + ex.ToString());
                        }

                        //Added on 20180109 -begin
                        //To notify decendent to unbook the connnected message
                        try
                        {
                            onSocketClosed(orso);
                        }
                        catch (Exception ex)
                        {
                            errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#33," + ex.ToString());
                        }
                        //Added on 20180109 -end
                        
                        return;
                    }
                    else
                    {
                        orso.lastReceiveTick = DateTime.Now.Ticks;
                    }

                    #endregion
                }

                // changed on 20171023 -end

                orso.parser.AddToParser(orso.buffer, 0, currentReceive);
                byte[] raw;
                //if (orso.parser.getRawMsg(out raw))
                while (orso.parser.getRawMsg(out raw))
                {

                    //Added on 20180103 -begin
                    if (orso.isEncryptedSession)
                    {
                        #region secure session logic
                        // Todo: check heartbeat message
                        // Warning: length 26 come from Andriod, 25 come from iOS
                        if (raw.Length == 26 || raw.Length == 25)
                        {
                            string text = System.Text.ASCIIEncoding.ASCII.GetString(raw, NetInfoParser.GOOD_PARSER_OFFSET, raw.Length - NetInfoParser.GOOD_PARSER_OFFSET);
                            if(text.StartsWith("TEST0001")){
                                handleClientMsg(socket, text, orso);
                                continue;
                            }
                        }

                        string clearText = null;
                        try
                        {
                            clearText = decryptData(orso, raw, NetInfoParser.GOOD_PARSER_OFFSET, raw.Length - NetInfoParser.GOOD_PARSER_OFFSET);
                        }
                        catch (Exception iex)
                        {
                            errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#50, " + iex.ToString());
                            // On decrypt exception, release all resouces on Socket, below line is used to release resouces
                            throw new SocketException(8888);
                        }

                        if (clearText == null)
                        {
                            errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#51, decryptData() return null for clearText");
                        }
                        else
                        {
                            handleClientMsg(socket, clearText, orso);
                        }
                        #endregion
                    }
                    else
                    {
                        //Added on 20180103 -end
                        if (hasCompressData && raw[NetInfoParserV2.GOOD_PARSER_OFFSET] == 1)
                        {
                            try
                            {
                                int dataLen = BitConverter.ToInt32(raw, 0);
                                byte[] dByte = null;
                                MemoryStream outputMs = new MemoryStream();
                                MemoryStream inputMs = new MemoryStream(raw, NetInfoParserV2.GOOD_PARSER_OFFSET + 1, dataLen - 1);
                                using (DeflateStream dzip = new DeflateStream(inputMs, CompressionMode.Decompress))
                                {
                                    dzip.CopyTo(outputMs);
                                    dzip.Close();
                                }
                                dByte = outputMs.ToArray();
                                outputMs.Close();
                                inputMs.Close();

                                if (orso.compressDataParser == null)
                                    orso.compressDataParser = new NetInfoParserV2(4 * 1024 * 1024);

                                orso.compressDataParser.AddToParser(dByte, 0, dByte.Length);
                                while (true)
                                {
                                    byte[] xMsg;
                                    if (orso.compressDataParser.getRawMsg(out xMsg))
                                    {
                                        string msg = System.Text.UTF8Encoding.UTF8.GetString(xMsg, NetInfoParser.GOOD_PARSER_OFFSET, xMsg.Length - NetInfoParser.GOOD_PARSER_OFFSET);
                                        handleClientMsg(socket, msg, orso);
                                    }
                                    else
                                        break;

                                }
                            }
                            catch (Exception ex)
                            {
                                if (errorLogger != null)
                                    errorLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
                            }
                        }
                        else
                        {
                            string msg = System.Text.UTF8Encoding.UTF8.GetString(raw, NetInfoParser.GOOD_PARSER_OFFSET, raw.Length - NetInfoParser.GOOD_PARSER_OFFSET);
                            handleClientMsg(socket, msg, orso);
                        }
                    }
                }
                socket.BeginReceive(orso.buffer, 0, orso.buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), orso);
            }
            catch (SocketException se)
            {

                #region handling SocketException
                try
                {
                    
                    string msg = string.Format("Socket exception: {0}, {1}, {2}", orso.workSocket.RemoteEndPoint.ToString(), se.SocketErrorCode, se.ToString());
                    errorLogger.log(ZDLogger.LVL_ERROR, msg);
                }
                catch (Exception ex)
                {
                    errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#1," + ex.ToString());
                }

                try
                {
                    onSocketClosed(orso);
                }
                catch (Exception ex)
                {
                    errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#2," + ex.ToString());
                }

                try
                {
                    socket.Close();
                }
                catch (Exception ex)
                {
                    errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#3," + ex.ToString());
                }


                try
                {
                    if (orso.buffer != null)
                    {
                        byte[] temp = orso.buffer;
                        pinBufferPool.PutObject(temp);

                        orso.buffer = null;
                    }
                }
                catch (Exception ex)
                {
                    errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#4," + ex.ToString());
                }

                try
                {
                    if (orso.sendBuffer != null)
                    {
                        byte[] temp = orso.sendBuffer;
                        pinBufferPool.PutObject(temp);

                        orso.sendBuffer = null;
                    }
                }
                catch (Exception ex)
                {
                    errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#5," + ex.ToString());
                }

                try
                {
                    orso.safeGurad.clearResource();
                }
                catch (Exception ex)
                {
                    errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#6," + ex.ToString());
                }

                #endregion
            }
            catch (Exception e)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#0," + e.ToString());
            }
        }

        public string decryptData(RecvStateObject orso, byte[] data, int idx, int dataSize)
        {

            if (orso.safeGurad.sessionWorkSate == SessionWorkState.WAIT_SYMMETRIC_KEY)
            {
                // Todo, will change
                byte[] dataTempArr = new byte[dataSize];
                Array.Copy(data, idx, dataTempArr, 0, dataSize);

                byte[] clearByteArr = rsaUtil.RSADecrypt(dataTempArr);
                string clearTxt = System.Text.UTF8Encoding.UTF8.GetString(clearByteArr);

                if (networkLogger != null)
                    networkLogger.log(ZDLogger.LVL_TRACE, orso.remoteIPPort + " " + clearTxt);

                string[] fieldArr = clearTxt.Split('@');

                orso.safeGurad.aesUtil = new AESUtil(fieldArr[1], fieldArr[2]);
                orso.safeGurad.sessionWorkSate = SessionWorkState.SYMMETRIC_KEY_READY;

                return clearTxt;
            }
            else if (orso.safeGurad.sessionWorkSate == SessionWorkState.SYMMETRIC_KEY_READY)
            {
                string strMsg = Convert.ToBase64String(data, idx, dataSize);
                networkLogger.log(ZDLogger.LVL_TRACE, orso.remoteIPPort + ", AES, len=" + dataSize + ":" + strMsg);
                

                // use symmetric key to decrypt data
                string clearTxt = orso.safeGurad.aesUtil.AESDecrypt(data, idx, dataSize);

                if (networkLogger != null)
                    networkLogger.log(ZDLogger.LVL_TRACE, orso.remoteIPPort + " " + clearTxt);

                return clearTxt;
            }

            return null;
        }

        
    }

    public abstract class ClientWaiterY         
    {
        private Socket listenSocket = null;
        private ManualResetEvent allDone = null;

        private Thread waiterThread = null;
        public volatile bool stopFlag = true;
        public ZDLogger networkLogger = null;
        public ZDLogger errorLogger = null;

        private ObjectPool<byte[]> pinBufferPool = null;

        public RSAUtil rsaUtil = null;

        public bool isCallbackOnBinary = false;

        public ClientWaiterY()
        {
        }

        public ClientWaiterY(string hostIP, string hostPort, ZDLogger networkLogger, ZDLogger errorLogger)
        {
            this.networkLogger = networkLogger;
            this.errorLogger = errorLogger;
            allDone = new ManualResetEvent(false);

            // Added on 20171009 -begin
            pinBufferPool = new ObjectPool<byte[]>(() => new byte[1024]);
            for (int i = 0; i < 6000; i++)
            {
                pinBufferPool.PutObject(new byte[1024]);
            }
            // Added on 20171009 -end

            int ftServerPort = Convert.ToInt32(hostPort);
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(hostIP), ftServerPort);
            listenSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(ipe);

            waiterThread = new Thread(new ThreadStart(startListen));
        }

        public ClientWaiterY(string hostIP, string hostPort, ZDLogger networkLogger, ZDLogger errorLogger, int socketCnt)
        {
            this.networkLogger = networkLogger;
            this.errorLogger = errorLogger;
            allDone = new ManualResetEvent(false);

            pinBufferPool = new ObjectPool<byte[]>(() => new byte[1024]);
            for (int i = 0; i < socketCnt * 2; i++)
            {
                pinBufferPool.PutObject(new byte[1024]);
            }

            int ftServerPort = Convert.ToInt32(hostPort);
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(hostIP), ftServerPort);
            listenSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(ipe);

            waiterThread = new Thread(new ThreadStart(startListen));
        }


        public void setRSAKeyFile(string keyFileName)
        {
            try
            {
                rsaUtil = new RSAUtil(keyFileName);
            }
            catch (Exception ex)
            {
                if (errorLogger != null)
                    errorLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
            }
        }

        private void startListen()
        {
            listenSocket.Listen(100);

            while (!stopFlag)
            {
                allDone.Reset();

                // Start an asynchronous socket to listen for connections.
                Console.WriteLine("Waiting for a connection...");
                listenSocket.BeginAccept(new AsyncCallback(AcceptCallback), listenSocket);

                // Wait until a connection is made before continuing.
                allDone.WaitOne();
            }
        }

        public void start()
        {
            stopFlag = false;
            waiterThread.Start();
        }

        public void stop()
        {
            stopFlag = true;
            allDone.Set();
            try
            {
                listenSocket.Close();
            }
            catch (Exception e)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, "step1: " + e.ToString());
            }

            //rsaUtil
            try
            {
                if (rsaUtil != null)
                    rsaUtil.Dispose();
            }
            catch (Exception e)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, "step2: " + e.ToString());

            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                Socket listenSocket = (Socket)ar.AsyncState;
                Socket socket = listenSocket.EndAccept(ar);

                socket.NoDelay = true;
                //Added on 20180126 -begin
                socket.SendTimeout = 5000;
                //Added on 20180126 -end

                //added on 20180104 -begin
                byte[] inOptionValues = new byte[12];
                BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);//是否启用Keep-Alive
                BitConverter.GetBytes((uint)3000).CopyTo(inOptionValues, 4);//多长时间开始第一次探测
                BitConverter.GetBytes((uint)3000).CopyTo(inOptionValues, 8);//探测时间间隔
                socket.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
                //added on 20180104 -end

                // Signal the main thread to continue.
                allDone.Set();

                string logMsg = socket.RemoteEndPoint.ToString() + " connected!";
                Console.WriteLine(logMsg);
                if (networkLogger != null)
                    networkLogger.log(ZDLogger.LVL_CRITCAL, logMsg);

                // Create the state object.
                RecvStateObject orso = new RecvStateObject();

                // Added on 20171009 -begin
                orso.buffer = pinBufferPool.GetObject();
                orso.sendBuffer = pinBufferPool.GetObject();
                // Added on 20171009 -end

                orso.workSocket = socket;
                onSocketCreated(orso);

                //Added on 20171228 -begin
                if (!orso.isCloseSocketSetted)
                {
                    //Added on 20171228 -end

                    // Give child class chance to assign an Parser implementation and if does not assign, assgin one default
                    if (orso.parser == null)
                    {
                        //orso.parser = new NetInfoParser();
                        NetInfoParserV2 parser = new NetInfoParserV2();
                        parser.setErrLogger(networkLogger);
                        orso.parser = parser;
                    }

                    socket.BeginReceive(orso.buffer, 0, orso.buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), orso);
                }
            }
            catch (ObjectDisposedException ode)
            {
                errorLogger.log(ZDLogger.LVL_CRITCAL, "Server stopped listening socket!");
            }
            catch (Exception e)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, e.ToString());
            }
        }

        /// <summary>
        /// Server implementation want to close sokcet connection intentionly
        /// </summary>
        /// <param name="orso"></param>
        public void closeSocketConnection(RecvStateObject orso)
        {
            try
            {
                // in fact is onSocketClosing()
                onSocketClosed(orso);
                errorLogger.log(ZDLogger.LVL_CRITCAL, "closeSocketConnection#1: close socket actively on demand: " + orso.remoteIPPort);

            }
            catch (Exception iex)
            {
                errorLogger.log(ZDLogger.LVL_CRITCAL, "closeSocketConnection#2: " + iex.ToString());
            }


            try
            {
                orso.workSocket.Shutdown(SocketShutdown.Both);
                orso.workSocket.Close();
            }
            catch (Exception iex)
            {
                errorLogger.log(ZDLogger.LVL_CRITCAL, "closeSocketConnection#3: " + iex.ToString());
            }

            orso.isCloseSocketSetted = true;
        }

        public virtual void onSocketCreated(RecvStateObject orso)
        {
            Socket socket = orso.workSocket;
            IPEndPoint endPoint = (IPEndPoint)socket.RemoteEndPoint;
            orso.remoteIPPort = endPoint.Address.ToString() + ":" + endPoint.Port;
        }

        public virtual void onSocketClosed(RecvStateObject orso)
        {
            // Do nothing
        }

        public virtual void handleClientMsg(Socket socket, string msg, RecvStateObject rso)
        {
        }

        public virtual string[] getAesKeyAndIV(string messageFromClient)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="msg"></param>
        /// <param name="offset"></param>
        /// <param name="count">if count is 0, means msg is used as recyclable buffer, then use first 4 bytes to get data lenth by user</param>
        /// <param name="rso"></param>
        public virtual void handleClientMsg(Socket socket, byte[] msg, int offset, int count, RecvStateObject rso)
        {
            //do nothing
        }

        /// <summary>
        /// Data packet format from client is:
        /// [Data(4bytes)][Command(1byte)][content]
        /// Command: 1 - connect
        ///          2 - disconnect
        ///          3 - get all available contracts
        ///          4 - subscribe contracts
        ///          5 - unsbuscribe contracts
        /// </summary>
        /// <param name="ar"></param>
        public void ReadCallback(IAsyncResult ar)
        {
            RecvStateObject orso = (RecvStateObject)ar.AsyncState;
            Socket socket = orso.workSocket;

            try
            {
                // Read data from the remote device.
                SocketError socketErr;
                int currentReceive = socket.EndReceive(ar, out socketErr);
                if (currentReceive == 0)
                {

                    #region handling of socket receving 0
                    if (errorLogger != null)
                        errorLogger.log(ZDLogger.LVL_CRITCAL, orso.remoteIPPort + " ReadCallback(), recv 0");


                    try
                    {
                        if (orso.buffer != null)
                        {
                            byte[] temp = orso.buffer;
                            pinBufferPool.PutObject(temp);

                            orso.buffer = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#21," + ex.ToString());
                    }

                    try
                    {
                        if (orso.sendBuffer != null)
                        {
                            byte[] temp = orso.sendBuffer;
                            pinBufferPool.PutObject(temp);

                            orso.sendBuffer = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#22," + ex.ToString());
                    }

                    //Added on 20171228 -begin
                    if (orso.isCloseSocketSetted)
                    {
                        return;
                    }
                    //Added on 20171228 -end

                    try
                    {
                        onSocketClosed(orso);
                    }
                    catch (Exception ex)
                    {
                        errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#23," + ex.ToString());
                    }

                    //Changed on 20171215 -begin
                    // moved from the begining of this block to here, allow user can access socket object to record something
                    try
                    {
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                    }
                    catch (Exception ex)
                    {
                        errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#20," + ex.ToString());
                    }
                    //Changed on 20171215 -end


                    try
                    {
                        orso.safeGurad.clearResource();
                    }
                    catch (Exception ex)
                    {
                        errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#24," + ex.ToString());
                    }
                    return;

                    #endregion
                }

                // changed on 20171023 -begin
                // mantis:1085
                string strData = null;
                // no matter is encrypted session or not, first piece of network data is clear text
                // so, even the encrypted session, first piece of data comes into following condition
                if (!orso.isEncryptedSession)
                {
                    strData = System.Text.UTF8Encoding.UTF8.GetString(orso.buffer, 0, currentReceive);
                    if (networkLogger != null)
                        networkLogger.log(ZDLogger.LVL_TRACE, orso.remoteIPPort + " " + strData);
                }

                if (orso.lastReceiveTick == 0)
                {
                    #region first piece of network data checking


                    if (!strData.StartsWith("{("))
                    {
                        try
                        {
                            if (orso.buffer != null)
                            {
                                byte[] temp = orso.buffer;
                                pinBufferPool.PutObject(temp);

                                orso.buffer = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#31," + ex.ToString());
                        }

                        try
                        {
                            if (orso.sendBuffer != null)
                            {
                                byte[] temp = orso.sendBuffer;
                                pinBufferPool.PutObject(temp);

                                orso.sendBuffer = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#32," + ex.ToString());
                        }

                        //Added on 20180109 -begin
                        //To notify decendent to unbook the connnected message
                        try
                        {
                            onSocketClosed(orso);
                        }
                        catch (Exception ex)
                        {
                            errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#33," + ex.ToString());
                        }
                        //Added on 20180109 -end

                        return;
                    }
                    else
                    {
                        orso.lastReceiveTick = DateTime.Now.Ticks;
                    }

                    #endregion
                }

                // changed on 20171023 -end

                orso.parser.AddToParser(orso.buffer, 0, currentReceive);
                byte[] raw;
                //if (orso.parser.getRawMsg(out raw))
                while (orso.parser.getRawMsg(out raw))
                {

                    //Added on 20180103 -begin
                    if (orso.isEncryptedSession)
                    {
                        #region secure session logic
                        // Todo: check heartbeat message
                        // Warning: length 26 come from Andriod, 25 come from iOS
                        if (raw.Length == 26 || raw.Length == 25)
                        {
                            string text = System.Text.ASCIIEncoding.ASCII.GetString(raw, NetInfoParser.GOOD_PARSER_OFFSET, raw.Length - NetInfoParser.GOOD_PARSER_OFFSET);
                            if (text.StartsWith("TEST0001"))
                            {
                                if (isCallbackOnBinary)
                                    handleClientMsg(socket, raw, 0, 0, orso);
                                else
                                    handleClientMsg(socket, text, orso);

                                continue;
                            }
                        }

                        string clearText = null;
                        try
                        {
                            clearText = decryptData(orso, raw, NetInfoParser.GOOD_PARSER_OFFSET, raw.Length - NetInfoParser.GOOD_PARSER_OFFSET);
                        }
                        catch (Exception iex)
                        {
                            errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#50, " + iex.ToString());
                            // On decrypt exception, release all resouces on Socket, below line is used to release resouces
                            throw new SocketException(8888);
                        }

                        //if (clearText == null)
                        //{
                        //    errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#51, RSA decryptData() return, no need callback");
                        //}
                        //else
                        //{
                        //    if (isCallbackOnBinary)
                        //        handleClientMsg(socket, raw, 0, 0, orso);
                        //    else
                        //        handleClientMsg(socket, clearText, orso);
                        //}
                        #endregion
                    }
                    else
                    {
                        if (isCallbackOnBinary)
                            handleClientMsg(socket, raw, 0, 0, orso);
                        else
                        {
                            string msg = System.Text.UTF8Encoding.UTF8.GetString(raw, NetInfoParser.GOOD_PARSER_OFFSET, raw.Length - NetInfoParser.GOOD_PARSER_OFFSET);
                            handleClientMsg(socket, msg, orso);
                        }
                    }
                }
                socket.BeginReceive(orso.buffer, 0, orso.buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), orso);
            }
            catch (SocketException se)
            {

                #region handling SocketException
                try
                {

                    string msg = string.Format("Socket exception: {0}, {1}, {2}", orso.workSocket.RemoteEndPoint.ToString(), se.SocketErrorCode, se.ToString());
                    errorLogger.log(ZDLogger.LVL_ERROR, msg);
                }
                catch (Exception ex)
                {
                    errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#1," + ex.ToString());
                }

                try
                {
                    onSocketClosed(orso);
                }
                catch (Exception ex)
                {
                    errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#2," + ex.ToString());
                }

                try
                {
                    socket.Close();
                }
                catch (Exception ex)
                {
                    errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#3," + ex.ToString());
                }


                try
                {
                    if (orso.buffer != null)
                    {
                        byte[] temp = orso.buffer;
                        pinBufferPool.PutObject(temp);

                        orso.buffer = null;
                    }
                }
                catch (Exception ex)
                {
                    errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#4," + ex.ToString());
                }

                try
                {
                    if (orso.sendBuffer != null)
                    {
                        byte[] temp = orso.sendBuffer;
                        pinBufferPool.PutObject(temp);

                        orso.sendBuffer = null;
                    }
                }
                catch (Exception ex)
                {
                    errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#5," + ex.ToString());
                }

                try
                {
                    orso.safeGurad.clearResource();
                }
                catch (Exception ex)
                {
                    errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#6," + ex.ToString());
                }

                #endregion
            }
            catch (Exception e)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, "ReadCallback#0," + e.ToString());
            }
        }

        public string decryptData(RecvStateObject orso, byte[] data, int idx, int dataSize)
        {

            if (orso.safeGurad.sessionWorkSate == SessionWorkState.WAIT_SYMMETRIC_KEY)
            {
                // Todo, will change
                byte[] dataTempArr = new byte[dataSize];
                Array.Copy(data, idx, dataTempArr, 0, dataSize);

                byte[] clearByteArr = rsaUtil.RSADecrypt(dataTempArr);
                string clearTxt = System.Text.UTF8Encoding.UTF8.GetString(clearByteArr);

                if (networkLogger != null)
                    networkLogger.log(ZDLogger.LVL_TRACE, orso.remoteIPPort + " " + clearTxt);

                //string[] fieldArr = clearTxt.Split('@');
                string[] fieldArr = getAesKeyAndIV(clearTxt);

                orso.safeGurad.aesUtil = new AESUtil(fieldArr[0], fieldArr[1]);
                orso.safeGurad.sessionWorkSate = SessionWorkState.SYMMETRIC_KEY_READY;

                StringBuilder sb = new StringBuilder();
                sb.Append('_', NetInfoParser.GOOD_PARSER_OFFSET);
                sb.Append(clearTxt);
                byte[] callbackBuffer = System.Text.ASCIIEncoding.ASCII.GetBytes(sb.ToString());
                handleClientMsg(orso.workSocket, callbackBuffer, 0, 0, orso);

                return null;
            }
            else if (orso.safeGurad.sessionWorkSate == SessionWorkState.SYMMETRIC_KEY_READY)
            {
                string strMsg = Convert.ToBase64String(data, idx, dataSize);
                networkLogger.log(ZDLogger.LVL_TRACE, orso.remoteIPPort + ", AES, len=" + dataSize + ":" + strMsg);


                // use symmetric key to decrypt data
                string clearTxt = null;

                if (isCallbackOnBinary)
                {
                    //clearTxt = orso.safeGurad.aesUtil.AESDecrypt(data, idx, dataSize);
                    byte[] buffer = orso.safeGurad.aesUtil.AESDecrypt_Rainer(data, idx, dataSize);
                    handleClientMsg(orso.workSocket, buffer, 0, 0, orso);
                }
                else
                {
                    clearTxt = orso.safeGurad.aesUtil.AESDecrypt(data, idx, dataSize);
                    handleClientMsg(orso.workSocket, clearTxt, orso);
                }


                //if (networkLogger != null)
                //    networkLogger.log(ZDLogger.LVL_TRACE, orso.remoteIPPort + " " + clearTxt);

                return clearTxt;
            }

            return null;
        }


    }

    public abstract class ClientWaiterX
    {
        private Socket listenSocket = null;
        private ManualResetEvent allDone = null;

        private Thread waiterThread = null;
        public volatile bool stopFlag = true;
        public ZDLogger networkLogger = null;
        public ZDLogger errorLogger = null;

        public ClientWaiterX()
        {
        }

        public ClientWaiterX(string hostIP, string hostPort, ZDLogger networkLogger, ZDLogger errorLogger)
        {
            this.networkLogger = networkLogger;
            this.errorLogger = errorLogger;
            allDone = new ManualResetEvent(false);

            int ftServerPort = Convert.ToInt32(hostPort);
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(hostIP), ftServerPort);
            listenSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(ipe);

            waiterThread = new Thread(new ThreadStart(startListen));
        }

        private void startListen()
        {
            listenSocket.Listen(100);

            while (!stopFlag)
            {
                allDone.Reset();

                // Start an asynchronous socket to listen for connections.
                Console.WriteLine("Waiting for a connection...");
                listenSocket.BeginAccept(new AsyncCallback(AcceptCallback), listenSocket);

                // Wait until a connection is made before continuing.
                allDone.WaitOne();
            }
        }

        public void start()
        {
            stopFlag = false;
            waiterThread.Start();
        }

        public void stop()
        {
            stopFlag = true;
            allDone.Set();
            try
            {
                listenSocket.Close();
            }
            catch (Exception e)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, e.ToString());
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                Socket listenSocket = (Socket)ar.AsyncState;
                Socket socket = listenSocket.EndAccept(ar);

                socket.NoDelay = true;
                // Signal the main thread to continue.
                allDone.Set();

                string logMsg = socket.RemoteEndPoint.ToString() + " connected!";
                Console.WriteLine(logMsg);
                if (networkLogger != null)
                    networkLogger.log(ZDLogger.LVL_CRITCAL, logMsg);

                // Create the state object.
                RecvStateObject orso = new RecvStateObject();
                orso.workSocket = socket;
                onSocketCreated(orso);
                // Give child class chance to assign an Parser implementation and if does not assign, assgin one default
                if (orso.parser == null)
                {
                    //orso.parser = new NetInfoParser();
                    NetInfoParserV2 parser = new NetInfoParserV2();
                    parser.setErrLogger(networkLogger);
                    orso.parser = parser;
                }

                //Added on 20180125 -begin
                if (orso.buffer == null)
                    orso.buffer = new byte[1024];
                //Added on 20180125 -end

                socket.BeginReceive(orso.buffer, 0, orso.buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), orso);
            }
            catch (ObjectDisposedException ode)
            {
                errorLogger.log(ZDLogger.LVL_CRITCAL, "Server stopped listening socket!");
            }
            catch (Exception e)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, e.ToString());
            }
        }

        public virtual void onSocketCreated(RecvStateObject orso)
        {
            Socket socket = orso.workSocket;
            IPEndPoint endPoint = (IPEndPoint)socket.RemoteEndPoint;
            orso.remoteIPPort = endPoint.Address.ToString() + ":" + endPoint.Port;
        }

        public virtual void onSocketClosed(RecvStateObject orso)
        {
            // Do nothing
        }

        public abstract void handleClientMsg(Socket socket, byte[] msg, RecvStateObject rso);

        /// <summary>
        /// Data packet format from client is:
        /// [Data(4bytes)][Command(1byte)][content]
        /// Command: 1 - connect
        ///          2 - disconnect
        ///          3 - get all available contracts
        ///          4 - subscribe contracts
        ///          5 - unsbuscribe contracts
        /// </summary>
        /// <param name="ar"></param>
        public void ReadCallback(IAsyncResult ar)
        {
            RecvStateObject orso = (RecvStateObject)ar.AsyncState;
            Socket socket = orso.workSocket;

            try
            {
                // Read data from the remote device.
                SocketError socketErr;
                int currentReceive = socket.EndReceive(ar, out socketErr);
                if (currentReceive == 0)
                {
                    onSocketClosed(orso);
                    if (errorLogger != null)
                        errorLogger.log(ZDLogger.LVL_CRITCAL, orso.remoteIPPort + " Client closed peer socket!");
                    try
                    {
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                    }
                    catch (Exception ex)
                    {
                        errorLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
                    }

                    return;
                }

                if (networkLogger != null)
                    networkLogger.log(ZDLogger.LVL_TRACE, orso.remoteIPPort + " " + System.Text.ASCIIEncoding.ASCII.GetString(orso.buffer, 0, currentReceive));

                orso.parser.AddToParser(orso.buffer, 0, currentReceive);
                byte[] raw;
                //if (orso.parser.getRawMsg(out raw))
                while (orso.parser.getRawMsg(out raw))
                {
                    handleClientMsg(socket, raw, orso);
                }
                socket.BeginReceive(orso.buffer, 0, orso.buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), orso);
            }
            catch (SocketException se)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, se.ToString());

                try
                {
                    string msg = string.Format("Socket with {0} is closed!", orso.workSocket.RemoteEndPoint.ToString());
                    errorLogger.log(ZDLogger.LVL_ERROR, msg);
                }
                catch (Exception ex)
                {
                    errorLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
                }

                onSocketClosed(orso);

                try
                {
                    socket.Close();
                }
                catch (Exception ex)
                {
                    errorLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
                }
            }
            catch (Exception e)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, e.ToString());
            }
        }
    }

    public class RecvStateObject
    {
        // Client  socket.
        public Socket workSocket = null;
        public Parser parser = null;
        

        // Added on 20171009 -begin
        //public byte[] buffer = new byte[1024];
        public byte[] buffer = null;
        public byte[] sendBuffer = null;
        // Added on 20171009 -end

        public object implProcessState = null;

        public string remoteIPPort;

        public long lastReceiveTick = 0;
        public long agreedHearbeatTick = 450000000;

        public bool isCloseSocketSetted = false;

        // Added on 20170103 -begin
        public bool isEncryptedSession = false;
        public SafeGuard safeGurad = new SafeGuard();
        // Added on 20170103 -end


        public DateTime recv0Timestamp = new DateTime(1970, 1, 1);
        public int recv0Count = 0;

        public NetInfoParserV2 compressDataParser = null; // new NetInfoParserV2(4 * 1024 * 1024);
    }

    public class NetInfoParser : Parser
    {
        private byte[] buffer_ = null;
        private int msgBeginindex = 0;
        private int spaceAvailabeIndex = 0;

        public const int GOOD_PARSER_OFFSET = 8;
        public const int LEN_PAKCKET_SIZE = 4;

        private ZDLogger errorLogger = null;

        public NetInfoParser()
        {
            buffer_ = new byte[1024 * 5];
        }

        public NetInfoParser(int bufferSize)
        {
            buffer_ = new byte[bufferSize];
        }

        public void setErrLogger(ZDLogger errorLogger)
        {
            this.errorLogger = errorLogger;
        }

        public void AddToParser(byte[] data, int offset, int count)
        {
            if (buffer_.Length - spaceAvailabeIndex < count)
            {
                int lengthOfPartMsg = spaceAvailabeIndex - msgBeginindex;
                if (buffer_.Length >= lengthOfPartMsg + count)
                {
                    // Shift partial data of current message
                    Array.Copy(buffer_, msgBeginindex, buffer_, 0, lengthOfPartMsg);
                    Array.Copy(data, 0, buffer_, lengthOfPartMsg, count);
                    msgBeginindex = 0;
                    spaceAvailabeIndex = lengthOfPartMsg + count;
                }
                else
                {
                    // Todo: exception or expand buffer_ size
                    throw new Exception("Buffer of Parser is not enough!");
                }
            }
            else
            {
                Array.Copy(data, 0, buffer_, spaceAvailabeIndex, count);
                spaceAvailabeIndex += count;
            }
        }

        public const string NET_HEAD_STR = "{(len=";
        public const int NET_HEAD_LEN = 6;

        public bool getRawMsg(out byte[] rawMsg)
        {
            rawMsg = null;

            // Message lenth part is not enougth
            int dataLength = spaceAvailabeIndex - msgBeginindex;
            if (dataLength < NET_HEAD_LEN) return false;

            bool isContinue = false;
            // Message data part is not enougth
            int i = msgBeginindex + NET_HEAD_LEN + 1;
            for (; i < spaceAvailabeIndex; i++)
            {
                if (buffer_[i] == ')')
                {
                    isContinue = true;
                    break;
                }
            }

            int contentSize = 0;
            int packetSize = 0;
            if (isContinue)
            {
                int lenBeginPos = msgBeginindex + NET_HEAD_LEN;
                string contentLen = System.Text.ASCIIEncoding.ASCII.GetString(buffer_, lenBeginPos, i - lenBeginPos);

                //contentSize = Convert.ToInt32(contentLen);
                // Network data get bad
                if (!int.TryParse(contentLen, out contentSize))
                {
                    int netDataLen = spaceAvailabeIndex - msgBeginindex;
                    string badMsg = System.Text.ASCIIEncoding.ASCII.GetString(buffer_, msgBeginindex, netDataLen);
                    if (errorLogger != null)
                        errorLogger.log(ZDLogger.LVL_ERROR, "Bad message data: " + badMsg);

                    int possibleNextGoodIdx = badMsg.IndexOf(NET_HEAD_STR, NET_HEAD_LEN);
                    if (possibleNextGoodIdx > -1)
                        msgBeginindex += possibleNextGoodIdx;
                    else
                    {
                        // Shift data to avoid buffer overflow
                        Array.Copy(buffer_, msgBeginindex, buffer_, 0, netDataLen);
                        msgBeginindex = 0;
                        spaceAvailabeIndex = netDataLen;
                    }

                    return false;
                }

                
                packetSize = i - msgBeginindex + 1 + contentSize + 1;
            }
            else
            {
                // Message length part is not enough
                return false;
            }

            if (dataLength < packetSize) return false;

            //rawMsg = new byte[GOOD_PARSER_OFFSET + contentSize];
            rawMsg = getByteArr(GOOD_PARSER_OFFSET + contentSize);
            Array.Copy(buffer_, i + 1, rawMsg, GOOD_PARSER_OFFSET, contentSize);
            Array.Copy(BitConverter.GetBytes(contentSize), rawMsg, LEN_PAKCKET_SIZE);
            msgBeginindex += packetSize;
            return true;
        }

        public virtual byte[] getByteArr(int size) 
        {
            return new byte[size];
        }
    }

    public class NetInfoParserV2 : Parser
    {
        private byte[] buffer_ = null;
        private int msgBeginindex = 0;
        private int spaceAvailabeIndex = 0;
        private int initSize = 0;

        // This flag should be set true for server side because server should not maintain big buffer for each connection
        public bool isIntendSmallMsg = true;

        public const int GOOD_PARSER_OFFSET = 8;
        public const int LEN_PAKCKET_SIZE = 4;

        private ZDLogger errorLogger = null;

        public void setErrLogger(ZDLogger errorLogger)
        {
            this.errorLogger = errorLogger;
        }

        public NetInfoParserV2()
        {
            initSize = 1024 * 5;
            buffer_ = new byte[initSize];
        }

        public NetInfoParserV2(int bufferSize)
        {
            initSize = bufferSize;
            buffer_ = new byte[bufferSize];
        }

        public void AddToParser(byte[] data, int offset, int count)
        {
            if (buffer_.Length - spaceAvailabeIndex < count)
            {
                int lengthOfPartMsg = spaceAvailabeIndex - msgBeginindex;
                if (buffer_.Length >= lengthOfPartMsg + count)
                {
                    // Shift partial data of current message
                    Array.Copy(buffer_, msgBeginindex, buffer_, 0, lengthOfPartMsg);
                    Array.Copy(data, offset, buffer_, lengthOfPartMsg, count);
                    msgBeginindex = 0;
                    spaceAvailabeIndex = lengthOfPartMsg + count;
                }
                else
                {
                    int i = 0;
                    int contentSize = 0;
                    int packetSize = 0;
                    getPacketInfo(out packetSize, out contentSize, out i);

                    // In case of one small message and one big message come at the same time,
                    //  or first message is big one
                    if (packetSize < count)
                        packetSize = count;

                    Array.Resize(ref buffer_, buffer_.Length + packetSize);
                    Array.Copy(data, offset, buffer_, spaceAvailabeIndex, count);
                    spaceAvailabeIndex += count;
                }
            }
            else
            {
                Array.Copy(data, offset, buffer_, spaceAvailabeIndex, count);
                spaceAvailabeIndex += count;
            }
        }

        public const string NET_HEAD_STR = "{(len=";
        public const int NET_HEAD_LEN = 6;

        public bool getRawMsg(out byte[] rawMsg)
        {
            rawMsg = null;

            // Message lenth part is not enougth
            int dataLength = spaceAvailabeIndex - msgBeginindex;
            if (dataLength < NET_HEAD_LEN) return false;

            int i = 0;
            int contentSize = 0;
            int packetSize = 0;
            if (!getPacketInfo(out packetSize, out contentSize, out i)) return false;

            if (dataLength < packetSize) return false;

            //rawMsg = new byte[GOOD_PARSER_OFFSET + contentSize];
            rawMsg = getByteArr(GOOD_PARSER_OFFSET + contentSize);
            Array.Copy(buffer_, i + 1, rawMsg, GOOD_PARSER_OFFSET, contentSize);
            Array.Copy(BitConverter.GetBytes(contentSize), rawMsg, LEN_PAKCKET_SIZE);
            msgBeginindex += packetSize;

            if (isIntendSmallMsg && packetSize > initSize)
            {
                byte[] newBuffer = new byte[initSize];
                // Shift partial data of current message
                int lengthOfPartMsg = spaceAvailabeIndex - msgBeginindex;
                Array.Copy(buffer_, msgBeginindex, newBuffer, 0, lengthOfPartMsg);
                msgBeginindex = 0;
                spaceAvailabeIndex = lengthOfPartMsg;
                buffer_ = newBuffer;
            }

            return true;
        }

        public bool getPacketInfo(out int packetSize, out int contentSize, out int index)
        {
            bool isContinue = false;
            // Message data part is not enougth
            int i = msgBeginindex + NET_HEAD_LEN + 1;
            for (; i < spaceAvailabeIndex; i++)
            {
                if (buffer_[i] == ')')
                {
                    isContinue = true;
                    break;
                }
            }
            
            if (isContinue)
            {
                int lenBeginPos = msgBeginindex + NET_HEAD_LEN;
                string contentLen = System.Text.ASCIIEncoding.ASCII.GetString(buffer_, lenBeginPos, i - lenBeginPos);
                //contentSize = Convert.ToInt32(contentLen);
                // Network data get bad
                if (!int.TryParse(contentLen, out contentSize))
                {
                    int netDataLen = spaceAvailabeIndex - msgBeginindex;
                    string badMsg = System.Text.ASCIIEncoding.ASCII.GetString(buffer_, msgBeginindex, netDataLen);
                    if (errorLogger != null)
                        errorLogger.log(ZDLogger.LVL_ERROR, "Bad message data: " + badMsg);

                    int possibleNextGoodIdx = badMsg.IndexOf(NET_HEAD_STR, NET_HEAD_LEN);
                    if (possibleNextGoodIdx > -1)
                        msgBeginindex += possibleNextGoodIdx;
                    else
                    {
                        // Shift data to avoid buffer overflow
                        Array.Copy(buffer_, msgBeginindex, buffer_, 0, netDataLen);
                        msgBeginindex = 0;
                        spaceAvailabeIndex = netDataLen;
                    }

                    packetSize = -1;
                    index = -1;
                    return false;
                }

                packetSize = i - msgBeginindex + 1 + contentSize + 1;
                index = i;
                return true;
            }
            else
            {
                contentSize = 0;
                packetSize = 0;
                index = 0;
                // Message length part is not enough
                return false;
            }
        }

        public virtual byte[] getByteArr(int size)
        {
            return new byte[size];
        }

    }

    public class AyersParser : Parser
    {
        private byte[] buffer_ = null;
        private int msgBeginindex = 0;
        private int spaceAvailabeIndex = 0;
        private int initSize = 0;

        // This flag should be set true for server side because server should not maintain big buffer for each connection
        public bool isIntendSmallMsg = true;

        public const int GOOD_PARSER_OFFSET = 8;
        public const int LEN_PAKCKET_SIZE = 4;

        private ZDLogger errorLogger = null;

        public void setErrLogger(ZDLogger errorLogger)
        {
            this.errorLogger = errorLogger;
        }

        public AyersParser()
        {
            initSize = 1024 * 5;
            buffer_ = new byte[initSize];
        }

        public AyersParser(int bufferSize)
        {
            initSize = bufferSize;
            buffer_ = new byte[bufferSize];
        }

        public void AddToParser(byte[] data, int offset, int count)
        {
            /*
            if (errorLogger != null)
            {
                string msg = System.Text.ASCIIEncoding.ASCII.GetString(data, offset, count);
                errorLogger.log(ZDLogger.LVL_TRACE, msg);
            }
            */

            if (buffer_.Length - spaceAvailabeIndex < count)
            {
                int lengthOfPartMsg = spaceAvailabeIndex - msgBeginindex;
                if (buffer_.Length >= lengthOfPartMsg + count)
                {
                    // Shift partial data of current message
                    Array.Copy(buffer_, msgBeginindex, buffer_, 0, lengthOfPartMsg);
                    Array.Copy(data, offset, buffer_, lengthOfPartMsg, count);
                    msgBeginindex = 0;
                    spaceAvailabeIndex = lengthOfPartMsg + count;
                }
                else
                {
                    int i = 0;
                    int contentSize = 0;
                    int packetSize = 0;
                    getPacketInfo(out packetSize, out contentSize, out i);

                    // In case of one small message and one big message come at the same time,
                    //  or first message is big one
                    if (packetSize < count)
                        packetSize = count;

                    Array.Resize(ref buffer_, buffer_.Length + packetSize);
                    Array.Copy(data, offset, buffer_, spaceAvailabeIndex, count);
                    spaceAvailabeIndex += count;
                }
            }
            else
            {
                Array.Copy(data, offset, buffer_, spaceAvailabeIndex, count);
                spaceAvailabeIndex += count;
            }
        }

        public const int NET_HEAD_LEN = 4;

        public bool getRawMsg(out byte[] rawMsg)
        {
            rawMsg = null;

            // Message lenth part is not enougth
            int dataLength = spaceAvailabeIndex - msgBeginindex;
            if (dataLength < NET_HEAD_LEN) return false;

            int i = 0;
            int contentSize = 0;
            int packetSize = 0;
            if (!getPacketInfo(out packetSize, out contentSize, out i)) return false;

            if (dataLength < packetSize) return false;

            //rawMsg = new byte[GOOD_PARSER_OFFSET + contentSize];
            rawMsg = getByteArr(GOOD_PARSER_OFFSET + contentSize);
            Array.Copy(buffer_, i, rawMsg, GOOD_PARSER_OFFSET, contentSize);
            Array.Copy(BitConverter.GetBytes(contentSize), rawMsg, LEN_PAKCKET_SIZE);
            msgBeginindex += packetSize;

            if (isIntendSmallMsg && packetSize > initSize)
            {
                byte[] newBuffer = new byte[initSize];
                // Shift partial data of current message
                int lengthOfPartMsg = spaceAvailabeIndex - msgBeginindex;
                Array.Copy(buffer_, msgBeginindex, newBuffer, 0, lengthOfPartMsg);
                msgBeginindex = 0;
                spaceAvailabeIndex = lengthOfPartMsg;
                buffer_ = newBuffer;
            }

            return true;
        }

        public bool getPacketInfo(out int packetSize, out int contentSize, out int index)
        {
            bool isContinue = false;
            int dataLength = spaceAvailabeIndex - msgBeginindex;
            if (dataLength > NET_HEAD_LEN) isContinue = true;

            if (isContinue)
            {
                //int lenBeginPos = msgBeginindex + NET_HEAD_LEN;
                contentSize = BitConverter.ToInt32(buffer_, msgBeginindex);
                // Network data get bad
                /*
                if (contentSize > 10000)
                {
                    int netDataLen = spaceAvailabeIndex - msgBeginindex;
                    string badMsg = System.Text.UTF8Encoding.UTF8.GetString(buffer_, msgBeginindex, netDataLen);
                    if (errorLogger != null)
                        errorLogger.log(ZDLogger.LVL_ERROR, "Bad message data: " + badMsg);

                    throw new Exception("Ayers net data got bad state, need to recover manually!");

                    //packetSize = -1;
                    //index = -1;
                    //return false;
                }
                */
                // It is optional whether each tag-value pair will be followed by acarriage return <CR> and line feed <LF>.
                packetSize = contentSize + NET_HEAD_LEN;
                index = msgBeginindex + NET_HEAD_LEN;
                return true;
            }
            else
            {
                contentSize = 0;
                packetSize = 0;
                index = 0;
                // Message length part is not enough
                return false;
            }
        }

        public virtual byte[] getByteArr(int size)
        {
            return new byte[size];
        }

    }

    public class AAStockParser : Parser
    {
        private byte[] buffer_ = null;
        private int msgBeginindex = 0;
        private int spaceAvailabeIndex = 0;
        private int initSize = 0;

        // This flag should be set true for server side because server should not maintain big buffer for each connection
        public bool isIntendSmallMsg = true;

        public const int GOOD_PARSER_OFFSET = 8;
        public const int LEN_PAKCKET_SIZE = 4;

        private ZDLogger errorLogger = null;

        public void setErrLogger(ZDLogger errorLogger)
        {
            this.errorLogger = errorLogger;
        }

        public AAStockParser()
        {
            initSize = 1024 * 5;
            buffer_ = new byte[initSize];
        }

        public AAStockParser(int bufferSize)
        {
            initSize = bufferSize;
            buffer_ = new byte[bufferSize];
        }

        public void AddToParser(byte[] data, int offset, int count)
        {
            /*
            if (errorLogger != null)
            {
                string msg = System.Text.ASCIIEncoding.ASCII.GetString(data, offset, count);
                errorLogger.log(ZDLogger.LVL_TRACE, msg);
            }
            */

            if (buffer_.Length - spaceAvailabeIndex < count)
            {
                int lengthOfPartMsg = spaceAvailabeIndex - msgBeginindex;
                if (buffer_.Length >= lengthOfPartMsg + count)
                {
                    // Shift partial data of current message
                    Array.Copy(buffer_, msgBeginindex, buffer_, 0, lengthOfPartMsg);
                    Array.Copy(data, offset, buffer_, lengthOfPartMsg, count);
                    msgBeginindex = 0;
                    spaceAvailabeIndex = lengthOfPartMsg + count;
                }
                else
                {
                    int i = 0;
                    int contentSize = 0;
                    int packetSize = 0;
                    getPacketInfo(out packetSize, out contentSize, out i);

                    // In case of one small message and one big message come at the same time,
                    //  or first message is big one
                    if (packetSize < count)
                        packetSize = count;

                    Array.Resize(ref buffer_, buffer_.Length + packetSize);
                    Array.Copy(data, offset, buffer_, spaceAvailabeIndex, count);
                    spaceAvailabeIndex += count;
                }
            }
            else
            {
                Array.Copy(data, offset, buffer_, spaceAvailabeIndex, count);
                spaceAvailabeIndex += count;
            }
        }

        public const int NET_HEAD_LEN = 2;

        public bool getRawMsg(out byte[] rawMsg)
        {
            rawMsg = null;

            // Message lenth part is not enougth
            int dataLength = spaceAvailabeIndex - msgBeginindex;
            if (dataLength < NET_HEAD_LEN) return false;

            int i = 0;
            int contentSize = 0;
            int packetSize = 0;
            if (!getPacketInfo(out packetSize, out contentSize, out i)) return false;

            if (dataLength < packetSize) return false;

            //rawMsg = new byte[GOOD_PARSER_OFFSET + contentSize];
            rawMsg = getByteArr(GOOD_PARSER_OFFSET + contentSize);
            Array.Copy(buffer_, i, rawMsg, GOOD_PARSER_OFFSET, contentSize);
            Array.Copy(BitConverter.GetBytes(contentSize), rawMsg, LEN_PAKCKET_SIZE);
            msgBeginindex += packetSize;

            if (isIntendSmallMsg && packetSize > initSize)
            {
                byte[] newBuffer = new byte[initSize];
                // Shift partial data of current message
                int lengthOfPartMsg = spaceAvailabeIndex - msgBeginindex;
                Array.Copy(buffer_, msgBeginindex, newBuffer, 0, lengthOfPartMsg);
                msgBeginindex = 0;
                spaceAvailabeIndex = lengthOfPartMsg;
                buffer_ = newBuffer;
            }

            return true;
        }

        public bool getPacketInfo(out int packetSize, out int contentSize, out int index)
        {
            bool isContinue = false;
            int dataLength = spaceAvailabeIndex - msgBeginindex;
            if (dataLength > NET_HEAD_LEN) isContinue = true;

            if (isContinue)
            {
                //int lenBeginPos = msgBeginindex + NET_HEAD_LEN;
                packetSize = BitConverter.ToUInt16(buffer_, msgBeginindex);
                contentSize = packetSize - NET_HEAD_LEN;
                // Network data get bad
                //...

                // It is optional whether each tag-value pair will be followed by acarriage return <CR> and line feed <LF>.
                index = msgBeginindex + NET_HEAD_LEN;
                return true;
            }
            else
            {
                contentSize = 0;
                packetSize = 0;
                index = 0;
                // Message length part is not enough
                return false;
            }
        }

        public virtual byte[] getByteArr(int size)
        {
            return new byte[size];
        }

    }


    public class NetInfoParserVTest : Parser
    {
        private byte[] buffer_ = null;
        private int msgBeginindex = 0;
        private int spaceAvailabeIndex = 0;
        private int initSize = 0;

        // This flag should be set true for server side because server should not maintain big buffer for each connection
        public bool isIntendSmallMsg = true;

        public const int GOOD_PARSER_OFFSET = 8;
        public const int LEN_PAKCKET_SIZE = 4;

        private ZDLogger errorLogger = null;

        public void setErrLogger(ZDLogger errorLogger)
        {
            this.errorLogger = errorLogger;
        }

        public NetInfoParserVTest()
        {
            initSize = 1024 * 5;
            buffer_ = new byte[initSize];
        }

        public NetInfoParserVTest(int bufferSize)
        {
            initSize = bufferSize;
            buffer_ = new byte[bufferSize];
        }

        public void AddToParser(byte[] data, int offset, int count)
        {
            if (buffer_.Length - spaceAvailabeIndex < count)
            {
                int lengthOfPartMsg = spaceAvailabeIndex - msgBeginindex;
                if (buffer_.Length >= lengthOfPartMsg + count)
                {
                    // Shift partial data of current message
                    Array.Copy(buffer_, msgBeginindex, buffer_, 0, lengthOfPartMsg);
                    Array.Copy(data, offset, buffer_, lengthOfPartMsg, count);
                    msgBeginindex = 0;
                    spaceAvailabeIndex = lengthOfPartMsg + count;
                }
                else
                {
                    int i = 0;
                    int contentSize = 0;
                    int packetSize = 0;
                    getPacketInfo(out packetSize, out contentSize, out i);

                    // In case of one small message and one big message come at the same time,
                    //  or first message is big one
                    if (packetSize < count)
                        packetSize = count;

                    Array.Resize(ref buffer_, buffer_.Length + packetSize);
                    Array.Copy(data, offset, buffer_, spaceAvailabeIndex, count);
                    spaceAvailabeIndex += count;
                }
            }
            else
            {
                Array.Copy(data, offset, buffer_, spaceAvailabeIndex, count);
                spaceAvailabeIndex += count;
            }
        }

        public const string NET_HEAD_STR = "{(len=";
        public const int NET_HEAD_LEN = 6;

        private string lastTradeData = null;
        private StringBuilder sb = new StringBuilder();
        private byte dataFlag = (byte)'Y';

        public bool getRawMsg(out byte[] rawMsg)
        {
            rawMsg = null;

            // Message lenth part is not enougth
            int dataLength = spaceAvailabeIndex - msgBeginindex;
            if (dataLength < NET_HEAD_LEN) return false;

            int i = 0;
            int contentSize = 0;
            int packetSize = 0;
            if (!getPacketInfo(out packetSize, out contentSize, out i)) return false;

            if (dataLength < packetSize) return false;

            int outputDataLen = GOOD_PARSER_OFFSET + contentSize;
            //rawMsg = new byte[GOOD_PARSER_OFFSET + contentSize];
            rawMsg = getByteArr(outputDataLen);
            Array.Copy(buffer_, i + 1, rawMsg, GOOD_PARSER_OFFSET, contentSize);
            Array.Copy(BitConverter.GetBytes(contentSize), rawMsg, LEN_PAKCKET_SIZE);
            msgBeginindex += packetSize;

            if (isIntendSmallMsg && packetSize > initSize)
            {
                byte[] newBuffer = new byte[initSize];
                // Shift partial data of current message
                int lengthOfPartMsg = spaceAvailabeIndex - msgBeginindex;
                Array.Copy(buffer_, msgBeginindex, newBuffer, 0, lengthOfPartMsg);
                msgBeginindex = 0;
                spaceAvailabeIndex = lengthOfPartMsg;
                buffer_ = newBuffer;
            }


            //  'Y'
            if (rawMsg[outputDataLen - 1] == dataFlag)
            {
                //33
                string content = System.Text.ASCIIEncoding.ASCII.GetString(rawMsg, 41, outputDataLen - 41);

                if (content == lastTradeData )
                {
                    sb.Append(content).Append("|a-").Append("msgPos:").Append(msgBeginindex)
                    .Append(",packetSz:").Append(packetSize)
                    .Append(",contentSz:").Append(contentSize)
                    .Append(",i:").Append(i);

                    errorLogger.log(ZDLogger.LVL_CRITCAL, sb.ToString());
                    sb.Clear();

                    try
                    {
                        int prvIdx = msgBeginindex - packetSize;
                        int len = spaceAvailabeIndex - prvIdx;
                        errorLogger.log(ZDLogger.LVL_ERROR, prvIdx + "," + len);
                        string dumpStr = System.Text.ASCIIEncoding.ASCII.GetString(buffer_, prvIdx, len);
                        errorLogger.log(ZDLogger.LVL_ERROR, dumpStr);
                    }
                    catch (Exception ex)
                    {
                        errorLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
                    }
                    
                }
                else if (lastTradeData == null)
                {
                    sb.Append(content).Append("|b-").Append("msgPos:").Append(msgBeginindex)
                    .Append(",packetSz:").Append(packetSize)
                    .Append(",contentSz:").Append(contentSize)
                    .Append(",i:").Append(i);

                    errorLogger.log(ZDLogger.LVL_CRITCAL, sb.ToString());
                    sb.Clear();
                }

                lastTradeData = content;
            }

            return true;
        }

        public bool getPacketInfo(out int packetSize, out int contentSize, out int index)
        {
            bool isContinue = false;
            // Message data part is not enougth
            int i = msgBeginindex + NET_HEAD_LEN + 1;
            for (; i < spaceAvailabeIndex; i++)
            {
                if (buffer_[i] == ')')
                {
                    isContinue = true;
                    break;
                }
            }

            if (isContinue)
            {
                int lenBeginPos = msgBeginindex + NET_HEAD_LEN;
                string contentLen = System.Text.ASCIIEncoding.ASCII.GetString(buffer_, lenBeginPos, i - lenBeginPos);
                //contentSize = Convert.ToInt32(contentLen);
                // Network data get bad
                if (!int.TryParse(contentLen, out contentSize))
                {
                    int netDataLen = spaceAvailabeIndex - msgBeginindex;
                    string badMsg = System.Text.ASCIIEncoding.ASCII.GetString(buffer_, msgBeginindex, netDataLen);
                    if (errorLogger != null)
                        errorLogger.log(ZDLogger.LVL_ERROR, "Bad message data: " + badMsg);

                    int possibleNextGoodIdx = badMsg.IndexOf(NET_HEAD_STR, NET_HEAD_LEN);
                    if (possibleNextGoodIdx > -1)
                        msgBeginindex += possibleNextGoodIdx;
                    else
                    {
                        // Shift data to avoid buffer overflow
                        Array.Copy(buffer_, msgBeginindex, buffer_, 0, netDataLen);
                        msgBeginindex = 0;
                        spaceAvailabeIndex = netDataLen;
                    }

                    packetSize = -1;
                    index = -1;
                    return false;
                }

                packetSize = i - msgBeginindex + 1 + contentSize + 1;
                index = i;
                return true;
            }
            else
            {
                contentSize = 0;
                packetSize = 0;
                index = 0;
                // Message length part is not enough
                return false;
            }
        }

        public virtual byte[] getByteArr(int size)
        {
            return new byte[size];
        }

    }

    public interface RecyclableParser : Parser
    {
        void recycleByteBuffer(byte[] buffer);
    }

    public class RecyclableNetInfoParser : NetInfoParser, RecyclableParser
    {
        private ObjectPool<byte[]> byteBufferPool = null;
        private int maxDataSize = 0;

        public RecyclableNetInfoParser(int maxDataSize)
        {
            this.maxDataSize = maxDataSize;
            byteBufferPool = new ObjectPool<byte[]>(() => new byte[maxDataSize]);
        }

        public void recycleByteBuffer(byte[] buffer)
        {
            if(buffer.Length <= maxDataSize)
                byteBufferPool.PutObject(buffer);
        }

        public override byte[] getByteArr(int size)
        {
            byte[] buffer = null;
            if(size > maxDataSize)
                buffer = new byte[size];
            else
                buffer = byteBufferPool.GetObject();
            
            return buffer;
        }

    }

    public class RecyclableAyersParser : AyersParser, RecyclableParser
    {
        private ObjectPool<byte[]> byteBufferPool = null;
        private int maxDataSize = 0;

        public RecyclableAyersParser(ObjectPool<byte[]> byteBufferPool, int maxDataSize)
        {
            this.maxDataSize = maxDataSize;
            //byteBufferPool = new ObjectPool<byte[]>(() => new byte[maxDataSize]);
            this.byteBufferPool = byteBufferPool;
        }

        public void recycleByteBuffer(byte[] buffer)
        {
            if (buffer.Length <= maxDataSize)
                byteBufferPool.PutObject(buffer);
        }

        public override byte[] getByteArr(int size)
        {
            byte[] buffer = null;
            if (size > maxDataSize)
                buffer = new byte[size];
            else
                buffer = byteBufferPool.GetObject();

            return buffer;
        }

    }


    public class NetDataState
    {
        public byte[] readBuffer = new byte[4096];
        public Parser networkParser = null;
    }

    public abstract class SocketInitor
    {
        private string hostIP = null;
        private int hostPort = 0;

        public NetDataState netDataState = null;

        private Thread dataFeedThread = null;
        public Socket receiveSocket = null;
        private bool isSocketBroken = false;
        private bool stopFlag = true;

        private DateTime lastSentTimeStamp = DateTime.Now;
        public byte[] heartbeatMsg = null;
        public int reconnectInervalSec = 0;
        public int heartbeatIntervalSec = 0;

        private ZDLogger errorLogger = null;

        public const int SOCKET_CONNECTED = 1;
        public const int SOCKET_DISCONNECTED = 2;

        public bool isUnitTestEnabled = false;

        public bool hasCompressData = false;

        // Added on 20181130 -begin
        private ZDSession zdSession = null;
        private bool isSessionServiceEnable = false;

        private NetInfoParserV2 compressDataParser = null; // new NetInfoParserV2(4 * 1024 * 1024);


        // Added on 20181130 -end

        // For unit test
        public SocketInitor()
        {
            isUnitTestEnabled = true;
        }


        public SocketInitor(ZDLogger errLogger, string strIP, string strPort, string reconnectInterval, string hearbeatInterval)
        {
            try
            {
                this.errorLogger = errLogger;
                hostIP = strIP;
                hostPort = Convert.ToInt32(strPort);

                reconnectInervalSec = Convert.ToInt32(reconnectInterval);
                heartbeatIntervalSec = Convert.ToInt32(hearbeatInterval);

                netDataState = new NetDataState();

                isSocketBroken = true;
                dataFeedThread = new Thread(new ThreadStart(onNetData));
            }
            catch (Exception ex)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
            }
        }

        private void onNetData()
        {
            try
            {
                while (!stopFlag)
                {

                    while (isSocketBroken)
                    {
                        reconnectMarket();
                        if (isSocketBroken)
                            Thread.Sleep(reconnectInervalSec * 1000);
                        else
                            break;

                        if (stopFlag) return;
                    }

                    byte[] data = syncReceive();
                    // Timeout
                    if (data != null)
                    {
                        if (isSessionServiceEnable)
                        {
                            bool isSeqnumGood = zdSession.isSeqnumGood(data);
                            if (!isSeqnumGood)
                            {
                                string resendReqMsg = zdSession.getResendRequestMsg();
                                byte[] resendReqRaw = NetworkLogic.ObjectStringToBytes2(resendReqMsg);
                                sendMsg(resendReqRaw);
                            }
                        }

                        int dataLen = BitConverter.ToInt32(data, 0);
                        if (BitConverter.ToInt32(data, 0) > 0)
                        {
                            //收到压缩数据
                            if (hasCompressData && data[NetInfoParserV2.GOOD_PARSER_OFFSET] == 1)
                            {
                                try
                                {
                                    byte[] dByte = null;
                                    MemoryStream outputMs = new MemoryStream();
                                    MemoryStream inputMs = new MemoryStream(data, NetInfoParserV2.GOOD_PARSER_OFFSET + 1, dataLen - 1);
                                    using (DeflateStream dzip = new DeflateStream(inputMs, CompressionMode.Decompress))
                                    {
                                        dzip.CopyTo(outputMs);
                                        dzip.Close();
                                    }
                                    dByte = outputMs.ToArray();
                                    outputMs.Close();
                                    inputMs.Close();

                                    if (compressDataParser == null)
                                        compressDataParser = new NetInfoParserV2(4 * 1024 * 1024);

                                    compressDataParser.AddToParser(dByte, 0, dByte.Length);
                                    while (true)
                                    {
                                        byte[] xMsg;
                                        if (compressDataParser.getRawMsg(out xMsg))
                                            onMsgReady(xMsg);
                                        else
                                            break;

                                    }
                                }
                                catch (Exception ex)
                                {
                                    if (errorLogger != null)
                                        errorLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
                                }
                            }
                            else
                            {
                                onMsgReady(data);
                            }
                        }
                    }
                }

                if (receiveSocket != null)
                    receiveSocket.Close();
            }
            catch (Exception ex)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
            }

        }

        private void reconnectMarket()
        {
            try
            {
                if (receiveSocket != null)
                    receiveSocket.Close();
            }
            catch (Exception e) { }

            try
            {
                receiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                receiveSocket.NoDelay = true;
                receiveSocket.Connect(hostIP, hostPort);
                isSocketBroken = false;
                onConnectStateChange(SOCKET_CONNECTED, DateTime.Now.ToString("[yyyyMMdd-HH:mm:ss] ") + "Reconnect to Server! local IP:Port->" + receiveSocket.LocalEndPoint.ToString());
            }
            catch (SocketException se)
            {
                onConnectStateChange(SOCKET_DISCONNECTED, DateTime.Now.ToString("[yyyyMMdd-HH:mm:ss] ") + "Connection to Server broken!");
                errorLogger.log(ZDLogger.LVL_ERROR, se.ToString());
            }

        }

        private byte[] syncReceive()
        {
            try
            {
                byte[] rawMsg = null;
                if (netDataState.networkParser.getRawMsg(out rawMsg))
                {
                    return rawMsg;
                }

                while (!isSocketBroken)
                {
                    if (stopFlag)
                        return null;
                    else
                    {
                        DateTime timeNow = DateTime.Now;
                        if (timeNow.Subtract(lastSentTimeStamp).TotalSeconds > heartbeatIntervalSec)
                        {
                            // send heartbeat message
                            if (heartbeatMsg == null)
                                onTimeSendHeartbeat();
                            else
                                sendMsg(heartbeatMsg);

                            lastSentTimeStamp = timeNow;
                        }
                    }

                    if (receiveSocket.Poll(1000000, SelectMode.SelectRead)) // one-second timeout
                    {
                        int bytesRead = receiveSocket.Receive(netDataState.readBuffer);
                        //string temp = Encoding.UTF8.GetString(networkState.readBuffer);
                        if (0 == bytesRead)
                        {
                            errorLogger.log(ZDLogger.LVL_CRITCAL, "receive 0 byte!");

                            // Changed on 20181120 -begin
                            if (heartbeatMsg == null)
                                onTimeSendHeartbeat();
                            else
                            {
                                bool retResult = sendMsg(heartbeatMsg);
                                //isSocketBroken = true;

                                //// Added on 20180828 -begin
                                ////onConnectStateChange(SOCKET_DISCONNECTED, DateTime.Now.ToString("[yyyyMMdd-HH:mm:ss] ") + "Connection to Server broken!");
                                //// Added on 20180828 -end

                                if (retResult)
                                    continue;
                                else
                                    return null;
                            }
                            // Changed on 20181120 -end

                            //avoid heartbeat flood
                            Thread.Sleep(300);

                        }

                        netDataState.networkParser.AddToParser(netDataState.readBuffer, 0, bytesRead);

                        if (netDataState.networkParser.getRawMsg(out rawMsg))
                            return rawMsg;
                    }
                }
            }
            catch (ObjectDisposedException ode)
            {
                // this exception means socket_ is already closed when poll() is called
                errorLogger.log(ZDLogger.LVL_ERROR, ode.ToString());
            }
            catch (SocketException se)
            {
                isSocketBroken = true;
                onConnectStateChange(SOCKET_DISCONNECTED, DateTime.Now.ToString("[yyyyMMdd-HH:mm:ss] ") + "Connection to Server broken!");
                errorLogger.log(ZDLogger.LVL_ERROR, se.ToString());
            }
            catch (Exception e)
            {
                errorLogger.log(ZDLogger.LVL_ERROR, e.ToString());
            }

            return null;
        }

        public void signalSocketBrokenOnSend()
        {
            isSocketBroken = true;
        }

        public bool sendMsg(byte[] data)
        {
            return sendMsg(data, 0, data.Length);
        }

        public bool sendMsg(byte[] data, int startIdx, int count)
        {
            try
            {
                if (isUnitTestEnabled) return true;

                if (isSocketBroken) return false;

                lock (receiveSocket)
                {
                    NetworkLogic.syncSend(receiveSocket, data, startIdx, count);
                }

                lastSentTimeStamp = DateTime.Now;
                return true;
            }
            catch (SocketException se)
            {
                isSocketBroken = true;
                errorLogger.log(ZDLogger.LVL_ERROR, se.ToString());
                onConnectStateChange(SOCKET_DISCONNECTED, DateTime.Now.ToString("[yyyyMMdd-HH:mm:ss] ") + "Connection to Server broken!");
                return false;
            }
        }

        public bool sendEncryptedMsg(byte[] data)
        {
            try
            {
                if (isSocketBroken) return false;
                NetworkLogic.sendEncryptedData(this.receiveSocket, data);
                lastSentTimeStamp = DateTime.Now;
                return true;
            }
            catch (SocketException ex)
            {
                isSocketBroken = true;
                errorLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
                onConnectStateChange(SOCKET_DISCONNECTED, DateTime.Now.ToString("[yyyyMMdd-HH:mm:ss] ") + "Connection to Server broken!");
            }
            return false;
        }

        public void start()
        {
            stopFlag = false;
            dataFeedThread.Start();
        }

        public void stop()
        {
            stopFlag = true;

            if (zdSession != null)
                zdSession.saveSeqnum();
        }

        public void resetLogger(ZDLogger errLogger)
        {
            this.errorLogger = errLogger;

            if (zdSession != null)
                zdSession.errLogger = errorLogger;

        }

        public void setHeartbeatMsg(byte[] heartbeatMsg)
        {
            this.heartbeatMsg = heartbeatMsg;
        }

        public abstract void onMsgReady(byte[] rawMsg);
        public abstract void onConnectStateChange(int code, string state);
        public abstract void onTimeSendHeartbeat();

        ///
        /// This method must be called before start() method
        ///
        public void setSessionInfo(string sessionId, string seqnumFilePath)
        {

            zdSession = new ZDSession(sessionId, seqnumFilePath, hostIP + "_" + hostPort);
            zdSession.errLogger = errorLogger;

            zdSession.loadSeqnum();
            isSessionServiceEnable = true;
        }
    }

    public class ZDSession
    {
        public string sessionId = null;
        public string seqnumFilePath = null;
        public string seqnumFileName = null;

        public ulong sessionNextSeq = 0;
        private bool isInResendRequest = false;

        public ZDLogger errLogger = null;

        public ZDSession(string sessionId, string seqnumFilePath, string hostIpPort = "")
        {
            this.sessionId = sessionId;
            this.seqnumFilePath = seqnumFilePath;

            if (!Directory.Exists(seqnumFilePath))
                Directory.CreateDirectory(seqnumFilePath);

            if (string.IsNullOrEmpty(hostIpPort))
                seqnumFileName = System.IO.Path.Combine(seqnumFilePath, "Session_" + sessionId + ".txt");
            else
                seqnumFileName = System.IO.Path.Combine(seqnumFilePath, "Session_" + hostIpPort + ".txt");

        }

        public void loadSeqnum()
        {
            if (System.IO.File.Exists(seqnumFileName))
            {
                using (StreamReader sReader = new StreamReader(File.Open(seqnumFileName, FileMode.Open), System.Text.Encoding.ASCII))
                {
                    while (!sReader.EndOfStream)
                    {
                        string oneLine = sReader.ReadLine().Trim();
                        if (!ulong.TryParse(oneLine, out sessionNextSeq))
                            sessionNextSeq = 1;
                    }
                }
            }
            else
            {
                sessionNextSeq = 1;
                saveSeqnum();
            }
        }

        public void saveSeqnum()
        {
            using (StreamWriter sWriter = new StreamWriter(File.Open(seqnumFileName, FileMode.Create, FileAccess.Write), System.Text.Encoding.ASCII))
            {
                sWriter.WriteLine(sessionNextSeq);
            }
        }

        public const byte AND_CHAR_VAL = (byte)'&';
        private ulong gapBeginSeq = 0;
        private ulong gapEndSeq = 0;
        private const string RESEND_REQ = "X@{0}@{1}@{2}@@@@@@&";

        public bool isSeqnumGood(byte[] data)
        {
            try
            {
                if (data == null || data.Length == 0)
                    return true;

                int seqNumBeginIdx = 0;
                int seqNumEndIdx = 0;

                for (int i = NetInfoParser.GOOD_PARSER_OFFSET; i < data.Length; i++)
                {
                    if (data[i] == 2)
                        seqNumBeginIdx = i + 1;
                    else if (data[i] == AND_CHAR_VAL || data[i] == 1)
                    {
                        // 消息中无序列号
                        if (seqNumBeginIdx == 0) return true;

                        seqNumEndIdx = i;
                        break;
                    }
                }

                string strSeq = System.Text.ASCIIEncoding.ASCII.GetString(data, seqNumBeginIdx, seqNumEndIdx - seqNumBeginIdx);
                ulong curSeq = ulong.Parse(strSeq);

                int msgLen = BitConverter.ToInt32(data, 0);

                if (curSeq == 0)
                {
                    // reset resend state
                    isInResendRequest = false;

                    string msg = System.Text.UTF8Encoding.UTF8.GetString(data, NetInfoParser.GOOD_PARSER_OFFSET, msgLen);

                    errLogger.log(ZDLogger.LVL_CRITCAL, msg);
                    errLogger.log(ZDLogger.LVL_CRITCAL, "Server side close gap fill");

                    for (int i = 0; i < NetInfoParser.GOOD_PARSER_OFFSET; i++)
                        data[i] = 0;

                    return true;
                }


                // cut off seqnum from message
                Array.Copy(BitConverter.GetBytes(msgLen - 1 - (seqNumEndIdx - seqNumBeginIdx)), data, NetInfoParser.LEN_PAKCKET_SIZE);
                Array.Copy(data, seqNumEndIdx, data, seqNumBeginIdx - 1, msgLen + NetInfoParser.GOOD_PARSER_OFFSET - seqNumEndIdx);


                if (isInResendRequest)
                {
                    if (curSeq <= gapEndSeq)
                        errLogger.log(ZDLogger.LVL_CRITCAL, "Fill Gap, seqnum=" + curSeq);
                    else
                        sessionNextSeq = curSeq + 1;


                    return true;
                }
                else
                {
                    // 服务器端周中重置到1的情况
                    if (curSeq <= sessionNextSeq)
                    {
                        sessionNextSeq = curSeq + 1;
                        return true;
                    }
                    else
                    {
                        isInResendRequest = true;
                        gapBeginSeq = sessionNextSeq;
                        gapEndSeq = curSeq - 1;
                        sessionNextSeq = curSeq + 1;

                        errLogger.log(ZDLogger.LVL_CRITCAL, "Gap detected, from " + gapBeginSeq + " to " + gapEndSeq);
                        return false;
                    }
                }
            }catch(Exception ex)
            {
                errLogger.log(ZDLogger.LVL_ERROR, ex.ToString());
            }

            return true;
        }

        public string getResendRequestMsg()
        {
            return string.Format(RESEND_REQ, gapBeginSeq, gapEndSeq, this.sessionId);
        }

        public void deleteSeqnumFile()
        {
            if (!string.IsNullOrEmpty(seqnumFileName) && File.Exists(seqnumFileName))
            {
                File.Delete(seqnumFileName);
            }
        }
    }

}
