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
using FarsiLibrary.Utils;


namespace familial_bank
{
    public partial class Balance_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public Balance_F()
        {
            InitializeComponent();
        }

        private void Balance_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            //*************
            comboBox1.DisplayMember = "AccNumber";
            comboBox1.ValueMember = "code";
            comboBox1.DataSource = familial_bankEntitiescontext.BankAccs.ToList();

            comboBox1.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label5.Text =  DLUtilsobj.temperoryobj.balance_acc(comboBox1.SelectedValue.ToString(), persianDateTimePicker2.Value.ToString("yyyy/MM/dd"), persianDateTimePicker1.Value.ToString("yyyy/MM/dd"));
            if (label5.Text == string.Empty)
                label5.Text = "0";

            label6.Text = ToWords.ToString(long.Parse(label5.Text))+" ریال";
            label5.Text = string.Format("{0:#,##0}", double.Parse(label5.Text));

        }
    }
}
