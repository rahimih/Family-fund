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
    public partial class Members_view_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;       
        public int usercode, code;
        public string ipadress;
        public Members_view_F()
        {
            InitializeComponent();
        }

        private bool loaddata()
        {
            DLUtilsobj.temperoryobj.membersview();
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
                radGridView1.Columns[4].HeaderText = "کد ملی";
                radGridView1.Columns[5].HeaderText = "تاریخ تولد";
                radGridView1.Columns[6].HeaderText = "شماره شناسنامه";
                radGridView1.Columns[7].HeaderText = "تاریخ عضویت";
                radGridView1.Columns[8].HeaderText = "وضعیت";
                radGridView1.Columns[9].IsVisible = false;
                radGridView1.Columns[10].IsVisible = false;
                radGridView1.Columns[11].IsVisible = false;
                //------------
                radGridView1.Columns[0].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[1].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[2].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[3].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[4].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[5].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[6].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[7].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                                

            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        private void button3_Click_1(object sender, EventArgs e)
        {
            Members_F Members_Frm = new Members_F();
            Members_Frm.usercode = usercode;
            Members_Frm.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (radGridView1.RowCount > 0)
            {
                if (radGridView1.CurrentRow.Cells[8].Value.ToString() == false.ToString())
                {
                    MessageBox.Show("عضو انتخابی غیر فعال می باشد", "خطا", MessageBoxButtons.OK);
                }
                else
                {
                    int a = int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());
                    Member Membertable = familial_bankEntitiescontext.Members.First(i => i.Code == a);
                    if (MessageBox.Show("آیا مطمئن به غیر فعال کردن عضو انتخابی می باشید؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Membertable.Status = false;

                        familial_bankEntitiescontext.SaveChanges();
                        MessageBox.Show("عضو انتخابی غیر فعال گردید", "Information", MessageBoxButtons.OK);
                        DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 8, Environment.MachineName, a);
                        loaddata();

                    }
                }
            }
        }

        private void Members_view_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            //*************
            loaddata();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radGridView1.RowCount > 0)
            {
                if (radGridView1.CurrentRow.Cells[8].Value.ToString() == true.ToString())
                {
                    MessageBox.Show("عضو انتخابی  فعال می باشد", "خطا", MessageBoxButtons.OK);
                }
                else
                {
                    int a = int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());
                    Member Membertable = familial_bankEntitiescontext.Members.First(i => i.Code == a);
                    if (MessageBox.Show("آیا مطمئن به فعال کردن عضو انتخابی می باشید؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Membertable.Status = true;
                        //Membertable.MainMemberShip = 0;
                        //Membertable.Relation = 1;

                        familial_bankEntitiescontext.SaveChanges();
                        MessageBox.Show("عضو انتخابی فعال گردید", "Information", MessageBoxButtons.OK);
                        DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 9, Environment.MachineName, a);
                        loaddata();

                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (radGridView1.RowCount > 0)
            {
                Members_F Members_Frm = new Members_F();
                Members_Frm.code = int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());
                Members_Frm.textBox1.Text = radGridView1.CurrentRow.Cells[0].Value.ToString();
                Members_Frm.textBox2.Text = radGridView1.CurrentRow.Cells[1].Value.ToString();
                Members_Frm.textBox3.Text = radGridView1.CurrentRow.Cells[2].Value.ToString();
                Members_Frm.textBox6.Text = radGridView1.CurrentRow.Cells[3].Value.ToString();
                Members_Frm.textBox5.Text = radGridView1.CurrentRow.Cells[4].Value.ToString();
                Members_Frm.textBox4.Text = radGridView1.CurrentRow.Cells[6].Value.ToString();
                if (radGridView1.CurrentRow.Cells[5].Value.ToString()!="")
                Members_Frm.persianDateTimePicker1.Value = DLUtilsobj.temperoryobj.shamsitomiladi(radGridView1.CurrentRow.Cells[5].Value.ToString());
                if (radGridView1.CurrentRow.Cells[7].Value.ToString() != "")
                Members_Frm.persianDateTimePicker2.Value = DLUtilsobj.temperoryobj.shamsitomiladi(radGridView1.CurrentRow.Cells[7].Value.ToString());

                if (radGridView1.CurrentRow.Cells[8].Value.ToString() == true.ToString())
                    Members_Frm.comboBox3.SelectedIndex = 0;
                else
                    Members_Frm.comboBox3.SelectedIndex = 1;

                if (radGridView1.CurrentRow.Cells[9].Value.ToString() == true.ToString())
                    Members_Frm.comboBox1.SelectedIndex = 0;
                else
                    Members_Frm.comboBox1.SelectedIndex = 1;

                Members_Frm.textBox7.Text = radGridView1.CurrentRow.Cells[11].Value.ToString();
                Members_Frm.comboBox2.SelectedIndex = int.Parse(radGridView1.CurrentRow.Cells[10].Value.ToString())-1;
                

                //*****************
                Members_Frm.editmode = true;
                Members_Frm.button6.Enabled = true;
                Members_Frm.button3.Enabled = false;
                Members_Frm.ShowDialog();
                loaddata();


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PayoffMembers_F PayoffMembers_Frm = new PayoffMembers_F();
            PayoffMembers_Frm.usercode = usercode;
            PayoffMembers_Frm.label11.Text = radGridView1.CurrentRow.Cells[0].Value.ToString();
            PayoffMembers_Frm.code = int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());
            PayoffMembers_Frm.label2.Text = radGridView1.CurrentRow.Cells[1].Value.ToString() + " " + radGridView1.CurrentRow.Cells[2].Value.ToString();
            //------------- مانده حساب
            PayoffMembers_Frm.label16.Text = DLUtilsobj.temperoryobj.balancemembers(PayoffMembers_Frm.label11.Text);
            PayoffMembers_Frm.stockavg = PayoffMembers_Frm.label16.Text;

            if (PayoffMembers_Frm.stockavg == string.Empty)
                PayoffMembers_Frm.stockavg = "0";
            
            if (PayoffMembers_Frm.label16.Text == string.Empty)
                PayoffMembers_Frm.label16.Text = "0";
            PayoffMembers_Frm.label16.Text = string.Format("{0:#,##0}", double.Parse(PayoffMembers_Frm.label16.Text));
            //----------------مانده وام
            PayoffMembers_Frm.label3.Text = DLUtilsobj.temperory2obj.payoffdebt(PayoffMembers_Frm.label11.Text);
            PayoffMembers_Frm.payoffdebt = PayoffMembers_Frm.label3.Text;
            if  (PayoffMembers_Frm.label3.Text!="0")
            PayoffMembers_Frm.label3.Text = string.Format("{0:#,##0}", double.Parse(PayoffMembers_Frm.label3.Text));            
            PayoffMembers_Frm.label7.Text = (double.Parse(PayoffMembers_Frm.stockavg) - double.Parse(PayoffMembers_Frm.payoffdebt)).ToString();
            PayoffMembers_Frm.textBox1.Text = PayoffMembers_Frm.label7.Text;
            if ((double.Parse(PayoffMembers_Frm.label7.Text)) > 0)
            PayoffMembers_Frm.label6.Text= "بستانکار";
            else
            PayoffMembers_Frm.label6.Text= "بدهکار";

            PayoffMembers_Frm.ShowDialog();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            PayoffDebtpayment_F PayoffDebtpayment_Frm = new PayoffDebtpayment_F();
            PayoffDebtpayment_Frm.usercode = usercode;
            PayoffDebtpayment_Frm.label11.Text = radGridView1.CurrentRow.Cells[0].Value.ToString();
            PayoffDebtpayment_Frm.code = int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());
            PayoffDebtpayment_Frm.label2.Text = radGridView1.CurrentRow.Cells[1].Value.ToString() + " " + radGridView1.CurrentRow.Cells[2].Value.ToString();
            //----------------مانده وام
            PayoffDebtpayment_Frm.label3.Text = DLUtilsobj.temperory2obj.payoffdebt(PayoffDebtpayment_Frm.label11.Text);
            PayoffDebtpayment_Frm.payoffdebt = PayoffDebtpayment_Frm.label3.Text;
            if (PayoffDebtpayment_Frm.label3.Text != "0")
                PayoffDebtpayment_Frm.label3.Text = string.Format("{0:#,##0}", double.Parse(PayoffDebtpayment_Frm.label3.Text));
            // convert to word
            PayoffDebtpayment_Frm.label10.Text = ToWords.ToString(long.Parse(PayoffDebtpayment_Frm.payoffdebt));

            PayoffDebtpayment_Frm.label7.Text = PayoffDebtpayment_Frm.label3.Text;
            PayoffDebtpayment_Frm.textBox1.Text = PayoffDebtpayment_Frm.label7.Text;
            if ((double.Parse(PayoffDebtpayment_Frm.label7.Text)) > 0)
                PayoffDebtpayment_Frm.label6.Text = "بدهکار";                
            else
               PayoffDebtpayment_Frm.label6.Text = "بستانکار";

            PayoffDebtpayment_Frm.kindd = 1;
            PayoffDebtpayment_Frm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Report_payment_F Report_payment_Frm = new Report_payment_F();
            Report_payment_Frm.ipadress = ipadress;
            Report_payment_Frm.memberscode = radGridView1.CurrentRow.Cells[0].Value.ToString();


            Report_payment_Frm.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {

            radGridView1.Columns[3].IsVisible = false;
            radGridView1.Columns[4].IsVisible = false;
            radGridView1.Columns[5].IsVisible = false;
            radGridView1.Columns[6].IsVisible = false;
            radGridView1.Columns[7].IsVisible = false;
            radGridView1.Columns[8].IsVisible = false;

            PrintPreviewDialog dialog = new PrintPreviewDialog();

            radPrintDocument1.RightHeader = "   لیست اعضا ";            
            dialog.Document = this.radPrintDocument1;
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.ShowDialog();

            //--------------
            radGridView1.Columns[3].IsVisible = true;
            radGridView1.Columns[4].IsVisible = true;
            radGridView1.Columns[5].IsVisible = true;
            radGridView1.Columns[6].IsVisible = true;
            radGridView1.Columns[7].IsVisible = true;
            radGridView1.Columns[8].IsVisible = true;



        }
    }
}
