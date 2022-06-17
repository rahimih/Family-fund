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
    public partial class Report_PaymentMembersSum_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        bool openExportFile = false;            
      
        public Report_PaymentMembersSum_F()
        {
            InitializeComponent();
        }

        private bool loaddata()
        {
            DLUtilsobj.temperory2obj.ReportpaymentmembersSum(persianDateTimePicker2.Value.ToString("yyyy/MM/dd"), persianDateTimePicker1.Value.ToString("yyyy/MM/dd"));
            SqlDataReader DataSource;
            DLUtilsobj.temperory2obj.Dbconnset(true);
            DataSource = DLUtilsobj.temperory2obj.temperory2clientdataset.ExecuteReader();
            radGridView1.DataSource = DataSource;
            DLUtilsobj.temperory2obj.Dbconnset(false);

            if (radGridView1.RowCount > 0)
            {
                radGridView1.Columns[0].HeaderText = "کد عضویت";
                radGridView1.Columns[1].HeaderText = "نام";
                radGridView1.Columns[2].HeaderText = "نام خانوادگی";
                radGridView1.Columns[3].HeaderText = "مبلغ";

                radGridView1.Columns[3].FormatString = "{0:#,##0}";
              

                radGridView1.Columns[0].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[1].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[2].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[3].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;



            }
            return true;
        }

        private void RunExportToExcelML(string fileName, ref bool openExportFile)
        {
            
            ExportToExcelML excelExporter = new ExportToExcelML(this.radGridView1);

                excelExporter.SheetName = "....";

          
            // excelExporter.SummariesExportOption = ExportAll;
          
            //set max sheet rows
                      
            //set exporting visual settings
            excelExporter.ExportVisualSettings = true;

            try
            {
                excelExporter.RunExport(fileName);

                //RadMessageBox.SetThemeName(this.radGridView1.ThemeName);
                DialogResult dr = MessageBox.Show("The data in the grid was exported successfully. Do you want to open the file?",
                    "Export to Excel", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    openExportFile = true;
                }
            }
            catch (IOException ex)
            {
                //RadMessageBox.SetThemeName(this.radGridView1.ThemeName);
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
            PrintPreviewDialog dialog = new PrintPreviewDialog();

            radPrintDocument1.RightHeader = "مشاهده مبلغ کل واریزی  به تفکیک اعضا ";
            radPrintDocument1.MiddleHeader = " از تاریخ" + persianDateTimePicker2.Value.ToString("yyyy/MM/dd") + "" + persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
            dialog.Document = this.radPrintDocument1;
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.ShowDialog(); 
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

    

  

        private void button2_Click_1(object sender, EventArgs e)
        {
            loaddata();
        }

 
        }
    }

