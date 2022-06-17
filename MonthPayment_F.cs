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
    public partial class MonthPayment_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public int usercode, code;
        public bool editmode = false;
        public MonthPayment_F()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("لطفا شرح را وارد نمائید", "خطا", MessageBoxButtons.OK);
            else if (textBox3.Text == "")
                MessageBox.Show("لطفا سال را وارد نمائید", "خطا", MessageBoxButtons.OK);
            else if (textBox2.Text == "")
                MessageBox.Show("لطفا مبلغ را وارد نمائید", "خطا", MessageBoxButtons.OK);
            else
            {
                MonthPayment MonthPaymenttable = new MonthPayment
                {                    
                    Description = textBox1.Text,
                    Year= int.Parse(textBox3.Text) ,
                    FromDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd"),
                    Todate = persianDateTimePicker2.Value.ToString("yyyy/MM/dd"),
                    Cashfixed= double.Parse(textBox2.Text),                    
                    UserCode = usercode,
                    IpAdress = Environment.MachineName,
                    Deleted = false
                };
                familial_bankEntitiescontext.MonthPayments.Add(MonthPaymenttable);
                familial_bankEntitiescontext.SaveChanges();
                MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                this.Close();
            }
        }

        private void MonthPayment_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void persianDateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }

        }

        private void persianDateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (button3.Enabled == true)
                button3_Click(toolStripMenuItem1, e);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (button6.Enabled == true)
                button6_Click(toolStripMenuItem4, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
             if (textBox1.Text == "")
                MessageBox.Show("لطفا شرح را وارد نمائید", "خطا", MessageBoxButtons.OK);
            else if (textBox3.Text == "")
                MessageBox.Show("لطفا سال را وارد نمائید", "خطا", MessageBoxButtons.OK);
            else if (textBox2.Text == "")
                MessageBox.Show("لطفا مبلغ را وارد نمائید", "خطا", MessageBoxButtons.OK);
            else
            {
                MonthPayment MonthPaymenttable = familial_bankEntitiescontext.MonthPayments.First(i => i.Code == code);
                if (MessageBox.Show("اطلاعات مورد نظر ثبت گردد؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    MonthPaymenttable.Description = textBox1.Text;
                    MonthPaymenttable.Year= int.Parse(textBox3.Text) ;
                    MonthPaymenttable.FromDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
                    MonthPaymenttable.Todate = persianDateTimePicker2.Value.ToString("yyyy/MM/dd");
                    MonthPaymenttable.Cashfixed = double.Parse(textBox2.Text);
        
                    familial_bankEntitiescontext.SaveChanges();
                    MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                    DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 13, Environment.MachineName, code);
                    this.Close();
                }
            }
        }
    

        private void persianDateTimePicker1_Leave(object sender, EventArgs e)
        {
            textBox3.Text = persianDateTimePicker1.Value.Year.ToString();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox2.Text = string.Format("{0:#,##0}", double.Parse(textBox2.Text));
        }
    }
}
