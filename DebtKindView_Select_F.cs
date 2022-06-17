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
    public partial class DebtKindView_Select_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        public string returncode="0";
        public string returnname,returncounta;
        public string vamcash, ghestcash,datep;
        public int memberscode;
        public DebtKindView_Select_F()
        {
            InitializeComponent();
        }

        public bool loaddata(int memberscode)
        {
            DLUtilsobj.temperoryobj.DebtKIndview_select(memberscode);
            SqlDataReader DataSource;
            DLUtilsobj.temperoryobj.Dbconnset(true);
            DataSource = DLUtilsobj.temperoryobj.temperoryclientdataset.ExecuteReader();
            radGridView1.DataSource = DataSource;
            DLUtilsobj.temperoryobj.Dbconnset(false);

            if (radGridView1.RowCount > 0)
            {
                radGridView1.Columns[0].HeaderText = "کد ";
                radGridView1.Columns[1].HeaderText = "شرح";
                radGridView1.Columns[2].HeaderText = " تعداد اقساط";
                radGridView1.Columns[3].HeaderText = "مبلغ وام";
                radGridView1.Columns[4].HeaderText = " مبلغ قسط";
                radGridView1.Columns[5].IsVisible = false;
                

            }
            return true;
        }
        private void MembersView_Select_F_Load(object sender, EventArgs e)
        {
            DLUtilsobj = new DLibraryUtils.DLUtils();
            loaddata(memberscode);   
        }

        private void radGridView1_DoubleClick(object sender, EventArgs e)
        {
            returncode = radGridView1.CurrentRow.Cells[0].Value.ToString();
            returnname = radGridView1.CurrentRow.Cells[1].Value.ToString();
            returncounta = radGridView1.CurrentRow.Cells[2].Value.ToString();
            vamcash = radGridView1.CurrentRow.Cells[3].Value.ToString();
            ghestcash = radGridView1.CurrentRow.Cells[4].Value.ToString();
            datep = radGridView1.CurrentRow.Cells[5].Value.ToString();       
           
            //----------------
            this.Close();
        }
    }
}
