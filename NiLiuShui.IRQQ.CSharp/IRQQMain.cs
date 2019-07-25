using NiLiuShui.IRQQ.CSharp.RPG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace NiLiuShui.IRQQ.CSharp
{
    /// <summary>
    /// 版权声明：此SDK是应{续写}邀请为IRQQ\CleverQQ编写，请合理使用无用于黄赌毒相关方面。
    /// 作者QQ：1276986643,铃兰
    /// 如果您对CleverQQ感兴趣，欢迎加入QQ群：763804063,476715371，进行讨论
    /// 最后修改时间为2019年6月28日 10:29:20,初夏
    /// 本SDK编译生成的插件需要有.Net4.0以上的环境才能运行
    /// </summary>
    public class IRQQMain
    {
        public static string logfile = "log.txt";

        /// <summary>
        /// 插件名称
        /// </summary>
        public const string pluginName = "huii_RPG";

        /// <summary>
        /// 插件版本
        /// </summary>
        public const string pluginVersion = "1.0.0";

        /// <summary>
        /// 插件作者
        /// </summary>
        public const string pluginAuthor = "huiiyi";

        /// <summary>
        /// 插件描述
        /// </summary>
        public const string pluginDescribe = "过于无聊写的RPG小插件";

        /// <summary>
        /// 插件Skey 请勿随意改动此项
        /// </summary>
        public const string pluginSkey = "8956RTEWDFG3216598WERDF3";//请勿随意改动此项

        /// <summary>
        /// 插件Sdk版本 请勿随意改动此项
        /// </summary>
        public const string pluginSdk = "S3";//请勿随意改动此项


        [DllExport(ExportName = nameof(IR_Create), CallingConvention = CallingConvention.StdCall)]
        public static string IR_Create()
        {
            string szBuffer = "插件名称{" + pluginName + "}\n插件版本{" + pluginVersion + "}\n插件作者{" + pluginAuthor + "}\n插件说明{" + pluginDescribe + "}\n插件skey{" + pluginSkey + "}插件sdk{" + pluginSdk + "}";
            return szBuffer;
        }
        [DllExport(ExportName = nameof(IR_Message), CallingConvention = CallingConvention.StdCall)]
        public static int IR_Message(IntPtr RobotQQ, IntPtr MsgType, IntPtr Msg, IntPtr Cookies, IntPtr SessionKey, IntPtr ClientKey)
        {
            return 1;
        }

        public static bool Debug { get; set; }
        public static List<string> iplayer = new List<string>();
        public static string iGroup;

        public static string StartRollClub(string qqid, string igroup)
        {
            if (iplayer.Count == 0)
            {
                iGroup = igroup;
                iplayer.Add(qqid);
                return "0";
            }
            else if (iplayer.Count < 4 && igroup == iGroup)
            {
                iplayer.Add(qqid);
                int playerCount = iplayer.Count;
                return playerCount.ToString();
            }
            else if (iplayer.Count == 4 && igroup == iGroup)
            {
                iplayer.Add(qqid);
                Random random = new Random();
                int winPlayerNum = random.Next(0, 5);
                string winPlayer = iplayer[winPlayerNum];
                iplayer.Clear();
                iGroup = string.Empty;
                return winPlayer;
            }
            else
            {
                return "-1";
            }
        }


        [DllExport(ExportName = nameof(IR_Event), CallingConvention = CallingConvention.StdCall)]
        public static int IR_Event(IntPtr RobotQQ, int MsgType, int MsgCType, IntPtr MsgFrom, IntPtr TigObjF, IntPtr TigObjC, IntPtr Msg, IntPtr MsgNum, IntPtr MsgID, IntPtr RawMsg, IntPtr Json, int pText)
        {
            String RobotQQStr = IRQQUtil.ToAnsiString(RobotQQ);
            String MsgFromStr = IRQQUtil.ToAnsiString(MsgFrom);
            String TigObjFStr = IRQQUtil.ToAnsiString(TigObjF);
            String TigObjCStr = IRQQUtil.ToAnsiString(TigObjC);
            String MsgStr = String.Empty;
            String MsgNumStr = String.Empty;
            String MsgIDStr = String.Empty;
            String RawMsgStr = IRQQUtil.ToAnsiString(RawMsg);
            String JsonStr = String.Empty;
            if (!(MsgType == IRQQConst.IRC_MRBPZJRLQ || MsgType == IRQQConst.IRC_MRQQRQ))
            {
                MsgStr = IRQQUtil.ToAnsiString(Msg);
                MsgNumStr = IRQQUtil.ToAnsiString(MsgNum);
                MsgIDStr = IRQQUtil.ToAnsiString(MsgID);
                JsonStr = IRQQUtil.ToAnsiString(Json);
            }
            return IR_Event(RobotQQStr, MsgType, MsgCType, MsgFromStr, TigObjFStr, TigObjCStr, MsgStr, MsgNumStr, MsgIDStr, RawMsgStr, JsonStr, pText);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RobotQQ">机器人QQ         多Q版用于判定哪个QQ接收到该消息</param>
        /// <param name="MsgType">消息类型         接收到消息类型，该类型可在常量表中查询具体定义，此处仅列举： - 1 未定义事件 1 好友信息 2, 群信息 3, 讨论组信息 4, 群临时会话 5, 讨论组临时会话 6, 财付通转账</param>
        /// <param name="MsgCType">消息子类型      此参数在不同消息类型下，有不同的定义，暂定：接收财付通转账时 1为好友 2为群临时会话 3为讨论组临时会话    有人请求入群时，不良成员这里为1</param>
        /// <param name="MsgFrom">消息来源         此消息的来源，如：群号、讨论组ID、临时会话QQ、好友QQ等</param>
        /// <param name="TigObjF">触发对象_主动    主动发送这条消息的QQ，踢人时为踢人管理员QQ</param>
        /// <param name="TigObjC">触发对象_被动    被动触发的QQ，如某人被踢出群，则此参数为被踢出人QQ</param>
        /// <param name="Msg">消息内容             此参数有多重含义，常见为：对方发送的消息内容，但当IRC_消息类型为 某人申请入群，则为入群申请理由</param>
        /// <param name="MsgNum">消息序号          此参数暂定用于消息回复，消息撤回</param>
        /// <param name="MsgID">消息ID             此参数暂定用于消息撤回</param>
        /// <param name="RawMsg">原始信息          UDP收到的原始信息，特殊情况下会返回JSON结构（收到群验证事件时，这里为该事件seq）</param>
        /// <param name="Json">Json信息            JSON格式转账解析</param>
        /// <param name="pText">信息回传文本指针   此参数用于插件加载拒绝理由  用法：写到内存（“拒绝理由”，IRC_信息回传文本指针_Out）</param>
        /// <returns></returns>
        ///此子程序会分发IRC_机器人QQ接收到的所有：事件，消息；您可在此函数中自行调用所有参数
        public static int IR_Event(string RobotQQ, int MsgType, int MsgCType, string MsgFrom, string TigObjF, string TigObjC, string Msg, string MsgNum, string MsgID, string RawMsg, string Json, int pText)
        {
            
            if (MsgType == 1)
            {

                //IRQQApi.Api_SendMsg(RobotQQ, 1, "", MsgFrom, "发送的消息为:"+Msg, 0);
                //string sPicLink = IRQQApi.Api_GetPicLink(RobotQQ, 2, "1", Msg);
                //IRQQApi.Api_SendMsg(RobotQQ, 1, "", MsgFrom, "图片链接为:" + sPicLink, 0);

            }
            else if (MsgType == 2)
            {
                if (Msg == "#帮助")
                {
                    IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + "#查询 查询你当前的雷币数量\n#查属性+[空格]+[属性名] 查询当前属性(目前仅开放了[幸运]属性)\n"+
                                                                                            "#洗属性+[空格]+[属性名] 花费300雷币重置当前属性(目前仅开放了[幸运]属性)\n"+
                                                                                            "#查技能 查看当前装备的技能\n" +
                                                                                            "#洗技能 花费300雷币重置当前技能数值\n" +
                                                                                            "*[幸运]属性会影响所有的几率事件", -1);
                }
                else if (Msg == "#查询")
                {
                    var oldGold = CurdToDB.SearchGoldFromDB(TigObjF);
                    IRQQUtil.WritePluginLogFile(logfile, "\n查询结果" + oldGold + "," + DateTime.Now);
                    if (oldGold == -1)
                    {
                        //string nickname = IRQQApi.Api_GetGroupCard(RobotQQ, MsgFrom, TigObjF);
                        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + "你没创建人物！随便说一句话就自动创建人物辣！！", -1);
                    }
                    else
                    {
                        //string nickname = IRQQApi.Api_GetGroupCard(RobotQQ, MsgFrom, TigObjF);
                        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + " 当前拥有" + oldGold + "个穆雷币！", -1);
                    }
                }
                else if (Msg=="#洗属性 幸运")
                {
                    int iResult;
                    iResult = CurdToDB.SearchGoldFromDB(TigObjF);
                    if (iResult==-1)
                    {
                        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + "没有你的角色！先发言创建角色吧！", -1);
                    }
                    else if (iResult>=300)
                    {
                        Random rd = new Random();
                        int iLucky = rd.Next(1, 101);
                        CurdToDB.UpdateGoldToDB(TigObjF, iResult, -300);
                        CurdToDB.UpdateLuckyToDB(TigObjF, iLucky);
                        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + "洗属性成功！你现在的幸运值为:"+ iLucky+"点", -1);
                    }
                    else
                    {
                        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + "你的雷币不够！洗属性需要300雷币，努力发言赚雷币吧", -1);
                    }
                }
                else if (Msg == "#查属性 幸运")
                {
                    int iResult;
                    iResult = CurdToDB.SearchLuckyFromDB(TigObjF);
                    if (iResult!=-1)
                    {
                        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + " 你的幸运值为:" + iResult + "点", -1);
                    }
                    else
                    {
                        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + " 你没创建人物！随便说一句话就自动创建人物辣！！", -1);
                    }
                }
                else if (Msg=="#轮盘")
                {
                    //int oldGold = CurdToDB.SearchGoldFromDB(TigObjF);
                    //if (oldGold>=50)
                    //{

                    //    string iResult = IRQQMain.StartRollClub(TigObjF, MsgFrom);
                    //    if (iResult == "0")
                    //    {
                    //        //发起了
                    //        CurdToDB.UpdateGoldToDB(TigObjF, oldGold, -50);
                    //        oldGold = oldGold - 50;
                    //        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + " 你花费50雷币 发起了 轮盘游戏!还缺4个人报名！\n你现在剩余"+ oldGold+"雷币！", -1);
                    //    }
                    //    else if (iResult.Length > 2)
                    //    {
                    //        //获胜

                    //        CurdToDB.UpdateGoldToDB(TigObjF, oldGold, -50);
                    //        oldGold = oldGold - 50;
                    //        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + " 你现在剩余" + oldGold + "雷币！\n人满了开始抽奖！", -1);

                    //        int winOldGold = CurdToDB.SearchGoldFromDB(iResult);
                    //        CurdToDB.UpdateGoldToDB(iResult, winOldGold, 250);
                    //        int winGold = winOldGold + 250;
                    //        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + iResult + "]" + " 你中奖啦！！！那4个弟弟的雷币归你了！雷币+250！\n你现在剩余" + winGold + "雷币！", -1);
                    //    }
                    //    else if (iResult != "-1")
                    //    {
                    //        //参加

                    //        CurdToDB.UpdateGoldToDB(TigObjF, oldGold, -50);
                    //        int iplay = 5 - int.Parse(iResult);
                    //        oldGold = oldGold - 50;
                    //        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + " 你花费50雷币 加入了 轮盘游戏!还缺" + iplay + "个人报名！\n你现在剩余" + oldGold + "雷币！", -1);
                    //    }
                    //    else if (iResult=="-1")
                    //    {
                    //        //出错了
                    //        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + " 不知道为什么轮盘游戏好像出错了！\n找GM吧！", -1);
                    //    }
                    //}
                    //else
                    //{
                    //    //钱不够
                    //    IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + " 你的雷币不够50！", -1);
                    //}

                }
                else if (Msg == "#查技能")
                {
                    SkillThief skillThief = new SkillThief();
                    skillThief = CurdToDB.SearchSkillThiefFromDB(TigObjF);
                    if (skillThief==null)
                    {
                        //没有技能
                        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + "你还不会任何技能！", -1);
                    }
                    else
                    {
                        //有技能
                        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + "偷窃：随机偷取一名玩家随机数量的雷币\n" +
                                                                                                 "触发几率：" + skillThief.SkillChance + "%\n" +
                                                                                                 "偷窃范围：" + skillThief.EffLower + " - " + skillThief.EffUpper, -1);
                    }
                    

                }
                else if (Msg == "#洗技能")
                {
                    SkillThief skillThief = new SkillThief();
                    skillThief = CurdToDB.SearchSkillThiefFromDB(TigObjF);
                    if (skillThief==null)
                    {
                        //没有技能
                        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + "你还不会任何技能！", -1);
                    }
                    else
                    {
                        var oldGold = CurdToDB.SearchGoldFromDB(TigObjF);
                        IRQQUtil.WritePluginLogFile(logfile, "\n查询结果" + oldGold + "," + DateTime.Now);
                        if (oldGold == -1)
                        {
                            
                            IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + "你没创建人物！随便说一句话就自动创建人物辣！！", -1);
                        }
                        else if (oldGold<300)
                        {
                            IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + " 当前拥有" + oldGold + "个穆雷币！\n"+
                                                                                                     " 洗技能需要300雷币！", -1);
                        }
                        else
                        {
                            CurdToDB.UpdateGoldToDB(TigObjF, oldGold, -300);
                            double luckyAddon = LuckyAddon.GetiLuckyAddon(TigObjF);
                            Random random = new Random();
                            int skillChance;
                            int effLower;
                            int effUpper;
                            int effRange;
                            skillChance = random.Next(1, (int)(6*luckyAddon));
                            effLower = random.Next(0, (int)(41 * luckyAddon));
                            effRange = random.Next(1, (int)(41 * luckyAddon));
                            effUpper = effLower + effRange;
                            bool iBool= CurdToDB.UpdateSkillThiefToDB(TigObjF, skillChance, effLower, effUpper);
                            if (iBool)
                            {
                                IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" +"洗技能成功！\n" +"偷窃：随机偷取一名玩家随机数量的雷币\n" +
                                                                                                 "触发几率：" + skillChance + "%\n" +
                                                                                                 "偷窃范围：" + effLower + " - " + effUpper, -1);

                            }
                            else
                            {
                                IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + "不知道为什么洗技能失败了，联系GM吧！", -1);
                            }
                        }
                        
                    }
                }
                else if (Msg.Length > 2)
                {
                    int iGold=GetGolds.DefaultGetGold(TigObjF);
                    if (iGold==-1)
                    {
                        return 1;
                    }
                    else if (iGold>1000)
                    {
                        string nickname = IRQQApi.Api_GetGroupCard(RobotQQ, MsgFrom, TigObjF);
                        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at="+ TigObjF + "]"+" 你就是天选之人？"+nickname + "击败Boss爆了一个藏宝图，找到了宝藏，共计获得了" + iGold + "个雷币！", -1);
                    }
                    else if (iGold>100)
                    {
                        //string nickname = IRQQApi.Api_GetGroupCard(RobotQQ, MsgFrom, TigObjF);
                        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]"  + " 打怪爆了一个金闪闪的钥匙，打开怪物的保险柜后收获了" + iGold + "个雷币！", -1);
                    }
                    else if (iGold>10)
                    {
                        //string nickname = IRQQApi.Api_GetGroupCard(RobotQQ, MsgFrom, TigObjF);
                        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]"  + " 在村民家的保险箱中摸到了" + iGold + "个雷币！", -1);
                    }
                    //判断是否获得技能
                    SkillThief skillThief = new SkillThief();
                    skillThief = GetGolds.DefaultGetSkill(TigObjF);
                    if (skillThief!=null)
                    {
                        IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + " 击杀了一只稀有精英怪，获得了一本盗贼技能书.\n偷窃：随机偷取一名玩家随机数量的雷币\n" +
                                                                                                 "触发几率："+ skillThief.SkillChance+"%\n"+
                                                                                                 "偷窃范围："+skillThief.EffLower+" - "+skillThief.EffUpper, -1);
                    }
                    //释放技能
                    ThiefAttack thiefAttack = new ThiefAttack();
                    thiefAttack=SkillAttack.ThiefSkillAttack(TigObjF);
                    if (thiefAttack!=null)
                    {
                        if (TigObjF==thiefAttack.QQid)
                        {
                            IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" + " 幸运的释放了偷窃技能，但不幸的是目标是自己\n" +
                                                                                                     "将"+thiefAttack.ThiefGold+"个穆雷币从左口袋放进了右口袋", -1);
                        }
                        else
                        {
                            IRQQApi.Api_SendMsg(RobotQQ, 2, MsgFrom, "", "[IR:at=" + TigObjF + "]" +
                                                                     " 幸运的释放了偷窃技能，偷走了" +
                                                                     "[IR:at=" + thiefAttack.QQid + "]" +" "+
                                                                      thiefAttack.ThiefGold + "个雷币！！", -1);
                        }
                        
                    }
                }
                
                else
                {
                    return 1;
                }

            }
            else if (MsgType == 3)
            {

            }
            //IRQQApi.Api_SendGroupMsg(RobotQQ,MsgFrom, "♪");
            //发送图片
            //String picPath = AppDomain.CurrentDomain.BaseDirectory + "1.jpg";
            //IRQQApi.Api_SendMsg(RobotQQ, MsgType, MsgFrom, TigObjF, IRQQConst.getPic(picPath), -1);
            return 1;
        }

        [DllExport(ExportName = nameof(IR_SetUp), CallingConvention = CallingConvention.StdCall)]
        ///启动窗体
        public static void IR_SetUp()
        {
            new FormMain().Show();
        }


        [DllExport(ExportName = nameof(IR_DestroyPlugin), CallingConvention = CallingConvention.StdCall)]
        ///插件即将被销毁
        public static int IR_DestroyPlugin()
        {
            return 0;
        }



        public static bool Test()
        {

            //数据库信息
            //
            string dbIP = "127.0.0.1";
            string dbName = "qqplugin";
            string dbUser = "root";
            string dbPwd = "qwerty123456";
            short dbPort = 3306;
            string dbFile = "";
            string QQID = "745569561";
            IRQQUtil.WritePluginLogFile(logfile, "\n开始查询" + DateTime.Now);
            try
            {
                DataBase.MySqlUtil mySqlUtil = new DataBase.MySqlUtil();
                mySqlUtil.SetDBInfo(dbIP, dbName, dbUser, dbPwd, dbPort, dbFile);

                string SqlString = "select * from gold";
                mySqlUtil.SetCammandText(SqlString);

                var iResult = mySqlUtil.Execute();

                Utils utils = new Utils();
                //GoldClass goldClass = new GoldClass();
                var goldClass= Utils.DataSetToEntity<GoldClass>(iResult,0);
                var a = iResult.Tables[0].Rows[0];
                var c = iResult.Tables[0].Rows.Count;
                if (c == 0)
                {
                    IRQQUtil.WritePluginLogFile(logfile, "\n数据库中没有" + DateTime.Now);
                    return false;
                }
                else
                {
                    string goldnum = iResult.Tables[0].Rows[0].ItemArray[0].ToString();
                    IRQQUtil.WritePluginLogFile(logfile, "\n查寻数据库成功" + DateTime.Now);
                    return true;
                }

            }
            catch (Exception ex)
            {
                IRQQUtil.WritePluginLogFile(logfile, "\n查寻数据库失败" + ex + DateTime.Now);
                return false;
            }
        }
    }
}
