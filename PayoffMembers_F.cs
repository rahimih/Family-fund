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
    public partial class PayoffMembers_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public int usercode, code, DebtPaymentcode1;
        public string stockavg, payoffdebt;
        string DebtPaymentcode, debtkindcode, debtcash;
        string sumpayoff, debtnumberp;
        double sumpayofftotal;
        public PayoffMembers_F()
        {
            InitializeComponent();
        }

        private void PayoffMembers_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((double.Parse(label7.Text)) > 0)
            {
                if (MessageBox.Show("اطلاعات مورد نظر ثبت گردد؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Member membertable = familial_bankEntitiescontext.Members.First(i => i.Code == code);
                    membertable.MembershipDateCancel = persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
                    membertable.Status = false;
                    familial_bankEntitiescontext.SaveChanges();
                    //----------------  پرداخت های ماهیانه
                    PaymentMember PaymentMembertable = new PaymentMember()
               {
                   MembersCode = code,
                   PaymentDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd"),
                   PaymentTime = DateTime.Now.ToShortTimeString(),
                   Cash = double.Parse(stockavg) * -1,
                   kind = 2,
                   Month = byte.Parse(persianDateTimePicker1.Value.Month.ToString()),
                   Year = (persianDateTimePicker1.Value.Year),
                   SerialNo = 0,
                   Comment = textBox9.Text ,
                   UserCode = usercode,
                   IpAdress = Environment.MachineName,
                   Deleted = false
               };
                    familial_bankEntitiescontext.PaymentMembers.Add(PaymentMembertable);
                    familial_bankEntitiescontext.SaveChanges();

                    //---------------------
                    if (double.Parse(payoffdebt) > 0)
                    {

                        //-------------
                        DLUtilsobj.temperoryobj.payoffdebtlist(code.ToString());
                        SqlDataReader DataSource;
                        DLUtilsobj.temperoryobj.Dbconnset(true);
                        DataSource = DLUtilsobj.temperoryobj.temperoryclientdataset.ExecuteReader();
                        while (DataSource.Read())
                        {
                            DebtPaymentcode = DataSource["code"].ToString();
                            debtkindcode = DataSource["debtkindcode"].ToString();
                            debtcash = DataSource["debtcash"].ToString();
                            //-------------
                            DebtPaymentcode1 = int.Parse(DebtPaymentcode);
                            DebtPayment DebtPaymenttable = familial_bankEntitiescontext.DebtPayments.First(i => i.Code == DebtPaymentcode1);
                            DebtPaymenttable.CashEndDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
                            DebtPaymenttable.PayOffDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
                            DebtPaymenttable.PayOffTime = DateTime.Now.ToShortTimeString();
                            DebtPaymenttable.Status = 2;
                            familial_bankEntitiescontext.SaveChanges();
                            DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 19, Environment.MachineName, code);
                            //------------------
                            DLUtilsobj.temperory2obj.payoffDebtMonthlyPayment(code.ToString(), DebtPaymentcode);
                            SqlDataReader DataSource2;
                            DLUtilsobj.temperory2obj.Dbconnset(true);
                            DataSource2 = DLUtilsobj.temperory2obj.temperory2clientdataset.ExecuteReader();
                            DataSource2.Read();
                            sumpayoff = DataSource2["sumpayoff"].ToString();
                            debtnumberp = DataSource2["debtnumberp"].ToString();
                            debtnumberp = (byte.Parse(debtnumberp) + byte.Parse("1")).ToString();
                            sumpayofftotal = double.Parse(debtcash) - double.Parse(sumpayoff);
                            DataSource2.Close();
                            DLUtilsobj.temperory2obj.Dbconnset(false);
                            //------------
                            DebtMonthlyPayment DebtMonthlyPaymenttable = new DebtMonthlyPayment
                           {
                               MembersCode = code,
                               DebtCode = int.Parse(DebtPaymentcode),
                               DebtDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd"),
                              // Debtime = DateTime.Now.ToShortTimeString(),
                               DebtNumber = byte.Parse(debtnumberp) ,
                               DebtCash = (sumpayofftotal),
                               Comment = "تسویه وام",
                               PayoffCash = (sumpayofftotal),
                               PenaltyCash = 0,
                               SerialNo = 0,
                               TotalCash = (sumpayofftotal),
                               UserCode = usercode,
                               IpAdress = Environment.MachineName,
                               Deleted = false,
                               Status = true
                           };
                            familial_bankEntitiescontext.DebtMonthlyPayments.Add(DebtMonthlyPaymenttable);
                            familial_bankEntitiescontext.SaveChanges();
                        }  //end of while
                        DataSource.Close();
                        DLUtilsobj.temperoryobj.Dbconnset(false);

                    } // end of  if (payoffdebt >0 ) 
                }
                
                MessageBox.Show("تسویه حساب عضو انتخابی انجام گردید." + "\n" + "تسویه وام عضو انتخابی نیز انجام گردید", "Information", MessageBoxButtons.OK);
            }
               else if ((double.Parse(label7.Text)) == 0)
               {
                   MessageBox.Show("موجودی حساب عضو انتخابی صفر می باشد", "خطا", MessageBoxButtons.OK);
               }
               else if ((double.Parse(label7.Text)) < 0)
                    {
                        MessageBox.Show("شما مجاز به تسویه حساب فرد انتخابی نمی باشید" + "\n" + "لطفا ابتدا نسبت به تسویه وام عضو انتخابی اقدام نمائید." + "\n" + "موجودی عضو انتخابی از مبلغ کل وام های دریافتی کمتر است.", "خطا", MessageBoxButtons.OK);
                    }
                        
         }
    }
}
