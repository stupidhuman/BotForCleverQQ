using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiLiuShui.IRQQ.CSharp.RPG
{
    public static class Stock
    {
        public static int stockPrice;
        public static int lower=20;
        public static int upper;
        public static int buyingPeople;
        public static int salingPeople;
        //public static System.Threading.Timer timer = new System.Threading.Timer();
        public static void StockPriceWithTime()
        {
            Random random = new Random();
            int rd = random.Next(1, 101);
            if (buyingPeople>salingPeople)
            {
                if (rd>40)
                {
                    //up
                }
                else
                {
                    //down
                }
            }
            else if (salingPeople>buyingPeople)
            {
                if (rd > 60)
                {
                    //up
                }
                else
                {
                    //down
                }
            }
            else
            {
                if (rd > 50)
                {
                    //up
                }
                else
                {
                    //down
                }
            }
        }
    }
}
