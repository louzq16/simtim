using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simTim
{
    public partial class Form_login : Form
    {
        Form2 mainForm;
        BLL.User2Server u2s = new BLL.User2Server();

        public Form_login()
        {
            InitializeComponent();
        }

        private void button_login_Click(object sender, EventArgs e)
        {

            string username, userpassword;
            common.user.username = textBox_account.Text.Trim().ToString();
            common.user.userpassword = textBox_password.Text.Trim().ToString();
            username = common.user.username;
            userpassword = common.user.userpassword;
            //MessageBox.Show(user.username + " " + user.userpassword);
            string restr = "";
            while(restr=="")
            {
                restr = u2s.userlog(username + "_" + userpassword, 1);
            }
            
            if (restr == "lol")
            {
                MessageBox.Show("登陆成功");
                this.Hide();
                this.textBox_account.Clear();
                this.textBox_password.Clear();
                mainForm = new Form2();
                mainForm.ShowDialog();
                if (mainForm.DialogResult == DialogResult.OK)
                {
                    mainForm.Dispose();
                    this.Show();
                    string restr2=u2s.userlog("logout" + username,1);
                    MessageBox.Show(restr2);
                }
                else
                {
                    mainForm.Dispose();
                    string restr2 = u2s.userlog("logout" + username,1);
                    MessageBox.Show(restr2);
                    System.Environment.Exit(0);
                }
            }
            else
            {
                MessageBox.Show(restr);
            }

        }
    }
}
