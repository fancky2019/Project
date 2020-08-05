using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Utility.MemoryDataManager.Persist
{
    class SQLitePersist : IPersist
    {
        private static readonly NLog.Logger _nLog = NLog.LogManager.GetCurrentClassLogger();
        private object _lockObj = new object();

        private string _conString = "";
        public SQLitePersist()
        {
            // Phillip
            _conString = $"Data Source={System.Environment.CurrentDirectory}\\SQLite\\Phillip.db;version=3;UseUTF16Encoding=True;Pooling=true;Max Pool Size=100;";

        }
        public void Load()
        {
            throw new NotImplementedException();
        }

        internal void UpdateClientOrderID(string clientOrderID)
        {
            //String insertCommand = "insert into Person(name)values('fancky1');";
            using (SQLiteConnection connection = new SQLiteConnection(_conString))
            {
                connection.Open();

                using (SQLiteCommand mycommand = new SQLiteCommand(connection))
                {
                    mycommand.CommandText = $"update ClientOrderID set ClientOrderID ='{clientOrderID}';";
                    mycommand.Prepare();
                    mycommand.ExecuteNonQuery();
                }
            }

        }

        internal void UpdateOrder(byte[] bytes)
        {
            //String insertCommand = "insert into Person(name)values('fancky1');";
            using (SQLiteConnection connection = new SQLiteConnection(_conString))
            {
                connection.Open();

                using (SQLiteCommand mycommand = new SQLiteCommand(connection))
                {
                    //mycommand.CommandText = $"update ClientOrderID set ClientOrderID ='{clientOrderID}';";
                    mycommand.Prepare();
                    mycommand.ExecuteNonQuery();
                }
            }
            //  SqlCommand cmd = new SqlCommand("select count(id) from Student where Name=@ass and Pwd=@add", conn);
        }

        internal void InsertOrder(byte[] bytes)
        {
            //String insertCommand = "insert into Person(name)values('fancky1');";
            using (SQLiteConnection connection = new SQLiteConnection(_conString))
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "Insert into 'Order' (OrderBytes) values (@OrderBytes);";
                    SQLiteParameter myParameter = new SQLiteParameter("@OrderBytes", DbType.Binary);
                    myParameter.Value = bytes;

                    //cmd.Parameters.AddWithValue("@OrderBytes", bytes);
                    cmd.Parameters.Add(myParameter);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }

        }

        internal void InsertClientOderID(string clientOderID)
        {
            //String insertCommand = "insert into Person(name)values('fancky1');";
            using (SQLiteConnection connection = new SQLiteConnection(_conString))
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "insert into ClientOrderID (CliOrderID) values (@clientOderID);";
                    cmd.Parameters.AddWithValue("@clientOderID", clientOderID);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }

        }

        internal byte[] SelectOrder()
        {
            byte[] bytes = null;
            using (SQLiteConnection connection = new SQLiteConnection(_conString))
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "select OrderBytes from 'Order' where id=1";

                    cmd.Prepare();
                    bytes = (byte[])cmd.ExecuteScalar();
                }
            }

            return bytes;

        }

        public void Persist()
        {
            lock (_lockObj)
            {
            }
        }
    }
}
