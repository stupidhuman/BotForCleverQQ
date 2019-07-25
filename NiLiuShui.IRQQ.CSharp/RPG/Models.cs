namespace NiLiuShui.IRQQ.CSharp
{
    class Models
    {
    }

    public class GoldClass
    {
        public string Id { get; set; }
        public int Gold { get; set; }
    }
    public class SkillThief
    {
        public string QQid { get; set; }
        public string SkillChance { get; set; } 
        public string EffLower { get; set; }
        public string EffUpper { get; set; }
    }

    public class ThiefAttack
    {
        public string QQid { get; set; }
        public int ThiefGold { get; set; }
    }

}
