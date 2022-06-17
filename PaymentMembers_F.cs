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
    public partial class PaymentMembers_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public int usercode, code;
        public string memberscode,returnstring;
        public bool editmode = false;
        public byte kind;
        public int editcode;
        bool insertmode = false;

        public PaymentMembers_F()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PaymentMembers_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            if (editmode == false)
            {
                comboBox1.Text = persianDateTimePicker1.Value.Year.ToString();
                comboBox3.SelectedIndex = persianDateTimePicker1.Value.Month - 1;
            }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                returnstring= DLUtilsobj.temperory2obj.searchmembers(textBox1.Text);
                if (returnstring == "0")
                {
                    MessageBox.Show("کد وارد شده اشتباه می باشد " + "\n" + "یا کد عضو وارد شده غیر فعال می باشد", "خطا", MessageBoxButtons.OK);
                    insertmode = false;
                    label11.Text = "-------------";
                }

                else
                {
                    label11.Text = returnstring;
                    insertmode = true;
                    memberscode = textBox1.Text;
                    textBox2.Focus();
                }
               
            }

        }

        private void persianDateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }

        }

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
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

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            MembersView_Select_F MembersView_Select_Frm = new MembersView_Select_F();
            MembersView_Select_Frm.statuse = "1";
            MembersView_Select_Frm.ShowDialog();
            memberscode = MembersView_Select_Frm.returncode;
            label11.Text = MembersView_Select_Frm.returnname;
            textBox1.Text = memberscode;
            insertmode = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text=="0") || (insertmode==false))
            {
                MessageBox.Show("لطفا کد عضویت را وارد نمائید","خطا",MessageBoxButtons.OK);
            }

            else if (textBox2.Text == "0")
            {
                MessageBox.Show("لطفا مبلغ را وارد نمائید", "خطا", MessageBoxButtons.OK);
            }


            else
            {
                PaymentMember PaymentMembertable = new PaymentMember()
                {
                    MembersCode = int.Parse(textBox1.Text),
                    PaymentDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd"),
                    PaymentTime = DateTime.Now.ToShortTimeString(),
                    Cash = double.Parse(textBox2.Text),
                    kind = kind,
                    Month = byte.Parse((comboBox3.SelectedIndex + 1).ToString()),
                    Year = int.Parse(comboBox1.Text),
                    SerialNo = int.Parse(textBox3.Text),
                    Comment = textBox5.Text,
                    UserCode = usercode,
                    IpAdress = Environment.MachineName,
                    Deleted = false
                };
                familial_bankEntitiescontext.PaymentMembers.Add(PaymentMembertable);
                familial_bankEntitiescontext.SaveChanges();
                MessageBox.Show("اطلاعات مورد نظر با کد  " + "** " + PaymentMembertable.Code+ "**  ثبت گردید.", "Information", MessageBoxButtons.OK);
                //this.Close();
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                textBox2.Text = "0";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
                textBox3.Text = "0";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //------------
            if (textBox1.Text == "0")
            {
                MessageBox.Show("لطفا کد عضویت را وارد نمائید", "خطا", MessageBoxButtons.OK);
            }

            else if (textBox2.Text == "0")
            {
                MessageBox.Show("لطفا مبلغ را وارد نمائید", "خطا", MessageBoxButtons.OK);
            }

             else
            {

                PaymentMember PaymentMembertable = familial_bankEntitiescontext.PaymentMembers.First(i => i.Code == editcode);
                 if (MessageBox.Show("اطلاعات مورد نظر ثبت گردد؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                 {

                    PaymentMembertable.MembersCode = int.Parse(textBox1.Text);
                    PaymentMembertable.PaymentDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
                    //PaymentMembertable.PaymentTime = maskedTextBox1.Text;
                    PaymentMembertable.Cash = double.Parse(textBox2.Text);
                    //PaymentMembertable.kind = kind,
                    PaymentMembertable.Month = byte.Parse((comboBox3.SelectedIndex + 1).ToString());
                    PaymentMembertable.Year = int.Parse(comboBox1.Text);
                    PaymentMembertable.SerialNo = int.Parse(textBox3.Text);

                     familial_bankEntitiescontext.SaveChanges();
                     MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                     DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 17, Environment.MachineName, editcode);
                     this.Close();

                 }
            }

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox2.Text = string.Format("{0:#,##0}", double.Parse(textBox2.Text));
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (memberscode != textBox1.Text)
            {
                returnstring = DLUtilsobj.temperory2obj.searchmembers(textBox1.Text);
                if (returnstring == "0")
                {
                    MessageBox.Show("کد وارد شده اشتباه می باشد " + "\n" + "یا کد عضو وارد شده غیر فعال می باشد", "خطا", MessageBoxButtons.OK);
                    insertmode = false;
                    label11.Text = "-------------";
                }

                else
                {
                    label11.Text = returnstring;
                    insertmode = true;
                }
            }
        }
    }
}
