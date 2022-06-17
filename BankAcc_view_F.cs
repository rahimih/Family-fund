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
    public partial class BankAcc_view_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;       
        public int usercode, code;

        public BankAcc_view_F()
        {
            InitializeComponent();
        }

        private bool loaddata()
        {
            DLUtilsobj.temperoryobj.viewAcc();
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
                radGridView1.Columns[5].HeaderText = "کد شعبه ";
                radGridView1.Columns[6].HeaderText = " نوع حساب";
                radGridView1.Columns[7].HeaderText = " وضعیت";
                radGridView1.Columns[8].IsVisible = false;
                radGridView1.Columns[9].IsVisible = false;

                radGridView1.Columns[0].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[1].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[2].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[3].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[4].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[5].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[6].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;



            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BankAcc_F BankAcc_Frm = new BankAcc_F();
            BankAcc_Frm.usercode = usercode ;
            BankAcc_Frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (radGridView1.RowCount > 0)
            {
                if (radGridView1.CurrentRow.Cells[8].Value.ToString() == false.ToString())
                {
                    MessageBox.Show("حساب انتخابی غیر فعال می باشد", "خطا", MessageBoxButtons.OK);
                }
                else
                {
                    int a = int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());
                    BankAcc BankAcctable = familial_bankEntitiescontext.BankAccs.First(i => i.Code == a);
                    if (MessageBox.Show("آیا مطمئن به غیر فعال کردن حساب انتخابی می باشید؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        BankAcctable.Status = false;

                        familial_bankEntitiescontext.SaveChanges();
                        MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                        DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 5, Environment.MachineName, a);
                        loaddata();

                    }
                }
            }
        }

        private void BankAcc_view_F_Load(object sender, EventArgs e)
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
                    MessageBox.Show("حساب انتخابی  فعال می باشد", "خطا", MessageBoxButtons.OK);
                }
                else
                {
                    int a = int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());
                    BankAcc BankAcctable = familial_bankEntitiescontext.BankAccs.First(i => i.Code == a);
                    if (MessageBox.Show("آیا مطمئن به فعال کردن حساب انتخابی می باشید؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        BankAcctable.Status = true;

                        familial_bankEntitiescontext.SaveChanges();
                        MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                        DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 5, Environment.MachineName, a);
                        loaddata();

                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (radGridView1.RowCount > 0)
            {
                 BankAcc_F BankAcc_Frm = new BankAcc_F();
                BankAcc_Frm.code= int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());
                BankAcc_Frm.Acc_textBox.Text =radGridView1.CurrentRow.Cells[1].Value.ToString();
                BankAcc_Frm.Name_textBox.Text = radGridView1.CurrentRow.Cells[2].Value.ToString();
                BankAcc_Frm.Branch_textBox.Text = radGridView1.CurrentRow.Cells[4].Value.ToString();
                BankAcc_Frm.BranchCode_textBox.Text = radGridView1.CurrentRow.Cells[5].Value.ToString();
                BankAcc_Frm.Bnak_comboBox.Text = radGridView1.CurrentRow.Cells[3].Value.ToString();
                if (radGridView1.CurrentRow.Cells[8].Value.ToString() == true.ToString())
                BankAcc_Frm.Status_comboBox.SelectedIndex=0;
                else 
                BankAcc_Frm.Status_comboBox.SelectedIndex=1;
                if (radGridView1.CurrentRow.Cells[9].Value.ToString() == "1" )
                BankAcc_Frm.Kind_comboBox.SelectedIndex=0;
                else
                BankAcc_Frm.Kind_comboBox.SelectedIndex=1;
                //*****************
                BankAcc_Frm.editmode = true;
                BankAcc_Frm.button6.Enabled = true;
                BankAcc_Frm.button3.Enabled = false;
                BankAcc_Frm.ShowDialog();
                loaddata();


            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            button3_Click(toolStripMenuItem1, e);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            button6_Click(toolStripMenuItem4, e);
        }

  
    }
}
