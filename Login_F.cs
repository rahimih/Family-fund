using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DLibraryUtils;

namespace familial_bank
{
    public partial class Login_F : Form
    {
        Main_f Main_Frm = new Main_f();
        public DLibraryUtils.DLUtils DLUtilsobj;
        string str2;
        
        public Login_F()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //--------------
            if (textBox1.Text.ToString() == "")
            {
                MessageBox.Show("لطفا نام عبور را وارد نمائید", "خطا", MessageBoxButtons.OK);
            }

            if ((textBox1.Text.ToString() != "") && (textBox2.Text.ToString() == ""))
            {
                MessageBox.Show("لطفا رمز عبور را وارد نمائید", "خطا", MessageBoxButtons.OK);
            }

            else if ((textBox1.Text.ToString() != "") && (textBox2.Text.ToString() != ""))
            {
               
                 if (DLUtilsobj.usercheckingobj.Userlogin_checking(textBox1.Text, textBox2.Text) == true)
                 {
                     SqlDataReader DataSource;
                     DLUtilsobj.usercheckingobj.Dbconnset(true);
                     DataSource = DLUtilsobj.usercheckingobj.usercheckingclientdataset.ExecuteReader();
                     DataSource.Read();
                     Main_Frm.label1.Text = DataSource["Firstname"].ToString() + " " + DataSource["Lastname"].ToString();
                     DLUtilsobj.EventsLogobj.insertEventsLog(DataSource["code"].ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 1, Environment.MachineName, int.Parse(DataSource["code"].ToString()));
                     Main_Frm.usercodetemp = int.Parse(DataSource["code"].ToString());
                     DataSource.Close();
                     DLUtilsobj.usercheckingobj.Dbconnset(false);

                     Main_Frm.sdate = DLUtilsobj.temperoryobj.getdate();
                     Main_Frm.sdate_shamsi = DLUtilsobj.temperoryobj.miladitoshamsi(Main_Frm.sdate);                    
                     this.Hide();
                     Main_Frm.ShowDialog();
                 }
                 else
                 {
                     MessageBox.Show("نام یا رمز عبور اشتباه می باشد.", "خطا", MessageBoxButtons.OK);
                 }
            }
        }

   

    
  
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                label1_Click (textBox2, e);
            }
        }

        private void Login_F_Load(object sender, EventArgs e)
        {
            DLUtilsobj = new DLibraryUtils.DLUtils();
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            if (textBox1.Visible == false)
            {
                textBox1.Text = str2;
                if (textBox2.Text.ToString() == "")
                {
                    MessageBox.Show("لطفا رمز عبور را وارد نمائید", "خطا", MessageBoxButtons.OK);
                }

            }


            if ((textBox1.Visible == true) && (textBox1.Text.ToString() == ""))
            {
                MessageBox.Show("لطفا نام عبور را وارد نمائید", "خطا", MessageBoxButtons.OK);
            }

            else
            {
                if (DLUtilsobj.usercheckingobj.Userlogin_checking(textBox1.Text, textBox2.Text) == true)
                {
                    SqlDataReader DataSource;
                    DLUtilsobj.usercheckingobj.Dbconnset(true);
                    DataSource = DLUtilsobj.usercheckingobj.usercheckingclientdataset.ExecuteReader();
                    DataSource.Read();
                    ChangePassword_F ChangePassword_Frm = new ChangePassword_F();
                    ChangePassword_Frm.usercode = int.Parse(DataSource["code"].ToString());
                    DataSource.Close();
                    ChangePassword_Frm.str2 = textBox2.Text;
                    ChangePassword_Frm.ShowDialog();
                    DLUtilsobj.usercheckingobj.Dbconnset(false);
                }
                else
                {
                    MessageBox.Show("نام یا رمز عبور اشتباه می باشد.", "خطا", MessageBoxButtons.OK);
                }

            }
        }
    }
}
