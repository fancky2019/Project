using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewClassesApi.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NewClassesApi
{
    public class NewClassesDbContext : DbContext
    {

        public NewClassesDbContext(DbContextOptions options) : base(options)
        {
         
        }

    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FeedBack>().ToTable("FeedBack", "dbo");
     
        }

       //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(Configuration["NewClassesConStr"]);

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(@"data source=(local);initial catalog=WMS;persist security info=True;user id=sa;password=123456");




        public virtual DbSet<FeedBack> FeedBack { get; set; }
  


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">存储过程名称</param>
        /// <param name="parameters">SqlParameter</param>
        /// <returns></returns>
        public List<T> QueryByProcedure<T>(string sql, params SqlParameter[] parameters) where T : new()
        {
            var connection = Database.GetDbConnection();
            using (var cmd = connection.CreateCommand())
            {
                Database.OpenConnection();
                cmd.CommandText = sql;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);
                var dr = cmd.ExecuteReader();
                var columnSchema = dr.GetColumnSchema();
                var data = new List<T>(); ;
                while (dr.Read())
                {
                    T item = new T();
                    Type type = item.GetType();
                    foreach (var kv in columnSchema)
                    {
                        var propertyInfo = type.GetProperty(kv.ColumnName);
                        if (kv.ColumnOrdinal.HasValue && propertyInfo != null)
                        {
                            //注意需要转换数据库中的DBNull类型
                            var value = dr.IsDBNull(kv.ColumnOrdinal.Value) ? null : dr.GetValue(kv.ColumnOrdinal.Value);
                            propertyInfo.SetValue(item, value);
                        }
                    }
                    data.Add(item);
                }
                // cmd.ExecuteScalar
                dr.Dispose();
                return data;
            }
        }

        public T ExecuteScalarByProcedure<T>(string sql, params SqlParameter[] parameters) where T : new()
        {
            var connection = Database.GetDbConnection();
            using (var cmd = connection.CreateCommand())
            {
                Database.OpenConnection();
                cmd.CommandText = sql;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);
                var result = (T)cmd.ExecuteScalar();
                return result;
            }
        }
    }
}
