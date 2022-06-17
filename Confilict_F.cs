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
    public partial class Confilict_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        public int kind;
        public Confilict_F()
        {
            InitializeComponent();
        }

        private bool loaddata()
        {
            DLUtilsobj.temperory2obj.confilit(comboBox1.Text,(comboBox3.SelectedIndex+1).ToString(),comboBox2.Text,(comboBox4.SelectedIndex+1).ToString(),kind);
            SqlDataReader DataSource;
            DLUtilsobj.temperory2obj.Dbconnset(true);
            DataSource = DLUtilsobj.temperory2obj.temperory2clientdataset.ExecuteReader();
            radGridView1.DataSource = DataSource;
            DLUtilsobj.temperory2obj.Dbconnset(false);

            if (radGridView1.RowCount > 0)
            {

                radGridView1.Columns[0].HeaderText = "کد عضویت ";
                radGridView1.Columns[1].HeaderText = "مبلغ  ";

            
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loaddata();
        }

        private void Confilict_F_Load(object sender, EventArgs e)
        {
            DLUtilsobj = new DLibraryUtils.DLUtils();

            comboBox1.Text = persianDateTimePicker1.Value.Year.ToString();
            comboBox3.SelectedIndex = persianDateTimePicker1.Value.Month - 1;

            comboBox2.Text = persianDateTimePicker1.Value.Year.ToString();
            comboBox4.SelectedIndex = persianDateTimePicker1.Value.Month - 1;
        }
    }
}
