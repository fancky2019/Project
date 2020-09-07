using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeTool.Common;
using TradeTool.Model;

namespace TradeTool.Service
{
    class ClintInToClintLog
    {
        private List<ClientInLog> ReadData(string fileName)
        {
            var _logger = LogManager.GetLogger("TradeTool");
            // String fileName = "C:\\Users\\Administrator\\Desktop\\ClientIn_20200115.log";
            List<ClientInLog> orderContent = new List<ClientInLog>();
            try
            {
                List<string> allContent = TxtFile.ReadTxtFile(fileName);


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
                            }

                            String logTimeStr = p.Substring(0, 21);
                            DateTime logTime = DateTime.Parse(logTimeStr);

                            listString.ForEach(str =>
                              {
                                  ClientInLog clientInLog = new ClientInLog();

                                  clientInLog.LogTime = logTime;
                                  clientInLog.Content = str;
                                  orderContent.Add(clientInLog);
                              });

                            //                        int m = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.WriteLog(p);
                        _logger.WriteLog(ex.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.WriteLog(ex.ToString());
            }
            return orderContent;
        }
    }
}
