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
using FarsiLibrary.Utils;


namespace familial_bank
{
    public partial class DebtCreditAcc_sod_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public int usercode, code ;
        public string returncodeacc;
        public byte kind,kindtemp;
        public bool editmode=false;
        public byte kinde;
        public DebtCreditAcc_sod_F()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //----------
            if (textBox6.Text == "0")

                MessageBox.Show("لطفا شماره حساب را انتخاب نمائید", "خطا", MessageBoxButtons.OK);

            if (textBox2.Text == "0")

                MessageBox.Show("لطفا مبلغ را وارد نمائید", "خطا", MessageBoxButtons.OK);
            else
            {

                if (kind == 1)
                {
                    DebtCreditAcc DebtCreditAcctable = new DebtCreditAcc
                    {
                        AccCode = byte.Parse(returncodeacc),
                        DateP = persianDateTimePicker1.Value.ToString("yyyy/MM/dd"),
                        TimeP = DateTime.Now.ToShortTimeString(),
                        Variz_Cash = double.Parse(textBox2.Text),
                        Bardasht_Cash = 0,
                        FishNumber = int.Parse(textBox3.Text),
                        Descriptions = textBox1.Text,
                        Kind = 5 ,
                        UserCode = usercode,
                        IpAdress = Environment.MachineName,
                        Deleted = false
                    };
                    familial_bankEntitiescontext.DebtCreditAccs.Add(DebtCreditAcctable);
                    familial_bankEntitiescontext.SaveChanges();
                    DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 20, Environment.MachineName, code);
                    MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                    this.Close();
                }

                //-----------
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            BankAcc_View_Select_F BankAcc_View_Select_Frm = new BankAcc_View_Select_F();
            BankAcc_View_Select_Frm.openform = 2;
            BankAcc_View_Select_Frm.ShowDialog();
            textBox6.Text = BankAcc_View_Select_Frm.returnacc;                        
            returncodeacc = BankAcc_View_Select_Frm.returncode;
            textBox5.Text = DLUtilsobj.temperoryobj.balance_acc(returncodeacc, "1370/01/01", persianDateTimePicker1.Value.ToString("yyyy/MM/dd"));
            label1.Text = string.Format("{0:#,##0}", double.Parse(textBox5.Text));

        }

        private void DebtCreditAcc_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            //if (editmode==false)
            //maskedTextBox1.Text = DateTime.Now.ToShortTimeString();
            //--------------
                                             

        }

   
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == string.Empty)
                textBox2.Text = "0";
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            textBox2.Text = (double.Parse(textBox4.Text) - double.Parse(textBox5.Text)).ToString();
            label7.Text = string.Format("{0:#,##0}", double.Parse(textBox2.Text));
        }
    }
}
