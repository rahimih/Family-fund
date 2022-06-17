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
    public partial class MonthPayment_View_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public int usercode, code, usercodetemp;

        public MonthPayment_View_F()
        {
            InitializeComponent();
        }

        private bool loaddata()
        {
            DLUtilsobj.temperoryobj.MonthPaymentview();
            SqlDataReader DataSource;
            DLUtilsobj.temperoryobj.Dbconnset(true);
            DataSource = DLUtilsobj.temperoryobj.temperoryclientdataset.ExecuteReader();
            radGridView1.DataSource = DataSource;
            DLUtilsobj.temperoryobj.Dbconnset(false);

            if (radGridView1.RowCount > 0)
            {
                radGridView1.Columns[0].HeaderText = "کد ";
                radGridView1.Columns[1].HeaderText = "شرح";
                radGridView1.Columns[2].HeaderText = " از تاریخ";
                radGridView1.Columns[3].HeaderText = " تا تاریخ";
                radGridView1.Columns[4].HeaderText = "سال";
                radGridView1.Columns[5].HeaderText = "مبلغ ";

                radGridView1.Columns[5].FormatString = "{0:#,##0}";

            }
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //-----------تعریف مبلغ پرداختی ماهیانه
            MonthPayment_F MonthPayment_Frm = new MonthPayment_F();
            MonthPayment_Frm.usercode = usercodetemp;
            MonthPayment_Frm.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (button3.Enabled == true)
                button3_Click(toolStripMenuItem1, e);

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (button6.Enabled == true)
                button6_Click(toolStripMenuItem4, e);
        }

        private void MonthPayment_View_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            //*************
            loaddata();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (radGridView1.RowCount > 0)
            {
                MonthPayment_F MonthPayment_Frm = new MonthPayment_F();

                MonthPayment_Frm.code = int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());
                MonthPayment_Frm.textBox4.Text = radGridView1.CurrentRow.Cells[0].Value.ToString();
                MonthPayment_Frm.textBox1.Text = radGridView1.CurrentRow.Cells[1].Value.ToString();
                if (radGridView1.CurrentRow.Cells[2].Value.ToString() != "")
                    MonthPayment_Frm.persianDateTimePicker1.Value = DLUtilsobj.temperoryobj.shamsitomiladi(radGridView1.CurrentRow.Cells[2].Value.ToString());
                if (radGridView1.CurrentRow.Cells[3].Value.ToString() != "")
                    MonthPayment_Frm.persianDateTimePicker2.Value = DLUtilsobj.temperoryobj.shamsitomiladi(radGridView1.CurrentRow.Cells[3].Value.ToString());
                MonthPayment_Frm.textBox3.Text = radGridView1.CurrentRow.Cells[4].Value.ToString();
                MonthPayment_Frm.textBox2.Text = radGridView1.CurrentRow.Cells[5].Value.ToString();

                //*****************
                MonthPayment_Frm.editmode = true;
                MonthPayment_Frm.button6.Enabled = true;
                MonthPayment_Frm.button3.Enabled = false;
                MonthPayment_Frm.ShowDialog();
                loaddata();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radGridView1.RowCount > 0)
            {
                int editcode = int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());
                MonthPayment MonthPaymenttable = familial_bankEntitiescontext.MonthPayments.First(i => i.Code == editcode);
                if (MessageBox.Show("رکورد انتخابی حذف گردد؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MonthPaymenttable.Deleted = true;
                    familial_bankEntitiescontext.SaveChanges();
                    MessageBox.Show("رکورد انتخابی حذف گردید", "Information", MessageBoxButtons.OK);
                    DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 25, Environment.MachineName, editcode);
                    loaddata();
                }
            }
        }
    }
}
