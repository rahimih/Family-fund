using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DLibraryUtils;
using System.Xml;

namespace familial_bank
{
    public partial class Main_f : Form
    {

        public DLibraryUtils.DLUtils DLUtilsobj;
     
        public int usercodetemp;
        public int accessleveltemp;
        public string ipadress;
        public DateTime sdate;
        public string sdate_shamsi;
        int year, month;
        public string str1;

      
        string month_namelabel;

        public Main_f()
        {
            InitializeComponent();
        }
    

        private string month_name(int month)
        {
            if (month == 1)
                month_namelabel = "فروردین";

            if (month == 2)
                month_namelabel = "اردیبهشت";

            if (month == 3)
                month_namelabel = "خرداد";

            if (month == 4)
                month_namelabel = "تیر";

            if (month == 5)
                month_namelabel = "مرداد";

            if (month == 6)
                month_namelabel = "شهریور";

            if (month == 7)
                month_namelabel = "مهر";

            if (month == 8)
                month_namelabel = "آبان";

            if (month == 9)
                month_namelabel = "آذر";

            if (month == 10)
                month_namelabel = "دی";

            if (month == 11)
                month_namelabel = "بهمن";

            if (month == 12)
                month_namelabel = "اسفند";

            return month_namelabel;


        }
     

        private void Main_f_FormClosed(object sender, FormClosedEventArgs e)
        {
            DLUtilsobj.EventsLogobj.insertEventsLog(usercodetemp.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 3, Environment.MachineName,0);
            Application.Exit();
        }

     

        private void Main_f_Load(object sender, EventArgs e)
        {

            DLUtilsobj = new DLibraryUtils.DLUtils();
            this.Size = new Size(SystemInformation.PrimaryMonitorSize.Width, SystemInformation.PrimaryMonitorSize.Height);
            //------------------

            XmlTextReader XmlRdr = new XmlTextReader("familialbank.xml");

            while (!XmlRdr.EOF)
            {

                if (XmlRdr.MoveToContent() == XmlNodeType.Element)

                    switch (XmlRdr.Name)
                    {


                        case "ipadress":

                            str1 = XmlRdr.ReadElementString();

                            break;

                        default:

                            XmlRdr.Read();

                            break;

                    }

                else

                    XmlRdr.Read();

            }

            XmlRdr.Close();
            ipadress = str1;


            //-------------------------

  /* if (accessleveltemp==2)
     {
       
      }*/
  
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //----------------مشاهده اعضا
            Members_view_F Members_view_Frm = new Members_view_F();
            Members_view_Frm.usercode = usercodetemp;
            Members_view_Frm.ipadress = ipadress;
            Members_view_Frm.ShowDialog();
        }

        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DebtMonthPayment_F DebtMonthPayment_Frm = new DebtMonthPayment_F();
            DebtMonthPayment_Frm.usercode = usercodetemp;
            DebtMonthPayment_Frm.ShowDialog();
        }

        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //-------------پرداخت وام
            DebtPayment_F DebtPayment_Frm = new DebtPayment_F();
            DebtPayment_Frm.usercode = usercodetemp;
            DebtPayment_Frm.ipadress = ipadress;
            DebtPayment_Frm.ShowDialog();
        }

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Members_view_F Members_view_Frm = new Members_view_F();
            Members_view_Frm.usercode = usercodetemp;
            Members_view_Frm.ipadress = ipadress;
            Members_view_Frm.ShowDialog();
        }

        private void navBarItem9_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //-----------تعریف مبلغ پرداختی ماهیانه
            MonthPayment_F MonthPayment_Frm = new MonthPayment_F();
            MonthPayment_Frm.usercode = usercodetemp;
            MonthPayment_Frm.ShowDialog();
        }

        private void navBarItem11_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            PaymentMembers_F PaymentMembers_Frm = new PaymentMembers_F();
            PaymentMembers_Frm.usercode = usercodetemp;
            PaymentMembers_Frm.kind = 1;
           // PaymentMembers_Frm.maskedTextBox1.Text = DateTime.Now.ToShortTimeString();
            PaymentMembers_Frm.Text = "دریافت حق عضویت";
            //----------------            
            PaymentMembers_Frm.textBox2.Text = DLUtilsobj.temperory2obj.MemberShipRightcash(PaymentMembers_Frm.persianDateTimePicker1.Value.Year);            
            PaymentMembers_Frm.ShowDialog();
        }

        private void navBarItem31_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //-----------ویرایش مبلغ پرداختی ماهیانه
            MonthPayment_View_F MonthPayment_View_Frm = new MonthPayment_View_F();
            MonthPayment_View_Frm.usercode = usercodetemp;
            MonthPayment_View_Frm.ShowDialog();
        }

        private void navBarItem10_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //-------------انواع وام
            DebtKind_F DebtKind_Frm = new DebtKind_F();
            DebtKind_Frm.usercode = usercodetemp;
            DebtKind_Frm.ShowDialog();
        }

        private void navBarItem12_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            PaymentMembers_F PaymentMembers_Frm = new PaymentMembers_F();
            PaymentMembers_Frm.usercode = usercodetemp;
            PaymentMembers_Frm.kind = 2;
            //PaymentMembers_Frm.maskedTextBox1.Text = DateTime.Now.ToShortTimeString();
            //----------------            
            PaymentMembers_Frm.textBox2.Text = DLUtilsobj.temperory2obj.PaymentMemberscash(PaymentMembers_Frm.persianDateTimePicker1.Value.Year);            
            PaymentMembers_Frm.ShowDialog();
        }

        private void navBarItem13_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            PaymentMembersgroup_F PaymentMembersgroup_Frm = new PaymentMembersgroup_F();
            PaymentMembersgroup_Frm.usercode = usercodetemp;
            PaymentMembersgroup_Frm.ShowDialog();
        }

        private void navBarItem19_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //----------- مشاهده مبلغ کل حق عضویت اعضا

        }

        private void navBarItem14_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Report_monthlypayment_F Report_monthlypayment_Frm = new Report_monthlypayment_F();
            Report_monthlypayment_Frm.ShowDialog();
        }

        private void navBarItem15_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Report_Debtpayment_F Report_Debtpayment_Frm = new Report_Debtpayment_F();
            Report_Debtpayment_Frm.ShowDialog();
        }

        private void navBarItem16_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //------------مشاهده اقساط پرداختی اعضا
            Report_DebtPayment_status_F Report_DebtPayment_status_Frm = new Report_DebtPayment_status_F();
            Report_DebtPayment_status_Frm.ShowDialog();
        }

        private void navBarItem17_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //----------  مشاهده کل مبلغ واریزی هر فرد
            Report_PaymentMembersSum_F Report_PaymentMembersSum_Frm = new Report_PaymentMembersSum_F();
            Report_PaymentMembersSum_Frm.ShowDialog();
        }

        private void navBarItem20_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //---------------- تعریف حساب
            BankAcc_F BankAcc_Frm = new BankAcc_F();
            BankAcc_Frm.usercode = usercodetemp;
            BankAcc_Frm.ShowDialog();
        }

        private void navBarItem18_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //مشاهده مبلغ کل ماهیانه واریزی اعضا
            Report_PaymentMonthly_F Report_PaymentMonthly_Frm = new Report_PaymentMonthly_F();
            Report_PaymentMonthly_Frm.ShowDialog();

        }

        private void navBarItem27_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //-----------چاپ دفترچه ماهیانه
            Members_view_F Members_view_Frm = new Members_view_F();
            Members_view_Frm.ipadress = ipadress;
            Members_view_Frm.ShowDialog();
        }

        private void navBarItem29_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }

        private void navBarItem21_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DebtCreditAcc_F DebtCreditAcc_Frm = new DebtCreditAcc_F();
            DebtCreditAcc_Frm.usercode = usercodetemp;
            DebtCreditAcc_Frm.kind = 1;
            DebtCreditAcc_Frm.kindtemp = 1;
            DebtCreditAcc_Frm.Text = "واریز به حساب";
            DebtCreditAcc_Frm.ShowDialog();
        }

        private void navBarItem22_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DebtCreditAcc_F DebtCreditAcc_Frm = new DebtCreditAcc_F();
            DebtCreditAcc_Frm.usercode = usercodetemp;
            DebtCreditAcc_Frm.kind = 2;
            DebtCreditAcc_Frm.kindtemp = 2;
            DebtCreditAcc_Frm.Text = "برداشت از حساب";
            DebtCreditAcc_Frm.ShowDialog();
        }

        private void navBarItem23_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SignaturesAcc_F SignaturesAcc_Frm = new SignaturesAcc_F();
            SignaturesAcc_Frm.usercode = usercodetemp;
            SignaturesAcc_Frm.ShowDialog();
            
        }

        private void navBarItem24_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            RequestCheck_F RequestCheck_Frm = new RequestCheck_F();
            RequestCheck_Frm.usercode = usercodetemp;
            RequestCheck_Frm.ShowDialog();
        }

        private void navBarItem28_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Report_box_F Report_box_Frm = new Report_box_F();
            Report_box_Frm.ShowDialog();
        }

        private void navBarItem30_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //-------------------- ویرایش مبلغ حق عضویت سالیانه
            MemberShipRight_View_F MemberShipRight_view_Frm = new MemberShipRight_View_F();
            MemberShipRight_view_Frm.usercode = -usercodetemp;
            MemberShipRight_view_Frm.ShowDialog();
        }

        private void navBarItem33_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //-------------مشاهده وام
            DebtKind_view_F DebtKind_view_Frm = new DebtKind_view_F();
            DebtKind_view_Frm.usercode = usercodetemp;
            DebtKind_view_Frm.ShowDialog();
        }

        private void navBarItem32_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //---------------- دریافت اقساط ماهیانه افراد
            DebtPaymentsgroup_F DebtPaymentsgroup_Frm = new DebtPaymentsgroup_F();
            DebtPaymentsgroup_Frm.usercode = usercodetemp;
            DebtPaymentsgroup_Frm.ShowDialog();
        }

        private void navBarItem39_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            RequestCheck_View_F RequestCheck_View_Frm = new RequestCheck_View_F();
            RequestCheck_View_Frm.usercode = usercodetemp;
            RequestCheck_View_Frm.ShowDialog();

        }

        private void navBarItem40_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            PaymentMembers_View_F PaymentMembers_View_Frm = new PaymentMembers_View_F();
            PaymentMembers_View_Frm.usercode = usercodetemp;
            PaymentMembers_View_Frm.ShowDialog();
        }

        private void navBarItem41_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            PaymentMembersRight_View_F PaymentMembersRight_View_Frm = new PaymentMembersRight_View_F();
            PaymentMembersRight_View_Frm.usercode = usercodetemp;
            PaymentMembersRight_View_Frm.ShowDialog();
        }

        private void navBarItem42_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            PaymentMembersRight_View_F PaymentMembersRight_View_Frm = new PaymentMembersRight_View_F();
            PaymentMembersRight_View_Frm.usercode = usercodetemp;
            PaymentMembersRight_View_Frm.ShowDialog();
        }

        private void navBarItem43_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            PaymentMembers_View_F PaymentMembers_View_Frm = new PaymentMembers_View_F();
            PaymentMembers_View_Frm.usercode = usercodetemp;
            PaymentMembers_View_Frm.ShowDialog();
        }

    

        private void navBarItem44_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //-------------------- حذف مبلغ حق عضویت سالیانه
            MemberShipRight_View_F MemberShipRight_view_Frm = new MemberShipRight_View_F();
            MemberShipRight_view_Frm.usercode = -usercodetemp;
            MemberShipRight_view_Frm.ShowDialog();
        }

     

        private void navBarItem45_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //-----------حذف مبلغ پرداختی ماهیانه
            MonthPayment_View_F MonthPayment_View_Frm = new MonthPayment_View_F();
            MonthPayment_View_Frm.usercode = usercodetemp;
            MonthPayment_View_Frm.ShowDialog();
        }

        private void navBarItem46_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //-------------غیر فعال کردن وام
            DebtKind_view_F DebtKind_view_Frm = new DebtKind_view_F();
            DebtKind_view_Frm.usercode = usercodetemp;
            DebtKind_view_Frm.ShowDialog();
        }

     
        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //----------------ثبت اعضا
            Members_F Members_Frm = new Members_F();
            Members_Frm.usercode = usercodetemp;
            Members_Frm.ShowDialog();
        }

        private void navBarItem38_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //----------------مشاهده حساب
            BankAcc_view_F BankAcc_view_Frm = new BankAcc_view_F();
            BankAcc_view_Frm.usercode = usercodetemp;
            BankAcc_view_Frm.ShowDialog();
        }

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //----------------مشاهده اعضا
            Members_view_F Members_view_Frm = new Members_view_F();
            Members_view_Frm.usercode = usercodetemp;
            Members_view_Frm.ipadress = ipadress;
            Members_view_Frm.ShowDialog();
        }

        private void navBarItem8_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //-------------------- تعریف مبلغ حق عضویت سالیانه
            MemberShipRight_F MemberShipRight_Frm = new MemberShipRight_F();
            MemberShipRight_Frm.usercode = usercodetemp;
            MemberShipRight_Frm.ShowDialog();
        }

        private void navBarItem34_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DebtPayment_F_view_F DebtPayment_F_view_Frm = new DebtPayment_F_view_F();
            DebtPayment_F_view_Frm.usercode = usercodetemp;
            DebtPayment_F_view_Frm.ipadress = ipadress;
            DebtPayment_F_view_Frm.ShowDialog();
        }

        private void navBarItem35_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DebtPayment_F_view_F DebtPayment_F_view_Frm = new DebtPayment_F_view_F();
            DebtPayment_F_view_Frm.usercode = usercodetemp;
            DebtPayment_F_view_Frm.ShowDialog();
        }

        private void navBarItem47_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DebtMonthPayment_view_F DebtMonthPayment_view_Frm = new DebtMonthPayment_view_F();
            DebtMonthPayment_view_Frm.usercode = usercodetemp;
            DebtMonthPayment_view_Frm.ShowDialog();
        }

        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DebtPayment_F_view_F DebtPayment_F_view_Frm = new DebtPayment_F_view_F();
            DebtPayment_F_view_Frm.usercode = usercodetemp;
            DebtPayment_F_view_Frm.ShowDialog();
        }

        private void navBarItem49_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DebtCreditAcc_view_F DebtCreditAcc_view_Frm = new DebtCreditAcc_view_F();
            DebtCreditAcc_view_Frm.usercode = usercodetemp;
            DebtCreditAcc_view_Frm.kindview = 2;           
            DebtCreditAcc_view_Frm.ShowDialog();
        }

        private void navBarItem50_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DebtCreditAcc_view_F DebtCreditAcc_view_Frm = new DebtCreditAcc_view_F();
            DebtCreditAcc_view_Frm.usercode = usercodetemp;
            DebtCreditAcc_view_Frm.kindview = 3;            
            DebtCreditAcc_view_Frm.ShowDialog();
        }

        private void navBarItem36_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Balance_F Balance_Frm = new Balance_F();
            Balance_Frm.ShowDialog();
        }

        private void navBarItem51_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Report_box_F Report_box_Frm = new Report_box_F();
            Report_box_Frm.ShowDialog();
        }

        private void navBarItem26_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //---------چاپ دفترچه اقساط
            DebtPayment_F_view_F DebtPayment_F_view_Frm = new DebtPayment_F_view_F();
            DebtPayment_F_view_Frm.usercode = usercodetemp;
            DebtPayment_F_view_Frm.ipadress = ipadress;
            DebtPayment_F_view_Frm.ShowDialog();
        }

        private void navBarItem37_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //---------ریز کارکرد حساب
            DebtCreditAcc_bill_F DebtCreditAcc_bill_Frm = new DebtCreditAcc_bill_F();
            DebtCreditAcc_bill_Frm.ShowDialog();
        }

        private void navBarItem19_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Confilict_F Confilict_Frm = new Confilict_F();
            Confilict_Frm.kind = 1;
            Confilict_Frm.ShowDialog();
        }

        private void navBarItem29_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Confilict_F Confilict_Frm = new Confilict_F();
            Confilict_Frm.kind = 2;
            Confilict_Frm.ShowDialog();
        }

        private void navBarItem52_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Report_debtPaymentMonthly_F Report_debtPaymentMonthly_Frm = new Report_debtPaymentMonthly_F();
            Report_debtPaymentMonthly_Frm.ShowDialog();

        }

        private void navBarItem53_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Report_debtPaymentMembersSum_F Report_debtPaymentMembersSum_Frm = new Report_debtPaymentMembersSum_F();
            Report_debtPaymentMembersSum_Frm.ShowDialog();

        }

        private void navBarControl1_Click(object sender, EventArgs e)
        {

        }

        private void navBarItem54_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DebtCreditAcc_sod_F DebtCreditAcc_sod_Frm = new DebtCreditAcc_sod_F();
            DebtCreditAcc_sod_Frm.usercode = usercodetemp;
            DebtCreditAcc_sod_Frm.kind = 1;
            DebtCreditAcc_sod_Frm.kindtemp = 1;
            DebtCreditAcc_sod_Frm.Text = "واریز سود";
            DebtCreditAcc_sod_Frm.ShowDialog();
        }

        private void navBarItem55_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Backup_F Backup_Frm = new Backup_F();
            Backup_Frm.label1.Text = "پشتیبان گیری از اطلاعات";
            Backup_Frm.button1.Visible = true;
            Backup_Frm.button3.Visible = true;
            Backup_Frm.pictureBox1.Visible = true;
            Backup_Frm.button5.Visible = false;
            Backup_Frm.button4.Visible = false;
            Backup_Frm.pictureBox2.Visible = false;
            Backup_Frm.ShowDialog();
        }

        private void navBarItem56_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Backup_F Backup_Frm = new Backup_F();
            Backup_Frm.label1.Text = "بازیابی اطلاعات";
            Backup_Frm.button1.Visible = false;
            Backup_Frm.button3.Visible = false;
            Backup_Frm.pictureBox1.Visible = false;
            Backup_Frm.button5.Visible = true;
            Backup_Frm.button4.Visible = true;
            Backup_Frm.pictureBox2.Visible = true;

            Backup_Frm.ShowDialog();
        }

        private void navBarControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F8)
            {
                navBarItem56.Enabled = true;
            }

            if (e.KeyData == Keys.F9)
            {
                navBarItem56.Enabled = false;
            }

        }

   
                
    }
}
 