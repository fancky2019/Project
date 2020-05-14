using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthCommon;
using System.Data.SqlClient;
using CommonClassLib;
using System.Configuration;
using System.Data;
using System.Reflection;

namespace TTMarketAdapter
{
    /// <summary>
    /// 周2,3,4,5清除结算价数据Model
    /// </summary>
    public class TNewDayTime
    {
        /// <summary>
        /// zd交易所名称
        /// </summary>
        public string ExchgName { get; set; }
        /// <summary>
        /// zd交易所代码
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 交易所开始挂单时间提前两分钟
        /// </summary>
        public int OffsetMinute { get; set; }

        public int ID { get; set; }
        public TimeSpan MarketTime { get; set; }

        public SecurityTypeEnum SecurityType { get; set; }
    }


    public class TCommodity
    {

        public int FutureID { get; set; }
        public string FutureCode { get; set; }
        public int OptionID { get; set; }
        public string OptionCode { get; set; }
        public string OptionName { get; set; }
        public string FExchangeNo { get; set; }
        public string FCurrencyNo { get; set; }
        public string CurrencyName { get; set; }
    }

    public interface ITNewDayTimeDBHandler
    {
        List<TNewDayTime> GetTNewDayTimeList();
    }

    public class DBHandlerFactory
    {
        private static ITNewDayTimeDBHandler _iMarketDataDBHandler = null;
        private static object myLock = new object();
     //   public static ZDLogger errorLogger = null;
        //static DBHandlerFactory()
        //{
        //    errorLogger = new SynWriteLogger("DB_Error.log");
        //    errorLogger.setLogLevel(ZDLogger.LVL_DEBUG);
        //}
        public static ITNewDayTimeDBHandler ITNewDayTimeDBHandler
        {
            get
            {
                if (_iMarketDataDBHandler == null)
                {
                    lock (myLock)
                    {
                        if (_iMarketDataDBHandler == null)
                        {
                            _iMarketDataDBHandler = new TNewDayTimeDBHandlerImpl();
                        }
                    }
                }
                return _iMarketDataDBHandler;
            }
        }

    }

    public class TNewDayTimeDBHandlerImpl : ITNewDayTimeDBHandler
    {

        private string connDBShare = null;
        //private ZDLogger errorLogger = null;
        public TNewDayTimeDBHandlerImpl()
        {
            //errorLogger = zdLogger;
            connDBShare = Configurations.ForeignShareStr;
        }



        public List<TNewDayTime> GetTNewDayTimeList()
        {
            List<TNewDayTime> tNewDayTimeList = new List<TNewDayTime>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connDBShare))
                {
                    string queryStr = @"SELECT  [ExchgName] ,[ProductCode] ,[OffsetMinute] FROM [ForeignShare].[dbo].[TNewDayTime]";
                    SqlCommand command = new SqlCommand(queryStr, connection);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TNewDayTime tNewDayTime = new TNewDayTime();

                            tNewDayTime.ExchgName = reader.GetString(0);
                            tNewDayTime.ProductCode = reader.GetString(1);
                            tNewDayTime.OffsetMinute = reader.GetInt32(2);
                            tNewDayTimeList.Add(tNewDayTime);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TT.Common.NLogUtility.Error( ex.ToString());
            }
            return tNewDayTimeList;

        }

      
    }

    public class DBHelper<T> where T : class
    {
        public static List<T> GetList(string selectCommand)
        {
            using (SqlConnection con = new SqlConnection(Configurations.ForeignShareStr))
            {
                SqlDataAdapter sda = new SqlDataAdapter(selectCommand, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return DataSetToList(ds);
            }
        }

        #region 将从数据中获取的数据装换成泛型实体
        /// <summary>
        /// 将从数据中获取的数据装换成泛型实体
        /// </summary>
        /// <typeparam name="T">要获得的数据类型</typeparam>
        /// <param name="dataSet">要转化成泛型list的dataset数据源</param>
        /// <param name="tableIndex">表索引</param>
        /// <returns></returns>
        public static List<T> DataSetToList(DataSet dataSet, int tableIndex = 0)
        {
            //确认参数有效
            if (dataSet == null || dataSet.Tables.Count <= 0 || tableIndex < 0)
            {
                return null;
            }
            DataTable dt = dataSet.Tables[tableIndex];
            List<T> list = new List<T>();
            //遍历所有行
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //创建泛型对象(用于保存遍历该行的数据）
                T _t = Activator.CreateInstance<T>();
                //获取对象所有属性
                PropertyInfo[] propertyInfo = _t.GetType().GetProperties();
                //遍历所有列
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        if (dt.Columns[j].ColumnName.Equals(info.Name))
                        {
                            if (dt.Rows[i][j] != DBNull.Value)
                            {
                                info.SetValue(_t, dt.Rows[i][j], null);
                            }
                            else
                            {
                                info.SetValue(_t, null, null);
                            }
                            break;
                        }
                    }
                }
                list.Add(_t);
            }
            return list;
        }
        #endregion

    }
}
