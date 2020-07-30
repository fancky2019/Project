using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTMarketAdapter.Utilities;

namespace TTMarketAdapter
{
    public class CompatibleOpenInterestContract
    {
        static List<(string Contract, int Decimal)> _openInterestOptions = null;

        static Dictionary<string, int> _productDecimal = null;
        static CompatibleOpenInterestContract()
        {
            _openInterestOptions = new List<(string Contract, int Decimal)>();
            _productDecimal = new Dictionary<string, int>();
            if (!string.IsNullOrEmpty(Configurations.Instance.OpenInterestContract))
            {
                var array = Configurations.Instance.OpenInterestContract.Split(';').ToList();
                array.ForEach(p =>
                {
                    var contractDecimal = p.Split(',');
                    int decimalCount = int.Parse(contractDecimal[1].Trim());
                    _openInterestOptions.Add((contractDecimal[0].Trim(), decimalCount));
                    var zdProduct = TTMarketAdapterCommon.GetZDProduct(contractDecimal[0].Trim(), SecurityTypeEnum.OPT);

                    if (!_productDecimal.ContainsKey(zdProduct))
                    {
                        _productDecimal.Add(zdProduct, decimalCount);
                    }
                });
            }

        }



        /// <summary>
        /// 兼容老TT有多余零的合约，将老TT有多余零给去除
        /// 
        /// DX_P2010 14.10-->DX_P2010 14.1
        /// </summary>
        /// <param name="optionContract">去零后的合约</param>
        /// <returns></returns>
        public static string ConvertToNewTTContract(string optionContract)
        {
            string newFormatContract = optionContract;

            var openInterestOption = _openInterestOptions.Where(p => p.Contract == optionContract).FirstOrDefault();
            if (!string.IsNullOrEmpty(openInterestOption.Contract))
            {
                var array = optionContract.Split(' ');
                //float strikePrice = float.Parse(array[1]);
                ////.##表示最多保留2位有效数字
                //StringBuilder formatStr = new StringBuilder();
                //formatStr.Append('#', openInterestOption.Decimal);
                //var newStrikePrice = strikePrice.ToString($"0.{formatStr.ToString()}");
                var newStrikePrice = RemoveRedundantZero(array[1], openInterestOption.Decimal);
                newFormatContract = $"{array[0]} {newStrikePrice}";
            }

            return newFormatContract;
        }

        /// <summary>
        /// 四舍五入保留小数位后,去除小数位后多余的零
        /// </summary>
        /// <param name="decimalString">要去除多余零的数字字符串</param>
        /// <param name="decimalCount">最多保留的小数点位数</param>
        /// <returns></returns>
        public static string RemoveRedundantZero(string decimalString, int decimalCount)
        {
            float number = float.Parse(decimalString);
            //.##表示最多保留2位有效数字
            StringBuilder formatStr = new StringBuilder();
            formatStr.Append('#', decimalCount);
            // 四舍五入
            var newdecimalString = number.ToString($"0.{formatStr.ToString()}");
            return newdecimalString;
        }

        /// <summary>
        /// 有持仓的合约转换成老格式合约
        /// </summary>
        /// <param name="optionContract"></param>
        /// <returns></returns>
        public static string CompatibleOpenInterestContracts(string optionContract)
        {
            string oldFormatContract = optionContract;
            var zdProduct = TTMarketAdapterCommon.GetZDProduct(optionContract, SecurityTypeEnum.OPT);
            if (!_productDecimal.ContainsKey(zdProduct))
            {
                return oldFormatContract;
            }

            var array = optionContract.Split(' ');
            var strikePrice = decimal.Parse(array[1]);
            //var oldFormatContractTemp = $"{array[0]} {SaveSpecifyCountDecimal(strikePrice, _productDecimal[zdProduct])}";

            var oldFormatContractTemp = $"{array[0]} {strikePrice.ToString($"F{_productDecimal[zdProduct]}")}";


            //品种的合约是否在有持仓的合约中。
            var openInterestOption = _openInterestOptions.Where(p => p.Contract == oldFormatContractTemp).FirstOrDefault();
            if (!string.IsNullOrEmpty(openInterestOption.Contract))
            {
                oldFormatContract = oldFormatContractTemp;
            }
            return oldFormatContract;
        }

        //public static string SaveSpecifyCountDecimal(decimal dec, int count)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("0.");
        //    sb.Append('0', count);
        //    var str = dec.ToString(sb.ToString());
        //    return str;
        //}
    }
}
