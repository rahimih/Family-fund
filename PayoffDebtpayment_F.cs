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
    public partial class PayoffDebtpayment_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public int usercode, code, DebtPaymentcode1;
        public string stockavg, payoffdebt, debtcode;
        string DebtPaymentcode, debtkindcode, debtcash;
        string sumpayoff, debtnumberp;
        double sumpayofftotal;
        public byte kindd;
        public PayoffDebtpayment_F()
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
                    //-------------
                    if (kindd == 1)
                    {
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
                               //Debtime = DateTime.Now.ToShortTimeString(),
                               DebtNumber = byte.Parse(debtnumberp),
                               DebtCash = (sumpayofftotal),
                               Comment =  textBox9.Text ,
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
                        MessageBox.Show("تسویه وام های عضو انتخابی انجام گردید.", "اطلاع", MessageBoxButtons.OK);
                        this.Close();
                    }

                    else if (kindd == 2)
                    {
                        DLUtilsobj.temperoryobj.payoffdebtlistone(code.ToString(), debtcode);
                        SqlDataReader DataSource;
                        DLUtilsobj.temperoryobj.Dbconnset(true);
                        DataSource = DLUtilsobj.temperoryobj.temperoryclientdataset.ExecuteReader();
                        DataSource.Read();
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
                            //Debtime = DateTime.Now.ToShortTimeString(),
                            DebtNumber = byte.Parse(debtnumberp),
                            DebtCash = (sumpayofftotal),
                            Comment = textBox9.Text , 
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

                        DataSource.Close();
                        DLUtilsobj.temperoryobj.Dbconnset(false);
                        MessageBox.Show("تسویه وام عضو انتخابی انجام گردید.", "اطلاع", MessageBoxButtons.OK);
                        this.Close();
                    }
                }
            }

            else if ((double.Parse(label7.Text)) == 0)
            {
                if (kindd == 1)
                {
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
                        MessageBox.Show("تسویه وام های عضو انتخابی انجام گردید.", "اطلاع", MessageBoxButtons.OK);
                        this.Close();
                    }

                } // if (kindd == 1)

                else if (kindd == 2)
                {
                    DLUtilsobj.temperoryobj.payoffdebtlistone(code.ToString(), debtcode);
                    SqlDataReader DataSource;
                    DLUtilsobj.temperoryobj.Dbconnset(true);
                    DataSource = DLUtilsobj.temperoryobj.temperoryclientdataset.ExecuteReader();

                    if (DataSource.Read() == true)
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
                        MessageBox.Show("تسویه وام عضو انتخابی انجام گردید.", "اطلاع", MessageBoxButtons.OK);
                        this.Close();
                    }
                }

            }  //  if ((double.Parse(label7.Text)) == 0)
        }

    }
}

