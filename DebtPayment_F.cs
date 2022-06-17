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
    public partial class DebtPayment_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public int usercode, code,day;
        public string memberscode, ghestcash, vamcash, sood, stockavg, returnstring;
        public string DebtCash1="0", DebtPay1="0", PaymentCash1="0", ipadress,fishnumber;
        public int codee;
        public bool editmode = false;
        bool insertmode = false;
        bool showmode = false;
        byte AccCodetemp;

        public DebtPayment_F()
        {
            InitializeComponent();
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

          else if (textBox3.Text == "0")
            {
                MessageBox.Show("لطفا مبلغ وام را وارد نمائید", "خطا", MessageBoxButtons.OK);
            }

               else if (insertmode == false)
               {
                   MessageBox.Show("لطفا کد عضویت صحیح  را وارد نمائید"+"\n"+"کد عضویت وارد شده اشتباه می باشد", "خطا", MessageBoxButtons.OK);
               }


            else
            {
            
              DebtPayment DebtPaymenttable = new DebtPayment
            {
                MembersCode = int.Parse(textBox1.Text),
                DebtKindCode = int.Parse(textBox6.Text),
                PaymentDate = persianDateTimePicker4.Value.ToString("yyyy/MM/dd"),
                paymentTime = DateTime.Now.ToShortTimeString() ,
                StockAvg = double.Parse(stockavg) ,
                DebtCash = double.Parse(DebtCash1) ,
                DebtPay = double.Parse(PaymentCash1) ,
                DebtWage = double.Parse(textBox2.Text) ,
                DebtWageKind = byte.Parse(comboBox1.SelectedIndex.ToString()),
                countt = byte.Parse(textBox5.Text),
                PaymentCash = double.Parse(DebtPay1) ,
                CashFirstDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd") ,
                CashEndDate = persianDateTimePicker2.Value.ToString("yyyy/MM/dd"),
                LatenPenalty = double.Parse(textBox10.Text) ,
                Status = 1 ,
                Comment = textBox9.Text,
                ChequeNumber = int.Parse(textBox4.Text),
                UserCode = usercode,
                IpAdress = Environment.MachineName
            };

            familial_bankEntitiescontext.DebtPayments.Add(DebtPaymenttable);
            familial_bankEntitiescontext.SaveChanges();
            MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
            //----------------   برداشت از حساب پس انداز            
            DebtCreditAcc DebtCreditAcctable = new DebtCreditAcc
            {
                AccCode = 1 ,
                DateP = persianDateTimePicker3.Value.ToString("yyyy/MM/dd"),
                TimeP = DateTime.Now.ToShortTimeString(),
                Variz_Cash = 0 ,
                Bardasht_Cash = double.Parse(textBox7.Text),
                FishNumber = DebtPaymenttable.Code,
                Descriptions = "برداشت از حساب بابت وام شماره "  + DebtPaymenttable.Code.ToString()+" " +label11.Text,
                Kind = 1 ,
                UserCode = usercode,
                IpAdress = Environment.MachineName,
                Deleted = false
            };
            familial_bankEntitiescontext.DebtCreditAccs.Add(DebtCreditAcctable);
            familial_bankEntitiescontext.SaveChanges();
            DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 20, Environment.MachineName, code);
            //--------------- update DebtPaymenttable
            int editcode = DebtPaymenttable.Code;
            int fishnumber = DebtCreditAcctable.Code;
             DebtPayment DebtPaymenttable2 = familial_bankEntitiescontext.DebtPayments.First(i => i.Code == editcode);
             DebtPaymenttable2.ChequeNumber = fishnumber;
             familial_bankEntitiescontext.SaveChanges();
            
            
            //---------------چاپ دفترچه             
             familial_bankEntitiescontext.DebtInsertprint(int.Parse(DebtCash1), int.Parse(DebtPay1), int.Parse(textBox5.Text), (DebtPaymenttable.Code));

            Report_debt_F Report_debt_Frm = new Report_debt_F();
            Report_debt_Frm.ipadress = ipadress;
            Report_debt_Frm.debtcode = DebtPaymenttable.Code.ToString();


            Report_debt_Frm.ShowDialog();
            this.Close();
          }   
        }

        private void DebtPayment_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            comboBox1.SelectedIndex = 0;
            //AccCodetemp = byte.Parse(DLUtilsobj.temperory2obj.defaultbankacc());

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

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }

        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MembersView_Select_F MembersView_Select_Frm = new MembersView_Select_F();
            MembersView_Select_Frm.statuse = "2";
            MembersView_Select_Frm.ShowDialog();
            memberscode = MembersView_Select_Frm.returncode;
            textBox1.Text = MembersView_Select_Frm.returncode;
            label11.Text = MembersView_Select_Frm.returnname;

            //------------
            if (memberscode != "0")
            {                
                label16.Text = DLUtilsobj.temperoryobj.balancemembers(memberscode);
                if (label16.Text == string.Empty)
                    label16.Text = "0"; 
                stockavg = label16.Text;                
                label16.Text = string.Format("{0:#,##0}", double.Parse(label16.Text));
                insertmode = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DebtKind_Select_F DebtKind_Select_Frm = new DebtKind_Select_F();            
            DebtKind_Select_Frm.ShowDialog();
            memberscode = DebtKind_Select_Frm.returncode;
            textBox6.Text = DebtKind_Select_Frm.returncode;
            label10.Text = DebtKind_Select_Frm.returnname;
            label12.Text = DebtKind_Select_Frm.returncounta;
            textBox3.Text = DebtKind_Select_Frm.vamcash;
            DebtCash1 = textBox3.Text;
            textBox2.Text = DebtKind_Select_Frm.wagecsh;
            textBox5.Text = DebtKind_Select_Frm.returncounta;
            textBox8.Text = DebtKind_Select_Frm.ghestcash;
            vamcash = DebtKind_Select_Frm.vamcash;
            ghestcash = DebtKind_Select_Frm.ghestcash;
            //-----------
            sood = ((int.Parse(DebtCash1)) * (double.Parse(textBox2.Text)) / 100).ToString();
             PaymentCash1 = ((int.Parse(DebtCash1)) - (int.Parse(sood))).ToString();
             textBox7.Text = PaymentCash1;
             textBox3_Leave(textBox3, e);
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (showmode == true)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    sood = ((int.Parse(DebtCash1)) * (double.Parse(textBox2.Text)) / 100).ToString();
                    PaymentCash1 = ((int.Parse(DebtCash1)) - (int.Parse(sood))).ToString();
                    textBox7.Text = PaymentCash1;
                    //------------
                    DebtPay1 = ((int.Parse(DebtCash1)) / (int.Parse(textBox5.Text))).ToString();
                    textBox8.Text = DebtPay1;


                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    PaymentCash1 = DebtCash1;
                    textBox7.Text = PaymentCash1;
                    //--------------
                    sood = ((int.Parse(DebtCash1)) * (double.Parse(textBox2.Text)) / 100).ToString();
                    DebtPay1 = (((int.Parse(DebtCash1)) + (int.Parse(sood))) / (int.Parse(textBox5.Text))).ToString();
                    textBox8.Text = DebtPay1;

                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == string.Empty)
                textBox3.Text = "0";            

            if (textBox3.Text == "0")
                textBox3.SelectAll();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            textBox8.Text = string.Format("{0:#,##0}", double.Parse(textBox8.Text));
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
             textBox7.Text = string.Format("{0:#,##0}", double.Parse(textBox7.Text));
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            DebtCash1 = textBox3.Text;
            
            if (comboBox1.SelectedIndex == 0)
            {
                DebtPay1 = ((int.Parse(DebtCash1)) / (int.Parse(textBox5.Text))).ToString();
                textBox8.Text = DebtPay1;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                sood = ((int.Parse(DebtCash1)) * (double.Parse(textBox2.Text)) / 100).ToString();
                DebtPay1 = (((int.Parse(DebtCash1)) + (int.Parse(sood))) / (int.Parse(textBox5.Text))).ToString();
                textBox8.Text = DebtPay1;
            }

            textBox3.Text = string.Format("{0:#,##0}", double.Parse(DebtCash1));
           

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if ((textBox5.Text == string.Empty) || (textBox5.Text=="0"))
                textBox5.Text = "1";
            //--------------
            dateTimePicker1.Value = persianDateTimePicker1.Value;
            dateTimePicker1.Value = dateTimePicker1.Value.AddMonths(int.Parse(textBox5.Text) - 1);
            persianDateTimePicker2.Value = dateTimePicker1.Value;


        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                DebtPay1 = ((int.Parse(DebtCash1)) / (int.Parse(textBox5.Text))).ToString();
                textBox8.Text = DebtPay1;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                sood = ((int.Parse(DebtCash1)) * (int.Parse(textBox2.Text)) / 100).ToString();
                DebtPay1 = (((int.Parse(DebtCash1)) + (int.Parse(sood))) / (int.Parse(textBox5.Text))).ToString();
                textBox8.Text = DebtPay1;

                //------------
                dateTimePicker1.Value = persianDateTimePicker1.Value;
                dateTimePicker1.Value = dateTimePicker1.Value.AddMonths(int.Parse(textBox5.Text) - 1);
                persianDateTimePicker2.Value = dateTimePicker1.Value;
            }
        }

        private void persianDateTimePicker1_ValueChanged(object sender, FreeControls.PersianMonthCalendarEventArgs e)
        {
                dateTimePicker1.Value = persianDateTimePicker1.Value;                
                dateTimePicker1.Value = dateTimePicker1.Value.AddMonths(int.Parse(textBox5.Text) - 1);
                persianDateTimePicker2.Value = dateTimePicker1.Value;
                            

        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar=='.' ))
                e.Handled = true;
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                sood = ((int.Parse(DebtCash1)) * (double.Parse(textBox2.Text)) / 100).ToString();
                PaymentCash1 = ((int.Parse(DebtCash1)) - (int.Parse(sood))).ToString();
                textBox7.Text = PaymentCash1;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                PaymentCash1 = DebtCash1;
                textBox7.Text = PaymentCash1;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.Text = DebtCash1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0")
            {
                MessageBox.Show("لطفا کد عضویت را انتخاب نمائید", "خطا", MessageBoxButtons.OK);
            }

            else if (textBox6.Text == "0")
            {
                MessageBox.Show("لطفا کد وام را انتخاب نمائید", "خطا", MessageBoxButtons.OK);
            }

            else if (textBox3.Text == "0")
            {
                MessageBox.Show("لطفا مبلغ وام را وارد نمائید", "خطا", MessageBoxButtons.OK);
            }

            else
            {
                DebtPayment DebtPaymenttable = familial_bankEntitiescontext.DebtPayments.First(i => i.Code == codee);

                if (MessageBox.Show("اطلاعات مورد نظر ثبت گردد؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DebtPaymenttable.MembersCode = int.Parse(textBox1.Text);
                    DebtPaymenttable.DebtKindCode = int.Parse(textBox6.Text);
                    DebtPaymenttable.PaymentDate = persianDateTimePicker4.Value.ToString("yyyy/MM/dd");
                    DebtPaymenttable.paymentTime = DateTime.Now.ToShortTimeString();
                    DebtPaymenttable.StockAvg = double.Parse(stockavg);
                    DebtPaymenttable.DebtCash = double.Parse(DebtCash1);
                    DebtPaymenttable.DebtPay = double.Parse(PaymentCash1);
                    DebtPaymenttable.DebtWage = double.Parse(textBox2.Text);
                    DebtPaymenttable.DebtWageKind = byte.Parse(comboBox1.SelectedIndex.ToString());
                    DebtPaymenttable.countt = byte.Parse(textBox5.Text);
                    DebtPaymenttable.PaymentCash = double.Parse(DebtPay1);
                    DebtPaymenttable.CashFirstDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
                    DebtPaymenttable.CashEndDate = persianDateTimePicker2.Value.ToString("yyyy/MM/dd");
                    DebtPaymenttable.LatenPenalty = double.Parse(textBox10.Text);
                    DebtPaymenttable.Comment = textBox9.Text;
                    DebtPaymenttable.ChequeNumber = int.Parse(textBox4.Text);
                    familial_bankEntitiescontext.SaveChanges();
                    MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                    DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 19, Environment.MachineName, code);
                    //----------------ویرایش فیش واریزی
                    int editcode2 = (int.Parse(fishnumber));
                    int vamcode = codee;
                    DebtCreditAcc DebtCreditAcctable = familial_bankEntitiescontext.DebtCreditAccs.First(i => i.Code == editcode2 && i.FishNumber == vamcode && i.Kind == 1);                    
                      DebtCreditAcctable.Bardasht_Cash = double.Parse(textBox7.Text);
                      DebtCreditAcctable.FishNumber = DebtPaymenttable.Code;
                      DebtCreditAcctable.Descriptions = "برداشت از حساب بابت وام شماره "  + DebtPaymenttable.Code.ToString()+" " +label11.Text;
                      familial_bankEntitiescontext.SaveChanges();
                      MessageBox.Show(" برداشت از حساب قرض الحسنه بابت وام انتخابی نیز ویرایش گردید.", "Information", MessageBoxButtons.OK);
                      DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 30, Environment.MachineName, editcode2);

                    this.Close();
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                returnstring = DLUtilsobj.temperory2obj.searchmembers(textBox1.Text);
                if (returnstring == "0")
                {
                    MessageBox.Show("کد وارد شده اشتباه می باشد " + "\n" + "یا کد عضو وارد شده غیر فعال می باشد", "خطا", MessageBoxButtons.OK);
                    insertmode = false;
                    label11.Text = "-------------";
                    label16.Text = "-------------";
                }

                else
                {
                    label11.Text = returnstring;
                    insertmode = true;
                    memberscode = textBox1.Text;

                    //-----------
                    label16.Text = DLUtilsobj.temperoryobj.balancemembers(memberscode);
                    stockavg = label16.Text;
                    label16.Text = string.Format("{0:#,##0}", double.Parse(label16.Text));
                    //------------
                    SendKeys.Send("{tab}");
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
                    label16.Text = "-------------";
                }

                else
                {
                    label11.Text = returnstring;
                    insertmode = true;
                    //-----------
                    label16.Text = DLUtilsobj.temperoryobj.balancemembers(memberscode);
                    stockavg = label16.Text;
                    label16.Text = string.Format("{0:#,##0}", double.Parse(label16.Text));
                    //------------
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == string.Empty)
                textBox2.Text = "0";
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text == string.Empty)
                textBox4.Text = "0";
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (textBox10.Text == string.Empty)
                textBox10.Text = "0";
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

        private void DebtPayment_F_Shown(object sender, EventArgs e)
        {
            showmode = true;
        }
                    

    }
}
