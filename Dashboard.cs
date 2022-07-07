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
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(@"data source=.\SQLEXPRESS; initial catalog=Hotel; integrated security=true;");

        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        private void Dashboard_Load(object sender, EventArgs e)
        {
            chargerDGVCheckIn();
            chargerDGVCheckOut();
        }
        public void chargerDGVCheckIn()
        {
            dt.Clear();
            SqlCommand cmd = new SqlCommand("select c.cin , c.nom , c.prenom , r.num_chambre , c.check_in from Customer c , catégorie cat,Room r where c.type_chambre=cat.Id_cat and r.type_chambre=cat.Id_cat and c.num_chambre= r.Id_room and c.check_in = '" + DateTime.Now.ToString()+"'", cn);
            if (cn.State == ConnectionState.Closed) { cn.Open(); }
            SqlDataReader drv = cmd.ExecuteReader();
            dt.Load(drv);
            dataGridView1.DataSource = dt;
            cn.Close();
        }
       public void chargerDGVCheckOut()
        {
            dt1.Clear();
            SqlCommand cmd = new SqlCommand("select c.cin , c.nom , c.prenom , r.num_chambre , c.check_out from Customer c , catégorie cat,Room r where c.type_chambre=cat.Id_cat and r.type_chambre=cat.Id_cat and c.num_chambre= r.Id_room and c.check_out = '" + DateTime.Now.ToString() + "'", cn);
            if (cn.State == ConnectionState.Closed) { cn.Open(); }
            SqlDataReader drv = cmd.ExecuteReader();
            dt1.Load(drv);
            dataGridView2.DataSource = dt1;
            cn.Close();
        }
    }
}
