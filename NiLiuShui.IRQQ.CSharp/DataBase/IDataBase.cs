using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataBase
{
    interface IDataBase
    {
        //数据库基本信息
        void SetDBInfo(string Server,string dbName,string user,string pwd,short port,string dbfile);
        //数据库基本信息
        void SetConnectionString(string ConnectionString);
        //执行语句
        void SetCammandText(string CommandText);
        //添加参数
        void AddParam(DataBaseParam dbparam);
        //清除参数
        void ParamListClear();
        //返回当前数据库名
        string GetDataBaseName();        
        //查询
        DataSet Execute();
        //参数执行
        int ParamExecuteNonQuery();
        //执行
        int ExecuteNonQuery();
        //查询单值
        object ExecuteScalar();
        //检查数据库是否连接
        bool IsConnection();
        //释放资源
        void Dispose();
    }
}
