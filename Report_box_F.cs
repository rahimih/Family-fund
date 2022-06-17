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
    public partial class Report_box_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        double sum1, sum2, sum3, sum4,sum5;

        public Report_box_F()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //-----------------
            textBox1.Text = DLUtilsobj.temperory2obj.Reportbox_monthly1(persianDateTimePicker2.Value.ToString("yyyy/MM/dd"), persianDateTimePicker1.Value.ToString("yyyy/MM/dd"));
            if (textBox1.Text == string.Empty)
                textBox1.Text = "0";
            textBox1.Text = string.Format("{0:#,##0}", double.Parse(textBox1.Text));

            textBox2.Text = DLUtilsobj.temperory2obj.Reportbox_monthly2(persianDateTimePicker2.Value.ToString("yyyy/MM/dd"), persianDateTimePicker1.Value.ToString("yyyy/MM/dd"));
            if (textBox2.Text == string.Empty)
                textBox2.Text = "0";
            textBox2.Text = string.Format("{0:#,##0}", double.Parse(textBox2.Text));

            textBox3.Text = DLUtilsobj.temperory2obj.Reportbox_monthly(persianDateTimePicker2.Value.ToString("yyyy/MM/dd"), persianDateTimePicker1.Value.ToString("yyyy/MM/dd"));
            if (textBox3.Text == string.Empty)
                textBox3.Text = "0";
            sum1 = double.Parse(textBox3.Text);
            textBox3.Text = string.Format("{0:#,##0}", double.Parse(textBox3.Text));

            //------------وام
            textBox6.Text = DLUtilsobj.temperory2obj.ReportBox_DebtPayment(persianDateTimePicker2.Value.ToString("yyyy/MM/dd"), persianDateTimePicker1.Value.ToString("yyyy/MM/dd"));
            if (textBox6.Text == string.Empty)
                textBox6.Text = "0";
            textBox5.Text = DLUtilsobj.temperory2obj.ReportBox_DebtPaymentmonth(persianDateTimePicker2.Value.ToString("yyyy/MM/dd"), persianDateTimePicker1.Value.ToString("yyyy/MM/dd"));
            if (textBox5.Text == string.Empty)
                textBox5.Text = "0";
            textBox4.Text = (long.Parse(textBox6.Text) - long.Parse(textBox5.Text)).ToString();
            if (textBox4.Text == string.Empty)
                textBox4.Text = "0";
             sum2 = double.Parse(textBox4.Text);

            textBox8.Text = DLUtilsobj.temperory2obj.ReportBox_debtwage(persianDateTimePicker2.Value.ToString("yyyy/MM/dd"), persianDateTimePicker1.Value.ToString("yyyy/MM/dd"));
            if (textBox8.Text == string.Empty)
                textBox8.Text = "0";
            sum4 = double.Parse(textBox8.Text);

            textBox9.Text = DLUtilsobj.temperory2obj.reportbox_sood(comboBox1.SelectedValue.ToString(), persianDateTimePicker2.Value.ToString("yyyy/MM/dd"), persianDateTimePicker1.Value.ToString("yyyy/MM/dd"));
            if (textBox9.Text == string.Empty)
                textBox9.Text = "0";
            sum3 = double.Parse(textBox9.Text);


            textBox12.Text = (sum1 - sum2).ToString();
            textBox13.Text = (sum3 + sum4).ToString();


            textBox6.Text = string.Format("{0:#,##0}", double.Parse(textBox6.Text));
            textBox5.Text = string.Format("{0:#,##0}", double.Parse(textBox5.Text));
            textBox4.Text = string.Format("{0:#,##0}", double.Parse(textBox4.Text));
            textBox8.Text = string.Format("{0:#,##0}", double.Parse(textBox8.Text));
            textBox9.Text = string.Format("{0:#,##0}", double.Parse(textBox9.Text));
            textBox12.Text = string.Format("{0:#,##0}", double.Parse(textBox12.Text));
            textBox13.Text = string.Format("{0:#,##0}", double.Parse(textBox13.Text));



            //-------------بانک
            textBox7.Text = DLUtilsobj.temperoryobj.balance_acc(comboBox1.SelectedValue.ToString(), persianDateTimePicker2.Value.ToString("yyyy/MM/dd"), persianDateTimePicker1.Value.ToString("yyyy/MM/dd"));
            if (textBox7.Text == string.Empty)
                textBox7.Text = "0";

            label11.Text = ToWords.ToString(long.Parse(textBox7.Text)) + " ریال";
            textBox7.Text = string.Format("{0:#,##0}", double.Parse(textBox7.Text));


            //------------- موجودی کل حسابهای بانکی
            textBox10.Text = DLUtilsobj.temperoryobj.balance_acc_total(persianDateTimePicker2.Value.ToString("yyyy/MM/dd"), persianDateTimePicker1.Value.ToString("yyyy/MM/dd"));
            if (textBox10.Text == string.Empty)
                textBox10.Text = "0";
            sum5 = double.Parse(textBox10.Text);
            
            textBox10.Text = string.Format("{0:#,##0}", double.Parse(textBox10.Text));

            
            textBox11.Text = ((sum1 - sum2) + sum3 + sum4).ToString();
            textBox11.Text = string.Format("{0:#,##0}", double.Parse(textBox11.Text));
            label17.Text = ((sum5) - ((sum1 - sum2) + sum3 + sum4)).ToString();
            label17.Text = string.Format("{0:#,##0}", double.Parse(label17.Text));

        }

        private void Report_box_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            //*************
            comboBox1.DisplayMember = "AccNumber";
            comboBox1.ValueMember = "code";
            comboBox1.DataSource = familial_bankEntitiescontext.BankAccs.ToList();

            comboBox1.SelectedIndex = 0;
        }
    }
}
