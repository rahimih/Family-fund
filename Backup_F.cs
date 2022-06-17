using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using DLibraryUtils;


namespace familial_bank
{
    public partial class Backup_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        public Backup_F()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            saveFileDialog1.Filter = "Backup files (*.bak)|*.bak";

            //Empty the FileName text box of the dialog

            saveFileDialog1.FileName = String.Empty;

            //Set default extension as .txt

            saveFileDialog1.DefaultExt = ".bak";

            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.FilterIndex = 1;
            string DateDay = FarsiLibrary.Utils.PersianDate.Now.ToString().Substring(0, 10);

            saveFileDialog1.FileName = DateDay.Replace("/", "") + "_" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();

            saveFileDialog1.Title = "Backup File";


            //Open the dialog and determine which button was pressed

            DialogResult result = saveFileDialog1.ShowDialog();


            //If the user presses the Save button

            if (result == DialogResult.OK)
            {

                //Create a file stream using the file name

                // FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                textBox1.Text = saveFileDialog1.FileName;

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            if (textBox1.Text!=string.Empty)
            {

                this.Cursor = Cursors.WaitCursor;
                progressBar1.Value = 25;                
                DLUtilsobj.backupobj.Backup(textBox1.Text);                
                DLUtilsobj.backupobj.Dbconnset(true);
                DLUtilsobj.backupobj.backupingclientdataset.ExecuteNonQuery();
                DLUtilsobj.backupobj.Dbconnset(false);

                progressBar1.Value = 50;                
                this.Cursor = Cursors.Default;
                progressBar1.Value = 100;
                MessageBox.Show(" پشتیبان گیری از اطلاعات با موفقیت انجام شد.", "پشتیبان گیری", MessageBoxButtons.OK);
                this.Close();                
            }


        }

        private void Backup_F_Load(object sender, EventArgs e)
        {
            
             DLUtilsobj = new DLibraryUtils.DLUtils();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            if (textBox1.Text != string.Empty)
            {

                this.Cursor = Cursors.WaitCursor;
                progressBar1.Value = 25;
                DLUtilsobj.backupobj.Restore(textBox1.Text);
                DLUtilsobj.backupobj.Dbconnset(true);
                DLUtilsobj.backupobj.backupingclientdataset.ExecuteNonQuery();
                DLUtilsobj.backupobj.Dbconnset(false);

                progressBar1.Value = 50;
                this.Cursor = Cursors.Default;
                progressBar1.Value = 100;
                MessageBox.Show("  بازیابی اطلاعات از نسخه پشتیبان با موفقیت انجام شد.", " بازیابی", MessageBoxButtons.OK);
                this.Close();
                
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Backup files (*.bak)|*.bak";

            //Empty the FileName text box of the dialog
                          

            openFileDialog1.FilterIndex = 1;
            
            openFileDialog1.Title = "Restore File";


            //Open the dialog and determine which button was pressed

            DialogResult result = openFileDialog1.ShowDialog();


            //If the user presses the Save button

            if (result == DialogResult.OK)
            {

                textBox1.Text = openFileDialog1.FileName;

            }
        }

    }
}
