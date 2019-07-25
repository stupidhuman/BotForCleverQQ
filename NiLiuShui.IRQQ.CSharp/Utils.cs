using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace NiLiuShui.IRQQ.CSharp
{
    public class Utils
    {
        /// <summary>
        ///DataSet 转化为IList的通用方法，可以转换为泛型类型
        /// </summary>
        /// <param name="ds">传入DataSet</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        //public List<T> ChangeDataSet<T>(DataSet ds, string tableName)
        //{
        //    //如果不存在此表 将返回空值
        //    if (ds.Tables[tableName] == null)
        //    {
        //        return null;
        //    }

        //    //获取操作DataTable
        //    DataTable dt = ds.Tables[tableName];

        //    //创建相应对象
        //    List<T> list = new List<T>();
        //    T model = default(T);

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        model = Activator.CreateInstance<T>();

        //        foreach (DataColumn dc in dr.Table.Columns)
        //        {
        //            PropertyInfo pi = model.GetType().GetProperty(dc.ColumnName);
        //            if (pi != null)
        //            {
        //                if (dr[dc.ColumnName] != DBNull.Value)
        //                {
        //                    var val = dr[dc.ColumnName];

        //                    #region 根据字段类型，转换为对应数据类型
        //                    if ("String".Equals(pi.PropertyType.Name))
        //                    {
        //                        val = Convert.ToString(val);
        //                    }
        //                    else if ("Int32".Equals(pi.PropertyType.Name))
        //                    {
        //                        val = Convert.ToInt32(val);
        //                    }
        //                    else if ("Int64".Equals(pi.PropertyType.Name))
        //                    {
        //                        val = Convert.ToInt64(val);
        //                    }
        //                    else if ("Boolean".Equals(pi.PropertyType.Name))
        //                    {
        //                        val = Convert.ToBoolean(val);
        //                    }
        //                    else if ("DateTime".Equals(pi.PropertyType.Name))
        //                    {
        //                        val = Convert.ToDateTime(val);
        //                    }
        //                    else if ("Decimal".Equals(pi.PropertyType.Name))
        //                    {
        //                        val = Convert.ToDecimal(val);
        //                    }
        //                    else if ("Double".Equals(pi.PropertyType.Name))
        //                    {
        //                        val = Convert.ToDouble(val);
        //                    }
        //                    else if ("Byte".Equals(pi.PropertyType.Name))
        //                    {
        //                        val = Convert.ToByte(val);
        //                    }
        //                    else if ("Char".Equals(pi.PropertyType.Name))
        //                    {
        //                        val = Convert.ToChar(val);
        //                    }
        //                    else if ("Guid".Equals(pi.PropertyType.Name))
        //                    {
        //                        val = (Guid)(val);
        //                    }
        //                    #endregion

        //                    pi.SetValue(model, val, null);
        //                }
        //                else
        //                    pi.SetValue(model, null, null);
        //            }
        //        }
        //        list.Add(model);
        //    }
        //    return list;
        //}

        /// <summary>
        /// DataSet转换为实体类
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="p_DataSet">DataSet</param>
        /// <param name="p_TableIndex">待转换数据表索引</param>
        /// <returns>实体类</returns>
        public static T DataSetToEntity<T>(DataSet p_DataSet, int p_TableIndex)
        {
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return default(T);
            if (p_TableIndex > p_DataSet.Tables.Count - 1)
                return default(T);
            if (p_TableIndex < 0)
                p_TableIndex = 0;
            if (p_DataSet.Tables[p_TableIndex].Rows.Count <= 0)
                return default(T);
            DataRow p_Data = p_DataSet.Tables[p_TableIndex].Rows[0];
            // 返回值初始化
            T _t = (T)Activator.CreateInstance(typeof(T));
            PropertyInfo[] propertys = _t.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                if (p_DataSet.Tables[p_TableIndex].Columns.IndexOf(pi.Name.ToUpper()) != -1 && p_Data[pi.Name.ToUpper()] != DBNull.Value)
                {
                    pi.SetValue(_t, p_Data[pi.Name.ToUpper()], null);
                }
                else
                {
                    pi.SetValue(_t, null, null);
                }
            }
            return _t;
        }

    }
}
