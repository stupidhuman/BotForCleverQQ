using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;

namespace DataBase
{
    class MySqlUtil : IDataBase
    {
        private static object locker = new object();//本工程，特殊情况，特殊处理
        private MySqlConnection Conn = null;
        private string connectionString = "";
        private string commandText = "";
        private ArrayList paramList = null;

        ////////////////↓↓↓//////////////////属性设置////////////////↓↓↓//////////////////
        public void SetDBInfo(string Server, string dbName, string user, string pwd, short port, string dbfile)
        {
            this.connectionString = "Database='" + dbName + "';Data Source='" + Server + "';Port=" + port.ToString()
                + ";User Id='" + user + "';Password='" + pwd + "';charset='utf8';pooling=true;Connection Timeout=3";
        }

        public void SetConnectionString(string ConnectionString)
        {
            this.connectionString = ConnectionString;
        }

        public void SetCammandText(string CommandText)
        {
            this.commandText = CommandText;
        }

        public void AddParam(DataBaseParam dbparam)
        {
            if (paramList == null)
            {
                paramList = new ArrayList();
            }
            this.paramList.Add(dbparam);
        }

        public void ParamListClear()
        {
            if (paramList != null)
            {
                this.paramList.Clear();
            }
        }

        ////////////////↓↓↓//////////////////数据库操作处理////////////////↓↓↓//////////////////
        public DataSet Execute()
        {
            DataSet dataSet = new DataSet();
            lock (locker)
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("数据连接字符串错误");
                }

                if (string.IsNullOrEmpty(commandText))
                {
                    throw new Exception("SQL语句错误。");
                }

                if (!IsConnection())
                {
                    throw new Exception("数据库连接失败。");
                }

                try
                {
                    Conn = new MySqlConnection();
                    Conn.ConnectionString = connectionString;

                    MySqlDataAdapter MySqlDa = new MySqlDataAdapter(commandText, Conn);

                    MySqlDa.Fill(dataSet);
                    MySqlDa.Dispose();
                }
                catch (MySqlException MySqlEx)
                {
                    throw MySqlEx;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return dataSet;

        }

        public int ParamExecuteNonQuery()
        {
            lock (locker)
            {
                int iQuery = 0;

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("数据连接字符串错误。");
                }

                if (string.IsNullOrEmpty(commandText))
                {
                    throw new Exception("SQL语句错误。");
                }

                if (!IsConnection())
                {
                    throw new Exception("数据库连接失败。");
                }

                MySqlTransaction Transaction = null;

                try
                {
                    MySqlCommand Comm = new MySqlCommand();
                    Conn = new MySqlConnection();

                    Conn.ConnectionString = connectionString;
                    Conn.Open();

                    Comm.Connection = Conn;
                    Comm.CommandText = commandText;

                    int i = 0;
                    foreach (DataBaseParam param in this.paramList)
                    {
                        Comm.Parameters.Add(param.ParamName, GetType(param), param.Size);
                        Comm.Parameters[i].Value = param.Value;
                        i++;
                    }

                    Transaction = Conn.BeginTransaction();
                    Comm.Transaction = Transaction;

                    iQuery = Comm.ExecuteNonQuery();
                    Transaction.Commit();

                    Comm.Parameters.Clear();

                    return iQuery;
                }
                catch (MySqlException MySqlEx)
                {
                    Transaction.Rollback();
                    throw MySqlEx;
                }
                catch (Exception ex)
                {
                    if (Transaction != null) Transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    if (Transaction != null) Transaction.Dispose();
                    Conn.Close();
                    Conn.Dispose();
                }
            }
        }


        public int ExecuteNonQuery()
        {
            lock (locker)
            {
                int iQuery = 0;

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("数据连接字符串错误。");
                }

                if (string.IsNullOrEmpty(commandText))
                {
                    throw new Exception("SQL语句错误。");
                }

                if (!IsConnection())
                {
                    throw new Exception("数据库连接失败。");
                }

                MySqlTransaction Transaction = null;

                try
                {
                    MySqlCommand Comm = new MySqlCommand();
                    Conn = new MySqlConnection();

                    Conn.ConnectionString = connectionString;
                    Conn.Open();

                    Comm.Connection = Conn;
                    Comm.CommandText = commandText;

                    Transaction = Conn.BeginTransaction();
                    Comm.Transaction = Transaction;

                    iQuery = Comm.ExecuteNonQuery();
                    Transaction.Commit();

                    return iQuery;
                }
                catch (MySqlException MySqlEx)
                {
                    Transaction.Rollback();
                    throw MySqlEx;
                }
                catch (Exception ex)
                {
                    if (Transaction != null) Transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    if (Transaction != null) Transaction.Dispose();
                    Conn.Close();
                    Conn.Dispose();
                }
            }
        }

        public object ExecuteScalar()
        {
            object iQuery = null;
            lock (locker)
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("数据连接字符串错误。");
                }

                if (string.IsNullOrEmpty(commandText))
                {
                    throw new Exception("SQL语句错误。");
                }

                if (!IsConnection())
                {
                    throw new Exception("数据库连接失败。");
                }

                MySqlTransaction Transaction = null;

                try
                {
                    MySqlCommand Comm = new MySqlCommand();
                    Conn = new MySqlConnection();
                    Conn.ConnectionString = connectionString;
                    Conn.Open();
                    Comm.Connection = Conn;
                    Comm.CommandText = commandText;

                    Transaction = Conn.BeginTransaction();
                    Comm.Transaction = Transaction;
                    iQuery = Comm.ExecuteScalar();
                    Transaction.Commit();

                }
                catch (MySqlException MySqlEx)
                {
                    Transaction.Rollback();
                    throw MySqlEx;
                }
                catch (Exception ex)
                {
                    if (Transaction != null) Transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    if (Transaction != null) Transaction.Dispose();
                    Conn.Close();
                    Conn.Dispose();
                }

            }
            return iQuery;
        }

        public bool IsConnection()
        {
            try
            {
                Conn = new MySqlConnection();
                Conn.ConnectionString = connectionString;
                Conn.Open();
                Conn.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            if (Conn != null)
            {
                this.Conn.Close();
                this.Conn.Dispose();
            }
        }

        public MySqlDbType GetType(DataBaseParam param)
        {
            switch (param.ParamType)
            {
                case ParamType.Text:
                    {
                        return MySqlDbType.Text;
                    }
                case ParamType.Integer:
                    {
                        return MySqlDbType.Int32;
                    }
                case ParamType.DateTime:
                    {
                        return MySqlDbType.DateTime;
                    }
                case ParamType.File:
                    {
                        return MySqlDbType.Blob;
                    }
                default:
                    {
                        return MySqlDbType.Text;
                    }
            }
        }
        ////////////////↓↓↓//////////////////其他处理////////////////↓↓↓//////////////////
        //返回当前数据库名
        public string GetDataBaseName()
        {
            return "MySql";
        }
    }
}
