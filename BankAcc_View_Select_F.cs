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
    public partial class BankAcc_View_Select_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        public string returncode="0";
        public string returnname;
        public string returnbankname,returnbranch,returnacc;
        public byte openform;

        public BankAcc_View_Select_F()
        {
            InitializeComponent();
        }

        private bool loaddata_jari()
        {
            DLUtilsobj.temperoryobj.viewAcc_select_jari();
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

                

            }
            return true;
        }

        private bool loaddata_all()
        {
            DLUtilsobj.temperoryobj.viewAcc_select_all();
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



            }
            return true;
        }
        private void BankAcc_View_Select_F_Load(object sender, EventArgs e)
        {
            DLUtilsobj = new DLibraryUtils.DLUtils();
            if (openform==1)
            loaddata_jari();
            if (openform == 2)
                loaddata_all();

            returncode = "0";
        }

        private void radGridView1_DoubleClick(object sender, EventArgs e)
        {
            returncode = radGridView1.CurrentRow.Cells[0].Value.ToString();
            returnname = radGridView1.CurrentRow.Cells[2].Value.ToString();
            returnacc = radGridView1.CurrentRow.Cells[1].Value.ToString();
            returnbankname = radGridView1.CurrentRow.Cells[3].Value.ToString();
            returnbranch = radGridView1.CurrentRow.Cells[4].Value.ToString();
           
            //----------------
            this.Close();
        }
    }
}
