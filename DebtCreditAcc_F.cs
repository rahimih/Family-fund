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
    public partial class DebtCreditAcc_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public int usercode, code ;
        public string returncodeacc;
        public byte kind,kindtemp;
        public bool editmode=false;
        public byte kinde;
        public DebtCreditAcc_F()
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
                        Kind = byte.Parse(Status_comboBox.SelectedValue.ToString()),
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
                if (kind == 2)
                {
                    DebtCreditAcc DebtCreditAcctable = new DebtCreditAcc
                    {
                        AccCode = byte.Parse(returncodeacc),
                        DateP = persianDateTimePicker1.Value.ToString("yyyy/MM/dd"),
                        //TimeP = maskedTextBox1.Text,
                        Variz_Cash = 0,
                        Bardasht_Cash = double.Parse(textBox2.Text),
                        FishNumber = int.Parse(textBox3.Text),
                        Descriptions = textBox1.Text,
                        Kind = byte.Parse(Status_comboBox.SelectedValue.ToString()),
                        UserCode = usercode,
                        IpAdress = Environment.MachineName,
                        Deleted = false
                    };
                    familial_bankEntitiescontext.DebtCreditAccs.Add(DebtCreditAcctable);
                    familial_bankEntitiescontext.SaveChanges();
                    DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 21, Environment.MachineName, code);
                    MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                    this.Close();

                }

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            BankAcc_View_Select_F BankAcc_View_Select_Frm = new BankAcc_View_Select_F();
            BankAcc_View_Select_Frm.openform = 2;
            BankAcc_View_Select_Frm.ShowDialog();
            textBox6.Text = BankAcc_View_Select_Frm.returnacc;
            label10.Text = BankAcc_View_Select_Frm.returnbankname;
            label12.Text = BankAcc_View_Select_Frm.returnbranch;
            returncodeacc = BankAcc_View_Select_Frm.returncode;
        }

        private void DebtCreditAcc_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            //if (editmode==false)
            //maskedTextBox1.Text = DateTime.Now.ToShortTimeString();
            //--------------
            Status_comboBox.DisplayMember = "Description";
            Status_comboBox.ValueMember = "Code";
            Status_comboBox.DataSource = familial_bankEntitiescontext.DebtCreditKinds.Where(P => P.Kind == kindtemp).ToList();
            if (editmode == true)
                Status_comboBox.SelectedValue = kinde;
                                             

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "0")

                MessageBox.Show("لطفا شماره حساب را انتخاب نمائید", "خطا", MessageBoxButtons.OK); 
  
            if (textBox2.Text == "0")

                MessageBox.Show("لطفا مبلغ  را وارد نمائید", "خطا", MessageBoxButtons.OK);
            else
            {
                     DebtCreditAcc DebtCreditAcctable = familial_bankEntitiescontext.DebtCreditAccs.First(i => i.Code == code);
                     if (MessageBox.Show("اطلاعات مورد نظر ثبت گردد؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                     {
                         if (kind == 1)
                         {
                             DebtCreditAcctable.AccCode = byte.Parse(returncodeacc);
                             DebtCreditAcctable.DateP = persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
                             //DebtCreditAcctable.TimeP = maskedTextBox1.Text;
                             DebtCreditAcctable.Variz_Cash = double.Parse(textBox2.Text);
                             DebtCreditAcctable.Bardasht_Cash = 0;
                             DebtCreditAcctable.FishNumber = int.Parse(textBox3.Text);
                             DebtCreditAcctable.Descriptions = textBox1.Text;
                             DebtCreditAcctable.Kind = byte.Parse(Status_comboBox.SelectedValue.ToString());
                             familial_bankEntitiescontext.SaveChanges();
                             MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                             DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 29, Environment.MachineName, code);
                             this.Close();
                         }

                         if (kind == 2)
                         {
                             DebtCreditAcctable.AccCode = byte.Parse(textBox6.Text);
                             DebtCreditAcctable.DateP = persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
                             //DebtCreditAcctable.TimeP = maskedTextBox1.Text;
                             DebtCreditAcctable.Variz_Cash = 0;
                             DebtCreditAcctable.Bardasht_Cash = double.Parse(textBox2.Text);
                             DebtCreditAcctable.FishNumber = int.Parse(textBox3.Text);
                             DebtCreditAcctable.Descriptions = textBox1.Text;
                             DebtCreditAcctable.Kind = byte.Parse(Status_comboBox.SelectedValue.ToString());
                             familial_bankEntitiescontext.SaveChanges();
                             MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                             DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 30, Environment.MachineName, code);
                             this.Close();
                         }
                     }


                   }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == string.Empty)
                textBox2.Text = "0";
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            label9.Text = ToWords.ToString(long.Parse(textBox2.Text)) + " ریال";
        }
    }
}
