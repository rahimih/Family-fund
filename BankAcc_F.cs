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
    public partial class BankAcc_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public int usercode,code;
        public bool editmode = false;
        bool statustemp;
        public BankAcc_F()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Acc_textBox.Text == "")

                MessageBox.Show("لطفا شماره حساب  را وارد نمائید", "خطا", MessageBoxButtons.OK);

            if (Name_textBox.Text == "")

                MessageBox.Show("لطفا نام حساب  را وارد نمائید", "خطا", MessageBoxButtons.OK);
            else
            {
                if (Status_comboBox.SelectedIndex == 0)
                    statustemp = true;
                else
                    statustemp = false;

                BankAcc bankacctable = new BankAcc
                {
                    Descriptions = Name_textBox.Text ,
                    Kind = byte.Parse(Kind_comboBox.SelectedIndex+1.ToString()) ,
                    Bnak = Bnak_comboBox.SelectedItem.ToString() ,
                    BranchName = Branch_textBox.Text ,
                    BranchCode = int.Parse(BranchCode_textBox.Text) , 
                    AccNumber = Acc_textBox.Text ,
                    UserCode =  usercode ,
                    Status = statustemp ,
                    IpAdress = Environment.MachineName ,
                    Deleted = false
                };
                familial_bankEntitiescontext.BankAccs.Add(bankacctable);
                familial_bankEntitiescontext.SaveChanges();
                MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                this.Close();
            }

        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void BankAcc_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            if (editmode == false)
            {
                Bnak_comboBox.SelectedIndex = 0;
                Kind_comboBox.SelectedIndex = 0;
                Status_comboBox.SelectedIndex = 0;
            }
        }

        private void BankAcc_F_FormClosing(object sender, FormClosingEventArgs e)
        {
            familial_bankEntitiescontext.Dispose();
            this.Dispose();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            BankAcc BankAcctable = familial_bankEntitiescontext.BankAccs.First(i => i.Code == code);
            if (MessageBox.Show("اطلاعات مورد نظر ثبت گردد؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                
                if (Status_comboBox.SelectedIndex == 0)
                    statustemp = true;
                else
                    statustemp = false;

                BankAcctable.Descriptions = Name_textBox.Text ;
                BankAcctable.Kind = byte.Parse(Kind_comboBox.SelectedIndex+1.ToString()) ;
                BankAcctable.Bnak = Bnak_comboBox.Text;
                BankAcctable.BranchName = Branch_textBox.Text ;
                BankAcctable.BranchCode = int.Parse(BranchCode_textBox.Text) ;
                BankAcctable.AccNumber = Acc_textBox.Text ;
                BankAcctable.Status = statustemp ;               

                familial_bankEntitiescontext.SaveChanges();
                MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 4, Environment.MachineName, code);
                this.Close();



            }


        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
           if (button3.Enabled==true)
            button3_Click(toolStripMenuItem1,e);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
           if (button6.Enabled==true)
            button6_Click(toolStripMenuItem4, e);
        }
    }
}
