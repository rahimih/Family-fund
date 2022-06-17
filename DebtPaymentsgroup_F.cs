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
using System.IO; 
using Telerik.Data; 
using Telerik.WinControls.UI; 
using Telerik.WinControls; 
using Telerik.WinControls.UI.Export;



namespace familial_bank
{
    public partial class DebtPaymentsgroup_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        bool openExportFile = false;
        bool entermode = false;
        int i, j;
        public int usercode;
        int DebtPaymentcode1;
        public DebtPaymentsgroup_F()
        {
            InitializeComponent();
        }

        private bool loaddata()
        {
            DLUtilsobj.temperory2obj.debtpaymentmonthly();
            SqlDataReader DataSource;
            DLUtilsobj.temperory2obj.Dbconnset(true);
            DataSource = DLUtilsobj.temperory2obj.temperory2clientdataset.ExecuteReader();
            radGridView1.DataSource = DataSource;
            DLUtilsobj.temperory2obj.Dbconnset(false);

            if (radGridView1.RowCount > 0)
            {
                radGridView1.Columns[0].HeaderText = "کد وام ";
                radGridView1.Columns[1].HeaderText = "کد عضویت ";
                radGridView1.Columns[2].HeaderText = "نام";
                radGridView1.Columns[3].HeaderText = " تعداد اقساط";
                radGridView1.Columns[4].HeaderText = " مبلغ وام";
                radGridView1.Columns[5].HeaderText = "مبلغ کل پرداختی ";
                radGridView1.Columns[6].HeaderText = "مبلغ باقیمانده  ";
                radGridView1.Columns[7].HeaderText = "شماره قسط";
                radGridView1.Columns[8].HeaderText = "مبلغ قسط ";      
          
                //radGridView1.Columns.Add("مبلغ پرداختی");
                GridViewDecimalColumn decimalColumn = new GridViewDecimalColumn();
                decimalColumn.Name = "مبلغ پرداختی";
                decimalColumn.HeaderText = "مبلغ پرداختی";
                decimalColumn.DecimalPlaces = 0;
                decimalColumn.Width = 200;
                decimalColumn.FormatString = "{0:#,##0}";
                radGridView1.Columns.Add(decimalColumn);

                radGridView1.Columns[4].FormatString = "{0:#,##0}";
                radGridView1.Columns[5].FormatString = "{0:#,##0}";
                radGridView1.Columns[6].FormatString = "{0:#,##0}";

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

        private void RunExportToExcelML(string fileName, ref bool openExportFile)
        {
            
            ExportToExcelML excelExporter = new ExportToExcelML(this.radGridView1);

                excelExporter.SheetName = comboBox1.Text+"_"+comboBox3.Text;

          
            // excelExporter.SummariesExportOption = ExportAll;
          
            //set max sheet rows
                      
            //set exporting visual settings
            excelExporter.ExportVisualSettings = true;

            try
            {
                excelExporter.RunExport(fileName);

                RadMessageBox.SetThemeName(this.radGridView1.ThemeName);
                DialogResult dr = MessageBox.Show("The data in the grid was exported successfully. Do you want to open the file?",
                    "Export to Excel", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    openExportFile = true;
                }
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(this.radGridView1.ThemeName);
                MessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PaymentMembersgroup_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();

            comboBox1.Text = persianDateTimePicker1.Value.Year.ToString();
            comboBox3.SelectedIndex = persianDateTimePicker1.Value.Month - 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            radGridView1.Columns.Clear();
            loaddata();
            j = radGridView1.RowCount ;        
            i=0;
            while (i<j)
             {
                 radGridView1.Rows[i].Cells[9].Value = "0";  //radGridView1.Rows[i].Cells[4].Value;
                 i = i + 1;
             }

        }

        private void radGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{DOWN}");
                
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            radGridView1.Columns[9].TextAlignment = System.Drawing.ContentAlignment.TopRight;
            
           
            PrintPreviewDialog dialog = new PrintPreviewDialog();

            radPrintDocument1.RightHeader = "دریافت اقساط ";
            radPrintDocument1.MiddleHeader = "سال" +comboBox1.Text +"  ماه "+comboBox3.Text+" تاریخ " +persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
            dialog.Document = this.radPrintDocument1;
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.ShowDialog(); 

            //-------------                    
            radGridView1.Columns[9].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) 
            { 
                return; 
            }
            
            if (saveFileDialog1.FileName.Equals(String.Empty))
            { 
                //RadMessageBox.SetThemeName(this.radGridView1.ThemeName);
                MessageBox.Show("Please enter a file name."); 
                return;
            }

            string fileName = this.saveFileDialog1.FileName; 
            RunExportToExcelML(fileName, ref openExportFile);


            if (openExportFile)
            { 
                try 
                { 
                    System.Diagnostics.Process.Start(fileName); 
                }
                catch (Exception ex)
                { 
                    string message = String.Format("The file cannot be opened on your system.\nError message: {0}", ex.Message); 
                    MessageBox.Show(message, "Open File", MessageBoxButtons.OK); 
                }
            } 
        }

        private void radGridView1_CurrentCellChanged(object sender, CurrentCellChangedEventArgs e)
        {
          if (entermode==true)
          { 
            radGridView1.Columns[9].FormatString = "{0:#,##0}";            
        }
        }

        private void radGridView1_Enter(object sender, EventArgs e)
        {
            entermode = true;
        }

        private void radGridView1_CurrentRowChanging(object sender, CurrentRowChangingEventArgs e)
        {
                      
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا مطمئن به ذخیره اطلاعات می باشید؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

               j = radGridView1.RowCount ;             
               i=0;
               while (i<j)
             {
                 if (radGridView1.Rows[i].Cells[9].Value.ToString() != "0" )
                 {

                     DebtMonthlyPayment DebtMonthlyPaymenttable = new DebtMonthlyPayment
                     {
                         MembersCode = int.Parse(radGridView1.Rows[i].Cells[1].Value.ToString()),
                         DebtCode = int.Parse(radGridView1.Rows[i].Cells[0].Value.ToString()),
                         DebtDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd"),
                         //Debtime = DateTime.Now.ToShortTimeString(),
                         DebtNumber = byte.Parse(radGridView1.Rows[i].Cells[7].Value.ToString()),
                         DebtCash = int.Parse(radGridView1.Rows[i].Cells[8].Value.ToString()),
                         Comment = "پرداخت قسط شماره" + radGridView1.Rows[i].Cells[7].Value.ToString(),
                         PayoffCash = int.Parse(radGridView1.Rows[i].Cells[9].Value.ToString()),
                         PenaltyCash = 0,
                         SerialNo = 0,
                         TotalCash = int.Parse(radGridView1.Rows[i].Cells[9].Value.ToString()),
                         UserCode = usercode,
                         IpAdress = Environment.MachineName,
                         Deleted = false,
                         Status = true
                     };
                     familial_bankEntitiescontext.DebtMonthlyPayments.Add(DebtMonthlyPaymenttable);
                     familial_bankEntitiescontext.SaveChanges();                                      

                     //---------------
                    if (int.Parse(radGridView1.Rows[i].Cells[9].Value.ToString()) >= (int.Parse(radGridView1.Rows[i].Cells[6].Value.ToString())))
                     {
                         DebtPaymentcode1 = int.Parse(radGridView1.Rows[i].Cells[0].Value.ToString());
                     DebtPayment DebtPaymenttable = familial_bankEntitiescontext.DebtPayments.First(i2 => i2.Code == DebtPaymentcode1);
                     DebtPaymenttable.CashEndDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
                     DebtPaymenttable.PayOffDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
                     DebtPaymenttable.PayOffTime = DateTime.Now.ToShortTimeString();
                     DebtPaymenttable.Status = 2;
                     familial_bankEntitiescontext.SaveChanges();
                     }

                 }
                     i = i + 1;
                 }

                MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                this.Close();
               }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog dialog = new PrintPreviewDialog();

            radPrintDocument1.RightHeader = "دریافت اقساط ";
            radPrintDocument1.MiddleHeader = "سال" + comboBox1.Text + "  ماه " + comboBox3.Text + " تاریخ " + persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
            dialog.Document = this.radPrintDocument1;
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.ShowDialog(); 

        }
        }
    }

