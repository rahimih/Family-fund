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
    public partial class Members_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public int usercode, code;
        public bool editmode = false;
        bool sextemp;
        bool statustemp;
        bool insertstatus = true;

        public Members_F()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
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

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
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

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
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

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
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

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            insertstatus = true;
            if (textBox2.Text == "")
            {
                MessageBox.Show("لطفا نام عضو را وارد نمائید", "خطا", MessageBoxButtons.OK);
                insertstatus = false;
            }

            else if (textBox3.Text == "")
            {
                MessageBox.Show("لطفا نام خانوادگی عضو را وارد نمائید", "خطا", MessageBoxButtons.OK);
                insertstatus = false;
            }


            else if (textBox7.Text == "")
            {
                MessageBox.Show("لطفا کد عضویت سرپرست را وارد نمائید", "خطا", MessageBoxButtons.OK);
                insertstatus = false;
            }


            else if ((comboBox2.SelectedIndex > 0) && (textBox7.Text == "0"))
            {
                MessageBox.Show("لطفا کد عضویت سرپرست را وارد نمائید", "خطا", MessageBoxButtons.OK);
                insertstatus = false;
            }

            else if (insertstatus == true)
            {
                if (MessageBox.Show("اطلاعات مورد نظر ثبت گردد؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (comboBox3.SelectedIndex == 0)
                        statustemp = true;
                    else
                        statustemp = false;

                    if (comboBox1.SelectedIndex == 0)
                        sextemp = true;
                    else
                        sextemp = false;

                    Member Membertable = new Member
                    {
                        FirstName = textBox2.Text,
                        LastName = textBox3.Text,
                        FatherName = textBox6.Text,
                        NationalCode = textBox5.Text,
                        IdentityNumber = textBox4.Text,
                        BirthDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd"),
                        Sex = sextemp,
                        Relation = byte.Parse((comboBox2.SelectedIndex + 1).ToString()),
                        MembershipDate = persianDateTimePicker2.Value.ToString("yyyy/MM/dd"),
                        MembershipTime = (DateTime.Now.ToShortTimeString()),
                        MainMemberShip = int.Parse(textBox7.Text),
                        userCode = usercode,
                        Status = statustemp,
                        ipadress = Environment.MachineName,
                        deleted = false
                    };
                    familial_bankEntitiescontext.Members.Add(Membertable);
                    familial_bankEntitiescontext.SaveChanges();
                    MessageBox.Show("عضو جدید با کد عضویت " +"* "+ Membertable.Code +" *"+ "ثبت گردید", "Information", MessageBoxButtons.OK);
                    this.Close();
                }
            }
        }

        private void Members_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            if (editmode == false)
            {
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                comboBox3.SelectedIndex = 0;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                textBox7.Text = "0";
                textBox7.Enabled = false;
                button4.Enabled = false;
            }
            else
            {
                textBox7.Text = "";
                textBox7.Enabled = false;
                button4.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MembersView_Select_F MembersView_Select_Frm = new MembersView_Select_F();
            MembersView_Select_Frm.ShowDialog();
            textBox7.Text = MembersView_Select_Frm.returncode;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            insertstatus = true;
            if (textBox2.Text == "")
            {
                MessageBox.Show("لطفا نام عضو را وارد نمائید", "خطا", MessageBoxButtons.OK);
                insertstatus = false;
            }

            else if (textBox3.Text == "")
            {
                MessageBox.Show("لطفا نام خانوادگی عضو را وارد نمائید", "خطا", MessageBoxButtons.OK);
                insertstatus = false;
            }


            else if (textBox7.Text == "")
            {
                MessageBox.Show("لطفا کد عضویت سرپرست را وارد نمائید", "خطا", MessageBoxButtons.OK);
                insertstatus = false;
            }

            else if ((comboBox2.SelectedIndex > 0) && (textBox7.Text == "0"))
            {
                MessageBox.Show("لطفا کد عضویت سرپرست را وارد نمائید", "خطا", MessageBoxButtons.OK);
                insertstatus = false;
            }

            else if (insertstatus==true)
            {
                Member membertable = familial_bankEntitiescontext.Members.First(i => i.Code == code);
                if (MessageBox.Show("اطلاعات مورد نظر ثبت گردد؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    if (comboBox3.SelectedIndex == 0)
                        statustemp = true;
                    else
                        statustemp = false;

                    if (comboBox1.SelectedIndex == 0)
                        sextemp = true;
                    else
                        sextemp = false;

                    membertable.FirstName = textBox2.Text;
                    membertable.LastName = textBox3.Text;
                    membertable.FatherName = textBox6.Text;
                    membertable.NationalCode = textBox5.Text;
                    membertable.IdentityNumber = textBox4.Text;
                    membertable.BirthDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
                    membertable.Sex = sextemp;
                    membertable.Relation = byte.Parse((comboBox2.SelectedIndex + 1).ToString());
                    membertable.MembershipDate = persianDateTimePicker2.Value.ToString("yyyy/MM/dd");
                    membertable.MainMemberShip = int.Parse(textBox7.Text);

                    familial_bankEntitiescontext.SaveChanges();
                    MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                    DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 7, Environment.MachineName, code);
                    this.Close();

                }
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
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
    }
}
