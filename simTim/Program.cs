using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simTim
{
    

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            common.label = 0;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form_login main = new Form_login();
           // Form2 main2 = new Form2();
            //与QT大作业开发类似考虑利用事件传参
            Application.Run(main);
        }

    }
    public static class common
    {
        public static Model.userinfo user = new Model.userinfo();
        public static Dictionary<string, chart> charts = new Dictionary<string, chart> { };
        //  public static BLL.User2Server user2server = new BLL.User2Server();
        public static int label;
    }

}
    