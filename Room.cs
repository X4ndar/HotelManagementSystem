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
    public partial class Room : UserControl
    {
        public Room()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(@"data source=.\SQLEXPRESS; initial catalog=Hotel; integrated security=true;");
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        bool s;
         public void chargerDGV()
        {
            dt.Clear();
            SqlCommand cmd = new SqlCommand("select  r.num_chambre,c.cat,r.prix_chambre,r.Disponibilite from Room r, catégorie c where r.type_chambre = c.Id_cat", cn);
            if (cn.State == ConnectionState.Closed) { cn.Open(); }
            SqlDataReader drv = cmd.ExecuteReader();
            dt.Load(drv);
            dataGridView1.DataSource = dt;
            cn.Close();
            
        }

        public void chargerCb()
        {
            DataTable dtco = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from catégorie",cn);
            cn.Open();
            SqlDataReader drv = cmd.ExecuteReader();
            dtco.Load(drv);
            comboBox1.DataSource = dtco;
            
            comboBox1.DisplayMember = "cat";
            comboBox1.ValueMember = "Id_cat";
            comboBox1.SelectedIndex = -1;
            cn.Close();
        }
        int methodeRechercher()
        {
            int y = -1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString()== textBox1.Text)
                        {

                    y = i;
                        }

            }
            return y;
         }
            
        

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Room_Load_1(object sender, EventArgs e)
        { 
            chargerDGV();
            chargerCb();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) { s = true; }
            else { s = false; }
            if( textBox1.Text=="" || textBox2.Text == "" || comboBox1.Text=="")
            {
                MessageBox.Show("Veuillez Remplir tout les champs ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string st = "update Room set prix_chambre= "+decimal.Parse(textBox2.Text)+" , Disponibilite= '"+s+"' where num_chambre = "+textBox1.Text;
            if (cn.State == ConnectionState.Closed) { cn.Open(); }
            SqlCommand cmd = new SqlCommand(st, cn);
            cmd.ExecuteNonQuery();
            chargerDGV();

            }
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) { s = true; }
            else { s = false; }
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Veuillez Remplir tout les champs ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int y = methodeRechercher();
                if (y == -1)
                {
                    string st = "insert into Room (num_chambre,type_chambre,prix_chambre,Disponibilite) values (" + textBox1.Text + "," + comboBox1.SelectedValue + ",'" + textBox2.Text + "','" + s + "')";
                 if (cn.State == ConnectionState.Closed) { cn.Open(); }
                     SqlCommand cmd = new SqlCommand(st, cn);
                    cmd.ExecuteNonQuery();
                     cn.Close();
                    chargerDGV();

                }
                else
                {
                    MessageBox.Show("Numéro de Chambre déjà existant ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
               

            }
           
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" )
            {
                MessageBox.Show("Veuillez Remplir le champ demandé ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {  string st = "delete Room where num_chambre=" + textBox1.Text;
            if (cn.State == ConnectionState.Closed) { cn.Open(); }
            SqlCommand cmd = new SqlCommand(st, cn);
            cmd.ExecuteNonQuery();
            chargerDGV();

            }
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string st = "select * from Room where num_chambre=" + textBox1.Text;
            if(cn.State == ConnectionState.Closed) { cn.Open(); }
            SqlCommand cmd = new SqlCommand(st, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Text = dr[2].ToString();
                textBox2.Text = dr[3].ToString();
                if (dr[4].ToString() == "True") { radioButton1.Checked = true; }
                else { radioButton2.Checked = true; }
            }
            cn.Close();
            chargerDGV();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text= dataGridView1.CurrentRow.Cells[2].Value.ToString();
            if(dataGridView1.CurrentRow.Cells[3].Value.ToString() == "True") { radioButton1.Checked = true; }
            if (dataGridView1.CurrentRow.Cells[3].Value.ToString() == "False") { radioButton2.Checked = true; }


        }
    }
}
