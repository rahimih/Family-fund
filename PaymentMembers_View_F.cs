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
    public partial class PaymentMembers_View_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public int usercode, code;

        public PaymentMembers_View_F()
        {
            InitializeComponent();
        }

        private bool loaddata()
        {
            DLUtilsobj.temperoryobj.membersactiveview();
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
                radGridView1.Columns[8].IsVisible = false;
                radGridView1.Columns[9].IsVisible = false;
                radGridView1.Columns[10].IsVisible = false;

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

        private void button3_Click(object sender, EventArgs e)
        {
            PaymentMembers_F PaymentMembers_Frm = new PaymentMembers_F();
            PaymentMembers_Frm.usercode = usercode;
            PaymentMembers_Frm.kind = 2;
            //PaymentMembers_Frm.maskedTextBox1.Text = DateTime.Now.ToShortTimeString();
            PaymentMembers_Frm.ShowDialog();
        }

        private void PaymentMembers_View_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            //*************
            loaddata();

        }

        private void radGridView1_SelectionChanging(object sender, Telerik.WinControls.UI.GridViewSelectionCancelEventArgs e)
        {
            if (radGridView1.RowCount > 0)
            {

            DLUtilsobj.temperory2obj.PaymentMembersview(int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString()));
            SqlDataReader DataSource2;
            DLUtilsobj.temperory2obj.Dbconnset(true);
            DataSource2 = DLUtilsobj.temperory2obj.temperory2clientdataset.ExecuteReader();
            radGridView2.DataSource = DataSource2;
            DLUtilsobj.temperory2obj.Dbconnset(false);

            if (radGridView2.RowCount > 0)
            {
                radGridView2.Columns[0].HeaderText = "کد";
                radGridView2.Columns[1].HeaderText = "تاریخ پرداخت";
                radGridView2.Columns[2].HeaderText = "ساعت پرداخت";
                radGridView2.Columns[3].HeaderText = "سال";
                radGridView2.Columns[4].HeaderText = "ماه";
                radGridView2.Columns[5].HeaderText = "مبلغ";
                radGridView2.Columns[6].HeaderText = "شماره فیش";
                radGridView2.Columns[7].HeaderText = "توضیحات";

                radGridView2.Columns[5].FormatString = "{0:#,##0}";

                radGridView2.Columns[0].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView2.Columns[1].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView2.Columns[2].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView2.Columns[3].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView2.Columns[4].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView2.Columns[5].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView2.Columns[6].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView2.Columns[7].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                

            }            
            
            }
            }

        private void button6_Click(object sender, EventArgs e)
        {
            if (radGridView2.RowCount > 0)
            {
                PaymentMembers_F PaymentMembers_Frm = new PaymentMembers_F();
                PaymentMembers_Frm.textBox4.Text = radGridView2.CurrentRow.Cells[0].Value.ToString();
                PaymentMembers_Frm.textBox1.Text = radGridView1.CurrentRow.Cells[0].Value.ToString();
                PaymentMembers_Frm.button2.Enabled=false;
                PaymentMembers_Frm.label11.Text = radGridView1.CurrentRow.Cells[1].Value.ToString()+" "+radGridView1.CurrentRow.Cells[2].Value.ToString() ;
                PaymentMembers_Frm.persianDateTimePicker1.Value =DLUtilsobj.temperoryobj.shamsitomiladi(radGridView2.CurrentRow.Cells[1].Value.ToString());
                //PaymentMembers_Frm.maskedTextBox1.Text =radGridView2.CurrentRow.Cells[2].Value.ToString();
                PaymentMembers_Frm.textBox2.Text= radGridView2.CurrentRow.Cells[5].Value.ToString();
                PaymentMembers_Frm.textBox2.Text = string.Format("{0:#,##0}", double.Parse(PaymentMembers_Frm.textBox2.Text));
                PaymentMembers_Frm.comboBox1.Text = radGridView2.CurrentRow.Cells[3].Value.ToString();
                PaymentMembers_Frm.comboBox3.SelectedIndex = int.Parse(radGridView2.CurrentRow.Cells[4].Value.ToString())-1;
                PaymentMembers_Frm.textBox3.Text = radGridView2.CurrentRow.Cells[6].Value.ToString();
                PaymentMembers_Frm.textBox5.Text = radGridView2.CurrentRow.Cells[7].Value.ToString();
                PaymentMembers_Frm.memberscode = radGridView1.CurrentRow.Cells[0].Value.ToString();
                PaymentMembers_Frm.editcode = int.Parse(radGridView2.CurrentRow.Cells[0].Value.ToString());
                PaymentMembers_Frm.editmode = true;
                PaymentMembers_Frm.button3.Enabled = false;
                PaymentMembers_Frm.button6.Enabled = true;
                PaymentMembers_Frm.ShowDialog();

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radGridView2.RowCount > 0)
            {
                int editcode= int.Parse(radGridView2.CurrentRow.Cells[0].Value.ToString());
                  PaymentMember PaymentMembertable = familial_bankEntitiescontext.PaymentMembers.First(i => i.Code == editcode);
                  if (MessageBox.Show("رکورد انتخابی حذف گردد؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                  {
                      PaymentMembertable.Deleted = true;
                      familial_bankEntitiescontext.SaveChanges();
                      MessageBox.Show("رکورد انتخابی حذف گردید", "Information", MessageBoxButtons.OK);
                      DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 22, Environment.MachineName, editcode);
                  }   
            }
        }

        }
    }

