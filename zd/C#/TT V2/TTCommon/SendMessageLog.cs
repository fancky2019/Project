using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TT.Common
{
    /// <summary>
    /// 安装压缩程序包 SharpZipLib 程序包 0086版本
    /// </summary>
    public class SendMessageLog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="future">合约名称</param>
        /// <param name="sendMsg">发送到二级行情的字符串</param>
        public static void Log(bool logSendMsg, string zdProduct, string zdCode, string sendMsg)
        {
           
            try
            {
                if (!logSendMsg)
                {
                    return;
                }
                //格式为：1811
                string replaceZDCodeStr = zdCode.Replace(zdProduct, "");
                if (replaceZDCodeStr.Contains(" ") && !replaceZDCodeStr.Contains("-"))
                {
                    if (replaceZDCodeStr.Length > 4)//说明是期权，格式：1811 34
                    {
                        string[] replaceZDCodeStrArray = replaceZDCodeStr.Split(' ');
                        string excutePrice = replaceZDCodeStrArray[1];//获取执行价。
                        if (excutePrice.Contains("."))//如果是小数
                        {
                            while (excutePrice.EndsWith("0"))
                            {
                                excutePrice = excutePrice.TrimEnd('0');
                            }
                            excutePrice = excutePrice.TrimEnd('.');
                        }
                        replaceZDCodeStr = replaceZDCodeStrArray[0] + " " + excutePrice;
                    }
                }
                string dic = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                 $@"SendMsg\{zdProduct}\{replaceZDCodeStr}\{DateTime.Now.Year}\{DateTime.Now.Month}");
                //$@"SendMsg\{DateTime.Now.Year}\{DateTime.Now.Month}\{DateTime.Now.Day}\{zdProduct}\{zdCode.Replace(zdProduct,"")}");
                if (!Directory.Exists(dic))
                {
                    Directory.CreateDirectory(dic);
                }
                string fileName = Path.Combine(dic, $"{DateTime.Now.Day}.txt");
                using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.ASCII))
                    {
                        sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")}  {sendMsg}");
                    }
                }
            }
            catch(Exception ex)
            {
                NLogUtility.Error(ex.ToString());
            }


        }



    }
}
