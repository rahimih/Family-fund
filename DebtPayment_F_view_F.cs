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
    public partial class DebtPayment_F_view_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public int usercode, code, usercodetemp;
        public string ipadress;

        public DebtPayment_F_view_F()
        {
            InitializeComponent();
        }

        private bool loaddata()
        {
            DLUtilsobj.temperoryobj.debtpaymentview();
            SqlDataReader DataSource;
            DLUtilsobj.temperoryobj.Dbconnset(true);
            DataSource = DLUtilsobj.temperoryobj.temperoryclientdataset.ExecuteReader();
            radGridView1.DataSource = DataSource;
            DLUtilsobj.temperoryobj.Dbconnset(false);

            if (radGridView1.RowCount > 0)
            {
                radGridView1.Columns[0].HeaderText = "کد ";
                radGridView1.Columns[1].HeaderText = "کد عضویت";
                radGridView1.Columns[2].HeaderText = "نام عضو";
                radGridView1.Columns[3].HeaderText = "میانگین موجودی";
                radGridView1.Columns[3].IsVisible = false;
                radGridView1.Columns[4].HeaderText = "کد وام";
                radGridView1.Columns[4].IsVisible = false;
                radGridView1.Columns[5].HeaderText = "نام وام";
                radGridView1.Columns[6].HeaderText = "تعداد اقساط";
                radGridView1.Columns[6].IsVisible = false;
                radGridView1.Columns[7].HeaderText = "مبلغ وام";
                radGridView1.Columns[8].HeaderText = "مبلغ پرداختی";
                radGridView1.Columns[9].HeaderText = "درصد کارمزد";
                radGridView1.Columns[10].IsVisible = false;               
                radGridView1.Columns[11].HeaderText = "تعداد اقساط";
                radGridView1.Columns[12].HeaderText = "مبلغ قسط";
                radGridView1.Columns[13].HeaderText = "تاریخ پرداخت اولین قسط";
                radGridView1.Columns[14].HeaderText = "تاریخ پرداخت آخرین قسط";
                radGridView1.Columns[15].HeaderText = "جریمه دیر کرد";
                radGridView1.Columns[16].HeaderText = "شماره چک";
                radGridView1.Columns[17].IsVisible = false;
                radGridView1.Columns[18].HeaderText = "تاریخ تسویه";                
                radGridView1.Columns[19].IsVisible = false;
                radGridView1.Columns[20].HeaderText = "وضعیت";
                radGridView1.Columns[21].IsVisible = false;

                radGridView1.Columns[3].FormatString = "{0:#,##0}";
                radGridView1.Columns[7].FormatString = "{0:#,##0}";
                radGridView1.Columns[8].FormatString = "{0:#,##0}";
                radGridView1.Columns[12].FormatString = "{0:#,##0}";

                radGridView1.Columns[0].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[1].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[2].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[5].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[7].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[8].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[9].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[11].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[12].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[13].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[14].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[15].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[16].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[18].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

               
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DebtKind_view_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            //*************
            loaddata();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //-------------پرداخت وام
            DebtPayment_F DebtPayment_Frm = new DebtPayment_F();
            DebtPayment_Frm.usercode = usercodetemp;
            DebtPayment_Frm.ShowDialog();
        }

  
     
        private void button6_Click(object sender, EventArgs e)
        {
          if (radGridView1.RowCount > 0)
            {
                int editcode = int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());
                if (DLUtilsobj.temperory2obj.debtmonthpaymentstatus(editcode) == true)
                {
                    MessageBox.Show("شما مجاز به ویرایش وام انتخابی نمی باشید", "خطا", MessageBoxButtons.OK);
                }

                else
                {
                    if (radGridView1.RowCount > 0)
                    {
                        DebtPayment_F DebtPayment_Frm = new DebtPayment_F();

                        if (radGridView1.CurrentRow.Cells[10].Value.ToString() == "0")
                            DebtPayment_Frm.comboBox1.SelectedIndex = 0;
                        else
                            DebtPayment_Frm.comboBox1.SelectedIndex = 1;

                        DebtPayment_Frm.codee = int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());
                        DebtPayment_Frm.textBox1.Text = radGridView1.CurrentRow.Cells[1].Value.ToString();
                        DebtPayment_Frm.label11.Text = radGridView1.CurrentRow.Cells[2].Value.ToString();
                        DebtPayment_Frm.label16.Text = radGridView1.CurrentRow.Cells[3].Value.ToString();
                        DebtPayment_Frm.stockavg = radGridView1.CurrentRow.Cells[3].Value.ToString();
                        DebtPayment_Frm.textBox6.Text = radGridView1.CurrentRow.Cells[4].Value.ToString();
                        DebtPayment_Frm.label10.Text = radGridView1.CurrentRow.Cells[5].Value.ToString();
                        DebtPayment_Frm.label12.Text = radGridView1.CurrentRow.Cells[6].Value.ToString();
                        DebtPayment_Frm.textBox3.Text = radGridView1.CurrentRow.Cells[7].Value.ToString();
                        DebtPayment_Frm.DebtCash1 = DebtPayment_Frm.textBox3.Text;
                        DebtPayment_Frm.textBox2.Text = radGridView1.CurrentRow.Cells[9].Value.ToString();                                             

                        DebtPayment_Frm.textBox7.Text = radGridView1.CurrentRow.Cells[8].Value.ToString();
                        DebtPayment_Frm.PaymentCash1 = DebtPayment_Frm.textBox7.Text;
                        DebtPayment_Frm.textBox5.Text = radGridView1.CurrentRow.Cells[11].Value.ToString();
                        DebtPayment_Frm.textBox8.Text = radGridView1.CurrentRow.Cells[12].Value.ToString();
                        DebtPayment_Frm.DebtPay1 = DebtPayment_Frm.textBox8.Text;
                        DebtPayment_Frm.persianDateTimePicker1.Value = DLUtilsobj.temperoryobj.shamsitomiladi(radGridView1.CurrentRow.Cells[13].Value.ToString());
                        if (radGridView1.CurrentRow.Cells[14].Value.ToString() != "")
                            DebtPayment_Frm.persianDateTimePicker2.Value = DLUtilsobj.temperoryobj.shamsitomiladi(radGridView1.CurrentRow.Cells[14].Value.ToString());

                        DebtPayment_Frm.persianDateTimePicker4.Value = DLUtilsobj.temperoryobj.shamsitomiladi(radGridView1.CurrentRow.Cells[21].Value.ToString());
                        DebtPayment_Frm.textBox10.Text = radGridView1.CurrentRow.Cells[15].Value.ToString();
                        DebtPayment_Frm.textBox4.Text = radGridView1.CurrentRow.Cells[16].Value.ToString();
                        DebtPayment_Frm.fishnumber = radGridView1.CurrentRow.Cells[16].Value.ToString();
                        DebtPayment_Frm.textBox9.Text = radGridView1.CurrentRow.Cells[17].Value.ToString();

                        //---------------غیر فعال کردن شماره چک
                       /* int editcode2 = (int.Parse(radGridView1.CurrentRow.Cells[16].Value.ToString()));
                        int vamcode = (int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString()));
                        if (DLUtilsobj.temperory2obj.findbankcreditacc(editcode2.ToString(), vamcode.ToString()) == true)
                        DebtPayment_Frm.textBox4.Enabled=false;*/
                        //*****************
                        DebtPayment_Frm.editmode = true;
                        DebtPayment_Frm.button6.Enabled = true;
                        DebtPayment_Frm.button3.Enabled = false;                        
                        DebtPayment_Frm.ShowDialog();
                       // loaddata();
                    }
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (radGridView1.RowCount > 0)
            {
                int editcode = int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());
                if (DLUtilsobj.temperory2obj.debtmonthpaymentstatus(editcode) == true)
                {                   
                    MessageBox.Show("شما مجاز به حذف وام انتخابی نمی باشید", "خطا", MessageBoxButtons.OK);             
                }
                else
                {
                    DebtPayment DebtPaymenttable = familial_bankEntitiescontext.DebtPayments.First(i => i.Code == editcode);
                    if (MessageBox.Show("آیا مطمئن به حذف وام انتخابی می باشید؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DebtPaymenttable.Status = 3;
                        familial_bankEntitiescontext.SaveChanges();
                        MessageBox.Show("وام انتخابی حذف گردید", "Information", MessageBoxButtons.OK);
                        DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 24, Environment.MachineName, editcode);
                        //-----------------حذف فیش بانکی
                        if (radGridView1.CurrentRow.Cells[16].Value.ToString() != "0")
                        {
                            int editcode2 = (int.Parse(radGridView1.CurrentRow.Cells[16].Value.ToString()));
                            int vamcode = (int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString()));
                            DebtCreditAcc DebtCreditAcctable = familial_bankEntitiescontext.DebtCreditAccs.First(i => i.Code == editcode2 && i.FishNumber == vamcode && i.Kind == 1);
                            DebtCreditAcctable.Deleted = true;
                            familial_bankEntitiescontext.SaveChanges();
                            MessageBox.Show(" برداشت از حساب قرض الحسنه بابت وام انتخابی نیز حذف گردید.", "Information", MessageBoxButtons.OK);
                            DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 33, Environment.MachineName, editcode);
                        }

                        loaddata();
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
              if (radGridView1.CurrentRow.Cells[19].Value.ToString()=="2")
            {
                MessageBox.Show(" وام انتخابی تسویه گردیده است ", "خطا", MessageBoxButtons.OK);
            }
            else

              {
                  PayoffDebtpayment_F PayoffDebtpayment_Frm = new PayoffDebtpayment_F();
                  PayoffDebtpayment_Frm.usercode = usercode;
                  PayoffDebtpayment_Frm.label11.Text = radGridView1.CurrentRow.Cells[1].Value.ToString();
                  PayoffDebtpayment_Frm.code = int.Parse(radGridView1.CurrentRow.Cells[1].Value.ToString());
                  PayoffDebtpayment_Frm.label2.Text = radGridView1.CurrentRow.Cells[2].Value.ToString();
                  PayoffDebtpayment_Frm.debtcode = radGridView1.CurrentRow.Cells[0].Value.ToString();
                  //----------------مانده وام
                  PayoffDebtpayment_Frm.label3.Text = DLUtilsobj.temperory2obj.payoffdebtone(PayoffDebtpayment_Frm.label11.Text, PayoffDebtpayment_Frm.debtcode);
                  PayoffDebtpayment_Frm.payoffdebt = PayoffDebtpayment_Frm.label3.Text;
                  if (PayoffDebtpayment_Frm.label3.Text != "0")
                      PayoffDebtpayment_Frm.label3.Text = string.Format("{0:#,##0}", double.Parse(PayoffDebtpayment_Frm.label3.Text));
                  // convert to word
                  PayoffDebtpayment_Frm.label10.Text = ToWords.ToString(long.Parse(PayoffDebtpayment_Frm.payoffdebt));

                  PayoffDebtpayment_Frm.label7.Text = PayoffDebtpayment_Frm.label3.Text;
                  PayoffDebtpayment_Frm.textBox1.Text = PayoffDebtpayment_Frm.label7.Text;
                  if ((double.Parse(PayoffDebtpayment_Frm.label7.Text)) > 0)
                      PayoffDebtpayment_Frm.label6.Text = "بستانکار";
                  else
                      PayoffDebtpayment_Frm.label6.Text = "بدهکار";

                  PayoffDebtpayment_Frm.kindd = 2;
                  PayoffDebtpayment_Frm.ShowDialog();
                  //-----------
                  loaddata();


              }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            //DLUtilsobj.temperory2obj.DebtInsertprint(radGridView1.CurrentRow.Cells[7].Value.ToString(), radGridView1.CurrentRow.Cells[12].Value.ToString(), radGridView1.CurrentRow.Cells[11].Value.ToString(), radGridView1.CurrentRow.Cells[0].Value.ToString());

            familial_bankEntitiescontext.DebtInsertprint(int.Parse(radGridView1.CurrentRow.Cells[7].Value.ToString()),int.Parse(radGridView1.CurrentRow.Cells[12].Value.ToString()), int.Parse(radGridView1.CurrentRow.Cells[11].Value.ToString()),int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString()));
            
            Report_debt_F Report_debt_Frm = new Report_debt_F();
            Report_debt_Frm.ipadress = ipadress;
            Report_debt_Frm.debtcode = radGridView1.CurrentRow.Cells[0].Value.ToString();


            Report_debt_Frm.ShowDialog();
           
        }

    
    }
          
}
