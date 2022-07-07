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
    public partial class Reception : UserControl
    {
        public Reception()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(@"data source=.\SQLEXPRESS; initial catalog=Hotel; integrated security=true;");
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        decimal prix = 1;

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "" || textBox3.Text=="" || comboBox2.Text=="")
            {
                MessageBox.Show("Veuillez Remplir tout les champs ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                double nbr_days=1;
                double TotalPrix=0;
                DateTime d1 = dateTimePicker1.Value;
                DateTime d2 = dateTimePicker2.Value;
                TimeSpan T = d2 - d1;
                if (double.Parse(T.TotalDays.ToString()) <= 1)
                {
                    prix = chercherprix(comboBox2.Text);
                    TotalPrix = double.Parse(prix.ToString());
                }
                else
                { 
                    
                     nbr_days =  Math.Round(double.Parse(T.TotalDays.ToString()),0, MidpointRounding.AwayFromZero);
                      prix= chercherprix(comboBox2.Text);
                     TotalPrix = double.Parse(prix.ToString()) * nbr_days;

                }
               

                //*******************************************************************//
                



                string st = "insert into Customer (cin,nom,prenom,numtel,type_chambre,num_chambre,check_in,check_out,nbr_jour,prix) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "'," + comboBox1.SelectedValue + "," + comboBox2.SelectedValue + ",'" + d1.ToString("dd/MM/yyyy") + "','" + d2.ToString("dd/MM/yyyy") + "','" + nbr_days + "','" + TotalPrix + "')";
                if (cn.State == ConnectionState.Closed) { cn.Open(); }
                SqlCommand cmd = new SqlCommand(st, cn);
                cmd.ExecuteNonQuery();
                cn.Close();
                chargerDGV();


            }
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
            public void chargerCb1()
            {
                DataTable dtco = new DataTable();
                SqlCommand cmd = new SqlCommand("select * from catégorie", cn);
                cn.Open();
                SqlDataReader drv = cmd.ExecuteReader();
                dtco.Load(drv);
                comboBox1.DataSource = dtco;

                comboBox1.DisplayMember = "cat";
                comboBox1.ValueMember = "Id_cat";
                comboBox1.SelectedIndex = -1;
                cn.Close();

            }
            public void chargerCb2(string a)
            {
                DataTable dtc = new DataTable();
                SqlCommand cmd = new SqlCommand("select r.Id_room, r.num_chambre from Room r, catégorie c where c.Id_cat = r.type_chambre and c.cat like '" + a + "'", cn);
                if (cn.State == ConnectionState.Closed) { cn.Open(); }
                SqlDataReader drv = cmd.ExecuteReader();
                dtc.Load(drv);
                comboBox2.DataSource = dtc;

                comboBox2.DisplayMember = "num_chambre";
                comboBox2.ValueMember = "Id_room";
                comboBox2.SelectedIndex = -1;
                cn.Close();
            }
            private void Reception_Load(object sender, EventArgs e)
            {

                chargerCb1();


                chargerDGV();
            }

            private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
            {
                chargerCb2(comboBox1.Text);

            }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox2.Text);
            //chercherprix(comboBox2.SelectedValue.ToString());
            
        }
        decimal chercherprix(string pp)
        {
            decimal Tprix=-1;
            string stprix = "select prix_chambre from Room where num_chambre like '" + pp +"'" ;
            if(pp != "" )
            {
            
            if (cn.State == ConnectionState.Closed) { cn.Open(); }
            SqlCommand cmdprix = new SqlCommand(stprix, cn);
                Tprix=decimal.Parse( cmdprix.ExecuteScalar().ToString());
              //  Tprix = cmdprix.ExecuteNonQuery();
            //prix = decimal.Parse();
            
            cn.Close();
            

            }
            return Tprix;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            dateTimePicker1.Value = DateTime.Parse( dataGridView1.CurrentRow.Cells[7].Value.ToString());
            dateTimePicker2.Value = DateTime.Parse(dataGridView1.CurrentRow.Cells[8].Value.ToString());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Veuillez Remplir le champ demandé ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {;
                string st = "delete Customer where cin = '"+textBox1.Text+"'";
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
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.ResetText();
            comboBox2.ResetText();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
    } 
