using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTMarketAdapter.Utilities;

namespace TTMarketAdapter
{
    public class CompatibleOptionCodeConverter
    {
        static List<(string TTExchange, string TTProduct, int Decimal)> _compatibleOption = null;
        static CompatibleOptionCodeConverter()
        {
            _compatibleOption = new List<(string TTExchange, string TTProduct, int Decimal)>();
            if (!string.IsNullOrEmpty(Configurations.Instance.CompatibleOption))
            {
                var array = Configurations.Instance.CompatibleOption.Split(';').ToList();
                array.ForEach(p =>
                {
                    var exchangeProductDecimal = p.Split(':');
                    var exchangeProduct = exchangeProductDecimal[0].Split(',');

                    _compatibleOption.Add((exchangeProduct[0], exchangeProduct[1], int.Parse(exchangeProductDecimal[1])));
                });
            }
        }

        public static bool IsCompatibleOption(string optionContract, ref string newOptionContract)
        {
            var zdProduct = TTMarketAdapterCommon.GetZDProduct(optionContract, SecurityTypeEnum.OPT);
            //SB_P2010 14
            var tt = Configurations.Instance.GetTTProductExchange(zdProduct, SecurityTypeEnum.OPT);

            var compatibleOption = _compatibleOption.FirstOrDefault(p => p.TTExchange == tt.TTExchange && p.TTProduct == tt.TTProduct);
            if (!string.IsNullOrEmpty(compatibleOption.TTExchange) && !string.IsNullOrEmpty(compatibleOption.TTProduct))
            {
                var array = optionContract.Split(' ');
                float strikePrice = float.Parse(array[1]);
                //.##表示最多保留2位有效数字
                StringBuilder formatStr = new StringBuilder();
                formatStr.Append('#', compatibleOption.Decimal);
                var newStrikePrice = strikePrice.ToString($"0.{formatStr.ToString()}");
                newOptionContract = $"{array[0]} {newStrikePrice}";
                return true;
            }
            return false;
        }
    }
}
