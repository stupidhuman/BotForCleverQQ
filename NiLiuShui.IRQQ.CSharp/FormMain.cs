using System;
using System.Windows.Forms;

namespace NiLiuShui.IRQQ.CSharp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            instance = this;
        }

        private static FormMain instance;
        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static FormMain getInstance()
        {
            return instance;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
