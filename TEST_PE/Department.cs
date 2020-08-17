using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace TEST_PE
{
    public partial class Department : Form
    {
        public Department()
        {
            InitializeComponent();

        }
        #region Create a connection object
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP_WIN-STQ4;Initial Catalog=FUH_COMPANY;Integrated Security=True");
        #endregion
        #region Create a Dataset and DataAdapter object
        DataSet ds = new DataSet();
        SqlDataAdapter da;

        #endregion

        public void LoadData()
        {

            da = new SqlDataAdapter("SELECT depNum, depName, mgrSSN, mgrAssDate FROM tblDepartment", conn);
            da.Fill(ds, "tblDepartment");
            tblDepartmentData.DataSource = ds.Tables["tblDepartment"];


        }
        public void ClearText()
        {
            txtDepNum.Text = "";
            txtDepName.Text = "";
            txtMgrSSN.Text = "";
            datePick.Value = DateTime.Now;
        }

        private void Department_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void tblDepartmentData_Click(object sender, EventArgs e)
        {

            int index = tblDepartmentData.CurrentRow.Index;
            txtDepNum.Text = tblDepartmentData.Rows[index].Cells[0].Value.ToString();
            txtDepName.Text = tblDepartmentData.Rows[index].Cells[1].Value.ToString();
            txtMgrSSN.Text = tblDepartmentData.Rows[index].Cells[2].Value.ToString();
            datePick.Value = DateTime.Parse(tblDepartmentData.Rows[index].Cells[3].Value.ToString());
            //khai báo 1 biến 
          

                
                
                }

        private void txtExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtInsert_Click(object sender, EventArgs e)
        {
            try
            {
             
                conn.Open();
                int depNum = Int32.Parse(txtDepNum.Text);
                String depName = txtDepName.Text;
                Decimal mgrSSN = Decimal.Parse(txtMgrSSN.Text);
                DateTime mgrAssDate = datePick.Value;

                SqlCommand com = new SqlCommand("sp_Insert_tblDepartment", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@depNum", SqlDbType.Int);
                com.Parameters.Add("@depName", SqlDbType.NVarChar, 50);
                com.Parameters.Add("@mgrSSN", SqlDbType.Decimal);
                com.Parameters.Add("@mgrAssDate", SqlDbType.DateTime);

                com.Parameters["@depNum"].Value = depNum;
                com.Parameters["@depName"].Value = depName;
                com.Parameters["@mgrSSN"].Value = mgrSSN;
                com.Parameters["@mgrAssDate"].Value = mgrAssDate;

                com.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Insert data successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ds.Clear();
                LoadData();
                ClearText();
            }
            catch
            {
                MessageBox.Show("Insert data failed!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        public bool ChkDepNum()
        {
            SqlCommand comchk = new SqlCommand("SELECT * FROM [tblDepartment] where [depNum] = '" + txtDepNum.Text + "'", conn);
            conn.Open();
            SqlDataReader dr = comchk.ExecuteReader();
            if (dr.Read())
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
        }

        private void txtUpdate_Click(object sender, EventArgs e)
        {

            if (ChkDepNum())
            {

                try
                {
                    conn.Open();
                    int depNum = Int32.Parse(txtDepNum.Text);
                    String depName = txtDepName.Text;
                    Decimal mgrSSN = Decimal.Parse(txtMgrSSN.Text);
                    DateTime mgrAssDate = datePick.Value;

                    SqlCommand com = new SqlCommand("sp_Update_tblDepartment", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@depNum", SqlDbType.Int);
                    com.Parameters.Add("@depName", SqlDbType.NVarChar, 50);
                    com.Parameters.Add("@mgrSSN", SqlDbType.Decimal);
                    com.Parameters.Add("@mgrAssDate", SqlDbType.DateTime);

                    com.Parameters["@depNum"].Value = depNum;
                    com.Parameters["@depName"].Value = depName;
                    com.Parameters["@mgrSSN"].Value = mgrSSN;
                    com.Parameters["@mgrAssDate"].Value = mgrAssDate;

                    com.ExecuteNonQuery();

                    MessageBox.Show("Update data successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ds.Clear();
                    LoadData();
                    ClearText();
                    conn.Close();
                }
                catch
                {
                    MessageBox.Show("Update data failed!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("DepNum does not exist!");
            }
        }

        private void txtDelete_Click(object sender, EventArgs e)
        {
            if (ChkDepNum())
            {
                try
                {
                    conn.Open();
                    int depNum = Int32.Parse(txtDepNum.Text);
                    SqlCommand com = new SqlCommand("sp_Delete_tblDepartment", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@depNum", SqlDbType.Int);
                    com.Parameters["@depNum"].Value = depNum;

                    com.ExecuteNonQuery();

                    MessageBox.Show("Delete data successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ds.Clear();
                    LoadData();
                    ClearText();
                    conn.Close();

                }
                catch
                {
                    MessageBox.Show("Delete data failed!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Close();
                }

            }
            else
            {
                MessageBox.Show("DepNum does not exist!");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearchByName.Text.Trim().Length == 0)
            {
                ds.Clear();
                LoadData();
            }
            else
            {
                conn.Open();
                da = new SqlDataAdapter("sp_Search_tblDepartment_byDepName \n" + txtSearchByName.Text, conn);
                ds.Clear();
                da.Fill(ds, "tblDepartment");
                tblDepartmentData.DataSource = ds.Tables["tblDepartment"];
                conn.Close();
            }

        }
    }
}
