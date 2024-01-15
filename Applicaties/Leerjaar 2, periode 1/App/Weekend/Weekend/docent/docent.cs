using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weekend.docent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void leerlingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if the panel is visible.
            if (pnlLeerling.Visible)
            {
                // Set the panel's visibility to false.
                pnlLeerling.Visible = false;
            }
            else
            {
                // Set the panel's visibility to true.
                pnlLeerling.Visible = true;
            }
        }

        private void klassenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if the panel is visible.
            if (pnlKlassen.Visible)
            {
                // Set the panel's visibility to false.
                pnlKlassen.Visible = false;
            }
            else
            {
                // Set the panel's visibility to true.
                pnlKlassen.Visible = true;
            }
        }

        private void gamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if the panel is visible.
            if (pnlGame.Visible)
            {
                // Set the panel's visibility to false.
                pnlGame.Visible = false;
            }
            else
            {
                // Set the panel's visibility to true.
                pnlGame.Visible = true;
            }
        }

        private void pnlKlassen_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
