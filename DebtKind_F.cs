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
    public partial class DebtKind_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public int usercode, code;
        public bool editmode = false;
        public DebtKind_F()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == string.Empty)

                MessageBox.Show("لطفا نام وام را وارد نمائید", "خطا", MessageBoxButtons.OK);

            else if (textBox5.Text == "0")

                MessageBox.Show("لطفا مبلغ وام را وارد نمائید", "خطا", MessageBoxButtons.OK);

            else if (textBox7.Text == "0")

                MessageBox.Show("لطفا تعداد اقساط را وارد نمائید", "خطا", MessageBoxButtons.OK);

            else if (textBox2.Text == "0")

                MessageBox.Show("لطفا مبلغ اقساط را وارد نمائید", "خطا", MessageBoxButtons.OK);

            else
            {

                DebtKInd DebtKIndtable = new DebtKInd
                {
                    
                    Descriptions = textBox1.Text,
                    count = byte.Parse(textBox7.Text),
                    DebtCash= double.Parse(textBox5.Text),
                    WageCash = double.Parse(textBox6.Text),
                    ReciveWageKind =byte.Parse(comboBox1.SelectedIndex.ToString()),
                    Cash = double.Parse(textBox2.Text),
                    Year= int.Parse(textBox3.Text) ,
                    Fromdate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd"),
                    ToDate = persianDateTimePicker2.Value.ToString("yyyy/MM/dd"),
                    Status = true,
                    UserCode = usercode,
                    IpAdress = Environment.MachineName,
                    Deleted = false
                };
                familial_bankEntitiescontext.DebtKInds.Add(DebtKIndtable);
                familial_bankEntitiescontext.SaveChanges();
                MessageBox.Show("وام جدید با کد  " + "* " + DebtKIndtable.Code + " *" + "ثبت گردید", "Information", MessageBoxButtons.OK);
                this.Close();
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
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

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
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

        private void DebtKind_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            comboBox1.SelectedIndex = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")

                MessageBox.Show("لطفا نام وام را وارد نمائید", "خطا", MessageBoxButtons.OK);

            else if (textBox5.Text == "0")

                MessageBox.Show("لطفا مبلغ وام را وارد نمائید", "خطا", MessageBoxButtons.OK);

            else if (textBox7.Text == "0")

                MessageBox.Show("لطفا تعداد اقساط را وارد نمائید", "خطا", MessageBoxButtons.OK);

                        else if (textBox2.Text == "0")

                            MessageBox.Show("لطفا مبلغ اقساط را وارد نمائید", "خطا", MessageBoxButtons.OK);

                        else
                        {
                            DebtKInd DebtKIndtable = familial_bankEntitiescontext.DebtKInds.First(i => i.Code == code);
                            if (MessageBox.Show("اطلاعات مورد نظر ثبت گردد؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {

                                DebtKIndtable.Descriptions = textBox1.Text;
                                DebtKIndtable.count = byte.Parse(textBox7.Text);
                                DebtKIndtable.DebtCash = double.Parse(textBox5.Text);
                                DebtKIndtable.WageCash = double.Parse(textBox6.Text);
                                DebtKIndtable.ReciveWageKind = byte.Parse(comboBox1.SelectedIndex.ToString());
                                DebtKIndtable.Cash = double.Parse(textBox2.Text);
                                DebtKIndtable.Year = int.Parse(textBox3.Text);
                                DebtKIndtable.Fromdate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
                                DebtKIndtable.ToDate = persianDateTimePicker2.Value.ToString("yyyy/MM/dd");

                                familial_bankEntitiescontext.SaveChanges();
                                MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                                DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 10, Environment.MachineName, code);
                                this.Close();
                            }
                        }
        }

        private void persianDateTimePicker1_Leave(object sender, EventArgs e)
        {
            textBox3.Text = persianDateTimePicker1.Value.Year.ToString();
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if ((textBox7.Text != "0") && (textBox5.Text != "0") && (textBox6.Text != "0"))
            {

                if (comboBox1.SelectedIndex == 0)
                {
                    textBox2.Text = (double.Parse(textBox5.Text) / double.Parse(textBox7.Text)).ToString();
                    textBox2.Text = string.Format("{0:#,##0}", double.Parse(textBox2.Text));
                }

                else if (comboBox1.SelectedIndex == 1)
                {
                    textBox2.Text = ((double.Parse(textBox5.Text) + (double.Parse(textBox5.Text) * (double.Parse(textBox6.Text)) / 100)) / double.Parse(textBox7.Text)).ToString();
                    textBox2.Text = string.Format("{0:#,##0}", double.Parse(textBox2.Text));
                }
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            textBox5.Text = string.Format("{0:#,##0}", double.Parse(textBox5.Text));
            //-----------
            if ((textBox7.Text != "0") && (textBox5.Text != "0") && (textBox6.Text != "0"))
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    textBox2.Text = (double.Parse(textBox5.Text) / double.Parse(textBox7.Text)).ToString();
                    textBox2.Text = string.Format("{0:#,##0}", double.Parse(textBox2.Text));
                }

                else if (comboBox1.SelectedIndex == 1)
                {
                    textBox2.Text = ((double.Parse(textBox5.Text) + (double.Parse(textBox5.Text) * (double.Parse(textBox6.Text)) / 100)) / double.Parse(textBox7.Text)).ToString();
                    textBox2.Text = string.Format("{0:#,##0}", double.Parse(textBox2.Text));
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((textBox7.Text != "0") && (textBox5.Text != "0") && (textBox6.Text != "0"))
            {

                if (comboBox1.SelectedIndex == 0)
                {
                    textBox2.Text = (double.Parse(textBox5.Text) / double.Parse(textBox7.Text)).ToString();
                    textBox2.Text = string.Format("{0:#,##0}", double.Parse(textBox2.Text));
                }

                else if (comboBox1.SelectedIndex == 1)
                {
                    textBox2.Text = ((double.Parse(textBox5.Text) + (double.Parse(textBox5.Text) * (double.Parse(textBox6.Text)) / 100)) / double.Parse(textBox7.Text)).ToString();
                    textBox2.Text = string.Format("{0:#,##0}", double.Parse(textBox2.Text));
                }
            }

        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if ((textBox7.Text != "0") && (textBox5.Text != "0") && (textBox6.Text != "0"))
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    textBox2.Text = (double.Parse(textBox5.Text) / double.Parse(textBox7.Text)).ToString();
                    textBox2.Text = string.Format("{0:#,##0}", double.Parse(textBox2.Text));
                }

                else if (comboBox1.SelectedIndex == 1)
                {
                    textBox2.Text = ((double.Parse(textBox5.Text) + (double.Parse(textBox5.Text) * (double.Parse(textBox6.Text)) / 100)) / double.Parse(textBox7.Text)).ToString();
                    textBox2.Text = string.Format("{0:#,##0}", double.Parse(textBox2.Text));
                }
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
    }
}
