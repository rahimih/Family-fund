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
    public partial class Report_DebtPayment_status_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        bool openExportFile = false;
        public string memberscode, returnstring;
        public Report_DebtPayment_status_F()
        {
            InitializeComponent();
        }

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

        private void RunExportToExcelML(string fileName, ref bool openExportFile)
        {

            ExportToExcelML excelExporter = new ExportToExcelML(this.radGridView1);

            excelExporter.SheetName = label11.Text;


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

        private void button5_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog dialog = new PrintPreviewDialog();

            radPrintDocument1.RightHeader = "مشاهده وضعیت اقساط پرداختی";
            radPrintDocument1.MiddleHeader = " " + label11.Text;
            dialog.Document = this.radPrintDocument1;
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.ShowDialog(); 
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

        private void button2_Click(object sender, EventArgs e)
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

