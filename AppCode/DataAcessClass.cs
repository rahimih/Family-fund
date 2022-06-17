using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using System.Windows.Forms;



namespace DataAcessClass
{
  public  class DataAcessClass
    {
     public static string aa;
     public static byte bb;
  
     public class dataworker
     {
         public string procedureName;
         public string procedureParameters;
         public int OperatorPersonalCode;
         public string OperatorDate;
         public string OperatorTime;
         public string conectionstring;
         public string result;
         public SqlCommand clientdataset;
         public SqlDataReader datareader;
         protected SqlConnection databaseconnection;
         public Int64 identitynumber;
         public DateTime miladidate;
         public string shamsidate;
         public DateTime sdate;
            public string str1;

         public dataworker()
         {


       //------------------

             XmlTextReader XmlRdr = new XmlTextReader("familialbank.xml");

            while(!XmlRdr.EOF)

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
       
         
             //-------------------------

            conectionstring = ("user id=familialbankUser;data source=\"" + str1 + "\";persist security in" +
                                   "fo=True;initial catalog=familial bank;password=\"FaBank@Ali\";connection timeout=120");
             databaseconnection = new SqlConnection(conectionstring);
             clientdataset = new SqlCommand("", databaseconnection);
         }

         public bool openconn()
         {
             databaseconnection.Open();
             return true;
         }

         public bool closeconn()
         {
             databaseconnection.Close();
             return true;
         }



         public DateTime shamsitomiladi(string shamsidate)
         {
             procedureName = "sp_shamsitomiladi ";
             procedureParameters =
               "@shamsidate = '" + shamsidate + "'";

             databaseconnection.Open();
             clientdataset.CommandText = "Exec " + procedureName + procedureParameters;
             datareader = clientdataset.ExecuteReader();
             datareader.Read();
             miladidate = Convert.ToDateTime(datareader["miladidate"].ToString());
             databaseconnection.Close();
             return miladidate;
         }

         public string miladitoshamsi(DateTime miladidate)
         {
             procedureName = "sp_miladitoshamsi ";
             procedureParameters =
               "@miladidate = '" + miladidate + "'";

             databaseconnection.Open();
             clientdataset.CommandText = "Exec " + procedureName + procedureParameters;
             datareader = clientdataset.ExecuteReader();
             datareader.Read();
             shamsidate = (datareader["shamsidate"].ToString());
             databaseconnection.Close();
             return shamsidate;
         }

         public DateTime getdate()
         {
             procedureName = "getdate ";
            
             databaseconnection.Open();
             clientdataset.CommandText = "Exec " + procedureName ;
             datareader = clientdataset.ExecuteReader();
             datareader.Read();
             sdate = Convert.ToDateTime(datareader["sdate"].ToString());
             databaseconnection.Close();
             return sdate;

         }
         public string quotedstr(string sstr)
         {
             return "'" + sstr + "'";
         }

         public Int64 generalinsert(string tablename,
                                  string fields,
                                  string values
                                  )
         {

             procedureName = "GeneralInsert ";
             procedureParameters =
               "@Tables = '" + tablename + "' , " +
               "@Fields = '" + fields + "' , " +
               "@Values = '" + values + "' ";
             databaseconnection.Open();
             clientdataset.CommandText = "Exec " + procedureName + procedureParameters;
             datareader = clientdataset.ExecuteReader();
             datareader.Read();
             identitynumber = Convert.ToInt64(datareader["identitynumber"].ToString());
             databaseconnection.Close();
             return identitynumber;

         }

         public int generalupdate(string tablename,
                                  string values,
                                  string conditions
                                  )
         {
             procedureName = " GeneralUpdate ";
             procedureParameters =
               "@Tables = '" + tablename + "' , " +
               "@Values = '" + values + "' , " +
               "@Conditions = '" + conditions + "' ";
             databaseconnection.Open();
             clientdataset.CommandText = "Exec " + procedureName + procedureParameters;
             clientdataset.ExecuteNonQuery();
             databaseconnection.Close();
             return 1;
             
         }

         public int generaldelete(string tablename,
                                  string conditions
                                  )
         {
             procedureName = " GeneralDelete ";
             procedureParameters =

               "@Tables = '" + tablename + "' , " +
               "@Conditions = '" + conditions + "' ";
             databaseconnection.Open();
             clientdataset.CommandText = "Exec " + procedureName + procedureParameters;
             clientdataset.ExecuteNonQuery();
             databaseconnection.Close();
             return 1;
             //		Result := DataWorkerClientDataSet.FieldByName("IdentityNumber").AsInteger;
         }

         public bool generalselect(string tablename,
                                  string fields,
                                  string conditions
                                  )
         {
             procedureName = " GeneralSelect ";
             procedureParameters =

               "@Tables = '" + tablename + "' , " +
               "@Conditions = '" + conditions + "' , " +
               "@Fields = '" + fields + "' , " +
               "@Code = 0";
           
             databaseconnection.Open();
             clientdataset.CommandText = "Exec " + procedureName + procedureParameters;
             if (clientdataset.ExecuteReader().HasRows == true)
             {

                 
                 databaseconnection.Close();
                 return true;
             }
             else
             {

                 databaseconnection.Close();
                 return false;
             }
             
         }

         public bool generalJoinselect(string tablename,
                                string fields,
                                string conditions,
                                string tablename2
                                )
         {
             procedureName = " GeneraljoinSelect ";
             procedureParameters =

               "@Tables = '" + tablename + "' , " +
               "@Conditions = '" + conditions + "' , " +
               "@Fields = '" + fields + "' , " +
               "@JoinTables  = '" + tablename2 + "' ";
             databaseconnection.Open();
             clientdataset.CommandText = "Exec " + procedureName + procedureParameters;
             if (clientdataset.ExecuteReader().HasRows == true)
             {
                 databaseconnection.Close();
                 return true;
             }
             else
             {
                 databaseconnection.Close();
                 return false;
             }
         }

         public string generalselect_count(string tablename,
                              string fields,
                              string conditions
                              )
         {

             procedureName = " GeneralSelect ";
             procedureParameters =

               "@Tables = '" + tablename + "' , " +
               "@Conditions = '" + conditions + "' , " +
               "@Fields = '" + fields + "' , " +
               "@Code = 0";
             databaseconnection.Open();
             clientdataset.CommandText = "Exec " + procedureName + procedureParameters;

             datareader = clientdataset.ExecuteReader();
             if (datareader.HasRows==false)
                result="0";
             else
           {
               datareader.Read();
               result = datareader["F1"].ToString();
//               if (result == "")
                   //result = "1";
           }
             databaseconnection.Close();
             return result;
         
         }



         public bool generalselect_CTE(string tablename,
                                  string aliasFields,
                                  string firstConditions,
                                  string Fields,
                                  string MainFields,
                                  string joinTables,
                                  string lastConditions
                                  )
         {
             procedureName = " GeneralSelect ";
             procedureParameters =
               "@Tables = '" + tablename + "' , " +
               "@AliasFields = '" + aliasFields + "' , " +
               "@Conditions = '" + firstConditions + "' , " +
               "@MainFields = '" + MainFields + "' , " +
               "@Fields = '" + Fields + "' , " +
               "@JoinTables = '" + joinTables + "' , " +
               "@Conditions_2 = '" + lastConditions + "' , " +
               "@Code = 1";

             databaseconnection.Close();
             databaseconnection.Open();
             clientdataset.CommandText = "Exec " + procedureName + procedureParameters;
             if (clientdataset.ExecuteReader().HasRows == true)
             {
                 databaseconnection.Close();
              
                 return true;
             }
             else
             {
                 databaseconnection.Close();
                 return false;
             }
             
         }


         public string payoffdebt(string memmerscode)
         {
               procedureName = " payoffdebt ";
               procedureParameters = "@memberscode = " + memmerscode;
               databaseconnection.Open();
               clientdataset.CommandText = "Exec " + procedureName + procedureParameters;

               datareader = clientdataset.ExecuteReader();
               if (datareader.HasRows == false)
                   result = "0";
               else
               {
                   datareader.Read();
                   if (datareader["F1"] != DBNull.Value)
                       result = datareader["F1"].ToString();
                   else
                       result = "0";
                 
               }
               databaseconnection.Close();
               return result;

         }

         public string payoffdebtone(string memmerscode, string debtcode)
         {
             procedureName = " payoffdebtone ";
             procedureParameters = "@memberscode = " + memmerscode + " , " +
                 "@debtcode = " + debtcode;
             databaseconnection.Open();
             clientdataset.CommandText = "Exec " + procedureName + procedureParameters;

             datareader = clientdataset.ExecuteReader();
             if (datareader.HasRows == false)
                 result = "0";
             else
             {
                 datareader.Read();
                 if (datareader["F1"] != DBNull.Value)
                     result = datareader["F1"].ToString();
                 else
                     result = "0";

             }
             databaseconnection.Close();
             return result;

         }


         public bool debtpaymentview()
         {
             procedureName = " debtpaymentview ";
             databaseconnection.Open();
             clientdataset.CommandText = "Exec " + procedureName + procedureParameters;
             databaseconnection.Close();
             return true;

         }


         public bool PaymentMembersgrouplist(string datee)
         {
             procedureName = " PaymentMembersgrouplist ";
             procedureParameters = "@enddate = '" + datee + "'" ;
                 
             databaseconnection.Open();
             clientdataset.CommandText = "Exec " + procedureName + procedureParameters;
             databaseconnection.Close();
             return true;
         }


         public bool debtpaymentmonthly()
         {
             procedureName = " debtpaymentmonthly ";
             databaseconnection.Open();
             clientdataset.CommandText = "Exec " + procedureName + procedureParameters;
             databaseconnection.Close();
             return true;
         }



         public bool  debtcreditaacview_v(string accode,string fromdate,string todate ,byte kindview)
         {
             procedureName = " debtcreditaacview_v ";
             procedureParameters = "@acc = " + accode + " , " +
                                   "@fromdate = '" + fromdate + "' ," +
                                   "@todate = '" + todate + "' ," + 
                                   "@kindview = " + kindview;

             databaseconnection.Open();
             clientdataset.CommandText = "Exec " + procedureName + procedureParameters;
             databaseconnection.Close();
             return true;
         }


         public bool DebtInsertprint(string DebtCash, string debtpayment, string debtcount, string debtcode)
         {

             procedureName = " DebtInsertprint ";
             procedureParameters = "@DebtCash = " + DebtCash + " , " +
                                   "@debtpayment = " + debtpayment + " ," +
                                   "@debtcount = " + debtcount + " ," +
                                   "@debtcode = " + debtcode;
             
             databaseconnection.Open();
             //clientdataset.CommandText = "Exec  DebtInsertprint @DebtCash = 2000000 , @debtpayment = 200000 ,@debtcount = 10 ,@debtcode = 8 ";
             clientdataset.CommandText = "Exec " + procedureName + procedureParameters;             
             databaseconnection.Close();
             
             return true;
         }


         public bool confilit(string fromyear, string frommonth , string toyear, string tomonth , int kind)
         {
             procedureName = " confilit ";
             procedureParameters = "@fromyear = " + fromyear + " , " +
                                   "@frommonth = " + frommonth + " ," +
                                   "@toyear = " + toyear + " ," +
                                   "@tomonth = " + tomonth + " ," +
                                   "@kind = " + kind;

             databaseconnection.Open();
             clientdataset.CommandText = "Exec " + procedureName + procedureParameters;
             databaseconnection.Close();
             return true;
         }

         public int Backup(string filename)
         {
             try
             {

                 databaseconnection.Open();
                 clientdataset.CommandText = " Backup DataBase [familial bank] To Disk='" + filename + "'";
                 databaseconnection.Close();

             }

             catch (Exception ex)
             {

                 MessageBox.Show("Error : ", ex.Message);

             }

             return 1;
         }


         public int Restore(string filename)
         {

             try
             {

                 databaseconnection.Open();
                 clientdataset.CommandText = "ALTER DATABASE [familial bank]  SET SINGLE_USER with ROLLBACK IMMEDIATE " + " USE master " + " RESTORE DATABASE [familial bank] FROM DISK= N'" + filename + "' WITH REPLACE";
                 databaseconnection.Close();


             }

             catch (Exception ex)
             {

                 MessageBox.Show("Error : ", ex.Message);

             }
             return 1;

         }

         
    
     }

    
    }

}
