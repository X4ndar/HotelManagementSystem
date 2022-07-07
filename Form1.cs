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
using System.Data.Sql;

namespace HotelManagementSystem
{
    public partial class LoginForm : Form
    {
        SqlConnection cn = new SqlConnection(@"data source=.\SQLEXPRESS; initial catalog=Hotel;integrated security=true;");
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Users where Username like '" + textBox1.Text+ "'and pass like '" + textBox2.Text+"' ", cn);
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Admin admin = new Admin();
                admin.Show();
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou le mot de passe est incorrect", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            this.Hide();
        }
    }
}
