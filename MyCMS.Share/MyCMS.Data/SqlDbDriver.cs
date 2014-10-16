using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MyCMS.Data
{
    public class SqlDbDriver : BaseDriver
    {
        public override string FormatField(Adorns adorn, string field)
        {
            switch (adorn)
            { 
                case Adorns.Max:
                    return string.Format("MAX([{0}]) AS [{0}]", field);
                case Adorns.Min:
                    return string.Format("MIN([{0}]) AS [{0}]", field);
                case Adorns.Average:
                    return string.Format("AVG([{0}]) AS [{0}]", field);
                case Adorns.Sum:
                    return string.Format("SUM([{0}]) AS [{0}]", field);
                case Adorns.None:
                case Adorns.Substring:
                    return string.Format("[{0}]", field);
                case Adorns.Count:
                    return string.Format("COUNT([{0}]) AS [{0}]", field);
                //case Adorns.Total:
                //    return string.Format("Total([{0}]) AS [{0}]", filed);
                default:
                    return string.Format("[{0}]", field);
            }
        }

        public override string FormatField(Adorns adorn, string field, int start, int length)
        {
            switch (adorn)
            {
                case Adorns.Substring:
                    return string.Format("SUBSTRING([{0}], {1}, {2})", field, start, length);
                case Adorns.Max:
                case Adorns.Min:
                case Adorns.Average:
                case Adorns.Sum:
                case Adorns.Count:
                case Adorns.None:
                default:
                    return string.Format("[{0}]", field);
            }
        }

        /// <summary>
        /// 将ConListField转换为t-sql字符串
        /// </summary>
        /// <param name="GlobalDBString"></param>
        /// <returns></returns>
        public override string FormatField(ConListField field)
        {
            switch (field.Adorn)
            {
                case Adorns.Max:
                    return string.Format("MAX([{0}]) AS [{1}]", field.Field, field.AliasField);
                case Adorns.Min:
                    return string.Format("MIN([{0}]) AS [{1}]", field.Field, field.AliasField);
                case Adorns.Average:
                    return string.Format("AVG([{0}]) AS [{1}]", field.Field, field.AliasField);
                case Adorns.Sum:
                    return string.Format("SUM([{0}]) AS [{1}]", field.Field, field.AliasField);
                case Adorns.None:
                    if (field.Field == field.AliasField)
                        return string.Format("[{0}]", field.Field);
                    else
                        return string.Format("[{0}] AS [{1}]", field.Field, field.AliasField);
                case Adorns.Substring:
                    return string.Format("SUBSTRING([{0}], [{1}], [{2}]) AS [{3}]", field.Field, field.Start, field.Length, field.AliasField);
                case Adorns.Count:
                default:
                    return string.Format("COUNT([{0}]) AS [{1}]", field.Field, field.AliasField);
            }
        }

        public override IConnection CreateConnection(string connectionString)
        {
            IConnectionEx cc = CreateConnection();
            connectionString = connectionString.Replace("{$App}", AppDomain.CurrentDomain.BaseDirectory);
            cc.ConnectionString = connectionString;
            cc.Driver = this;
            cc.Create = true;

            return cc;
        }

        public override IConnection CreateConnection(string connectionString, bool create)
        {
            IConnectionEx cc = CreateConnection();
            connectionString = connectionString.Replace("{$App}", AppDomain.CurrentDomain.BaseDirectory);
            cc.ConnectionString = connectionString;
            cc.Driver = this;
            cc.Create = create;

            return cc;
        }

        public IConnectionEx CreateConnection()
        {
            return new SqlDbConnection();
        }

        class SqlDbConnection : IConnectionEx
        {
            string connectionString;
            public string ConnectionString
            {
                get
                {
                    return connectionString;
                }
                set
                {
                    connectionString = value;
                }
            }

            bool create;
            public bool Create
            {
                get
                {
                    return create;
                }
                set
                {
                    create = value;
                }
            }

            IDbDriver driver;
            public IDbDriver Driver
            {
                get
                {
                    return driver;
                }
                set
                {
                    driver = value;
                }
            }

            bool isTransaction;
            public bool IsTransaction
            {
                get
                {
                    return isTransaction;
                }
                set
                {
                    isTransaction = value;
                }
            }

            SqlConnection connection;
            public SqlConnection Connection
            {
                get { return connection; }
            }

            SqlTransaction transaction;

            
            public void Commit()
            {
                if (transaction != null)
                {
                    transaction.Commit();
                    transaction.Dispose();
                    transaction = null;
                }
            }

            public void Rollback()
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    transaction.Dispose();
                    transaction = null;
                }
            }

            public DataTable Query(SqlStatement sql)
            {
                using (SqlCommand cmd = CreateCommand(sql))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    PopulateCommand(cmd, sql);

                    if (!create)
                        Dispose(true);

                    return table;
                }
            }

            public object QueryScalar(SqlStatement sql)
            {
                using (SqlCommand cmd = CreateCommand(sql))
                {
                    object o = cmd.ExecuteScalar();

                    PopulateCommand(cmd, sql);

                    if (!create)
                        Dispose(true);

                    return o;
                }
            }

            public int Update(SqlStatement sql)
            {
                using (SqlCommand cmd = CreateCommand(sql))
                {
                    int affected = cmd.ExecuteNonQuery();
                    PopulateCommand(cmd, sql);

                    if (!create)
                        Dispose(true);
                    return affected;
                }
            }

            public bool TableExists(string tablename)
            {
                try
                {
                    SqlStatement sql = new SqlStatement();
                    sql.SqlClause = "SELECT COUNT(*) FROM " + tablename;
                    using (SqlCommand cmd = CreateCommand(sql))
                    {
                        int ret = cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }

            void PopulateCommand(SqlCommand cmd, SqlStatement sql)
            {
                for (int i = 0; i < sql.Parameters.Count; i++)
                {
                    DataParameter dp = sql.Parameters[i];
                    if (dp.Direction != ParameterDirection.Input)
                    {
                        dp.Value = cmd.Parameters[i].Value;
                    }
                }
            }

            public void Dispose()
            {
                Dispose(true);
            }

            protected void Dispose(bool isDisposing)
            { 
                if (isDisposing)
                {
                    if (transaction != null)
                    {
                        Commit();
                    }
                    if (connection != null)
                    {
                        connection.Close();
                        connection.Dispose();
                        connection = null;
                    }

                    GC.SuppressFinalize(this);
                }
            }

            SqlCommand CreateCommand(SqlStatement sql)
            {
                SqlCommand cmd = new SqlCommand(sql.SqlClause);
                if (connection == null)
                {
                    connection = new SqlConnection(connectionString);
                    connection.Open();
                }

                if (isTransaction)
                {
                    if (transaction == null)
                    {
                        transaction = connection.BeginTransaction();
                    }
                    cmd.Transaction = transaction;
                }

                cmd.Connection = connection;
                cmd.CommandType = sql.Type;
                cmd.CommandTimeout = 300;

                foreach (DataParameter dp in sql.Parameters)
                {
                    SqlParameter p = new SqlParameter();

                    if (dp.Value == null)
                        p.Value = DBNull.Value;
                    else
                        p.Value = dp.Value;

                    if (dp.DbType == DbType.DateTime && dp.Value != null)
                    {
                        if (Convert.ToDateTime(dp.Value) <= DateTime.Parse("1/1/1753 12:00:00") ||
                            Convert.ToDateTime(dp.Value) >= DateTime.Parse("12/31/9999 23:59:59"))
                            p.Value = DBNull.Value;
                    }

                    p.ParameterName = dp.ParameterName;
                    p.Precision = dp.Precision;
                    p.Size = dp.Size;
                    p.Scale = dp.Scale;
                    p.Direction = dp.Direction;

                    cmd.Parameters.Add(p);
                }

                return cmd;
            }


        }

        
    }
}
