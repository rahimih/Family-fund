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
    public partial class RequestCheck_View_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        public int usercode, code, usercodetemp;

        public RequestCheck_View_F()
        {
            InitializeComponent();
        }

        private bool loaddata()
        {
            DLUtilsobj.temperoryobj.requsetcheckview();
            SqlDataReader DataSource;
            DLUtilsobj.temperoryobj.Dbconnset(true);
            DataSource = DLUtilsobj.temperoryobj.temperoryclientdataset.ExecuteReader();
            radGridView1.DataSource = DataSource;
            DLUtilsobj.temperoryobj.Dbconnset(false);

            if (radGridView1.RowCount > 0)
            {
                radGridView1.Columns[0].HeaderText = "ردیف";
                radGridView1.Columns[1].HeaderText = "شماره حساب";
                radGridView1.Columns[2].HeaderText = "شرح";
                radGridView1.Columns[3].HeaderText = "بانک";
                radGridView1.Columns[4].HeaderText = "شعبه";
                radGridView1.Columns[5].HeaderText = "تعداد ";
                radGridView1.Columns[6].HeaderText = "از شماره";
                radGridView1.Columns[7].HeaderText = "تا شماره";
                radGridView1.Columns[8].HeaderText = "تاریخ دریافت";
                radGridView1.Columns[9].HeaderText = "وضعیت";
                radGridView1.Columns[10].IsVisible = false;
                radGridView1.Columns[11].IsVisible = false;
                radGridView1.Columns[12].IsVisible = false;
                radGridView1.Columns[13].IsVisible = false;


                radGridView1.Columns[0].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[1].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[2].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[3].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[4].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[5].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[6].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[7].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[8].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;




            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RequestCheck_F RequestCheck_Frm = new RequestCheck_F();
            RequestCheck_Frm.usercode = usercode;
            RequestCheck_Frm.ShowDialog();
        }

        private void RequestCheck_View_F_Load(object sender, EventArgs e)
        {
            DLUtilsobj = new DLibraryUtils.DLUtils();
            //*************
            loaddata();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            RequestCheck_F RequestCheck_Frm = new RequestCheck_F();
            RequestCheck_Frm.returncodeacc = radGridView1.CurrentRow.Cells[10].Value.ToString();
            RequestCheck_Frm.textBox6.Text = radGridView1.CurrentRow.Cells[1].Value.ToString();
            RequestCheck_Frm.label10.Text = radGridView1.CurrentRow.Cells[3].Value.ToString();
            RequestCheck_Frm.label12.Text = radGridView1.CurrentRow.Cells[4].Value.ToString();
            RequestCheck_Frm.numericUpDown1.Value = byte.Parse(radGridView1.CurrentRow.Cells[5].Value.ToString());
            RequestCheck_Frm.numericUpDown2.Value = int.Parse(radGridView1.CurrentRow.Cells[6].Value.ToString());
            RequestCheck_Frm.numericUpDown3.Value = int.Parse(radGridView1.CurrentRow.Cells[7].Value.ToString());
            RequestCheck_Frm.persianDateTimePicker1.Value = DLUtilsobj.temperoryobj.shamsitomiladi(radGridView1.CurrentRow.Cells[8].Value.ToString());
            RequestCheck_Frm.textBox1.Text = radGridView1.CurrentRow.Cells[12].Value.ToString() + ' ' + radGridView1.CurrentRow.Cells[13].Value.ToString();
            RequestCheck_Frm.transfeecode = radGridView1.CurrentRow.Cells[11].Value.ToString();
            RequestCheck_Frm.editcode = int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());

            RequestCheck_Frm.button3.Enabled = false;
            RequestCheck_Frm.ShowDialog();
            loaddata();
        }
    }
}
