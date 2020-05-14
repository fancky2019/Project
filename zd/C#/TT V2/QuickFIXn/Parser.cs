using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickFix
{

    public class Parser
    {
        public const int CTL_LENGTH = 12;    // 8=FIX 4.2[]9=
        public const byte DELIMITER = 1;
        public const byte TRAILER_LEN = 7;


        private byte[] buffer_ = new byte[1024 * 1000];
        private int msgBeginindex = 0;
        private int spaceAvailabeIndex = 0;

        //private Encoding e = Encoding.GetEncoding("iso-8859-1");
        //private char tradeMark = (char)174;

        public void AddToParser(ref byte[] data, int offset, int count)
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
                    throw new QuickFIXException("Buffer of Parser is not enough!");
                }
            }
            else
            {
                Array.Copy(data, 0, buffer_, spaceAvailabeIndex, count);
                spaceAvailabeIndex += count;
            }
        }

        public bool ReadFixMessage(out string msg)
        {
            msg = null;

            int offset = msgBeginindex;
            int msgLength = getMsgLength(buffer_, ref offset);

            if (spaceAvailabeIndex - offset - TRAILER_LEN < msgLength)
                return false;
            else
            {
                int msgEndIndex = offset + msgLength + TRAILER_LEN;

                msg = System.Text.Encoding.ASCII.GetString(buffer_, msgBeginindex, msgEndIndex - msgBeginindex);
                               

                msgBeginindex = msgEndIndex;
                return true;
            }

        }

        /// <summary>
        /// offset : postion of next byte after BodyLength delimiter
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public int getMsgLength(byte[] buffer, ref int offset)
        {
            int lengthOfPartMsg = spaceAvailabeIndex - msgBeginindex;
            // BodyLength data is not available
            if (lengthOfPartMsg < 19) return int.MaxValue;

            int count = CTL_LENGTH + offset;
            offset = count;

            byte data = 0;
            while (data != DELIMITER)
            {
                data = buffer[count++];
            }

            string msgLengthBytes = System.Text.Encoding.ASCII.GetString(buffer, offset, count - offset - 1);

            int msgLength = Convert.ToInt32(msgLengthBytes);

            offset = count;
            return msgLength;
        }

    }

    
}
