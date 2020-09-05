using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonClassLib.ContractTransfer;

namespace StockAdapterHKEX
{
    class CodeTransfer_HKEX
    {
        private static Dictionary<string, HKCodeBean> zd2hkexMapping = new Dictionary<string, HKCodeBean>();
        private static Dictionary<string, HKCodeBean> hkex2ZdMapping = new Dictionary<string, HKCodeBean>();

        public static void init()
        {
        }

        public static HKCodeBean getZDCodeInfoByUpperCode(string upperCode, string upperExchgCode)
        {
            if (hkex2ZdMapping.ContainsKey(upperCode))
                return hkex2ZdMapping[upperCode];

            return null;
        }

        public static HKCodeBean getUpperCodeInfoByZDCode(string zdCode, string zdExchgCode)
        {
            if (zd2hkexMapping.ContainsKey(zdCode))
                return zd2hkexMapping[zdCode];
            else
            {
                HKCodeBean codeBean = new HKCodeBean();
                codeBean.upperExchg = "XHKG";
                //codeBean.upperProduct = int.Parse(zdCode.Replace(".HK","")).ToString();//为了去除股票代码左边的0
                codeBean.upperProduct = zdCode.Replace(".HK", "").TrimStart('0');//为了去除股票代码左边的0
                codeBean.zdExchg = "HKEX";
                codeBean.zdProduct = zdCode;
                codeBean.stockSize = 1000;

                zd2hkexMapping.Add(zdCode, codeBean);
                hkex2ZdMapping.Add(codeBean.upperProduct, codeBean);
                return codeBean;
            }
        }
    }
}
