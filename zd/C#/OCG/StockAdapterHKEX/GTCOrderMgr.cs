using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Concurrent;
using AuthCommon;
using StockAdapterHKEX;
using CommonClassLib;
using StockAdapterHKEX.Common;

namespace StockAdapterHKEX
{
    public class GTCOrderMgr
    {
        private static string fileName = null;
        private static ConcurrentDictionary<long, RefObj> xReference = null;
        private static ConcurrentDictionary<string, RefObj> downReference = null;
        private static Log globexIFLogger = null;

        public static void loadGTCOrder(string fileName, ConcurrentDictionary<long, RefObj> xReference, ConcurrentDictionary<string, RefObj> downReference, Log globexIFLogger)
        {
            GTCOrderMgr.globexIFLogger = globexIFLogger;
            GTCOrderMgr.fileName = fileName;
            GTCOrderMgr.xReference = xReference;
            GTCOrderMgr.downReference = downReference;

            OrderRefMgr.prepareFile(fileName);
            OrderRefMgr.loadOrder(fileName, xReference, downReference);
        }

        public static void persistGTCOrder()
        {
            if (GTCOrderMgr.fileName == null || GTCOrderMgr.xReference == null) return;
            OrderRefMgr.prepareFile(fileName);
            OrderRefMgr.persistOrder(fileName, OrderCategory.GTCOrder, xReference, globexIFLogger);
        }

    }

    public class NonGTCOrderMgr
    {
        private static string fileName = null;
        private static ConcurrentDictionary<long, RefObj> xReference = null;
        private static ConcurrentDictionary<string, RefObj> downReference = null;
        private static Log globexIFLogger = null;


        public static void loadNonGTCOrder(string fileName, ConcurrentDictionary<long, RefObj> xReference, ConcurrentDictionary<string, RefObj> downReference, Log globexIFLogger)
        {
            NonGTCOrderMgr.globexIFLogger = globexIFLogger;
            NonGTCOrderMgr.fileName = fileName;
            NonGTCOrderMgr.xReference = xReference;
            NonGTCOrderMgr.downReference = downReference;

            OrderRefMgr.prepareFile(fileName);
            OrderRefMgr.loadOrder(fileName, xReference, downReference);

            if (File.Exists(fileName))
                File.Delete(fileName);
        }

        public static void persistNonGTCOrder()
        {
            if (NonGTCOrderMgr.fileName == null || NonGTCOrderMgr.xReference == null) return;
            OrderRefMgr.prepareFile(fileName);
            OrderRefMgr.persistOrder(fileName, OrderCategory.Non_GTCOrder, xReference, globexIFLogger);
        }

    }

    

    public enum OrderCategory
    {
        GTCOrder,
        Non_GTCOrder
    }

    class OrderRefMgr
    {
        public static void prepareFile(string fileName)
        {
            int idx = fileName.LastIndexOf(@"\");
            string path = fileName.Substring(0, idx);
            //string actualFileName = fileName.Substring(idx + 1);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static void loadOrder(string fileName, ConcurrentDictionary<long, RefObj> xReference, ConcurrentDictionary<string, RefObj> downReference)
        {
            if (File.Exists(fileName))
            {
                using (StreamReader sReader = new StreamReader(File.Open(fileName, FileMode.Open), System.Text.Encoding.ASCII))
                {
                    int stateOther = 0;
                    //int stateNewOrderSingle = 1;
                    int stateFromClient = 2;
                    int stateFromGlobex = 3;

                    int sateFlag = -1;
                    RefObj refObj = null;

                    while (!sReader.EndOfStream)
                    {
                        string oneLine = sReader.ReadLine().Trim();
                        if (oneLine.Length > 0)
                        {

                            if (oneLine[0] == '[')
                            {
                                if (oneLine.StartsWith("[Entry Begin]"))
                                {
                                    sateFlag = stateOther;
                                }
                                else if (oneLine.StartsWith("[ClinetOrderID]"))
                                {
                                    int idx = oneLine.IndexOf(':');
                                    string strClOrderId = oneLine.Substring(idx + 1);
                                    long clOrderId = long.Parse(strClOrderId);
                                    RefObj oneObj = new RefObj();
                                    oneObj.clOrderID = strClOrderId;
                                    xReference.TryAdd(clOrderId, oneObj);
                                    refObj = oneObj;
                                }
                                else if (oneLine.StartsWith("[orderID]"))
                                {
                                    int idx = oneLine.IndexOf(':');
                                    refObj.orderID = oneLine.Substring(idx + 1);
                                    downReference.TryAdd(refObj.orderID, refObj);
                                }
                                else if (oneLine.StartsWith("[cumFilled]"))
                                {
                                    int idx = oneLine.IndexOf(':');
                                    refObj.cumFilled = int.Parse(oneLine.Substring(idx + 1));
                                }
                                else if (oneLine.StartsWith("[xRef]"))
                                {
                                    int idx = oneLine.IndexOf(':');
                                    refObj.strArray = oneLine.Substring(idx + 1).Split(',');
                                }
                                //else if (oneLine.StartsWith("[newOrderSingle]"))
                                //{
                                //    sateFlag = stateNewOrderSingle;
                                //}
                                else if (oneLine.StartsWith("[lastSendInfo]"))
                                {
                                    int idx = oneLine.IndexOf(':');
                                    NetInfo netInfo = new NetInfo();
                                    netInfo.MyReadString(oneLine.Substring(idx + 1));
                                    refObj.lastSendInfo = netInfo;
                                }
                                else if (oneLine.StartsWith("[fromClient]"))
                                {
                                    sateFlag = stateFromClient;
                                }
                                else if (oneLine.StartsWith("[fromGlobex]"))
                                {
                                    refObj.newOrderSingle = new QuickFix.FIX50.NewOrderSingle();
                                    refObj.newOrderSingle.FromString(refObj.fromClient[0].ToString(), false, null, null);

                                    sateFlag = stateFromGlobex;
                                }
                            }
                            else
                            {
                                //if (sateFlag == stateNewOrderSingle)
                                //{
                                //    refObj.newOrderSingle = new QuickFix.FIX50.NewOrderSingle();
                                //    refObj.newOrderSingle.FromString(oneLine, false, null, null);
                                //}
                                //else 
                                if (sateFlag == stateFromClient)
                                {
                                    QuickFix.Message msg = new QuickFix.Message();
                                    msg.FromString(oneLine, false, null, null);
                                    refObj.fromClient.Add(msg);
                                }
                                else if (sateFlag == stateFromGlobex)
                                {
                                    QuickFix.FIX50.ExecutionReport msg = new QuickFix.FIX50.ExecutionReport();
                                    msg.FromString(oneLine, false, null, null);
                                    refObj.fromGlobex.Add(msg);
                                }
                            }
                        }
                    }
                }


            }
        }

        public static void persistOrder(string fileName, OrderCategory orderCategory, ConcurrentDictionary<long, RefObj> xReference, Log globexIFLogger)
        {

            using (StreamWriter sWriter = new StreamWriter(File.Open(fileName, FileMode.Create, FileAccess.Write), System.Text.Encoding.ASCII))
            {

                StringBuilder sb = new StringBuilder();

                foreach (long key in xReference.Keys)
                {
                    RefObj refObj = xReference[key];

                    try
                    {
                        //if (orderCategory == OrderCategory.GTCOrder)
                        //{
                        //    if (refObj.newOrderSingle.TimeInForce.getValue() != QuickFix.Fields.TimeInForce.GOOD_TILL_CANCEL) continue;
                        //}
                        //else if (orderCategory == OrderCategory.Non_GTCOrder)
                        //{
                        //    if (refObj.newOrderSingle.TimeInForce.getValue() == QuickFix.Fields.TimeInForce.GOOD_TILL_CANCEL) continue;
                        //}

                        sWriter.WriteLine("[Entry Begin]");
                        sWriter.WriteLine("[ClinetOrderID]:" + key);
                        sWriter.WriteLine("[orderID]:" + refObj.orderID);
                        sWriter.WriteLine("[cumFilled]:" + refObj.cumFilled);
                        // Changed on 20160601 -begin
                        //sWriter.WriteLine("[xRef]:" + refObj.strArray[0] + "," + refObj.strArray[1] + "," + refObj.strArray[2] + "," + refObj.strArray[3] + "," + refObj.strArray[4]);
                        sb.Clear();
                        sb.Append("[xRef]:");

                        if (refObj.strArray != null)
                        {
                            sb.Append((refObj.strArray.Length > 0) ? refObj.strArray[0] : "");
                            for (int i = 1; i < refObj.strArray.Length; i++)
                                sb.Append(',').Append(refObj.strArray[i]);
                        }
                        sWriter.WriteLine(sb.ToString());

                        sWriter.WriteLine($"[lastSendInfo]:{refObj.lastSendInfo.MyToString()}");

                        // Changed on 20160601 -end

                        //sWriter.WriteLine("[newOrderSingle]:");
                        //sWriter.WriteLine(refObj.newOrderSingle.ToString());

                        sWriter.WriteLine("[fromClient]:" + refObj.fromClient.Count);
                        for (int i = 0; i < refObj.fromClient.Count; i++)
                            sWriter.WriteLine(refObj.fromClient[i].ToString());

                        sWriter.WriteLine("[fromGlobex]:" + refObj.fromGlobex.Count);
                        for (int i = 0; i < refObj.fromGlobex.Count; i++)
                            sWriter.WriteLine(refObj.fromGlobex[i].ToString());


          
                        
                        sWriter.Flush();
                    }
                    catch (Exception ex)
                    {
                        if(refObj.newOrderSingle != null)
                            globexIFLogger.WriteLog( "For debug:" + refObj.newOrderSingle.ToString());

                        try
                        {
                            globexIFLogger.WriteLog( "refObj.strArray:" + refObj.strArray.ToString());
                        }catch(Exception)
                        {
                        }

                        globexIFLogger.WriteLog(ex.ToString());
                    }
                }
            }
        }
    }
}
