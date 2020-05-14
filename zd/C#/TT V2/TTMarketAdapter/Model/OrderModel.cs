using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TTMarketAdapter.Model
{
    public class OrderModel
    {
        /// <summary>
        /// 直达的合约
        /// </summary>
        public string ZDContract { get; set; }
        /// <summary>
        /// Tag 55
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Tag 167
        /// </summary>
        public string SecurityType { get; set; }
        /// <summary>
        /// Tag 207
        /// </summary>
        public string SecurityExchange { get; set; }
        /// <summary>
        /// Tag 455
        /// BRN Aug25
        /// 455=BRN Jul20-Oct25
        /// </summary>
        public string SecurityAltID { get; set; }
        /// <summary>
        /// Tag 456
        /// </summary>
        public string SecurityAltIDSource { get; set; } = "97";

        public override string ToString()
        {
            //str:"BRN,ICE,FUT,BRN May19,97"
            return $"{ZDContract},{Symbol},{SecurityExchange},{SecurityType},{SecurityAltID},{SecurityAltIDSource}";
        }

        public (bool Success, string ErrorMessage) Validate()
        {
            bool success = true;
            StringBuilder errorMessage = new StringBuilder("Server Exception:");
            if (string.IsNullOrEmpty(this.Symbol))
            {
                success = false;
                errorMessage.Append("Product Not Found!");
            }
            if (string.IsNullOrEmpty(this.SecurityExchange))
            {
                success = false;
                errorMessage.Append("Exchange Not Found!");
            }

            if (string.IsNullOrEmpty(this.SecurityType))
            {
                success = false;
                errorMessage.Append("Server deals SecurityType Exception!");
            }

            if (string.IsNullOrEmpty(this.SecurityAltID))
            {
                success = false;
                errorMessage.Append("Server deals SecurityAltID Exception!");
            }

            return (success, errorMessage.ToString());
        }
        public static OrderModel FromString(string str)
        {
            //str:"BRN,ICE,FUT,BRN May19,97"
            try
            {
                OrderModel orderModel = new OrderModel();
                var array = str.Split(',');
                orderModel.ZDContract = array[0];
                orderModel.Symbol = array[1];
                orderModel.SecurityExchange = array[2];
                orderModel.SecurityType = array[3];
                orderModel.SecurityAltID = array[4];
                orderModel.SecurityAltIDSource = array[5];
                return orderModel;
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error($"OrderModel.FromString: {str}");
                return null;
            }
        }

        public static List<OrderModel> FromFile(string fileName = @"config/OrderModel.csv")
        {
            List<OrderModel> list = new List<OrderModel>();
            try
            {
                if (File.Exists(fileName))
                {
                    using (StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open)))
                    {
                        try
                        {
                            sr.ReadLine();//读取列头
                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine().Trim();
                                if (!string.IsNullOrEmpty(line))
                                {
                                    var model = FromString(line);
                                    if (model == null)
                                    {
                                        TT.Common.NLogUtility.Info($"生成OrderModel出错：{line}");
                                    }
                                    else
                                    {
                                        list.Add(model);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            TT.Common.NLogUtility.Error(ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error(ex.ToString());
            }
            return list;
        }

        /// <summary>
        /// 删除一个月以前的合约
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static List<OrderModel> FilterExpire(List<OrderModel> list)
        {
            List<OrderModel> unExpireList = new List<OrderModel>();

            // DX_S1806-1809--->BRN Jul20-Oct25 Calendar
            list?.ForEach(p =>
            {
                bool isExpire = false;
                switch (p.SecurityType)
                {
                    case "FUT":
                        //NI3M:跳过LME交易所的
                        if (!p.ZDContract.EndsWith("3M"))
                        {
                            // BRN1908
                            var yearMonth = p.ZDContract.Substring(p.ZDContract.Length - 4, 4);
                            isExpire = IsExpire(yearMonth);
                        }
                        break;
                    case "MLEG":
                        //  DX_S1806 - 1809 
                        var _sIndex = p.ZDContract.IndexOf("_S");//2
                        var lefLegYearMonth = p.ZDContract.Substring(_sIndex + 2, 4);//1806
                        isExpire = IsExpire(lefLegYearMonth);
                        break;
                    case "OPT":
                        break;
                }
                if (!isExpire)
                {
                    unExpireList.Add(p);
                }
            });
            return unExpireList;
        }

        private static bool IsExpire(string yearMonth)
        {
            //19
            var yearNumber = yearMonth.Substring(0, 2);
            //08
            var monthNumber = yearMonth.Substring(2, 2);
            var yearBegin = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Substring(0, 2);

            var dateStr = $"{yearBegin}{yearNumber}{monthNumber}";
            DateTime contractDate = DateTime.ParseExact(dateStr, "yyyyMM", CultureInfo.InvariantCulture);
            DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);
            return oneMonthAgo > contractDate;
        }

        public static void SaveToFile(List<OrderModel> list, string fileName = @"config/OrderModel.csv")
        {

            // var fileName =Path.Combine( AppDomain.CurrentDomain.BaseDirectory, @"config/账号文档链接.txt");
            // fileName = @"config/账号文档链接.txt";
            fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (File.Exists(fileName))
            {
                //备份之前文件
                var extension = Path.GetExtension(fileName);
                //移动：备份之前文件
                File.Move(fileName, @"config/" + Path.GetFileNameWithoutExtension(fileName) + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + extension);
            }
            WriteDada(fileName, FilterExpire(list));


        }

        public static void WriteDada(string fileName, List<OrderModel> list)
        {
            using (StreamWriter sw = new StreamWriter(File.Open(fileName, FileMode.Create, FileAccess.Write), System.Text.Encoding.Default))
            {
                string header = "ZDContract,Symbol,SecurityExchange,SecurityType,SecurityAltID,SecurityAltIDSource";
                sw.WriteLine(header);//写列头
                list?.ForEach(p =>
                {
                    sw.WriteLine(p.ToString());
                });
            }
        }




    }
}
