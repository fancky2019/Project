using QuickFix.Fields;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDTradeClientTT
{
    public class PersistOrders
    {

        public static void LoadOrder(ConcurrentDictionary<long, RefObj> xReference, ConcurrentDictionary<string, RefObj> downReference)
        {
            var fileName = ZDTradeClientTTConfiurations.PersistOrders;
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
                                else if (oneLine.StartsWith("[fromClient]"))
                                {
                                    sateFlag = stateFromClient;
                                }
                                else if (oneLine.StartsWith("[fromGlobex]"))
                                {
                                    refObj.newOrderSingle = new QuickFix.FIX42.NewOrderSingle();
                                    refObj.newOrderSingle.FromString(refObj.fromClient[0].ToString(), false, null, null);

                                    sateFlag = stateFromGlobex;
                                }
                            }
                            else
                            {
                                //if (sateFlag == stateNewOrderSingle)
                                //{
                                //    refObj.newOrderSingle = new QuickFix.FIX42.NewOrderSingle();
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
                                    QuickFix.FIX42.ExecutionReport msg = new QuickFix.FIX42.ExecutionReport();
                                    msg.FromString(oneLine, false, null, null);
                                    refObj.fromGlobex.Add(msg);
                                }
                            }
                        }
                    }
                }


            }
        }

        public static void PersistOrder(ConcurrentDictionary<long, RefObj> xReference)
        {
            var fileName = ZDTradeClientTTConfiurations.PersistOrders;
            var directory = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (StreamWriter sWriter = new StreamWriter(File.Open(fileName, FileMode.Create, FileAccess.ReadWrite), System.Text.Encoding.UTF8))
            {
                foreach (long key in xReference.Keys)
                {
                    RefObj refObj = xReference[key];

                    try
                    {
                        //将GTC单和日单持久化在一个文件中。
                        if (refObj.newOrderSingle.TimeInForce.getValue() != QuickFix.Fields.TimeInForce.GOOD_TILL_CANCEL)
                        {
                            //日单-只保存当日的
                            //Time, in UTC, the message was sent.52=20200623-05:05:55.931
                            string sendingTimeStr = refObj.newOrderSingle.Header.GetField(Tags.SendingTime);
                            var sendingTime = DateTime.ParseExact(sendingTimeStr, "yyyyMMdd-HH:mm:ss.fff", CultureInfo.InvariantCulture);
                            sendingTime = sendingTime.AddHours(8);
                            if (sendingTime.Date != DateTime.Now.Date)
                            {
                                continue;
                            }
                        }


                        sWriter.WriteLine("[Entry Begin]");
                        sWriter.WriteLine("[ClinetOrderID]:" + key);
                        sWriter.WriteLine("[orderID]:" + refObj.orderID);
                        sWriter.WriteLine("[cumFilled]:" + refObj.cumFilled);
                        //mantis0002295  add strArray[5] by xiang at 20171219

                        //fancky  modify 20180629
                        StringBuilder sb = new StringBuilder("[xRef]:");
                        for (int i = 0; i < refObj.strArray.Length; i++)
                        {
                            if (i != refObj.strArray.Length - 1)
                            {
                                sb.Append($"{refObj.strArray[i]},");
                            }
                            else
                            {
                                sb.Append($"{refObj.strArray[i]}");
                            }
                        }
                        //sWriter.WriteLine("[xRef]:" + refObj.strArray[0] + "," + refObj.strArray[1] + "," + refObj.strArray[2] + "," + refObj.strArray[3] + "," + refObj.strArray[4] + "," + refObj.strArray[5]);
                        sWriter.WriteLine(sb.ToString());

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
                        TT.Common.NLogUtility.Error("refObj.newOrderSingle:" + refObj.newOrderSingle?.ToString());
                        TT.Common.NLogUtility.Error("refObj.strArray:" + refObj.strArray?.ToString());
                        TT.Common.NLogUtility.Error(ex.ToString());
                    }
                }
            }
        }

    }
}
