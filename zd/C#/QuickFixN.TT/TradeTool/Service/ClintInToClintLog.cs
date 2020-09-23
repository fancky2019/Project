using CommonClassLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeTool.Common;
using TradeTool.Model;

namespace TradeTool.Service
{
    public class ClintInToClintLog
    {
        Log _logger = LogManager.GetLogger("TradeTool");

        public List<ClientInLog> ReadClientInData(string fileName)
        {
            // String fileName = "C:\\Users\\Administrator\\Desktop\\ClientIn_20200115.log";
            List<ClientInLog> orderContent = new List<ClientInLog>();
            try
            {
                List<string> allContent = TxtFile.ReadTxtFile(fileName);

                var skipCount = 0;
                var repeatCount = 0;
                var commandCount = 0;
                foreach (var p in allContent)
                {
                    try
                    {
                        //                    int nineteenIndex = p.indexOf("len=");
                        //                    if (nineteenIndex == -1) {
                        //                        continue;
                        //                    }
                        //                    int rightParenthesesIndex = p.indexOf(")");
                        //                    String lenStr = p.substring(nineteenIndex + 4, rightParenthesesIndex);
                        //                    Integer len = Integer.parseInt(lenStr);
                        //                    //不是下单指令，可能是心跳
                        //                    if (len < 100) {
                        //                        continue;
                        //                    }
                        //                    String logTimeStr = p.substring(0, 21);
                        //                    LocalDateTime logTime = LocalDateTime.parse(logTimeStr, DateTimeFormatter.ofPattern("yyyyMMdd HH:mm:ss:SSS"));
                        //                    int rightParenthesisIndex = p.indexOf(")");
                        //                    String content = p.substring(rightParenthesisIndex, p.length() - 1);


                        string logContent = p.Substring(62);
                        int logContentLength = logContent.Length;
                        List<string> listString = new List<string>();
                        //不是下单指令，可能是心跳、登录指令
                        if (logContentLength < 100)
                        {
                            skipCount++;
                            continue;
                        }
                        else
                        {

                            int firstIndex = logContent.IndexOf('}');
                            logContentLength = logContent.Length;

                            if (firstIndex == logContentLength - 1)
                            {
                                listString.Add(logContent);
                            }
                            else
                            {
                                _logger.WriteLog($"repeat - {logContent}");
                                String currentContent = "";
                                while (firstIndex != logContentLength - 1)
                                {
                                    currentContent = logContent.Substring(0, firstIndex + 1);
                                    listString.Add(currentContent);

                                    logContent = logContent.Substring(firstIndex + 1);
                                    logContentLength = logContent.Length;
                                    firstIndex = logContent.IndexOf('}');
                                }
                                listString.Add(logContent);
                                repeatCount += listString.Count - 1;

                            }

                            String logTimeStr = p.Substring(0, 21);
                            DateTime logTime = DateTime.ParseExact(logTimeStr, "yyyyMMdd HH:mm:ss:fff", CultureInfo.InvariantCulture);

                            //DateTime contractDate = DateTime.ParseExact(timeStr, "yyyyMMdd-HH:mm:ss.fff", CultureInfo.InvariantCulture);

                            listString.ForEach(str =>
                              {
                                  ClientInLog clientInLog = new ClientInLog();

                                  clientInLog.LogTime = logTime;
                                  var netInfoStr = GetNetInfoStr(str);
                                  NetInfo netInfo = new NetInfo();
                                  netInfo.MyReadString(netInfoStr);
                                  clientInLog.NetInfo = netInfo;

                                  orderContent.Add(clientInLog);
                              });



                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.WriteLog(p);
                        _logger.WriteLog(ex.ToString());
                    }
                }
                commandCount = orderContent.Count;
                _logger.WriteLog($"totalCount - {allContent.Count} = skipCount:{skipCount} - repeatCount:{repeatCount} + orderContent:{orderContent.Count}");
            }
            catch (Exception ex)
            {
                _logger.WriteLog(ex.ToString());
            }
            return orderContent;
        }

        public List<ClientInLog> ReadToClientData(string fileName)
        {
            List<ClientInLog> orderContent = new List<ClientInLog>();
            try
            {
                List<string> allContent = TxtFile.ReadTxtFile(fileName);
                var commandCount = 0;
                foreach (var p in allContent)
                {
                    try
                    {
                        if (p.Contains("00007818JM000002"))
                        {

                        }
                        var startIndex = p.IndexOf(": ");
                        string logContent = p.Substring(startIndex + 2);
                        int logContentLength = logContent.Length;
                        String logTimeStr = p.Substring(0, 21);
                        DateTime logTime = DateTime.ParseExact(logTimeStr, "yyyyMMdd HH:mm:ss:fff", CultureInfo.InvariantCulture);
                        ClientInLog clientInLog = new ClientInLog();

                        clientInLog.LogTime = logTime;

                        NetInfo netInfo = new NetInfo();
                        netInfo.MyReadString(logContent);
                        clientInLog.NetInfo = netInfo;
                        CheckSystemCode(netInfo);
                        orderContent.Add(clientInLog);
                    }
                    catch (Exception ex)
                    {
                        _logger.WriteLog(p);
                        _logger.WriteLog(ex.ToString());
                    }
                }
                commandCount = orderContent.Count;
                _logger.WriteLog($"totalCount - {allContent.Count} = orderContent:{orderContent.Count}");
            }
            catch (Exception ex)
            {
                _logger.WriteLog(ex.ToString());
            }
            return orderContent.Distinct().ToList();
        }

        /// <summary>
        /// OCG Fill返回头里未赋值SysCode
        /// </summary>
        /// <param name="netInfo"></param>
        private void CheckSystemCode(NetInfo netInfo)
        {
            if (!string.IsNullOrEmpty(netInfo.systemCode))
            {
                return;
            }
            switch (netInfo.code)
            {
                case "ORDER001":
                case "OrdeStHK":


                    break;
                case "CANCST01":
                case "CancStHK":

                    break;
                case "MODIFY01":
                case "ModiStHK":


                    break;
                case "FILCST01":
                case "FillStHK":

                    FilledResponseInfo filledResponseInfo = new FilledResponseInfo();
                    filledResponseInfo.MyReadString(netInfo.infoT);
                    netInfo.systemCode = filledResponseInfo.systemNo;
                    break;
                default:
                    _logger.WriteLog($"订单指令有误 - {netInfo.MyToString()}");
                    break;
            }
        }

        private string GetNetInfoStr(string content)
        {
            //{(len=167)CancStHK@00000@00011140LH000098@8005773@@CO020990001@HKEX@@@@0000588&HKEX_1@@CO020990001@123456@@00011140LH000098@14064865@HKEX@20098.HK@1@12000@0.880000@0@@7@@P@@@@@@}{(len=167)CancStHK@00000@00011140LH000099@8005773@@CO020990001@HKEX@@@@0000589&HKEX_1@@CO020990001@123456@@00011140LH000099@14064866@HKEX@20098.HK@1@12000@0.880000@0@@7@@P@@@@@@}
            int firstIndex = content.IndexOf(')');
            var netInfoStrLength = content.Length - (firstIndex + 1) - 1;
            var netInfoStr = content.Substring(firstIndex + 1, netInfoStrLength);
            return netInfoStr;
        }
    }
}
