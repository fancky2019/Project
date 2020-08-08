using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDFixService.Service.MemoryDataManager.Persist
{
    class SQLiteHelper
    {
        private static string _conString = "";
        static SQLiteHelper()
        {
            _conString = $"Data Source={System.Environment.CurrentDirectory}\\SQLite\\Phillip.db;version=3;UseUTF16Encoding=True;Pooling=true;Max Pool Size=100;";
        }

   
        internal static void UpdateClientOrderID(string clientOrderID)
        {
            //String insertCommand = "insert into Person(name)values('fancky1');";
            using (SQLiteConnection connection = new SQLiteConnection(_conString))
            {
                connection.Open();

                using (SQLiteCommand mycommand = new SQLiteCommand(connection))
                {
                    mycommand.CommandText = $"update ClientOrderID set CliOrderID  ='{clientOrderID}';";
                    mycommand.Prepare();
                    mycommand.ExecuteNonQuery();
                }
            }

        }

        internal static void UpdateOrder(byte[] bytes)
        {
            //String insertCommand = "insert into Person(name)values('fancky1');";
            using (SQLiteConnection connection = new SQLiteConnection(_conString))
            {
                connection.Open();

                using (SQLiteCommand mycommand = new SQLiteCommand(connection))
                {
                    mycommand.CommandText = $"update 'Order' set OrderBytes = @OrderBytes;";
                    mycommand.Parameters.AddWithValue("@OrderBytes", bytes);
                    mycommand.Prepare();
                    mycommand.ExecuteNonQuery();
                }
            }
            //  SqlCommand cmd = new SqlCommand("select count(id) from Student where Name=@ass and Pwd=@add", conn);
        }

        internal static void InsertOrder(byte[] bytes)
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

        internal static void InsertClientOderID(string clientOderID)
        {
            //String insertCommand = "insert into Person(name)values('fancky1');";
            using (SQLiteConnection connection = new SQLiteConnection(_conString))
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "insert into ClientOderID (CliOrderID) values (@CliOrderID);";
                    cmd.Parameters.AddWithValue("@CliOrderID", clientOderID);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }

        }

        internal static byte[] SelectOrder()
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

        internal static string SelectClientOrderID()
        {
            string clientOrderID = null;
            using (SQLiteConnection connection = new SQLiteConnection(_conString))
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "select CliOrderID from 'ClientOrderID' where id=1";

                    cmd.Prepare();
                    clientOrderID = (string)cmd.ExecuteScalar();
                }
            }

            return clientOrderID;

        }
    }
}
