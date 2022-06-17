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
    public partial class DebtMonthPayment_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public int usercode, code, DebtPaymentcode1,delaydate;
        public string memberscode="0", oldmemberscode="0", returnstring,datep,debtcode;
        public bool editmode = false;
        public string vamcash, ghestcash,remaincash;
        public string countt, summ;
        public bool insertmode = false;
        public Int32 editcode;

        public DebtMonthPayment_F()
        {
            InitializeComponent();
        }

        public bool loadcountandsumpayment(int membercose, int debtcode)
        {
            DLUtilsobj.temperory2obj.debtpaymentcountsumcash(membercose, debtcode);
            SqlDataReader DataSource;
            DLUtilsobj.temperory2obj.Dbconnset(true);
            DataSource = DLUtilsobj.temperory2obj.temperory2clientdataset.ExecuteReader();
            DataSource.Read();
            countt = DataSource["Expr1"].ToString();
            summ = DataSource["Expr2"].ToString();
            if (countt == "")
                countt = "0";
            if (summ == "")
                summ = "0";
            DataSource.Close();
            DLUtilsobj.temperory2obj.Dbconnset(false);
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
          if (textBox1.Text=="0")
            {
                MessageBox.Show("لطفا کد عضویت را انتخاب نمائید","خطا",MessageBoxButtons.OK);
            }

            else if (textBox6.Text == "0")
            {
                MessageBox.Show("لطفا کد وام را انتخاب نمائید", "خطا", MessageBoxButtons.OK);
            }

          else if (textBox8.Text == "0")
            {
                MessageBox.Show("لطفا مبلغ پرداختی را وارد نمائید", "خطا", MessageBoxButtons.OK);
            }

               else if (insertmode == false)
               {
                   MessageBox.Show("لطفا کد عضویت صحیح  را وارد نمائید"+"\n"+"کد عضویت وارد شده اشتباه می باشد", "خطا", MessageBoxButtons.OK);
               }

            else
            {

            DebtMonthlyPayment DebtMonthlyPaymenttable = new DebtMonthlyPayment
            {
                MembersCode = int.Parse(textBox1.Text),
                DebtCode = int.Parse(textBox6.Text),
                DebtDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd"),
                DebtDateReal = persianDateTimePicker1.Value.ToString("yyyy/MM/dd"),
                DelayDate = delaydate,
                DebtNumber = byte.Parse(textBox3.Text),
                DebtCash = double.Parse(textBox2.Text),
                Comment = textBox9.Text,
                PayoffCash = double.Parse(textBox8.Text),
                PenaltyCash = double.Parse(textBox7.Text),
                SerialNo = int.Parse(textBox10.Text),
                TotalCash = double.Parse(textBox5.Text),
                UserCode = usercode,
                IpAdress = Environment.MachineName,
                Deleted = false,
                Status = true
            };
            familial_bankEntitiescontext.DebtMonthlyPayments.Add(DebtMonthlyPaymenttable);
            familial_bankEntitiescontext.SaveChanges();
            
            //--------------تسویه وام
             if ((double.Parse(textBox8.Text))>= (double.Parse(label16.Text)))
             {
                 if (MessageBox.Show("تمامی اقساط وام انتخابی پرداخت گردیده است"+"\n"+" آیا مایل به تسویه وام انتخابی می باشید؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                 {
                     DebtPaymentcode1 = int.Parse(debtcode);
                     DebtPayment DebtPaymenttable = familial_bankEntitiescontext.DebtPayments.First(i => i.Code == DebtPaymentcode1);
                     DebtPaymenttable.CashEndDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
                     DebtPaymenttable.PayOffDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
                     DebtPaymenttable.PayOffTime = DateTime.Now.ToShortTimeString();
                     DebtPaymenttable.Status = 2;
                     familial_bankEntitiescontext.SaveChanges();
                     DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 19, Environment.MachineName, code);
                 }
             }


            MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
            this.Close();
          }

        }

        private void DebtMonthPayment_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();

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

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
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

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }

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

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;

        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MembersView_Select_F MembersView_Select_Frm = new MembersView_Select_F();
            MembersView_Select_Frm.statuse = "2";
            MembersView_Select_Frm.ShowDialog();
            oldmemberscode = textBox1.Text;
            memberscode = MembersView_Select_Frm.returncode;
            textBox1.Text = MembersView_Select_Frm.returncode;
            label11.Text = MembersView_Select_Frm.returnname;
            insertmode = true;

            //-------------clear 
            if (oldmemberscode != textBox1.Text)
            {
                textBox6.Text = "0";
                label10.Text = "---------";
                label12.Text = "---------";
                label17.Text = "---------";
                label16.Text = "---------";
                label20.Text = "---------";
                textBox2.Text = "0";
                textBox3.Text = "0";
                textBox5.Text = "0";
                textBox7.Text = "0";
                textBox8.Text = "0";

            }
            //-------------
            if ((textBox1.Text != "0") && (textBox6.Text != "0"))
            {
                loadcountandsumpayment(int.Parse(textBox1.Text), int.Parse(textBox6.Text));
                label17.Text = countt.ToString();
                label20.Text = summ.ToString();
                label20.Text = string.Format("{0:#,##0}", double.Parse(label20.Text));
                textBox3.Text = (int.Parse(label17.Text) + 1).ToString();
                textBox2.Text = ghestcash;
                label16.Text = (double.Parse(vamcash) - double.Parse(label20.Text)).ToString();
                label16.Text = string.Format("{0:#,##0}", double.Parse(label16.Text));

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0")
            {
                MessageBox.Show("لطفا عضو  را ابتدا مشخص نمائید. ", "خطا", MessageBoxButtons.OK);
            }
            else
            {
                DebtKindView_Select_F DebtKindView_Select_Frm = new DebtKindView_Select_F();
                DebtKindView_Select_Frm.memberscode = int.Parse(textBox1.Text);
                DebtKindView_Select_Frm.ShowDialog();
                debtcode = DebtKindView_Select_Frm.returncode;
                textBox6.Text = DebtKindView_Select_Frm.returncode;
                label10.Text = DebtKindView_Select_Frm.returnname;
                label12.Text = DebtKindView_Select_Frm.returncounta;
                vamcash = DebtKindView_Select_Frm.vamcash;
                ghestcash = DebtKindView_Select_Frm.ghestcash;
                datep = DebtKindView_Select_Frm.datep;
            }
            //--------------
            if ((textBox1.Text != "0") && (textBox6.Text != "0"))
            {
                loadcountandsumpayment(int.Parse(textBox1.Text), int.Parse(textBox6.Text));
                label17.Text = countt.ToString();
                label20.Text = summ.ToString();
                label20.Text = string.Format("{0:#,##0}", double.Parse(label20.Text));
                textBox3.Text = (int.Parse(label17.Text) + 1).ToString();
                textBox2.Text = ghestcash;
                textBox2.Text = string.Format("{0:#,##0}", double.Parse(textBox2.Text));
                textBox5.Text = textBox2.Text;
                textBox8.Text = textBox2.Text;
                label16.Text = (double.Parse(vamcash) - double.Parse(label20.Text)).ToString();
                remaincash = label16.Text;
                label16.Text = string.Format("{0:#,##0}", double.Parse(label16.Text));

                //------------تاریخ پرداخت قسط
                persianDateTimePicker1.Value = DLUtilsobj.temperoryobj.shamsitomiladi(datep);
                dateTimePicker1.Value = persianDateTimePicker1.Value;
                dateTimePicker1.Value = dateTimePicker1.Value.AddMonths(int.Parse(textBox3.Text) - 1);
                persianDateTimePicker1.Value = dateTimePicker1.Value;
                delaydate = (dateTimePicker2.Value - persianDateTimePicker1.Value).Days; 
                MessageBox.Show(delaydate.ToString());

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //------------
            if (textBox1.Text=="0")
            {
                MessageBox.Show("لطفا کد عضویت را انتخاب نمائید","خطا",MessageBoxButtons.OK);
            }

            else if (textBox6.Text == "0")
            {
                MessageBox.Show("لطفا کد وام را انتخاب نمائید", "خطا", MessageBoxButtons.OK);
            }

          else if (textBox8.Text == "0")
            {
                MessageBox.Show("لطفا مبلغ پرداختی را وارد نمائید", "خطا", MessageBoxButtons.OK);
            }

            else if (insertmode == false)
            {
                MessageBox.Show("لطفا کد عضویت صحیح  را وارد نمائید" + "\n" + "کد عضویت وارد شده اشتباه می باشد", "خطا", MessageBoxButtons.OK);
            }


            else
            {
                DebtMonthlyPayment DebtMonthlyPaymenttable = familial_bankEntitiescontext.DebtMonthlyPayments.First(i => i.Code == editcode);
                if (MessageBox.Show("اطلاعات مورد نظر ثبت گردد؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                DebtMonthlyPaymenttable.MembersCode = int.Parse(textBox1.Text);
                DebtMonthlyPaymenttable.DebtCode = int.Parse(textBox6.Text);
                DebtMonthlyPaymenttable.DebtDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
                //DebtMonthlyPaymenttable.Debtime = DateTime.Now.ToShortTimeString();
                DebtMonthlyPaymenttable.DebtNumber = byte.Parse(textBox3.Text);
                DebtMonthlyPaymenttable.DebtCash = double.Parse(textBox2.Text);
                DebtMonthlyPaymenttable.Comment = textBox9.Text;
                DebtMonthlyPaymenttable.PayoffCash = double.Parse(textBox8.Text);
                DebtMonthlyPaymenttable.PenaltyCash = double.Parse(textBox7.Text);
                DebtMonthlyPaymenttable.SerialNo = int.Parse(textBox10.Text);
                DebtMonthlyPaymenttable.TotalCash = double.Parse(textBox5.Text);
                familial_bankEntitiescontext.SaveChanges();
                MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 28, Environment.MachineName, editcode);
                this.Close();

                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (textBox10.Text == "")
                textBox10.Text = "0";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                returnstring = DLUtilsobj.temperory2obj.searchmembers_total(textBox1.Text);
                if (returnstring == "0")
                {
                    MessageBox.Show("کد وارد شده اشتباه می باشد " , "خطا", MessageBoxButtons.OK);
                    insertmode = false;
                    label11.Text = "-------------";
                    //**************
                    textBox6.Text = "0";
                    label10.Text = "-------------";
                    label12.Text = "-------------";
                    label17.Text = "-------------";
                    label16.Text = "-------------";
                    label20.Text = "-------------";
                    vamcash = "0";
                    ghestcash = "0";
                    textBox2.Text = "0";
                    textBox3.Text = "0";
                    textBox5.Text = "0";
                    textBox8.Text = "0";
                }

                else
                {
                    label11.Text = returnstring;
                    insertmode = true;
                    memberscode = textBox1.Text;
                }



            }
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
                    //**************
                    textBox6.Text = "0";
                    label10.Text = "-------------";
                    label12.Text = "-------------";
                    label17.Text = "-------------";
                    label16.Text = "-------------";
                    label20.Text = "-------------";
                    vamcash = "0";
                    ghestcash = "0";
                    textBox2.Text = "0";
                    textBox3.Text = "0";
                    textBox5.Text = "0";
                    textBox8.Text = "0";
                }

                else
                {
                    label11.Text = returnstring;
                    insertmode = true;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
                textBox1.Text = "0";

            if (textBox1.Text == "0")
                textBox1.SelectAll();

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text == "0")
                textBox8.SelectAll();
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

    }
}