
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;
using System.Drawing.Text;
using MySql.Data.MySqlClient;
using Weekend.leerling.flappybird;

namespace Weekend.leerling
{
    public partial class leerling : Form
    {
        private flappybird.FlappyBird temp;
        private int spelid;
        private MySqlConnection CreateConnection()
        {
            return new MySqlConnection(GetConnectString());
        }

        protected virtual string GetConnectString()
        {
            return @"Server=127.0.0.1;Database=reken-app;Uid=root;Pwd=;";
        }

        private void connection()
        {
            using (MySqlConnection connection = CreateConnection())
            {
                try
                {
                    connection.Open();
                    // als er geen connectie is dan
                    if (connection.State == ConnectionState.Open)
                    {
                        // Test
                        MySqlCommand command = new MySqlCommand("SELECT * FROM Account", connection);
                        // Execute
                        MySqlDataReader reader = command.ExecuteReader();
                        // Read the data
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["Gebruikersnaam"]);
                            lbName.Text = reader["voornaam"].ToString();
                        }
                        // Close the SqlDataReader object.
                        reader.Close();
                        Console.WriteLine("true");
                        Console.WriteLine("geen connectie");
                    }
                    //return
                    else
                    {
                        Console.WriteLine("false");
                        return;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void LoadHighScores()
        {
            LsBnaam.Items.Clear();
            LsBscore.Items.Clear();
            lblScore.Visible = true;
            lblWho.Visible = true;
            LsBnaam.Visible = true;
            LsBscore.Visible = true;
            connection();
            using (var connection = CreateConnection())
            {
                var getspel = spelid;
                connection.Open();
                var query = "SELECT * FROM `score` LEFT JOIN `account` ON account.AccountID = score.AccountID WHERE score.SpelID=@spelid ORDER BY score.score DESC;  ; ";
                MySqlCommand HighscoresCommand = new MySqlCommand(query, connection);
                HighscoresCommand.Parameters.AddWithValue("@spelid", getspel);
                MySqlDataReader HighscoreReader = HighscoresCommand.ExecuteReader();
                while (HighscoreReader.Read())
                {
                    LsBnaam.Items.Add(HighscoreReader["voornaam"]);
                    LsBscore.Items.Add(HighscoreReader["score"]);
                }
                HighscoreReader.Close();
            }
        }  
        public leerling()
        {
            InitializeComponent();
        }
        private void scoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlScores.Visible = true;
            pnlHighS.Visible = false;
            pnlOpdrachten.Visible = false;
        }

        private void highScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlHighS.Visible = true;
            pnlOpdrachten.Visible=false;
            pnlScores.Visible=false;
        }
        private void opdrachtenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlOpdrachten.Visible = true;
            pnlScores.Visible=false;
            pnlHighS.Visible=false;
        }

        private void leerling_Load(object sender, EventArgs e)
        {
            connection();
            //FillTextYESS();
            //LoadHighScores();
            datum();
        }
        public void datum()
        {
            lblDatum.Text = DateTime.Now.ToString();
        }

        private void btnOpdr1_Click(object sender, EventArgs e)
        {
            pnlOpdr1.Show();
            Randomnummer();
        }
        
        private void FlappyBird_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show(); // laat het orgigineel weer zien
        }

        private void WhackAMole_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Visible = true; // laat het orgigineel weer zien
        }
        private void txtNaam_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnOpdracht2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            var game = new WhackAmole.Whack_A_Mole();
            game.FormClosed += WhackAMole_FormClosed;
            game.Visible = true;
        }
        public void Randomnummer()
        {
            var random = new Random(); // Create a new instance of Random
            getal11 = random.Next(1, 11);
            getal12 = random.Next(1, 10);
            getal21 = random.Next(1, 10);
            getal22 = random.Next(1, 10);
            getal31 = random.Next(1, 10);
            getal32 = random.Next(1, 10);
            getal41 = random.Next(1, 10);
            getal42 = random.Next(1, 10);

            lbSom11.Text = getal11.ToString();
            lbSom12.Text = getal12.ToString();
            lbSom21.Text = getal21.ToString();
            lbSom22.Text = getal22.ToString();
            lbSom31.Text = getal31.ToString();
            lbSom32.Text = getal32.ToString();
            lblSom41.Text = getal41.ToString();
            lblSom42.Text = getal42.ToString();
        }

        private int getal11;
        private int getal12;
        private int getal21;
        private int getal22;
        private int getal31;
        private int getal32;
        private int getal41;
        private int getal42;

        private bool antwoorden()
        {
            var getal13 = getal11 + getal12;
            var getal23 = getal21 - getal22;
            var getal33 = getal31 * getal32;
            var getal43 = getal41 * getal42;
            var antw1 = txtAntw1.Text;
            var antw2 = txtAntw2.Text;
            var antw3 = txtAntw3.Text;
            var antw4 = txtAntw4.Text;

            // Check if all answers are correct
            if (antw1 == getal13.ToString() && antw2 == getal23.ToString() && antw3 == getal33.ToString() && antw4 == getal43.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnBev1_Click(object sender, EventArgs e)
        {
            // Pass the correct and incorrect values to the `antwoorden()` method
            if (antwoorden()==true)
            {
                pnlOpdr1.Hide();
                this.Hide();
                temp = new flappybird.FlappyBird();
                temp.FormClosed += FlappyBird_FormClosed;
                temp.Show();
                
            }
            else
            {
                MessageBox.Show("probeer opnieuw");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        

        private void txtNaam_TextChanged_1(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void btnScoreFlappy_Click(object sender, EventArgs e)
        {
            spelid = 1;
            LoadHighScores();
        }

        private void btnShowWhack_Click(object sender, EventArgs e)
        {
            spelid = 2;
            LoadHighScores();
        }

        private void btnScoreSnake_Click(object sender, EventArgs e)
        {
            spelid = 3;
            LoadHighScores();
        }
    }
}
