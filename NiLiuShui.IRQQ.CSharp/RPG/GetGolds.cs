using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiLiuShui.IRQQ.CSharp
{
    public class GetGolds
    {
        /// <summary>
        /// 发言获得金币
        /// </summary>
        /// <param name="qqid">qq号</param>
        /// <returns></returns>
        public static int DefaultGetGold(string qqid)
        {
            Random rd = new Random();
            int newGold;
            int newLucky;
            int iResult;
            newGold = rd.Next(1, 10);
            newLucky = rd.Next(1, 50);

            int oldGold = CurdToDB.SearchGoldFromDB(qqid);
            int getLucky = CurdToDB.SearchLuckyFromDB(qqid);
            double iLucky = 1 + (getLucky * 0.5 / 100);
            double dLucky = 1 - (getLucky * 0.5 / 100);
            if (oldGold==-2)
            {
                return -1;
            }
            else if (oldGold == -1)
            {
                CurdToDB.AddGoldToDB(qqid, newGold, newLucky);
                return newGold;
            }
            else if (oldGold != -1 && oldGold != -2)
            {
                int iGold=newGold;
                CurdToDB.UpdateGoldToDB(qqid, oldGold, newGold);
                iResult = rd.Next(1, (int)(10000 * dLucky));
                if (iResult == 1)
                {
                    iGold = (int)(rd.Next(1001, 5000) * iLucky);
                    CurdToDB.UpdateGoldToDB(qqid, oldGold, iGold);
                    return iGold;
                }
                else if (iResult < 10)
                {
                    iGold = (int)(rd.Next(101, 1000) * iLucky);
                    CurdToDB.UpdateGoldToDB(qqid, oldGold, iGold);
                    return iGold;
                }
                else if (iResult < 110)
                {
                    iGold = (int)(rd.Next(10, 100) * iLucky);
                    CurdToDB.UpdateGoldToDB(qqid, oldGold, iGold);
                    return iGold;
                }
                return iGold;
            }

            return -1;
        }
        /// <summary>
        /// 发言获得技能
        /// </summary>
        /// <param name="qqid"></param>
        /// <returns></returns>
        public static SkillThief DefaultGetSkill(string qqid)
        {
            SkillThief skillThief = new SkillThief();
            Random random = new Random();
            int skillChance;
            int effLower;
            int effUpper;
            int effRange;
            skillChance = random.Next(1, 6);
            effLower = random.Next(0, 21);
            effRange = random.Next(1, 21);
            effUpper = effLower + effRange;

            int rdChance = random.Next(1, 101);

            skillThief.QQid = qqid;
            skillThief.SkillChance = skillChance.ToString();
            skillThief.EffLower = effLower.ToString();
            skillThief.EffUpper = effUpper.ToString();

            if (rdChance==5)
            {
                //查询之前是否已经拥有
                var iResult = CurdToDB.SearchSkillThiefFromDB(qqid);
                if (iResult == null)
                {
                    //之前没有
                    bool aResult = CurdToDB.AddSkillThiefToDB(qqid, skillChance, effLower, effUpper);
                    if (aResult)
                    {
                        CurdToDB.AddSkillOwnToDB(qqid, 1);
                        return skillThief;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    //之前有了  不做任何操作
                    return null;
                }
            }
            else
            {
                return null;
            }

        }


        //public static int RareGetGold(string qqid, int oldGold)
        //{
        //    Random rd = new Random();
        //    int iResult;
        //    iResult = rd.Next(1, 10000);
        //    if (iResult==9999)
        //    {
        //        int newGold = rd.Next(1, 5000);
        //        CurdToDB.UpdateGoldToDB(qqid, oldGold, newGold);
        //        return 2;
        //    }
        //    else if (iResult<10)
        //    {
        //        int newGold = rd.Next(1, 500);
        //        CurdToDB.UpdateGoldToDB(qqid, oldGold, newGold);
        //        return 1;
        //    }
        //    else if (iResult<110)
        //    {
        //        int newGold = rd.Next(1, 50);
        //        CurdToDB.UpdateGoldToDB(qqid, oldGold, newGold);
        //        return 0;
        //    }
        //    return -1;
        //}
    }
}
