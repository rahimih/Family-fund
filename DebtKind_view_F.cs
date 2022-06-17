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
    public partial class DebtKind_view_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public int usercode, code, usercodetemp;

        public DebtKind_view_F()
        {
            InitializeComponent();
        }

        private bool loaddata()
        {
            DLUtilsobj.temperoryobj.Debtkindview();
            SqlDataReader DataSource;
            DLUtilsobj.temperoryobj.Dbconnset(true);
            DataSource = DLUtilsobj.temperoryobj.temperoryclientdataset.ExecuteReader();
            radGridView1.DataSource = DataSource;
            DLUtilsobj.temperoryobj.Dbconnset(false);

            if (radGridView1.RowCount > 0)
            {
                radGridView1.Columns[0].HeaderText = "کد ";
                radGridView1.Columns[1].HeaderText = "شرح";
                radGridView1.Columns[2].HeaderText = "مبلغ وام";
                radGridView1.Columns[3].HeaderText = "درصد کارمزد";
                radGridView1.Columns[4].HeaderText = "نحوه پرداخت کارمزذ";
                radGridView1.Columns[5].HeaderText = "تعداد اقساط";
                radGridView1.Columns[6].HeaderText = "مبلغ هر قسط";
                radGridView1.Columns[7].HeaderText = "سال";
                radGridView1.Columns[8].HeaderText = " از تاریخ";
                radGridView1.Columns[9].HeaderText = " تا تاریخ";
                radGridView1.Columns[10].HeaderText = "وضعیت";
                //---------------
                radGridView1.Columns[0].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[1].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[2].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[3].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[4].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[5].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[6].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[7].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[8].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                radGridView1.Columns[9].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DebtKind_view_F_Load(object sender, EventArgs e)
        {
            familial_bankEntitiescontext = new familial_bankEntities();
            DLUtilsobj = new DLibraryUtils.DLUtils();
            //*************
            loaddata();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DebtKind_F DebtKind_Frm = new DebtKind_F();
            DebtKind_Frm.usercode = usercodetemp;
            DebtKind_Frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (radGridView1.RowCount > 0)
            {
                if (radGridView1.CurrentRow.Cells[10].Value.ToString() == false.ToString())
                {
                    MessageBox.Show("وام انتخابی غیر فعال می باشد", "خطا", MessageBoxButtons.OK);
                }
                else
                {
                    int a = int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());
                    DebtKInd DebtKIndtable = familial_bankEntitiescontext.DebtKInds.First(i => i.Code == a);
                    if (MessageBox.Show("آیا مطمئن به غیر فعال کردن وام انتخابی می باشید؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DebtKIndtable.Status = false;

                        familial_bankEntitiescontext.SaveChanges();
                        MessageBox.Show("وام انتخابی غیر فعال گردید", "Information", MessageBoxButtons.OK);
                        DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 12, Environment.MachineName, a);
                        loaddata();

                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
             if (radGridView1.RowCount > 0)
            {
                if (radGridView1.CurrentRow.Cells[10].Value.ToString() == true.ToString())
                {
                    MessageBox.Show("وام انتخابی  فعال می باشد", "خطا", MessageBoxButtons.OK);
                }
                else
                {
                    int a = int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());
                    DebtKInd DebtKIndtable = familial_bankEntitiescontext.DebtKInds.First(i => i.Code == a);
                    if (MessageBox.Show("آیا مطمئن به  فعال کردن وام انتخابی می باشید؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DebtKIndtable.Status = true;

                        familial_bankEntitiescontext.SaveChanges();
                        MessageBox.Show("وام انتخابی فعال گردید", "Information", MessageBoxButtons.OK);
                        DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 11, Environment.MachineName, a);
                        loaddata();

                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
               if (radGridView1.RowCount > 0)
            {
                DebtKind_F DebtKind_Frm = new DebtKind_F();
                DebtKind_Frm.code = int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString());
                DebtKind_Frm.textBox4.Text = radGridView1.CurrentRow.Cells[0].Value.ToString();
                DebtKind_Frm.textBox1.Text = radGridView1.CurrentRow.Cells[1].Value.ToString();
                DebtKind_Frm.textBox5.Text = radGridView1.CurrentRow.Cells[2].Value.ToString();
                DebtKind_Frm.textBox6.Text = radGridView1.CurrentRow.Cells[3].Value.ToString();
                DebtKind_Frm.textBox7.Text = radGridView1.CurrentRow.Cells[5].Value.ToString();
                DebtKind_Frm.textBox2.Text = radGridView1.CurrentRow.Cells[6].Value.ToString();
                DebtKind_Frm.textBox3.Text = radGridView1.CurrentRow.Cells[7].Value.ToString();
                if (radGridView1.CurrentRow.Cells[8].Value.ToString()!="")
                DebtKind_Frm.persianDateTimePicker1.Value = DLUtilsobj.temperoryobj.shamsitomiladi(radGridView1.CurrentRow.Cells[8].Value.ToString());
                if (radGridView1.CurrentRow.Cells[9].Value.ToString() != "")
                DebtKind_Frm.persianDateTimePicker2.Value = DLUtilsobj.temperoryobj.shamsitomiladi(radGridView1.CurrentRow.Cells[9].Value.ToString());

                if (radGridView1.CurrentRow.Cells[4].Value.ToString() == true.ToString())
                    DebtKind_Frm.comboBox1.SelectedIndex = 0;
                else
                    DebtKind_Frm.comboBox1.SelectedIndex = 1;

                //*****************
                DebtKind_Frm.editmode = true;
                DebtKind_Frm.button6.Enabled = true;
                DebtKind_Frm.ShowDialog();
                loaddata();


            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (button3.Enabled == true)
                button3_Click(toolStripMenuItem1, e);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (button6.Enabled == true)
                button6_Click(toolStripMenuItem4, e);
        }
    }
          
}
