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

namespace TEST_PE
{
    public partial class Login : Form
    {
        public bool LoginResult = false;
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=PHMCHONG1FCE;Initial Catalog=LOGIN;Integrated Security=True");
            conn.Open();
            SqlCommand com = new SqlCommand("SELECT * FROM [tblAccount] where [username] = '" + txtUser.Text + "' and [pass] = '" + txtPassword.Text +"'",conn);
            
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                LoginResult = true;
                MessageBox.Show("Login successful");
                this.Close();
            }
            else
            {
                MessageBox.Show("Login failed!");
                txtUser.Text = "";
                txtPassword.Text = "";
                txtUser.Focus();

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
