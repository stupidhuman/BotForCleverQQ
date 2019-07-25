using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiLiuShui.IRQQ.CSharp.RPG
{
    public class LuckyAddon
    {
        public static double GetiLuckyAddon(string qqid)
        {
            int getLucky = CurdToDB.SearchLuckyFromDB(qqid);
            double iLucky = 1 + (getLucky * 0.5 / 100);
            return iLucky;
        }
        public static double GetdLuckyAddon(string qqid)
        {
            int getLucky = CurdToDB.SearchLuckyFromDB(qqid);
            double dLucky = 1 - (getLucky * 0.5 / 100);
            return dLucky;
        }
    }
}
