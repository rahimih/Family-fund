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
    public partial class MembersView_Select_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        public string returncode="0";
        public string returnname;
        public string statuse="1";
        public MembersView_Select_F()
        {
            InitializeComponent();
        }

        private bool loaddata()
        {
            if (statuse=="1")
            DLUtilsobj.temperoryobj.membersview_select_active();
            if (statuse=="2")
            DLUtilsobj.temperoryobj.membersview_select_total();

            SqlDataReader DataSource;
            DLUtilsobj.temperoryobj.Dbconnset(true);
            DataSource = DLUtilsobj.temperoryobj.temperoryclientdataset.ExecuteReader();
            radGridView1.DataSource = DataSource;
            DLUtilsobj.temperoryobj.Dbconnset(false);

            if (radGridView1.RowCount > 0)
            {
                radGridView1.Columns[0].HeaderText = "کد ";
                radGridView1.Columns[1].HeaderText = "نام";
                radGridView1.Columns[2].HeaderText = "نام خانوادگی";
                radGridView1.Columns[3].HeaderText = "نام پدر ";
                

            }
            return true;
        }
        private void MembersView_Select_F_Load(object sender, EventArgs e)
        {
            DLUtilsobj = new DLibraryUtils.DLUtils();
            loaddata();   
        }

        private void radGridView1_DoubleClick(object sender, EventArgs e)
        {
            returncode = radGridView1.CurrentRow.Cells[0].Value.ToString();
            returnname = radGridView1.CurrentRow.Cells[1].Value.ToString() + " " + radGridView1.CurrentRow.Cells[2].Value.ToString();
           
            //----------------
            this.Close();
        }
    }
}
