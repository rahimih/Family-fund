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
    public partial class SignaturesAcc_F : Form
    {
        public DLibraryUtils.DLUtils DLUtilsobj;
        familial_bankEntities familial_bankEntitiescontext;
        public bool editmode = false;
        public string returncode;
        public int usercode,code;
        bool statusc;
        byte editclick = 1;
        public SignaturesAcc_F()
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
            }
            return true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "0")

                MessageBox.Show("لطفا شماره حساب را وارد نمائید", "خطا", MessageBoxButtons.OK);

            else if (textBox1.Text == "0")

                MessageBox.Show("لطفا کد عضویت را وارد نمائید", "خطا", MessageBoxButtons.OK);
            else
            {
                if (comboBox3.SelectedIndex == 0)
                    statusc = true;
                else
                    statusc = false;
                    
                SignaturesAcc SignaturesAcctable = new SignaturesAcc
                {
                    MembersCode = int.Parse(textBox1.Text),
                    AccCode = byte.Parse(textBox6.Text),
                    FromDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd"),
                    ToDate = persianDateTimePicker2.Value.ToString("yyyy/MM/dd"),
                    UserCode = usercode,
                    IpAdress = Environment.MachineName,
                    Deleted = false,
                    Status = statusc
                };
                familial_bankEntitiescontext.SignaturesAccs.Add(SignaturesAcctable);
                familial_bankEntitiescontext.SaveChanges();
                MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                //this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MembersView_Select_F MembersView_Select_Frm = new MembersView_Select_F();
            MembersView_Select_Frm.ShowDialog();
            textBox1.Text = MembersView_Select_Frm.returncode;
            label11.Text = MembersView_Select_Frm.returnname;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BankAcc_View_Select_F BankAcc_View_Select_Frm = new BankAcc_View_Select_F();
            BankAcc_View_Select_Frm.openform = 2;
            BankAcc_View_Select_Frm.ShowDialog();
            textBox6.Text = BankAcc_View_Select_Frm.returnacc;
            label10.Text = BankAcc_View_Select_Frm.returnbankname;
            label12.Text = BankAcc_View_Select_Frm.returnbranch;
            returncode = BankAcc_View_Select_Frm.returncode;

        }

        private void SignaturesAcc_F_Load(object sender, EventArgs e)
        {
            DLUtilsobj = new DLibraryUtils.DLUtils();
            familial_bankEntitiescontext = new familial_bankEntities();
            comboBox3.SelectedIndex = 0;
            loaddata();   

        }

        private void radGridView1_SelectionChanging(object sender, Telerik.WinControls.UI.GridViewSelectionCancelEventArgs e)
        {
            if (radGridView1.RowCount > 0)
            {

            DLUtilsobj.temperory2obj.view_signature_select(int.Parse(radGridView1.CurrentRow.Cells[0].Value.ToString()));
            SqlDataReader DataSource2;
            DLUtilsobj.temperory2obj.Dbconnset(true);
            DataSource2 = DLUtilsobj.temperory2obj.temperory2clientdataset.ExecuteReader();
            radGridView2.DataSource = DataSource2;
            DLUtilsobj.temperory2obj.Dbconnset(false);

            if (radGridView2.RowCount > 0)
            {
                radGridView2.Columns[0].HeaderText = "نام ";
                radGridView2.Columns[1].HeaderText = "نام خانوادگی ";
                radGridView2.Columns[2].HeaderText = "از تاریخ";
                radGridView2.Columns[3].HeaderText = "تا تاریخ";
               // radGridView2.Columns[4].HeaderText = "وضعیت";
                radGridView2.Columns[4].IsVisible = false;
                radGridView2.Columns[5].IsVisible = false;
                radGridView2.Columns[6].IsVisible = false;
                radGridView2.Columns[7].IsVisible = false;
                radGridView2.Columns[8].IsVisible = false;
                radGridView2.Columns[9].IsVisible = false;

            }            
            
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
              if (radGridView2.RowCount > 0)
              {
                  textBox4.Text = radGridView2.CurrentRow.Cells[5].Value.ToString();
                  code = int.Parse(textBox4.Text);
                  textBox1.Text = radGridView2.CurrentRow.Cells[7].Value.ToString();
                  textBox6.Text = radGridView2.CurrentRow.Cells[6].Value.ToString();
                  persianDateTimePicker1.Value = DLUtilsobj.temperoryobj.shamsitomiladi(radGridView2.CurrentRow.Cells[2].Value.ToString());
                  persianDateTimePicker2.Value = DLUtilsobj.temperoryobj.shamsitomiladi(radGridView2.CurrentRow.Cells[3].Value.ToString());
                  if (bool.Parse(radGridView2.CurrentRow.Cells[4].Value.ToString()) == true)
                      comboBox3.SelectedIndex = 0;
                  else
                      comboBox3.SelectedIndex = 1;
                  label11.Text = radGridView2.CurrentRow.Cells[0].Value.ToString() + ' ' + radGridView2.CurrentRow.Cells[1].Value.ToString();
                  label10.Text = radGridView2.CurrentRow.Cells[8].Value.ToString();
                  label12.Text = radGridView2.CurrentRow.Cells[9].Value.ToString();
                  //-----------------------
                  button3.Enabled = false;
                  button6.Visible=false;
                  button5.Visible=true;
              }
             
                }

        private void button5_Click(object sender, EventArgs e)
        {
                SignaturesAcc SignaturesAcctable = familial_bankEntitiescontext.SignaturesAccs.First(i => i.Code == code);
                if (MessageBox.Show("اطلاعات مورد نظر ثبت گردد؟", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
              {

                if (textBox6.Text == "0")

                    MessageBox.Show("لطفا شماره حساب را وارد نمائید", "خطا", MessageBoxButtons.OK);

                else if (textBox1.Text == "0")

                    MessageBox.Show("لطفا کد عضویت را وارد نمائید", "خطا", MessageBoxButtons.OK);
                else
                {
                    if (comboBox3.SelectedIndex == 0)
                        statusc = true;
                    else
                        statusc = false;

                    SignaturesAcctable.MembersCode = int.Parse(textBox1.Text);
                    SignaturesAcctable.AccCode = byte.Parse(textBox6.Text);
                    SignaturesAcctable.FromDate = persianDateTimePicker1.Value.ToString("yyyy/MM/dd");
                    SignaturesAcctable.ToDate = persianDateTimePicker2.Value.ToString("yyyy/MM/dd");
                    SignaturesAcctable.Status = statusc;

                    familial_bankEntitiescontext.SaveChanges();
                    MessageBox.Show("اطلاعات مورد نظر ثبت گردید", "Information", MessageBoxButtons.OK);
                    DLUtilsobj.EventsLogobj.insertEventsLog(usercode.ToString(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.ToShortTimeString(), 15, Environment.MachineName, code);
                    button3.Enabled = true;
                    button6.Visible = true;
                    button5.Visible = false;
                    //---------------
                    textBox1.Text = "0";
                    textBox4.Text = "";
                    textBox6.Text = "0";
                    persianDateTimePicker1.Value = persianDateTimePicker3.Value;
                    persianDateTimePicker2.Value = persianDateTimePicker3.Value;
                    label10.Text = "-----------";
                    label11.Text = "-----------";
                    label12.Text = "-----------";

               }
            }
         }
      }        
   }
    

