using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonClassLib.ContractTransfer;
using System.IO;

namespace StockAdapterCiti
{
    class CodeTransfer_Citi
    {

        // Key: ZD code, ex GC1201
        public static Dictionary<string, CodeBean> zd2PhillipMapping = new Dictionary<string, CodeBean>();
        // Key: Security ID
        public static Dictionary<string, CodeBean> phillip2ZdMapping = new Dictionary<string, CodeBean>();


        public static void init(string stockFileStr)
        {
            /*
            if (string.IsNullOrEmpty(stockFileStr))
            {
                stockFileStr = @"config\HKSTOCKINFO_20150613.txt";
            }

            string[] fileStrings = stockFileStr.Split(';');

            foreach (string oneFile in fileStrings)
            {
                if (oneFile.IndexOf("HKSTOCKINFO") > -1)
                    doMapping_HK(oneFile);
                else if (oneFile.IndexOf("USSTOCKINFO") > -1)
                    doMapping_US(oneFile);

            }
            */
        }

        private static void doMapping_US(string fileName)
        {
            using (StreamReader sReader = new StreamReader(File.Open(fileName, FileMode.Open), System.Text.Encoding.ASCII))
            {
                string strHead = sReader.ReadLine().Trim();
                string exchgFlag = strHead.Substring(9, 2);

                while (!sReader.EndOfStream)
                {
                    string oneLine = sReader.ReadLine().Trim();
                    if (string.IsNullOrEmpty(oneLine))
                        continue;

                    if (oneLine.StartsWith("T"))
                    {
                        string text = oneLine.Substring(1).Trim();
                        int tempInt;
                        if (int.TryParse(text, out tempInt)) continue;
                    }

                    string upperExchgCd = null;
                    //Exchange Char 162 165
                    upperExchgCd = oneLine.Substring(161, 4).Trim();


                    //CompanyCode Char 1,10
                    string zdCode = oneLine.Substring(0, 10).Trim();
                    

                    //Symbol Char,166,177
                    //string symbol = oneLine.Substring(165, 12).Trim();

                    //Market Char 160,161
                    string marketCd = oneLine.Substring(159, 2).Trim();

                    CodeBean oneBean = new CodeBean();
                    oneBean.upperExchg = "US"; //upperExchgCd;
                    oneBean.upperProduct = zdCode;
                    oneBean.zdExchg = upperExchgCd;
                    oneBean.zdProduct = zdCode;

                    zd2PhillipMapping.Add(zdCode, oneBean);
                    phillip2ZdMapping.Add(zdCode + "_" + marketCd, oneBean);
                }
            }
        }

        private static void doMapping_HK(string fileName)
        {
            using (StreamReader sReader = new StreamReader(File.Open(fileName, FileMode.Open), System.Text.Encoding.ASCII))
            {
                string strHead = sReader.ReadLine().Trim();
                string exchgFlag = strHead.Substring(9, 2);

                string zdExchgCd = null;
                if (exchgFlag == "HK")
                    zdExchgCd = "HKEX";

                while (!sReader.EndOfStream)
                {
                    string oneLine = sReader.ReadLine().Trim();
                    if (string.IsNullOrEmpty(oneLine))
                        continue;

                    if (oneLine.StartsWith("T"))
                    {
                        string text = oneLine.Substring(1).Trim();
                        int tempInt;
                        if (int.TryParse(text, out tempInt)) continue;
                    }

                    string zdCode = null;
                    //CompanyCode Char 1,10
                    string companyCd = oneLine.Substring(0, 10).Trim();
                    if (companyCd.Length == 7 && char.IsDigit(companyCd, 0))
                        zdCode = "0" + companyCd;
                    else
                        zdCode = companyCd;

                    //Symbol Char,166,177
                    string symbol = oneLine.Substring(165, 12).Trim();
                    //Market Char 160,161
                    string marketCd = oneLine.Substring(159, 2).Trim();

                    CodeBean oneBean = new CodeBean();
                    oneBean.upperExchg = marketCd;
                    oneBean.upperProduct = symbol;
                    oneBean.zdExchg = zdExchgCd;
                    oneBean.zdProduct = zdCode;

                    zd2PhillipMapping.Add(zdCode, oneBean);
                    phillip2ZdMapping.Add(symbol + "_" + marketCd, oneBean);
                }
            }
        }

        public static CodeBean getZDCodeInfoByUpperCode(string upperCode, string upperExchgCode)
        {
            /*
            string key = upperCode + "_" + upperExchgCode;
            if(phillip2ZdMapping.ContainsKey(key))
                return phillip2ZdMapping[key];
            else
                return null;
             */

            string key = upperCode + "_" + upperExchgCode;
            if (phillip2ZdMapping.ContainsKey(key))
                return phillip2ZdMapping[key];
            else
            {
                CodeBean codeBean = new CodeBean();
                codeBean.zdCode = upperCode;
                codeBean.zdExchg = upperExchgCode;
                codeBean.upperProduct = upperCode;
                codeBean.upperExchg = upperExchgCode;

                return codeBean;
            }
        }

        public static CodeBean getUpperCodeInfoByZDCode(string zdCode, string zdExchgCode)
        {
            /*
            if (zd2PhillipMapping.ContainsKey(zdCode))
                return zd2PhillipMapping[zdCode];
            else
                return null;
            */

            if (zd2PhillipMapping.ContainsKey(zdCode))
                return zd2PhillipMapping[zdCode];
            else
            {
                CodeBean codeBean = new CodeBean();
                codeBean.zdCode = zdCode;
                codeBean.zdExchg = zdExchgCode;
                codeBean.upperProduct = zdCode;
                codeBean.upperExchg = zdExchgCode;

                return codeBean;
            }
        }
    }
}
