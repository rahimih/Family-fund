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
    public partial class DebtMonthPayment_view_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public int usercode, code;
        public string countt, summ;
        public string memberscode, returnstring;

        public DebtMonthPayment_view_F()
        {
            InitializeComponent();
        }

     /*   private bool loaddata()
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
      */
 
            private bool loaddata(string memberscode)
        {
            DLUtilsobj.temperoryobj.report_DebtPayment(memberscode);
            SqlDataReader DataSource;
            DLUtilsobj.temperoryobj.Dbconnset(true);
            DataSource = DLUtilsobj.temperoryobj.temperoryclientdataset.ExecuteReader();
            radGridView1.DataSource = DataSource;
            DLUtilsobj.temperoryobj.Dbconnset(false);

            if (radGridView1.RowCount > 0)
            {
                radGridView1.Columns[0].HeaderText = "کد ";
                radGridView1.Columns[1].HeaderText = "تاریخ";
                radGridView1.Columns[2].HeaderText = "مبلغ وام";
                radGridView1.Columns[3].HeaderText = "تعداد اقساط";
                radGridView1.Columns[4].HeaderText = "مبلغ قسط";
                radGridView1.Columns[5].HeaderText = "تاریخ شروع اقساط";
                radGridView1.Columns[6].HeaderText = "تاریخ پایان اقساط";
                radGridView1.Columns[7].HeaderText = "تاریخ تسویه ";
                radGridView1.Columns[8].HeaderText = "توضیحات";
                radGridView1.Columns[9].HeaderText = "وضعیت";
                radGridView1.Columns[10].IsVisible = false;

                radGridView1.Columns[2].FormatString = "{0:#,##0}";
                radGridView1.Columns[4].FormatString = "{0:#,##0}";


                radGridView1.Columns[0].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[1].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[2].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[3].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[4].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[5].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[6].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[7].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[8].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[9].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;


            }
            return true;        
        }

        public bool loadcountandsumpayment(int membercose, int debtcode)
        {
            DLUtilsobj.temperory2obj.debtpaymentcountsumcash(membercose, debtcode);
            SqlDataReader DataSource;
            DLUtilsobj.temperory2obj.Dbconnset(true);
            DataSource = DLUtilsobj.temperory2obj.temperory2clientdataset.ExecuteReader();
            DataSource.Read();
            countt = DataSource["Expr1"].ToString();
            countt = (int.Parse(countt) - 1).ToString();
            summ = DataSource["Expr2"].ToString();
            if (countt == "")
                countt = "0";
            if (summ == "")
                summ = "0";
            DataSource.Close();
            DLUtilsobj.temperory2obj.Dbconnset(false);
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DebtMonthPayment_F DebtMonthPayment_Frm = new DebtMonthPayment_F();
            DebtMonthPayment_Frm.usercode = usercode;
            DebtMonthPayment_Frm.ShowDialog();
            //loaddata();
            loaddata(textBox1.Text);
        }

        private void PaymentMembers_View_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            //*************
            //loaddata();
            loaddata(textBox1.Text);

        }

        private void radGridView1_SelectionChanging(object sender, Telerik.WinControls.UI.GridViewSelectionCancelEventArgs e)
        {
            if (radGridView1.RowCount > 0)
            {

            DLUtilsobj.temperory2obj.debtMonthPaymentviews(int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString()));
            SqlDataReader DataSource2;
            DLUtilsobj.temperory2obj.Dbconnset(true);
            DataSource2 = DLUtilsobj.temperory2obj.temperory2clientdataset.ExecuteReader();
            radGridView2.DataSource = DataSource2;
            DLUtilsobj.temperory2obj.Dbconnset(false);

            if (radGridView2.RowCount > 0)
            {
                radGridView2.Columns[0].HeaderText = "کد";
                radGridView2.Columns[1].HeaderText = "تاریخ پرداخت";
                radGridView2.Columns[2].HeaderText = " شماره قسط";
                radGridView2.Columns[3].HeaderText = "مبلغ قسط";
                radGridView2.Columns[4].HeaderText = "مبلغ جریمه";
                radGridView2.Columns[5].HeaderText = "مبلغ کل";
                radGridView2.Columns[6].HeaderText = "مبلغ پرداختی";
                radGridView2.Columns[7].HeaderText = "توضیحات";
                radGridView2.Columns[8].HeaderText = "شماره فیش";

                radGridView2.Columns[3].FormatString = "{0:#,##0}";
                radGridView2.Columns[5].FormatString = "{0:#,##0}";
                radGridView2.Columns[6].FormatString = "{0:#,##0}";
                               

                radGridView2.Columns[0].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView2.Columns[1].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView2.Columns[2].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView2.Columns[3].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView2.Columns[4].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView2.Columns[5].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView2.Columns[6].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView2.Columns[7].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView2.Columns[8].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                

            }            
            
            }
            }

        private void button6_Click(object sender, EventArgs e)
        {
            if (radGridView2.RowCount > 0)
            {
                //-----------
                if (radGridView1.CurrentRow.Cells[10].Value.ToString() == "2")
                {
                    MessageBox.Show("شما مجاز به ویرایش قسط پرداخت شده انتخابی نمی باشید", "خطا", MessageBoxButtons.OK);
                }

                else
                {
                    DebtMonthPayment_F DebtMonthPayment_Frm = new DebtMonthPayment_F();
                    DebtMonthPayment_Frm.textBox4.Text = radGridView2.CurrentRow.Cells[0].Value.ToString();
                    DebtMonthPayment_Frm.textBox1.Text = textBox1.Text;//radGridView1.CurrentRow.Cells[1].Value.ToString();
                    DebtMonthPayment_Frm.textBox6.Text = radGridView1.CurrentRow.Cells[0].Value.ToString();
                    DebtMonthPayment_Frm.persianDateTimePicker1.Value = DLUtilsobj.temperoryobj.shamsitomiladi(radGridView2.CurrentRow.Cells[1].Value.ToString());
                    DebtMonthPayment_Frm.textBox3.Text = radGridView2.CurrentRow.Cells[2].Value.ToString();
                    DebtMonthPayment_Frm.textBox2.Text = radGridView2.CurrentRow.Cells[3].Value.ToString();
                    DebtMonthPayment_Frm.textBox7.Text = radGridView2.CurrentRow.Cells[4].Value.ToString();
                    DebtMonthPayment_Frm.textBox5.Text = radGridView2.CurrentRow.Cells[5].Value.ToString();
                    DebtMonthPayment_Frm.textBox8.Text = radGridView2.CurrentRow.Cells[6].Value.ToString();
                    DebtMonthPayment_Frm.textBox9.Text = radGridView2.CurrentRow.Cells[7].Value.ToString();
                    DebtMonthPayment_Frm.textBox10.Text = radGridView2.CurrentRow.Cells[8].Value.ToString();

                    DebtMonthPayment_Frm.label11.Text = label11.Text;//radGridView1.CurrentRow.Cells[2].Value.ToString();
                    //DebtMonthPayment_Frm.label10.Text = radGridView1.CurrentRow.Cells[5].Value.ToString();
                    DebtMonthPayment_Frm.label12.Text = radGridView1.CurrentRow.Cells[3].Value.ToString();

                    //----------------

                    loadcountandsumpayment(int.Parse(DebtMonthPayment_Frm.textBox1.Text), int.Parse(DebtMonthPayment_Frm.textBox6.Text));
                    DebtMonthPayment_Frm.label17.Text = countt.ToString();
                    DebtMonthPayment_Frm.label20.Text = (double.Parse(summ) - double.Parse(DebtMonthPayment_Frm.textBox8.Text)).ToString();
                    DebtMonthPayment_Frm.label20.Text = string.Format("{0:#,##0}", double.Parse(DebtMonthPayment_Frm.label20.Text));
                    DebtMonthPayment_Frm.vamcash = radGridView1.CurrentRow.Cells[2].Value.ToString();
                    DebtMonthPayment_Frm.label16.Text = (double.Parse(DebtMonthPayment_Frm.vamcash) - double.Parse(DebtMonthPayment_Frm.label20.Text)).ToString();
                    DebtMonthPayment_Frm.label16.Text = string.Format("{0:#,##0}", double.Parse(DebtMonthPayment_Frm.label16.Text));

                    DebtMonthPayment_Frm.editcode = Int32.Parse(radGridView2.CurrentRow.Cells[0].Value.ToString());
                    DebtMonthPayment_Frm.editmode = true;
                    DebtMonthPayment_Frm.insertmode = true;
                    DebtMonthPayment_Frm.button3.Enabled = false;
                    DebtMonthPayment_Frm.button6.Enabled = true;
                    DebtMonthPayment_Frm.ShowDialog();
                    //loaddata();
                    loaddata(textBox1.Text);
                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radGridView2.RowCount > 0)
            {
                if (radGridView1.CurrentRow.Cells[10].Value.ToString() == "2")
                {
                    //MessageBox.Show("شما مجاز به حذف قسط پرداخت شده انتخابی نمی باشید", "خطا", MessageBoxButtons.OK);
                    if (MessageBox.Show(" وام انتخابی تسویه گردیده است " + "\n" + "در صورت حذف قسط انتخابی وام فوق از وضعیت تسویه خارج می گردد." + "\n" + "آیا مطمئن به حذف قسط انتخابی می باشید؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int editcode = int.Parse(radGridView2.CurrentRow.Cells[0].Value.ToString());
                        DebtMonthlyPayment DebtMonthlyPaymenttable = familial_bankEntitiescontext.DebtMonthlyPayments.First(i => i.Code == editcode);
                        DebtMonthlyPaymenttable.Deleted = true;
                        familial_bankEntitiescontext.SaveChanges();                       
                        DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 27, Environment.MachineName, editcode);                        //------------ عدم تسویه 
                        //-----------------  عدم تسویه
                         editcode = int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());
                        DebtPayment DebtPaymenttable = familial_bankEntitiescontext.DebtPayments.First(i => i.Code == editcode);
                        DebtPaymenttable.CashEndDate = "";
                        DebtPaymenttable.PayOffDate = "";
                        DebtPaymenttable.PayOffTime = "";
                        DebtPaymenttable.Status = 1;
                        familial_bankEntitiescontext.SaveChanges();
                        DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 19, Environment.MachineName, code);
                        MessageBox.Show("رکورد انتخابی حذف گردید", "Information", MessageBoxButtons.OK);
                        //loaddata();
                        loaddata(textBox1.Text);
                    }
                }

                else
                {
                    int editcode = int.Parse(radGridView2.CurrentRow.Cells[0].Value.ToString());
                    DebtMonthlyPayment DebtMonthlyPaymenttable = familial_bankEntitiescontext.DebtMonthlyPayments.First(i => i.Code == editcode);
                    if (MessageBox.Show("رکورد انتخابی حذف گردد؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DebtMonthlyPaymenttable.Deleted = true;
                        familial_bankEntitiescontext.SaveChanges();
                        MessageBox.Show("رکورد انتخابی حذف گردید", "Information", MessageBoxButtons.OK);
                        DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 27, Environment.MachineName, editcode);
                        //loaddata();
                        loaddata(textBox1.Text);
                    }
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                returnstring = DLUtilsobj.temperory2obj.searchmembers_total(textBox1.Text);
                if (returnstring == "0")
                {
                    MessageBox.Show("کد وارد شده اشتباه می باشد ", "خطا", MessageBoxButtons.OK);
                    //insertmode = false;
                    label11.Text = "-------------";
                }

                else
                {
                    label11.Text = returnstring;
                    //insertmode = true;
                    memberscode = textBox1.Text;
                    loaddata(memberscode);

                }

            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (memberscode != textBox1.Text)
            {
                returnstring = DLUtilsobj.temperory2obj.searchmembers_total(textBox1.Text);
                if (returnstring == "0")
                {
                    MessageBox.Show("کد وارد شده اشتباه می باشد ", "خطا", MessageBoxButtons.OK);
                    //insertmode = false;
                    label11.Text = "-------------";
                }

                else
                {
                    label11.Text = returnstring;
                    //insertmode = true;
                    //--------------
                    loaddata(textBox1.Text);

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MembersView_Select_F MembersView_Select_Frm = new MembersView_Select_F();
            MembersView_Select_Frm.statuse = "2";
            MembersView_Select_Frm.ShowDialog();
            memberscode = MembersView_Select_Frm.returncode;
            label11.Text = MembersView_Select_Frm.returnname;
            textBox1.Text = memberscode;
            //insertmode = true;
            //---------------
            loaddata(textBox1.Text);
        }

        }
    }

