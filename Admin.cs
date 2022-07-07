using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagementSystem
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button4_Click(object sender, EventArgs e)
        {

            this.panel4.Controls.Clear();
            Room R = new Room();
            this.panel4.Controls.Add(R);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.panel4.Controls.Clear();
            Reception Re = new Reception();
            this.panel4.Controls.Add(Re);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.panel4.Controls.Clear();
            Guest G = new Guest();
            this.panel4.Controls.Add(G);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.panel4.Controls.Clear();
            Dashboard Dash = new Dashboard();
            this.panel4.Controls.Add(Dash);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Admin_Load(object sender, EventArgs e)
        {
            this.panel4.Controls.Clear();
            Dashboard Dash = new Dashboard();
            this.panel4.Controls.Add(Dash);
        }
    }
}
