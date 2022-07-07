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

namespace HotelManagementSystem
{
    public partial class Guest : UserControl
    {
        public Guest()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(@"data source=.\SQLEXPRESS; initial catalog=Hotel; integrated security=true;");
        
        DataTable dt = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true) { groupBox1.Enabled = false; textBox1.Clear(); textBox2.Clear(); }
            else { groupBox1.Enabled = true; }
            chargerDGV();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) { groupBox2.Enabled = false; comboBox1.ResetText(); comboBox2.ResetText(); }
            else { groupBox2.Enabled = true; }
            chargerDGV();
        }

        private void Guest_Load(object sender, EventArgs e)
        {
            chargerCBtype();
            chargerCBNumChambre();
            radioButton1.Checked = true;
            chargerDGV();
            


        }

        public void chargerDGV()
        {
            dt.Clear();
            SqlCommand cmd = new SqlCommand("select * from Customer", cn);
            if (cn.State == ConnectionState.Closed) { cn.Open(); }
            SqlDataReader drv = cmd.ExecuteReader();
            dt.Load(drv);
            dataGridView1.DataSource = dt;
            cn.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dt.Clear();
            SqlCommand cmd = new SqlCommand("select * from Customer where cin like '"+textBox1.Text+"%'", cn);
            if (cn.State == ConnectionState.Closed) { cn.Open(); }
            SqlDataReader drv = cmd.ExecuteReader();
            dt.Load(drv);
            dataGridView1.DataSource = dt;
            cn.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt.Clear();
            SqlCommand cmd = new SqlCommand("select * from Customer where type_chambre like '" + comboBox1.SelectedValue  + "'", cn);
            if (cn.State == ConnectionState.Closed) { cn.Open(); }
            SqlDataReader drv = cmd.ExecuteReader();
            dt.Load(drv);
            dataGridView1.DataSource = dt;
            cn.Close();
            
        }

        public void chargerCBtype()
        {
            DataTable dtco = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from catégorie", cn);
            cn.Open();
            SqlDataReader drv = cmd.ExecuteReader();
            dtco.Load(drv);
            comboBox1.DataSource = dtco;
            comboBox1.DisplayMember = "cat";
            comboBox1.ValueMember = "Id_cat";
            
            cn.Close();
        }
        public void chargerCBNumChambre()
        {
            DataTable dtc = new DataTable();
            SqlCommand cmd = new SqlCommand("Select * from Room", cn);
            if (cn.State == ConnectionState.Closed) { cn.Open(); }
            SqlDataReader drv = cmd.ExecuteReader();
            dtc.Load(drv);
            comboBox2.DataSource = dtc;

            comboBox2.DisplayMember = "num_chambre";
            comboBox2.ValueMember = "Id_room";
            
            cn.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt.Clear();
            SqlCommand cmd = new SqlCommand("select * from Customer where num_chambre like '" + comboBox2.SelectedValue + "'", cn);
            if (cn.State == ConnectionState.Closed) { cn.Open(); }
            SqlDataReader drv = cmd.ExecuteReader();
            dt.Load(drv);
            dataGridView1.DataSource = dt;
            cn.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dt.Clear();
            SqlCommand cmd = new SqlCommand("select * from Customer where nom like '" + textBox2.Text + "%'", cn);
            if (cn.State == ConnectionState.Closed) { cn.Open(); }
            SqlDataReader drv = cmd.ExecuteReader();
            dt.Load(drv);
            dataGridView1.DataSource = dt;
            cn.Close();
        }
    }
}
