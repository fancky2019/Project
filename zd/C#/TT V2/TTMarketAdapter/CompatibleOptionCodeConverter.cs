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
            if (!string.IsNullOrEmpty(Configurations.CompatibleOption))
            {
                var array = Configurations.CompatibleOption.Split(';').ToList();
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
            var tt = Configurations.GetTTProductExchange(zdProduct, SecurityTypeEnum.OPT);
            if (_compatibleOption.Exists(p => p.TTExchange == tt.TTExchange && p.TTProduct == tt.TTProduct))
            {
                var array = optionContract.Split(' ');
                float strikePrice = float.Parse(array[1]);
                var newStrikePrice = strikePrice.ToString("0.##");
                newOptionContract = $"{array[0]} {newStrikePrice}";
                return true;
            }
            return false;
        }
    }
}
