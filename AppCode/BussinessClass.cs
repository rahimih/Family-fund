using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAcessClass;
using System.Windows.Forms;




namespace BussinessClass
{
    public class BussinessClass
    {

     
        public class userchecking
        {

            public string conectionstring;
            public System.Data.SqlClient.SqlDataReader datasource;
            public System.Data.SqlClient.SqlCommand usercheckingclientdataset;
            protected DataAcessClass.DataAcessClass.dataworker dataworkerObj;

           
            public userchecking()
            {
                dataworkerObj = new DataAcessClass.DataAcessClass.dataworker();
                usercheckingclientdataset = dataworkerObj.clientdataset;
                conectionstring = dataworkerObj.conectionstring;
            }


            public bool Dbconnset(bool connected)
            {
                if (connected)
                    dataworkerObj.openconn();
                else
                    dataworkerObj.closeconn();
                return connected;
            }

            public bool Userlogin_checking(

                                    string username,
                                    string password
                                  )
            {
                return dataworkerObj.generalselect
                (
                  " TblUsers ",
                  " * ",
                  " UserName=  ''" + username.ToString() + " ''" +
                  " and Password=  ''" + password.ToString() + " ''"

                );
                
            }

    
            public int Changepassword(int usercode, string oldpassword, string password)
            {
                return dataworkerObj.generalupdate("TblUsers", "password = " + "''" + password + "''", "code = " + usercode.ToString() + "and password = " + "''" + oldpassword + "''");
            }

            public bool viewacesslevel(int usercode)
            {
                return dataworkerObj.generalselect
               (
                 " TblUsers ",
                 " * ",
                 " Code =  " + usercode
                 );
            }
            

        }
        public class EventsLog
        {
            public System.Data.SqlClient.SqlDataReader datasource;
            public System.Data.SqlClient.SqlCommand EventsLogclientdataset;
            protected DataAcessClass.DataAcessClass.dataworker dataworkerObj;

            public EventsLog()
            {
                dataworkerObj = new DataAcessClass.DataAcessClass.dataworker();
                EventsLogclientdataset = dataworkerObj.clientdataset;
            }


            public bool Dbconnset(bool connected)
            {
                if (connected)
                    dataworkerObj.openconn();
                else
                    dataworkerObj.closeconn();
                return connected;
            }

            public Int64 insertEventsLog(

                                                  string usercode,
                                                  string logdate,
                                                  string logtime,
                                                  int Logcode,
                                                  string IPAdress,
                                                  Int64 changecode
                                                 )
            {
                return dataworkerObj.generalinsert
                (
                " EventsLog ",
                " usercode,logdate,logtime,Logcode,IPAdress,changecode",
                "''" + usercode + "''" + " , " +
                "''" + logdate + "''" + " , " +
                "''" + logtime + "''" + " , " +
                Logcode + " , " +
                "''" + IPAdress + "''" + " , " +
                changecode
                );
            }


            public bool findEventsLog(int EventsLogCode)
            {
                return dataworkerObj.generalselect
                (
                " EventsLog ",
                " * ",
                " EventsLogCode= " + EventsLogCode.ToString()
                );
            }

            public string selectEventsLog(string fields, string conditon)
            {
                return dataworkerObj.generalselect_count
                 (
                 " EventsLog ",
                 fields
                 ,
                 conditon
                 );

            }

       

        }
        public class temperory
        {

            public string conectionstring;
            public System.Data.SqlClient.SqlDataReader datasource;
            public System.Data.SqlClient.SqlCommand temperoryclientdataset;
            protected DataAcessClass.DataAcessClass.dataworker dataworkerObj;

           

            public temperory()
            {
                dataworkerObj = new DataAcessClass.DataAcessClass.dataworker();
                temperoryclientdataset = dataworkerObj.clientdataset;
                conectionstring = dataworkerObj.conectionstring;
            }


            public bool Dbconnset(bool connected)
            {
                if (connected)
                    dataworkerObj.openconn();
                else
                    dataworkerObj.closeconn();
                return connected;
            }
      
            public bool viewAcc()
            {
                return dataworkerObj.generalselect("bankacc", " Code,AccNumber, Descriptions, Bnak, BranchName, BranchCode, (case kind when 1 then ''پس انداز'' else ''جاری'' end) as aaa, (case Status when 1 then ''فعال'' else ''غیر فعال'' end ) as bbb , status , kind ", "1=1");
            }

            public bool viewAcc_select_jari()
            {
                return dataworkerObj.generalselect("bankacc", " Code,AccNumber, Descriptions,Bnak, BranchName", "status=1 and kind=2");
            }

            public bool viewAcc_select_all()
            {
                return dataworkerObj.generalselect("bankacc", " Code,AccNumber, Descriptions,Bnak, BranchName", "status=1 ");
            }
            
           
            public bool membersview_select_active()
            {
                return dataworkerObj.generalselect("members", "Code, FirstName, LastName, FatherName", "status=1");
            }

            public bool membersview_select_total()
            {
                return dataworkerObj.generalselect("members", "Code, FirstName, LastName, FatherName", "1=1");
            }

            public bool DebtKIndview_select(int memberscode)
            {
                return dataworkerObj.generalselect("DebtPayment LEFT OUTER JOIN DebtKInd ON DebtPayment.DebtKindCode = DebtKInd.Code ", " DebtPayment.Code, DebtKInd.Descriptions,DebtPayment.countt,DebtPayment.DebtCash,DebtPayment.PaymentCash,DebtPayment.CashFirstDate", "DebtPayment.MembersCode =" + memberscode + " and DebtPayment.status=1");
            }

            public bool DebtKInd_select()
            {
                return dataworkerObj.generalselect("DebtKInd", "Code,Descriptions,count,DebtCash,cash,WageCash,year ", "status=1");
            }
   
            public bool membersview()
            {
                return dataworkerObj.generalselect("members", "Code, FirstName, LastName, FatherName,NationalCode, BirthDate, IdentityNumber,MembershipDate,status,Sex, Relation,MainMemberShip", "1=1");
            }

            public bool membersactiveview()
            {
                return dataworkerObj.generalselect("members", "Code, FirstName, LastName, FatherName,NationalCode, BirthDate, IdentityNumber,MembershipDate,Sex, Relation,MainMemberShip", "status=1");
            }


            public bool Debtkindview()
            {
                return dataworkerObj.generalselect("DebtKInd", "Code, Descriptions, DebtCash, WageCash, (case ReciveWageKind when 0 then ''کسر از مبلغ وام'' else ''تقسیم روی اقساط'' end) as aaa ,count, Cash, Year, Fromdate, ToDate, Status", "1=1");
            }

            public bool MemberShipRightview()
            {
                return dataworkerObj.generalselect("MemberShipRight", "Code, Description, FromDate, Todate ,Year, Cashfixed", "deleted=0");
            }

            public bool MonthPaymentview()
            {
                return dataworkerObj.generalselect("MonthPayment", "Code, Description, FromDate, Todate ,Year, Cashfixed", "deleted=0");
            }
            
            public DateTime shamsitomiladi(string shamsidate)
            {
                return dataworkerObj.shamsitomiladi(shamsidate);
            }

            public string miladitoshamsi(DateTime miladidate)
            {
                return dataworkerObj.miladitoshamsi(miladidate);
            }

            public DateTime getdate()
            {
                return dataworkerObj.getdate();
            }

            public bool requsetcheckview()
            {
                return dataworkerObj.generalselect("RequestCheck INNER JOIN BankAcc ON RequestCheck.AccCode = BankAcc.Code inner join   Members ON RequestCheck.transferee_user = Members.Code", "RequestCheck.Code, BankAcc.AccNumber, BankAcc.Descriptions, BankAcc.Bnak, BankAcc.BranchName, RequestCheck.countt ,RequestCheck.CheckNumberFrom,RequestCheck.CheckNumberTo, RequestCheck.Delivery_date, RequestCheck.Status,BankAcc.code,RequestCheck.transferee_user, Members.FirstName,Members.LastName ", "1=1");
            }

            public string balancemembers(string memberscode)
            {
                return dataworkerObj.generalselect_count("PaymentMembers", "sum(cash) as F1", "MembersCode= " + memberscode + " and kind<=2 and deleted=0 ");
            }

            public bool debtpaymentview()
            {
                return dataworkerObj.debtpaymentview();
            }

            public bool payoffdebtlist(string memberscode)
            {
                return dataworkerObj.generalselect("DebtPayment", "*", "MembersCode= " + memberscode + " and status=1");
            }

            public bool payoffdebtlistone(string memberscode,string debtcode)
            {
                return dataworkerObj.generalselect("DebtPayment", "*", "MembersCode= " + memberscode +" and code=" + debtcode + " and status=1");
            }

            public int debtcreditaacdelete (string code)
            {
                return dataworkerObj.generalupdate("DebtCreditAcc", "deleted=1", "code=" + code);
            }

            public string balance_acc (string acccode,string fromdate,string todate)
            {
                return dataworkerObj.generalselect_count("DebtCreditAcc","SUM(Variz_Cash) - SUM(Bardasht_Cash) AS F1"," deleted=0 and AccCode = " + acccode + " and datep between ''" + fromdate +"''"+ " and ''" + todate+" ''");
            }

            public string balance_acc_total(string fromdate, string todate)
            {
                return dataworkerObj.generalselect_count("DebtCreditAcc", "SUM(Variz_Cash) - SUM(Bardasht_Cash) AS F1", " deleted=0   and datep between ''" + fromdate + "''" + " and ''" + todate + " ''");
            }

            public bool report_monthlypayments(string memberscode)
            {
                return dataworkerObj.generalselect("PaymentMembers", "Code, PaymentDate, Cash, Year, Month, SerialNo, Comment", "MembersCode = " + memberscode + " and deleted=0 order by PaymentDate");
            }

            public bool report_DebtPayment(string memberscode)
            {
                return dataworkerObj.generalselect("DebtPayment", "Code, PaymentDate, DebtCash, countt, PaymentCash, CashFirstDate, CashEndDate, PayOffDate, Comment , CASE Status WHEN 2 THEN ''تسویه'' WHEN 1 THEN ''در حال پرداخت اقساط'' END AS statuskind , status ", "MembersCode = " + memberscode + " and status<3");
            }

 
        }

        public class temperory2
        {

            public string conectionstring;
            public System.Data.SqlClient.SqlDataReader datasource;
            public System.Data.SqlClient.SqlCommand temperory2clientdataset;
            protected DataAcessClass.DataAcessClass.dataworker dataworkerObj;



            public temperory2()
            {
                dataworkerObj = new DataAcessClass.DataAcessClass.dataworker();
                temperory2clientdataset = dataworkerObj.clientdataset;
                conectionstring = dataworkerObj.conectionstring;
            }


            public bool Dbconnset(bool connected)
            {
                if (connected)
                    dataworkerObj.openconn();
                else
                    dataworkerObj.closeconn();
                return connected;
            }

            public bool view_signature_select(int acccode)
            {
                return dataworkerObj.generalselect("SignaturesAcc INNER JOIN members ON SignaturesAcc.MembersCode = members.Code inner join bankacc on SignaturesAcc.AccCode=bankacc.Code", "members.FirstName, members.Lastname , SignaturesAcc.FromDate, SignaturesAcc.ToDate,SignaturesAcc.Status,SignaturesAcc.code,acccode,memberscode,bankacc.Bnak,BankAcc.BranchName", "acccode=" + acccode.ToString());
            }

            public bool debtpaymentcountsumcash(int memberscode, int debtcode)
            {
                return dataworkerObj.generalselect("DebtMonthlyPayment ", "COUNT(*) AS Expr1, SUM(PayoffCash) AS Expr2", "MembersCode =" + memberscode + " and DebtCode =" + debtcode + "and deleted=0");
            }

            public bool PaymentMembersview(int memberscode)
            {
                return dataworkerObj.generalselect("PaymentMembers", "Code,PaymentDate, PaymentTime, Year, Month, cash,serialno,comment ", "MembersCode = " + memberscode + "and kind=2 and deleted=0");
            }

            public bool PaymentMembersRightview(int memberscode)
            {
                return dataworkerObj.generalselect("PaymentMembers", "Code,PaymentDate, PaymentTime, Year, Month, cash,serialno,comment ", "MembersCode = " + memberscode + "and kind=1 and deleted=0");
            }

            public string payoffdebt(string memmerscode)
            {
                return dataworkerObj.payoffdebt(memmerscode);
            }

            public string payoffdebtone(string memmerscode,string debtcode)
            {
                return dataworkerObj.payoffdebtone(memmerscode,debtcode);
            }
                       
            public bool payoffDebtMonthlyPayment(string memmerscode, string debtcode )
            {
                return dataworkerObj.generalselect("DebtMonthlyPayment","sum(PayoffCash)-sum(PenaltyCash) as sumpayoff,max(DebtNumber) as debtnumberp","MembersCode= "+ memmerscode+ "and debtcode=" +debtcode +"and status=1 and deleted=0");
            }

            public string PaymentMemberscash(int yearj)
            {
                return dataworkerObj.generalselect_count("MonthPayment", "Cashfixed as F1", "year=" + yearj);
            }

            public string MemberShipRightcash(int yearj)
            {
                return dataworkerObj.generalselect_count("MemberShipRight", "Cashfixed as F1", "year=" + yearj);
            }

            public bool PaymentMembersgrouplist(string datee)
            {
                return dataworkerObj.PaymentMembersgrouplist(datee);
            }
            
            public string  searchmembers(string memberscode)
            {
                return dataworkerObj.generalselect_count("members", "firstname+" + "'' ''+" + "lastname as F1", "code = " + memberscode + "and status=1");
            }

            public string searchmembers_total(string memberscode)
            {
                return dataworkerObj.generalselect_count("members", "firstname+" + "'' ''+" + "lastname as F1", "code = " + memberscode);
            }
       
            public bool debtmonthpaymentstatus(int debtcode)
            {
                return dataworkerObj.generalselect("DebtMonthlyPayment", "*", "DebtCode=" + debtcode);
            }


            public bool debtMonthPaymentviews(int DebtCode)
            {
                return dataworkerObj.generalselect("DebtMonthlyPayment", "Code, DebtDate, DebtNumber, DebtCash, PenaltyCash, TotalCash, PayoffCash, Comment, SerialNo ", "DebtCode = " + DebtCode + " and deleted=0");
            }

               public bool debtpaymentmonthly()
            {
                return dataworkerObj.debtpaymentmonthly();
            }

              public bool  debtcreditaacview_v(string accode,string fromdate,string todate ,byte kindview)
               {
                   return dataworkerObj.debtcreditaacview_v(accode, fromdate, todate, kindview);
               }

            //--------------- report box
         public string Reportbox_monthly1(string fromdate,string todate)
              {
                  return dataworkerObj.generalselect_count("PaymentMembers", "sum(cash) as F1", "kind=1 and deleted=0 and PaymentDate between ''" + fromdate + "'' and ''" + todate+ " ''");
              }

         public string Reportbox_monthly2(string fromdate, string todate)
            {
                return dataworkerObj.generalselect_count("PaymentMembers", "sum(cash) as F1", "kind=2 and deleted=0 and PaymentDate between ''" + fromdate + "'' and ''" + todate + " ''");
            }

         public string Reportbox_monthly(string fromdate, string todate)
            {
                   return dataworkerObj.generalselect_count("PaymentMembers", "sum(cash) as F1", " deleted=0 and PaymentDate between ''" + fromdate + "'' and ''" + todate + " ''");
            }

         public string reportbox_sood(string acccode, string fromdate, string todate)
         {
             return dataworkerObj.generalselect_count("DebtCreditAcc", "SUM(Variz_Cash) - SUM(Bardasht_Cash) AS F1", " (kind=5 or kind=9) and deleted =0 and AccCode = " + acccode + " and datep between ''" + fromdate + "''" + " and ''" + todate + " ''");
         }

          public string ReportBox_DebtPayment(string fromdate, string todate)
            {
                return dataworkerObj.generalselect_count("DebtPayment", " SUM(DebtCash) as F1", "status<3 and PaymentDate between ''" + fromdate + "'' and ''" + todate + " ''");
            }

          public string ReportBox_DebtPaymentmonth(string fromdate, string todate)
            {
                return dataworkerObj.generalselect_count("DebtMonthlyPayment", " SUM(PayoffCash) as F1", "deleted=0  and debtdate between ''" + fromdate + "'' and ''" + todate + " ''");
            }

          public string ReportBox_debtwage(string fromdate, string todate)
          {
              return dataworkerObj.generalselect_count("DebtPayment", " SUM(DebtCash * DebtWage / 100) as F1", " status<3  and PaymentDate between ''" + fromdate + "'' and ''" + todate + " ''");
          }

           public bool ReportpaymentMonthly(string fromdate, string todate)
          {
              return dataworkerObj.generalselect("PaymentMembers", " Year, Month, CASE kind WHEN 1 THEN ''حق عضویت'' WHEN 2 THEN ''ماهیانه'' END AS Expr2, SUM(Cash) AS Expr1", "Deleted = 0 and PaymentDate between ''" + fromdate + "'' and ''" + todate +" ''" + " GROUP BY Year, Month, kind  order by year,month " );
          }


           public bool ReportdebtpaymentMonthly(string fromdate, string todate)
           {
               return dataworkerObj.generalselect("DebtMonthlyPayment", " SUBSTRING(DebtDate, 1, 4) AS Expr1, SUBSTRING(DebtDate, 6, 2) AS Expr2, ''اقساط'', SUM(PayoffCash) AS Expr1", "Deleted = 0 and DebtDate between ''" + fromdate + "'' and ''" + todate + " ''" + " GROUP BY SUBSTRING(DebtDate, 1, 4) , SUBSTRING(DebtDate, 6, 2)  order by SUBSTRING(DebtDate, 1, 4) , SUBSTRING(DebtDate, 6, 2) ");
           }

           public bool ReportpaymentmembersSum(string fromdate, string todate)
           {
               return dataworkerObj.generalselect("PaymentMembers left JOIN  Members ON PaymentMembers.MembersCode = Members.Code", "PaymentMembers.MembersCode, Members.FirstName, Members.LastName, SUM(PaymentMembers.Cash) AS Expr1 ",
               "PaymentMembers.Deleted = 0 and PaymentDate between ''" + fromdate + "'' and ''" + todate + " ''" + " GROUP BY PaymentMembers.MembersCode, Members.FirstName, Members.LastName  HAVING (SUM(PaymentMembers.Cash) > 0) ");
           }


           public bool ReportdebtpaymentmembersSum(string fromdate, string todate)
           {
               return dataworkerObj.generalselect("DebtMonthlyPayment left JOIN  Members ON DebtMonthlyPayment.MembersCode = Members.Code", "DebtMonthlyPayment.MembersCode, Members.FirstName, Members.LastName, SUM(PayoffCash) AS Expr1 ",
               "DebtMonthlyPayment.Deleted = 0 and DebtDate between ''" + fromdate + "'' and ''" + todate + " ''" + " GROUP BY DebtMonthlyPayment.MembersCode, Members.FirstName, Members.LastName  HAVING (SUM(PayoffCash) > 0) ");
           }

           public bool DebtInsertprint(string DebtCash, string debtpayment, string debtcount, string debtcode)
           {
               
               return dataworkerObj.DebtInsertprint(DebtCash, debtpayment, debtcount, debtcode);               
           }

         public bool findbankcreditacc(string codev,string fishnumber)
         {
             //return dataworkerObj.generalselect("DebtCreditAcc", "*", "Code = " codev + " and FishNumber= " + fishnumber + " and kind=1");
             return false;
         }

         public bool confilit(string fromyear, string frommonth , string toyear, string tomonth , int kind)
         {
             return dataworkerObj.confilit(fromyear, frommonth, toyear, tomonth, kind);
         }

         public string defaultbankacc()
         {
             return dataworkerObj.generalselect_count("bankacc", "code as F1", " defaults=true ");
         }
            
        }

        public class backuping
        {

            public string conectionstring;
            public System.Data.SqlClient.SqlDataReader datasource;
            public System.Data.SqlClient.SqlCommand backupingclientdataset;
            protected DataAcessClass.DataAcessClass.dataworker dataworkerObj;


            public backuping()
            {
                dataworkerObj = new DataAcessClass.DataAcessClass.dataworker();
                backupingclientdataset = dataworkerObj.clientdataset;
                conectionstring = dataworkerObj.conectionstring;
            }


            public bool Dbconnset(bool connected)
            {
                if (connected)
                    dataworkerObj.openconn();
                else
                    dataworkerObj.closeconn();
                return connected;
            }


            public int Backup(string filename)
            {

                return dataworkerObj.Backup(filename);

            }


            public int Restore(string filename)
            {

                return dataworkerObj.Restore(filename);

            }

        }
     
      
            
    }

}

    