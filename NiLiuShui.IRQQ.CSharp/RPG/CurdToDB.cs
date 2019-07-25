using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiLiuShui.IRQQ.CSharp
{
    public class CurdToDB
    {
        //数据库信息
        //
        static string dbIP = "127.0.0.1";
        static string dbName = "qqplugin";
        static string dbUser = "root";
        static string dbPwd = "qwerty123456";
        static short dbPort = 3306;
        static string dbFile = "";
        public static string logfile = "log.txt";
        /// <summary>
        /// 新建人物
        /// </summary>
        /// <param name="QQID"></param>
        /// <param name="newGold"></param>
        /// <param name="newLucky"></param>
        /// <returns></returns>
        public static bool AddGoldToDB(string QQID, int newGold,int newLucky)
        {
            DataBase.MySqlUtil mySqlUtil = new DataBase.MySqlUtil();
            mySqlUtil.SetDBInfo(dbIP, dbName, dbUser, dbPwd, dbPort, dbFile);

            string SqlString = "insert into characters values(\"" +
                                QQID + "\",\"" +
                                newGold + "\",\"" +
                                newLucky + "\")";
            mySqlUtil.SetCammandText(SqlString);
            try
            {
                mySqlUtil.ExecuteNonQuery();
                IRQQUtil.WritePluginLogFile(logfile, "增加数据库成功" + DateTime.Now);
                return true;
            }
            catch (Exception ex)
            {
                IRQQUtil.WritePluginLogFile(logfile, "增加数据库失败"+ex + DateTime.Now);
                return false;
            }
        }
        /// <summary>
        /// 查询余额
        /// </summary>
        /// <param name="QQID"></param>
        /// <returns></returns>
        public static int SearchGoldFromDB(string QQID)
        {
            IRQQUtil.WritePluginLogFile(logfile, "\n开始查询" + DateTime.Now);
            try
            {
                DataBase.MySqlUtil mySqlUtil = new DataBase.MySqlUtil();
                mySqlUtil.SetDBInfo(dbIP, dbName, dbUser, dbPwd, dbPort, dbFile);

                string SqlString = "select gold from characters where qqid =" + QQID;
                mySqlUtil.SetCammandText(SqlString);

                var iResult = mySqlUtil.Execute();
                var c = iResult.Tables[0].Rows.Count;
                if (c == 0)
                {
                    IRQQUtil.WritePluginLogFile(logfile, "\n数据库中没有" + DateTime.Now);
                    return -1;
                }
                else
                {
                    string goldnum = iResult.Tables[0].Rows[0].ItemArray[0].ToString();
                    IRQQUtil.WritePluginLogFile(logfile, "\n查寻数据库成功" + DateTime.Now);
                    return int.Parse(goldnum);
                }

            }
            catch (Exception ex)
            {
                IRQQUtil.WritePluginLogFile(logfile, "\n查寻数据库失败" + ex + DateTime.Now);
                return -2;
            }
        }
        /// <summary>
        /// 更新余额
        /// </summary>
        /// <param name="QQID"></param>
        /// <param name="oldGold"></param>
        /// <param name="newGold"></param>
        /// <returns></returns>
        public static bool UpdateGoldToDB(string QQID, int oldGold, int newGold)
        {
            DataBase.MySqlUtil mySqlUtil = new DataBase.MySqlUtil();
            mySqlUtil.SetDBInfo(dbIP, dbName, dbUser, dbPwd, dbPort, dbFile);
            int allGold = oldGold + newGold;
            string SqlString = "update characters set gold = " + allGold + " where qqid = " + QQID;
            mySqlUtil.SetCammandText(SqlString);
            try
            {
                mySqlUtil.ExecuteNonQuery();
                IRQQUtil.WritePluginLogFile(logfile, "写入数据库成功(gold)" + DateTime.Now);
                return true;
            }
            catch (Exception ex)
            {
                IRQQUtil.WritePluginLogFile(logfile, "写入数据库失败(gold)" + DateTime.Now);
                return false;
            }
        }
        /// <summary>
        /// 更新幸运值
        /// </summary>
        /// <param name="QQID"></param>
        /// <param name="newLucky"></param>
        /// <returns></returns>
        public static bool UpdateLuckyToDB(string QQID, int newLucky)
        {
            DataBase.MySqlUtil mySqlUtil = new DataBase.MySqlUtil();
            mySqlUtil.SetDBInfo(dbIP, dbName, dbUser, dbPwd, dbPort, dbFile);
            string SqlString = "update characters set lucky = " + newLucky + " where qqid = " + QQID;
            mySqlUtil.SetCammandText(SqlString);
            try
            {
                mySqlUtil.ExecuteNonQuery();
                IRQQUtil.WritePluginLogFile(logfile, "写入数据库成功(lucky)" + DateTime.Now);
                return true;
            }
            catch (Exception ex)
            {
                IRQQUtil.WritePluginLogFile(logfile, "写入数据库失败(lucky)" + DateTime.Now);
                return false;
            }
        }
        /// <summary>
        /// 查询幸运值
        /// </summary>
        /// <param name="QQID"></param>
        /// <returns></returns>
        public static int SearchLuckyFromDB(string QQID)
        {
            IRQQUtil.WritePluginLogFile(logfile, "\n开始查询" + DateTime.Now);
            try
            {
                DataBase.MySqlUtil mySqlUtil = new DataBase.MySqlUtil();
                mySqlUtil.SetDBInfo(dbIP, dbName, dbUser, dbPwd, dbPort, dbFile);

                string SqlString = "select lucky from characters where qqid =" + QQID;
                mySqlUtil.SetCammandText(SqlString);

                var iResult = mySqlUtil.Execute();
                var c = iResult.Tables[0].Rows.Count;
                if (c == 0)
                {
                    IRQQUtil.WritePluginLogFile(logfile, "\n数据库中没有" + DateTime.Now);
                    return -1;
                }
                else
                {
                    string goldnum = iResult.Tables[0].Rows[0].ItemArray[0].ToString();
                    IRQQUtil.WritePluginLogFile(logfile, "\n查寻数据库成功" + DateTime.Now);
                    return int.Parse(goldnum);
                }

            }
            catch (Exception ex)
            {
                IRQQUtil.WritePluginLogFile(logfile, "\n查寻数据库失败" + ex + DateTime.Now);
                return -2;
            }
        }

        /// <summary>
        /// 查询技能装备状态
        /// </summary>
        /// <param name="QQID">qqid</param>
        /// <returns></returns>
        public static int SearchSkillOwnFromDB(string QQID)
        {
            IRQQUtil.WritePluginLogFile(logfile, "\n开始查询" + DateTime.Now);
            try
            {
                DataBase.MySqlUtil mySqlUtil = new DataBase.MySqlUtil();
                mySqlUtil.SetDBInfo(dbIP, dbName, dbUser, dbPwd, dbPort, dbFile);

                string SqlString = "select skill_equip from skill_own where qqid =" + QQID;
                mySqlUtil.SetCammandText(SqlString);

                var iResult = mySqlUtil.Execute();
                var c = iResult.Tables[0].Rows.Count;
                if (c == 0)
                {
                    IRQQUtil.WritePluginLogFile(logfile, "\n数据库中没有" + DateTime.Now);
                    return -1;
                }
                else
                {
                    string goldnum = iResult.Tables[0].Rows[0].ItemArray[0].ToString();
                    IRQQUtil.WritePluginLogFile(logfile, "\n查寻数据库成功" + DateTime.Now);
                    return int.Parse(goldnum);
                }

            }
            catch (Exception ex)
            {
                IRQQUtil.WritePluginLogFile(logfile, "\n查寻数据库失败" + ex + DateTime.Now);
                return -2;
            }
        }

        /// <summary>
        /// 新增技能装备状态
        /// </summary>
        /// <param name="QQID"></param>
        /// <param name="skillEquip"></param>
        /// <returns></returns>
        public static bool AddSkillOwnToDB(string QQID, int skillEquip)
        {
            DataBase.MySqlUtil mySqlUtil = new DataBase.MySqlUtil();
            mySqlUtil.SetDBInfo(dbIP, dbName, dbUser, dbPwd, dbPort, dbFile);

            string SqlString = "insert into skill_own values(\"" +
                                QQID + "\",\"" +
                                skillEquip + "\")";
            mySqlUtil.SetCammandText(SqlString);
            try
            {
                mySqlUtil.ExecuteNonQuery();
                IRQQUtil.WritePluginLogFile(logfile, "增加数据库成功" + DateTime.Now);
                return true;
            }
            catch (Exception ex)
            {
                IRQQUtil.WritePluginLogFile(logfile, "增加数据库失败" + ex + DateTime.Now);
                return false;
            }
        }

        /// <summary>
        /// 新增盗贼技能
        /// </summary>
        /// <param name="QQID">qqid</param>
        /// <param name="skillChance">触发几率</param>
        /// <param name="effLower">效果下限</param>
        /// <param name="effUpper">效果上限</param>
        /// <returns></returns>
        public static bool AddSkillThiefToDB(string QQID,int skillChance,int effLower, int effUpper)
        {
            DataBase.MySqlUtil mySqlUtil = new DataBase.MySqlUtil();
            mySqlUtil.SetDBInfo(dbIP, dbName, dbUser, dbPwd, dbPort, dbFile);

            string SqlString = "insert into skill_thief values(\"" +
                                QQID + "\",\"" +
                                skillChance + "\",\"" +
                                effLower + "\",\"" +
                                effUpper + "\")";
            mySqlUtil.SetCammandText(SqlString);
            try
            {
                mySqlUtil.ExecuteNonQuery();
                IRQQUtil.WritePluginLogFile(logfile, "增加数据库成功" + DateTime.Now);
                return true;
            }
            catch (Exception ex)
            {
                IRQQUtil.WritePluginLogFile(logfile, "增加数据库失败" + ex + DateTime.Now);
                return false;
            }
        }
        /// <summary>
        /// 查询是否有技能
        /// </summary>
        /// <param name="QQID"></param>
        /// <returns></returns>
        public static SkillThief SearchSkillThiefFromDB(string QQID)
        {
            IRQQUtil.WritePluginLogFile(logfile, "\n开始查询" + DateTime.Now);
            try
            {
                DataBase.MySqlUtil mySqlUtil = new DataBase.MySqlUtil();
                mySqlUtil.SetDBInfo(dbIP, dbName, dbUser, dbPwd, dbPort, dbFile);

                string SqlString = "select * from skill_thief where qqid =" + QQID;
                mySqlUtil.SetCammandText(SqlString);

                var iResult = mySqlUtil.Execute();

                var c = iResult.Tables[0].Rows.Count;
                //
                if (c == 0)
                {
                    IRQQUtil.WritePluginLogFile(logfile, "\n数据库中没有" + DateTime.Now);
                    return null;
                }
                else
                {
                    SkillThief skillThief = new SkillThief();
                    skillThief.QQid= iResult.Tables[0].Rows[0].ItemArray[0].ToString();
                    skillThief.SkillChance= iResult.Tables[0].Rows[0].ItemArray[1].ToString();
                    skillThief.EffLower = iResult.Tables[0].Rows[0].ItemArray[2].ToString();
                    skillThief.EffUpper = iResult.Tables[0].Rows[0].ItemArray[3].ToString();
                    IRQQUtil.WritePluginLogFile(logfile, "\n查寻数据库成功" + DateTime.Now);
                    return skillThief;
                }
                //

            }
            catch (Exception ex)
            {
                IRQQUtil.WritePluginLogFile(logfile, "\n查寻数据库失败" + ex + DateTime.Now);
                return null;
            }
        }
        /// <summary>
        /// 更新技能数值
        /// </summary>
        /// <param name="QQID"></param>
        /// <param name="skillChance"></param>
        /// <param name="effLower"></param>
        /// <param name="effUpper"></param>
        /// <returns></returns>
        public static bool UpdateSkillThiefToDB(string QQID, int skillChance, int effLower, int effUpper)
        {
            DataBase.MySqlUtil mySqlUtil = new DataBase.MySqlUtil();
            mySqlUtil.SetDBInfo(dbIP, dbName, dbUser, dbPwd, dbPort, dbFile);
            
            string SqlString = "update skill_thief set skill_chance = " + skillChance +
                                                     ",eff_lower = "+ effLower +
                                                     ",eff_upper = "+ effUpper +
                                                     " where qqid = " + QQID;
            mySqlUtil.SetCammandText(SqlString);
            try
            {
                mySqlUtil.ExecuteNonQuery();
                IRQQUtil.WritePluginLogFile(logfile, "写入数据库成功(gold)" + DateTime.Now);
                return true;
            }
            catch (Exception ex)
            {
                IRQQUtil.WritePluginLogFile(logfile, "写入数据库失败(gold)" + DateTime.Now);
                return false;
            }
        }
        /// <summary>
        /// 查询余额大于某值的人员列表
        /// </summary>
        /// <param name="goldNum"></param>
        /// <returns></returns>
        public static List<string> SearchGoldListFromDB(int goldNum)
        {
            IRQQUtil.WritePluginLogFile(logfile, "\n开始查询" + DateTime.Now);
            try
            {
                DataBase.MySqlUtil mySqlUtil = new DataBase.MySqlUtil();
                mySqlUtil.SetDBInfo(dbIP, dbName, dbUser, dbPwd, dbPort, dbFile);

                string SqlString = "select qqid from characters where gold >=" + goldNum;
                mySqlUtil.SetCammandText(SqlString);

                var iResult = mySqlUtil.Execute();
                var c = iResult.Tables[0].Rows.Count;
                if (c == 0)
                {
                    IRQQUtil.WritePluginLogFile(logfile, "\n数据库中没有" + DateTime.Now);
                    return null;
                }
                else
                {
                    List<string> qqIDlist = new List<string>();
                    for (int i = 0; i < c; i++)
                    {
                        var a = iResult.Tables[0].Rows[i];
                        qqIDlist.Add(a.ItemArray[0].ToString());
                    }


                    IRQQUtil.WritePluginLogFile(logfile, "\n查寻数据库成功" + DateTime.Now);
                    return qqIDlist;
                }

            }
            catch (Exception ex)
            {
                IRQQUtil.WritePluginLogFile(logfile, "\n查寻数据库失败" + ex + DateTime.Now);
                return null;
            }
        }
        /// <summary>
        /// 测试发工资用
        /// </summary>
        public static void GiveGold()
        {
            DataBase.MySqlUtil mySqlUtil = new DataBase.MySqlUtil();
            mySqlUtil.SetDBInfo(dbIP, dbName, dbUser, dbPwd, dbPort, dbFile);
            string SqlString = "update characters set gold = gold + 200";
            mySqlUtil.SetCammandText(SqlString);
            try
            {
                mySqlUtil.ExecuteNonQuery();
                IRQQUtil.WritePluginLogFile(logfile, "写入数据库成功(gold)" + DateTime.Now);
            }
            catch (Exception ex)
            {
                IRQQUtil.WritePluginLogFile(logfile, "写入数据库失败(gold)" + DateTime.Now);
            }
        }
    }
}
