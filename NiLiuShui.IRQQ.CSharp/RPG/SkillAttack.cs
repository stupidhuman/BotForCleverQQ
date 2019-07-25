using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiLiuShui.IRQQ.CSharp.RPG
{
    public class SkillAttack
    {
        public static ThiefAttack ThiefSkillAttack(string qqid)
        {
            SkillThief skillThief = new SkillThief();
            skillThief = CurdToDB.SearchSkillThiefFromDB(qqid);
            if (skillThief==null)
            {
                //没有技能
                return null;
            }
            else
            {
                Random random = new Random();
                double iLuckyAddon = LuckyAddon.GetiLuckyAddon(qqid);
                int iChance = (int)(int.Parse(skillThief.SkillChance)*iLuckyAddon);
                int iResult = random.Next(1,101);

                //测试
                //iResult = 2;
                //

                if (iResult>=1&&iResult<iChance)
                {
                    //判定成功
                    ThiefAttack thiefAttack = new ThiefAttack();
                    int thiefGold=random.Next(int.Parse(skillThief.EffLower), int.Parse(skillThief.EffUpper) + 1);
                    List<string> qqIdList = CurdToDB.SearchGoldListFromDB(thiefGold);
                    int attNum = random.Next(0, qqIdList.Count);
                    int oldGold=CurdToDB.SearchGoldFromDB(qqIdList[attNum]);
                    CurdToDB.UpdateGoldToDB(qqIdList[attNum], oldGold, -thiefGold);
                    int ioldGold = CurdToDB.SearchGoldFromDB(qqid);
                    CurdToDB.UpdateGoldToDB(qqid, ioldGold, thiefGold);
                    thiefAttack.QQid = qqIdList[attNum];
                    thiefAttack.ThiefGold = thiefGold;
                    return thiefAttack;
                }
                else
                {
                    //未触发
                    return null;
                }
                
            }

        }
    }
}
