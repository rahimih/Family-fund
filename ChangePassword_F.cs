using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace familial_bank
{
    public partial class ChangePassword_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        public int usercode;
        public string str2;
        public ChangePassword_F()
        {
            InitializeComponent();
        }

        private void Ins_Button_Click(object sender, EventArgs e)
        {
            //---------------
            if (Password_textBox.Text != Confirmpass_textBox.Text)

                MessageBox.Show(" رمز عبور وارد شده با هم همخوانی ندارد. ", "خطا", MessageBoxButtons.OK);
            else
            {
                DLUtilsobj.usercheckingobj.Changepassword(usercode,str2, Password_textBox.Text);
                MessageBox.Show("رمز عبور تغییر یافت", "Information", MessageBoxButtons.OK);
                DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 2, Environment.MachineName, usercode);
                this.Close();
            }
        }

        private void ChangePassword_F_Load(object sender, EventArgs e)
        {
            DLUtilsobj = new DLibraryUtils.DLUtils();
        }

        private void ChangePassword_F_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
    }
}
