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
    public partial class DebtCreditAcc_view_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public int usercode, code;
        public string countt, summ;
        public byte kindview;
        bool entermode=false;
        bool openExportFile = false;
        

        public DebtCreditAcc_view_F()
        {
            InitializeComponent();
        }

        private bool loaddata()
        {
            DLUtilsobj.temperoryobj.viewAcc();
            SqlDataReader DataSource;
            DLUtilsobj.temperoryobj.Dbconnset(true);
            DataSource = DLUtilsobj.temperoryobj.temperoryclientdataset.ExecuteReader();
            radGridView1.DataSource = DataSource;
            DLUtilsobj.temperoryobj.Dbconnset(false);

            if (radGridView1.RowCount > 0)
            {
                radGridView1.Columns[0].HeaderText = "کد ";
                radGridView1.Columns[1].HeaderText = "شماره حساب";
                radGridView1.Columns[2].HeaderText = "شرح حساب";
                radGridView1.Columns[3].HeaderText = "بانک";
                radGridView1.Columns[4].HeaderText = "شعبه ";
                radGridView1.Columns[5].HeaderText = "کد شعبه ";
                radGridView1.Columns[6].HeaderText = " نوع حساب";
                radGridView1.Columns[7].HeaderText = " وضعیت";
                radGridView1.Columns[8].IsVisible = false;
                radGridView1.Columns[9].IsVisible = false;
            }
            return true;
        }

        private bool loaddate2()
        {
            DLUtilsobj.temperory2obj.debtcreditaacview_v(radGridView1.CurrentRow.Cells[0].Value.ToString(), persianDateTimePicker2.Value.ToString("yyyy/MM/dd"), persianDateTimePicker1.Value.ToString("yyyy/MM/dd"), kindview);
               SqlDataReader DataSource2;
               DLUtilsobj.temperory2obj.Dbconnset(true);
               DataSource2 = DLUtilsobj.temperory2obj.temperory2clientdataset.ExecuteReader();
               radGridView2.DataSource = DataSource2;
               DLUtilsobj.temperory2obj.Dbconnset(false);

               if (radGridView2.RowCount > 0)
               {
                 if (kindview==1)
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

                 else if (kindview==2)
                 {
                   radGridView2.Columns[0].HeaderText = "کد";
                   radGridView2.Columns[1].HeaderText = "تاریخ ";
                   radGridView2.Columns[2].HeaderText = " ساعت";
                   radGridView2.Columns[3].IsVisible =true;
                   radGridView2.Columns[3].HeaderText = " مبلغ واریز";
                   radGridView2.Columns[4].IsVisible = false;
                   radGridView2.Columns[5].HeaderText = " شماره فیش/چک";
                   radGridView2.Columns[6].HeaderText = " شرح";
                   radGridView2.Columns[7].HeaderText = " بابت";
                   radGridView2.Columns[8].IsVisible = false;

                   radGridView2.Columns[3].FormatString = "{0:#,##0}";

                   radGridView2.Columns[0].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                   radGridView2.Columns[1].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                   radGridView2.Columns[2].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                   radGridView2.Columns[3].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                   radGridView2.Columns[4].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                   radGridView2.Columns[5].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                   radGridView2.Columns[6].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                   radGridView2.Columns[7].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                   
                 }

                 else if (kindview==3)
                 {
                   radGridView2.Columns[0].HeaderText = "کد";
                   radGridView2.Columns[1].HeaderText = "تاریخ ";
                   radGridView2.Columns[2].HeaderText = " ساعت";
                   radGridView2.Columns[3].IsVisible = false;
                   radGridView2.Columns[4].IsVisible = true;
                   radGridView2.Columns[4].HeaderText = " مبلغ برداشت";
                   radGridView2.Columns[5].HeaderText = " شماره فیش/چک";
                   radGridView2.Columns[6].HeaderText = " شرح";
                   radGridView2.Columns[7].HeaderText = " بابت";
                   radGridView2.Columns[8].IsVisible = false;

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
               }
               return true;
               }

        private void RunExportToExcelML(string fileName, ref bool openExportFile)
        {

            ExportToExcelML excelExporter = new ExportToExcelML(this.radGridView2);

            excelExporter.SheetName = (radGridView1.CurrentRow.Cells[1].Value.ToString()); 


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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void PaymentMembers_View_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            //*************
            loaddata();
            //loaddate2();

        }

        private void radGridView1_SelectionChanging(object sender, Telerik.WinControls.UI.GridViewSelectionCancelEventArgs e)
        {
            if (entermode==true)
            loaddate2();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (radGridView2.RowCount > 0)
            {
                DebtCreditAcc_F DebtCreditAcc_Frm = new DebtCreditAcc_F();                
                DebtCreditAcc_Frm.returncodeacc = radGridView1.CurrentRow.Cells[0].Value.ToString();
                DebtCreditAcc_Frm.textBox6.Text = (radGridView1.CurrentRow.Cells[1].Value.ToString());
                DebtCreditAcc_Frm.label10.Text = (radGridView1.CurrentRow.Cells[3].Value.ToString());
                DebtCreditAcc_Frm.label12.Text = (radGridView1.CurrentRow.Cells[4].Value.ToString());
                DebtCreditAcc_Frm.persianDateTimePicker1.Value = DLUtilsobj.temperoryobj.shamsitomiladi(radGridView2.CurrentRow.Cells[1].Value.ToString());
               // DebtCreditAcc_Frm.maskedTextBox1.Text = (radGridView2.CurrentRow.Cells[2].Value.ToString());
                if ((kindview == 1) && (int.Parse(radGridView2.CurrentRow.Cells[3].Value.ToString()) > 0))
                {
                    DebtCreditAcc_Frm.textBox2.Text = (radGridView2.CurrentRow.Cells[3].Value.ToString());
                    DebtCreditAcc_Frm.kindtemp = 1;
                    DebtCreditAcc_Frm.kind = 1;

                }
                if ((kindview == 1) && (int.Parse(radGridView2.CurrentRow.Cells[4].Value.ToString()) > 0))
                {
                    DebtCreditAcc_Frm.textBox2.Text = (radGridView2.CurrentRow.Cells[4].Value.ToString());
                    DebtCreditAcc_Frm.kindtemp = 2;
                    DebtCreditAcc_Frm.kind = 2;
                }
                if (kindview == 2)
                {
                    DebtCreditAcc_Frm.textBox2.Text = (radGridView2.CurrentRow.Cells[3].Value.ToString());
                    DebtCreditAcc_Frm.kindtemp = 1;
                    DebtCreditAcc_Frm.kind = 1;
                }
                if (kindview == 3)
                {
                    DebtCreditAcc_Frm.textBox2.Text = (radGridView2.CurrentRow.Cells[4].Value.ToString());
                    DebtCreditAcc_Frm.kindtemp = 2;
                    DebtCreditAcc_Frm.kind = 2;
                }
                DebtCreditAcc_Frm.textBox3.Text = (radGridView2.CurrentRow.Cells[5].Value.ToString());
                DebtCreditAcc_Frm.textBox1.Text = (radGridView2.CurrentRow.Cells[6].Value.ToString());
                DebtCreditAcc_Frm.kinde = byte.Parse(radGridView2.CurrentRow.Cells[8].Value.ToString());
                DebtCreditAcc_Frm.code = int.Parse(radGridView2.CurrentRow.Cells[0].Value.ToString());
                DebtCreditAcc_Frm.editmode = true;
                DebtCreditAcc_Frm.button3.Enabled = false;
                DebtCreditAcc_Frm.button6.Enabled = true;
                DebtCreditAcc_Frm.ShowDialog();
                button4_Click(button6, e);

            }

        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (radGridView2.RowCount > 0)
            {
                if (MessageBox.Show("رکورد انتخابی حذف گردد؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int editcode = (int.Parse(radGridView2.CurrentRow.Cells[0].Value.ToString()));
                    DLUtilsobj.temperoryobj.debtcreditaacdelete(radGridView2.CurrentRow.Cells[0].Value.ToString());
                    MessageBox.Show("رکورد انتخابی حذف گردید", "Information", MessageBoxButtons.OK);
                    DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 33, Environment.MachineName, editcode);
                    button4_Click(button2, e);
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
                kindview = 1;
            else if (radioButton1.Checked == true)
                kindview = 2;
            else if (radioButton2.Checked == true)
                kindview = 3;

            loaddate2();

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
                kindview = 1;
            else if (radioButton1.Checked == true)
                kindview = 2;
            else if (radioButton2.Checked == true)
                kindview = 3;

            loaddate2();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
                kindview = 1;
            else if (radioButton1.Checked == true)
                kindview = 2;
            else if (radioButton2.Checked == true)
                kindview = 3;

            loaddate2();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
                kindview = 1;
            else if (radioButton1.Checked == true)
                kindview = 2;
            else if (radioButton2.Checked == true)
                kindview = 3;

            loaddate2();
        }

        private void radGridView1_Enter(object sender, EventArgs e)
        {
            entermode = true;
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
            //radPrintDocument1.MiddleHeader = "سال" + comboBox1.Text + "  ماه " + comboBox3.Text + " تاریخ " + persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
            dialog.Document = this.radPrintDocument1;
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.ShowDialog(); 
        }

    



    }

}