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
    public partial class DebtCreditAcc_bill_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;            
        bool openExportFile = false;


        public DebtCreditAcc_bill_F()
        {
            InitializeComponent();
        }
   

        private bool loaddate()
        {
            DLUtilsobj.temperory2obj.debtcreditaacview_v(comboBox1.SelectedValue.ToString(), persianDateTimePicker2.Value.ToString("yyyy/MM/dd"), persianDateTimePicker1.Value.ToString("yyyy/MM/dd"), 1);
               SqlDataReader DataSource2;
               DLUtilsobj.temperory2obj.Dbconnset(true);
               DataSource2 = DLUtilsobj.temperory2obj.temperory2clientdataset.ExecuteReader();
               radGridView2.DataSource = DataSource2;
               DLUtilsobj.temperory2obj.Dbconnset(false);

               if (radGridView2.RowCount > 0)
               {                 
                   radGridView2.Columns[0].HeaderText = "کد";
                   radGridView2.Columns[1].HeaderText = "تاریخ ";
                   radGridView2.Columns[2].HeaderText = " ساعت";
                   radGridView2.Columns[3].IsVisible = true;
                   radGridView2.Columns[4].IsVisible = true;
                   radGridView2.Columns[3].HeaderText = " مبلغ واریز";
                   radGridView2.Columns[4].HeaderText = " مبلغ برداشت";
                   radGridView2.Columns[5].HeaderText = " شماره فیش/چک";
                   radGridView2.Columns[6].HeaderText = " شرح";
                   radGridView2.Columns[7].HeaderText = " بابت";
                   radGridView2.Columns[8].IsVisible = false;

                   radGridView2.Columns[3].FormatString = "{0:#,##0}";
                   radGridView2.Columns[4].FormatString = "{0:#,##0}";

                   radGridView2.Columns[0].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                   radGridView2.Columns[1].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                   radGridView2.Columns[2].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                   radGridView2.Columns[3].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                   radGridView2.Columns[4].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                   radGridView2.Columns[5].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                   radGridView2.Columns[6].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                   radGridView2.Columns[7].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;                 
              
               }
               return true;
               }

        private void RunExportToExcelML(string fileName, ref bool openExportFile)
        {

            ExportToExcelML excelExporter = new ExportToExcelML(this.radGridView2);

            excelExporter.SheetName = (" ریز کارکرد حساب " + comboBox1.Text); 


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

 
        private void PaymentMembers_View_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            //*************
            comboBox1.DisplayMember = "AccNumber";
            comboBox1.ValueMember = "code";
            comboBox1.DataSource = familial_bankEntitiescontext.BankAccs.ToList();

            comboBox1.SelectedIndex = 0;
           
            loaddate();

        }


   
        private void button4_Click(object sender, EventArgs e)
        {           
            loaddate();

        }
         

        private void button7_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog dialog = new PrintPreviewDialog();

            radPrintDocument1.RightHeader = " ریز کارکرد حساب";
            radPrintDocument1.MiddleHeader = " از تاریخ" + persianDateTimePicker2.Value.ToString("yyyy/MM/dd") + "" + persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
            dialog.Document = this.radPrintDocument1;
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.ShowDialog(); 
        }

    



    }

}