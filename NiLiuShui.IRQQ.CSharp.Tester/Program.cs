using System;
using System.Windows.Forms;

namespace NiLiuShui.IRQQ.CSharp.Tester
{
    public class Program
    {

        static void Main(string[] args)
        {
            //在本项目运行插件内的方法时,涉及到调用IRQQApi的请自行提供数据,否则调用任何API都会返回空内容!!!
            IRQQMain.Debug = true;//调试模式
            Console.WriteLine(IRQQMain.IR_Create());
            int MsgType = 2;
            int MsgCType = 1;
            int pText = 1;
            IRQQMain.IR_Event("462876418", MsgType, MsgCType, "96955256", "745569561", "TigObjFC", "#查询", "MsgNum", "MsgID", "RawMsg", "Json", pText);
            IRQQMain.IR_Event("462876418", MsgType, MsgCType, "96955256", "745569560", "TigObjFC", "#轮盘", "MsgNum", "MsgID", "RawMsg", "Json", pText);
            IRQQMain.IR_Event("462876418", MsgType, MsgCType, "96955256", "745569562", "TigObjFC", "#轮盘", "MsgNum", "MsgID", "RawMsg", "Json", pText);
            IRQQMain.IR_Event("462876418", MsgType, MsgCType, "96955256", "745569563", "TigObjFC", "#轮盘", "MsgNum", "MsgID", "RawMsg", "Json", pText);
            IRQQMain.IR_Event("462876418", MsgType, MsgCType, "96955256", "745569565", "TigObjFC", "#轮盘", "MsgNum", "MsgID", "RawMsg", "Json", pText);

            Application.Run(new FormMain());//运行插件设置窗口
        }
    }
}
