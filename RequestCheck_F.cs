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
    public partial class RequestCheck_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public bool editmode = false;
        public string returncodeacc;
        public int usercode;
        public string transfeecode;
        public int editcode;
        public RequestCheck_F()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "0")

                MessageBox.Show("لطفا شماره حساب را وارد نمائید", "خطا", MessageBoxButtons.OK);

            else if (numericUpDown1.Value==0)

                MessageBox.Show("لطفا تعداد را وارد نمائید", "خطا", MessageBoxButtons.OK);

            else if (numericUpDown2.Value==0)

                MessageBox.Show("لطفا شماره ابتدایی دسته چک را وارد نمائید", "خطا", MessageBoxButtons.OK);

            else if (numericUpDown3.Value==0)

                MessageBox.Show("لطفا شماره انتهایی دسته چک  را وارد نمائید", "خطا", MessageBoxButtons.OK);

            else if (textBox1.Text=="0")

                MessageBox.Show("لطفا کد عضویت کاربر تحویل گیرنده را وارد نمائید", "خطا", MessageBoxButtons.OK);

               else
            {

                RequestCheck RequestChecktable = new RequestCheck
                {
                    AccCode =byte.Parse(returncodeacc),
                    CheckNumberFrom = int.Parse(numericUpDown2.Value.ToString()),
                    CheckNumberTo =int.Parse(numericUpDown3.Value.ToString()),
                    countt = byte.Parse(numericUpDown1.Value.ToString()),
                    transferee_user = int.Parse(transfeecode),
                    Delivery_date = persianDateTimePicker1.Value.ToString("yyyy/MM/dd"),
                    Usercode = usercode,
                    IpAdress = Environment.MachineName,
                    Status = true
                };
                familial_bankEntitiescontext.RequestChecks.Add(RequestChecktable);
                familial_bankEntitiescontext.SaveChanges();
                MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                this.Close();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            BankAcc_View_Select_F BankAcc_View_Select_Frm = new BankAcc_View_Select_F();
            BankAcc_View_Select_Frm.openform = 1;
            BankAcc_View_Select_Frm.ShowDialog();
            textBox6.Text = BankAcc_View_Select_Frm.returnacc;
            label10.Text = BankAcc_View_Select_Frm.returnbankname;
            label12.Text = BankAcc_View_Select_Frm.returnbranch;
            returncodeacc = BankAcc_View_Select_Frm.returncode;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MembersView_Select_F MembersView_Select_Frm = new MembersView_Select_F();
            MembersView_Select_Frm.ShowDialog();
            transfeecode = MembersView_Select_Frm.returncode;
            textBox1.Text = MembersView_Select_Frm.returnname;

        }

        private void RequestCheck_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();

        }

        private void numericUpDown2_Leave(object sender, EventArgs e)
        {
            numericUpDown3.Value = numericUpDown2.Value + numericUpDown1.Value;
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            numericUpDown3.Value = numericUpDown2.Value + numericUpDown1.Value;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
                textBox6.Text = "0";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                textBox1.Text = "0";

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "0")

                MessageBox.Show("لطفا شماره حساب را وارد نمائید", "خطا", MessageBoxButtons.OK);

            else if (numericUpDown1.Value==0)

                MessageBox.Show("لطفا تعداد را وارد نمائید", "خطا", MessageBoxButtons.OK);

            else if (numericUpDown2.Value==0)

                MessageBox.Show("لطفا شماره ابتدایی دسته چک را وارد نمائید", "خطا", MessageBoxButtons.OK);

            else if (numericUpDown3.Value==0)

                MessageBox.Show("لطفا شماره انتهایی دسته چک  را وارد نمائید", "خطا", MessageBoxButtons.OK);

            else if (textBox1.Text=="0")

                MessageBox.Show("لطفا کد عضویت کاربر تحویل گیرنده را وارد نمائید", "خطا", MessageBoxButtons.OK);

               else
            {

                RequestCheck RequestChecktable = familial_bankEntitiescontext.RequestChecks.First(i => i.Code == editcode);
                 if (MessageBox.Show("اطلاعات مورد نظر ثبت گردد؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                 {

                     RequestChecktable.AccCode = byte.Parse(returncodeacc);
                     RequestChecktable.CheckNumberFrom = int.Parse(numericUpDown2.Value.ToString());
                     RequestChecktable.CheckNumberTo = int.Parse(numericUpDown3.Value.ToString());
                     RequestChecktable.countt = byte.Parse(numericUpDown1.Value.ToString());
                     RequestChecktable.transferee_user = int.Parse(transfeecode);
                     RequestChecktable.Delivery_date = persianDateTimePicker1.Value.ToString("yyyy/MM/dd");

                     familial_bankEntitiescontext.SaveChanges();
                     MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                     DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 16, Environment.MachineName, editcode);
                     this.Close();

                 }
            }

        }
    }
}
