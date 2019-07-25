using System;
using System.Xml;

namespace DataBase
{
    public enum DataBaseType
    {
        MySql = 0,
        SQLite = 1,
        Access = 2,
        SQLServer = 3,
        Excel = 4
    }

    class DataBaseUtil
    {
        //指定数据库类型
        public static IDataBase GetDataBase(DataBaseType type)
        {
            IDataBase idb = null;

            switch (type)
            {
                case DataBaseType.MySql:
                    {
                        idb = new MySqlUtil();
                    }
                    break;
            }

            return idb;
        }

        /// <summary>
        /// 从配置获取数据库类型
        /// </summary>
        /// <returns></returns>
        public static IDataBase GetDataBase()
        {
            IDataBase idb = null;

            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "Config.xml");

                XmlElement root = xmlDoc.DocumentElement;
                XmlElement child = (XmlElement)root.SelectSingleNode("//dbtype");

                switch (child.InnerText.ToLower())
                {
                    case "mysql":
                        {
                            idb = new MySqlUtil();
                        }
                        break;
                }
            }
            catch
            {
                idb = new MySqlUtil();
            }

            return idb;
        }
    }
}
