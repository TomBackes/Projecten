using System;
using System.Media;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Security.Cryptography;
using System.Reflection;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using Google.Protobuf.WellKnownTypes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Weekend.leerling.WhackAmole
{
    public partial class Whack_A_Mole : Form
    {
        public Whack_A_Mole()
        {
            InitializeComponent();
        }
        private int a,b,k,score,q,r,x,difficulty;
        private float c,d,f,g,h,i,j,Ans;
        private MySqlConnection connection;
        SoundPlayer Music = new SoundPlayer();






        private void InsertScore()
        {
            MySqlConnection connection = new MySqlConnection("Server=127.0.0.1;Database=reken-app;Uid=root;Pwd=;");
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                string AccountID = Gevevens.Gebruikersnaam;
                string stmt = "INSERT INTO `score`(`AccountID`,`Score`,`SpelID`) VALUES (@AccountID,@Score,'2');";
                MySqlCommand SQL = new MySqlCommand(stmt, connection);
                SQL.Parameters.AddWithValue("@AccountID", AccountID);
                SQL.Parameters.AddWithValue("@Score", lblScore.Text);
                MessageBox.Show(AccountID + " | " + score);
                SQL.ExecuteNonQuery();
            }
        }

        private void btnExitGame_Click(object sender, EventArgs e)
        {
            difficulty = 0;
            Music.Stop();
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void PlayMusic()
        {
            for (int timer = 0; timer++ < 10;) {
                if (difficulty == 0)
                {
                    break;
                }
                else
                {
                    Music.SoundLocation = "Street Party.wav";
                    Music.Play();
                    Console.WriteLine("Playing music...");
                    Console.ReadLine();
                    await Task.Delay(123000);
                    timer = 0;
                }
            }
        }

        private void cmbDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbDifficulty.SelectedIndex == 0)
            {
                lblDiffDesc.Text = "";
                lblDiffDesc.Text = "Je krijgt 30 vragen. voor iedere vraag heb je 7.5 seconden.\ner kan maar 1 vraag tegenlijkertijd op het scherm zijn. \nje kunt een maximum van 225000 punten behalen.";
            }
            else if(cmbDifficulty.SelectedIndex == 1)
            {
                lblDiffDesc.Text = "";
                lblDiffDesc.Text = "Je krijgt 30 vragen. voor iedere vraag heb je 12.5 seconden.\ner kan maar 1 vraag tegenlijkertijd op het scherm zijn. \nje kunt een maximum van 375000 punten behalen.\n\n Deze vragen zijn een stuk moeilijker dan de optie \"Makelijk\"";
            }
        }

        private void WhackAMole_FormClosed(object sender, FormClosedEventArgs e)
        {
            difficulty = 0;
            Music.Stop();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblGameTitle_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            if (cmbDifficulty.SelectedIndex > -1)
            {
                var cmb_difficulty = cmbDifficulty.SelectedItem;
                string SelectedDifficulty = cmb_difficulty.ToString();
                if (SelectedDifficulty == "Makkelijk")
                {
                    difficulty = 1;
                }
                else if (SelectedDifficulty == "Moeilijk")
                {
                    difficulty = 2;
                }
                pnlGameHomeScreen.Visible = false;
                RNGesus();
            }
            else
            {
                MessageBox.Show("Selecteer Een Optie");
            }
        }

        private async void RNGesus()
        {
            PlayMusic();
            await Task.Delay(5000);           
            for (x = 0; x++ <= 30;)
            {
                
                Random rng = new Random();
                Random Delay1 = new Random();
                lblAss1.Text = lblAss2.Text = lblAss3.Text = lblAss4.Text = lblAss5.Text = lblAss6.Text = "...";
                var Ass = "";
                a = b = 0;
                c = d = f = g = h = i = j = 0;
                await Task.Delay(Convert.ToInt32(Delay1.Next(0, 2500)));
                int AssType = rng.Next(1, 5);
                
                if(difficulty == 0)
                {
                    break;
                }
                else if(difficulty == 1) 
                {
                    switch (AssType)
                    {
                        case 1:
                            a = rng.Next(0, 101);
                            b = rng.Next(0, 101);
                            c = b + a;
                            Ass = b + "+" + a;
                            break;
                        case 2:
                            a = rng.Next(0, 101);
                            b = rng.Next(0, 101);
                            if (b > a || b == a)
                            {
                                c = b - a;
                                Ass = b + "-" + a;
                            }
                            else
                            {
                                c = a - b;
                                Ass = a + "-" + b;
                            }
                            break;
                        case 3:
                            a = rng.Next(1, 11);
                            b = rng.Next(1, 11);
                            c = b * a;
                            Ass = b + "*" + a;
                            break;
                        case 4:
                            a = rng.Next(1, 11);
                            b = rng.Next(1, 11);
                            c = b / a;
                            Ass = b + "/" + a;
                            break;
                    }

                    Random RNGHole = new Random();
                    int Hole = RNGHole.Next(1, 7);
                    r = 1;
                    switch (Hole)
                    {
                        case 1:
                            lblAss1.Text = Ass;
                            txtAnswer1.Focus();
                            d = c;
                            for (q = 7500; q > 0; q = q - 10)
                            {
                                if (k == 1)
                                {
                                    score = score + q;
                                    lblScore.Text = score.ToString();
                                    lblAss1.Text = "...";
                                    txtAnswer1.Text = "";
                                    k = q = 0;
                                    d = 0;
                                    break;
                                }
                                else if (k == 2)
                                {
                                    MessageBox.Show("Wrong.\nQuestion: " + Ass + ".\nYour Answer: " + Ans + ".\nCorrect Answer: " + d + ".");
                                    lblAss1.Text = "...";
                                    txtAnswer1.Text = "";
                                    k = q = 0;
                                    d = 0;
                                    break;
                                }
                                await Task.Delay(10);
                            }
                            break;
                        case 2:
                            lblAss2.Text = Ass;
                            txtAnswer2.Focus();
                            f = c;
                            for (q = 7500; q > 0; q = q - 10)
                            {
                                if (k == 1)
                                {
                                    score = score + q;
                                    lblScore.Text = score.ToString();
                                    lblAss2.Text = "...";
                                    txtAnswer2.Text = "";
                                    k = q = 0;
                                    f = 0;
                                    break;
                                }
                                else if (k == 2)
                                {
                                    MessageBox.Show("Wrong.\nQuestion: " + Ass + ".\nYour Answer: " + Ans + ".\nCorrect Answer: " + f + ".");
                                    lblAss2.Text = "...";
                                    txtAnswer2.Text = "";
                                    k = q = 0;
                                    f = 0;
                                    break;
                                }
                                await Task.Delay(10);
                            }
                            break;
                        case 3:
                            lblAss3.Text = Ass;
                            txtAnswer3.Focus();
                            g = c;
                            for (q = 7500; q > 0; q = q - 10)
                            {
                                if (k == 1)
                                {
                                    score = score + q;
                                    lblScore.Text = score.ToString();
                                    lblAss3.Text = "...";
                                    txtAnswer3.Text = "";
                                    k = q = 0;
                                    g = 0;
                                    break;
                                }
                                else if (k == 2)
                                {
                                    MessageBox.Show("Wrong.\nQuestion: " + Ass + ".\nYour Answer: " + Ans + ".\nCorrect Answer: " + g + ".");
                                    lblAss3.Text = "...";
                                    txtAnswer3.Text = "";
                                    k = q = 0;
                                    g = 0;
                                    break;
                                }
                                await Task.Delay(10);
                            }
                            break;
                        case 4:
                            lblAss4.Text = Ass;
                            txtAnswer4.Focus();
                            h = c;
                            for (q = 7500; q > 0; q = q - 10)
                            {
                                if (k == 1)
                                {
                                    score = score + q;
                                    lblScore.Text = score.ToString();
                                    lblAss4.Text = "...";
                                    txtAnswer4.Text = "";
                                    k = q = 0;
                                    h = 0;
                                    break;
                                }
                                else if (k == 2)
                                {
                                    MessageBox.Show("Wrong.\nQuestion: " + Ass + ".\nYour Answer: " + Ans + ".\nCorrect Answer: " + h + ".");
                                    lblAss4.Text = "...";
                                    txtAnswer4.Text = "";
                                    k = q = 0;
                                    h = 0;
                                    break;
                                }
                                await Task.Delay(10);
                            }
                            break;
                        case 5:
                            lblAss5.Text = Ass;
                            txtAnswer5.Focus();
                            i = c;
                            for (q = 7500; q > 0; q = q - 10)
                            {
                                if (k == 1)
                                {
                                    score = score + q;
                                    lblScore.Text = score.ToString();
                                    lblAss5.Text = "...";
                                    txtAnswer5.Text = "";
                                    k = q = 0;
                                    i = 0;
                                    break;
                                }
                                else if (k == 2)
                                {
                                    MessageBox.Show("Wrong.\nQuestion: " + Ass + ".\nYour Answer: " + Ans + ".\nCorrect Answer: " + i + ".");
                                    lblAss5.Text = "...";
                                    txtAnswer5.Text = "";
                                    k = q = 0;
                                    i = 0;
                                    break;
                                }
                                await Task.Delay(10);
                            }
                            break;
                        case 6:
                            lblAss6.Text = Ass;
                            txtAnswer6.Focus();
                            j = c;
                            for (q = 7500; q > 0; q = q - 10)
                            {
                                if (k == 1)
                                {
                                    score = score + q;
                                    lblScore.Text = score.ToString();
                                    lblAss6.Text = "...";
                                    txtAnswer6.Text = "";
                                    k = q = 0;
                                    j = 0;
                                    break;
                                }
                                else if (k == 2)
                                {
                                    MessageBox.Show("Wrong.\nQuestion: " + Ass + ".\nYour Answer: " + Ans + ".\nCorrect Answer: " + j + ".");
                                    lblAss6.Text = "...";
                                    txtAnswer6.Text = "";
                                    k = q = 0;
                                    j = 0;
                                    break;
                                }
                                await Task.Delay(10);
                            }
                            break;
                    }
                }
                else if(difficulty == 2) 
                {
                    switch (AssType)
                    {
                        case 1:
                            a = rng.Next(0, 1001);
                            b = rng.Next(0, 1001);
                            c = b + a;
                            Ass = b + "+" + a;
                            break;
                        case 2:
                            a = rng.Next(0, 101);
                            b = rng.Next(100, 1001);
                            c = b - a;
                            Ass = b + "-" + a;
                            break;
                        case 3:
                            a = rng.Next(1, 11);
                            b = rng.Next(1, 101);
                            c = b * a;
                            Ass = b + "*" + a;
                            break;
                        case 4:
                            a = rng.Next(1, 11);
                            b = rng.Next(1, 101);
                            c = b / a;
                            Ass = b + "/" + a;
                            break;
                    }

                    Random RNGHole = new Random();
                    int Hole = RNGHole.Next(1, 7);
                    r = 1;
                    switch (Hole)
                    {
                        case 1:
                            lblAss1.Text = Ass;
                            txtAnswer1.Focus();
                            d = c;
                            for (q = 12500; q > 0; q = q - 10)
                            {
                                if (k == 1)
                                {
                                    score = score + q;
                                    lblScore.Text = score.ToString();
                                    lblAss1.Text = "...";
                                    txtAnswer1.Text = "";
                                    k = q = 0;
                                    d = 0;
                                    break;
                                }
                                else if (k == 2)
                                {
                                    MessageBox.Show("Wrong.\nQuestion: " + Ass + ".\nYour Answer: " + Ans + ".\nCorrect Answer: " + d + ".");
                                    lblAss1.Text = "...";
                                    txtAnswer1.Text = "";
                                    k = q = 0;
                                    d = 0;
                                    break;
                                }
                                await Task.Delay(10);
                            }
                            break;
                        case 2:
                            lblAss2.Text = Ass;
                            txtAnswer2.Focus();
                            f = c;
                            for (q = 12500; q > 0; q = q - 10)
                            {
                                if (k == 1)
                                {
                                    score = score + q;
                                    lblScore.Text = score.ToString();
                                    lblAss2.Text = "...";
                                    txtAnswer2.Text = "";
                                    k = q = 0;
                                    f = 0;
                                    break;
                                }
                                else if (k == 2)
                                {
                                    MessageBox.Show("Wrong.\nQuestion: " + Ass + ".\nYour Answer: " + Ans + ".\nCorrect Answer: " + f + ".");
                                    lblAss2.Text = "...";
                                    txtAnswer2.Text = "";
                                    k = q = 0;
                                    f = 0;
                                    break;
                                }
                                await Task.Delay(10);
                            }
                            break;
                        case 3:
                            lblAss3.Text = Ass;
                            txtAnswer3.Focus();
                            g = c;
                            for (q = 12500; q > 0; q = q - 10)
                            {
                                if (k == 1)
                                {
                                    score = score + q;
                                    lblScore.Text = score.ToString();
                                    lblAss3.Text = "...";
                                    txtAnswer3.Text = "";
                                    k = q = 0;
                                    g = 0;
                                    break;
                                }
                                else if (k == 2)
                                {
                                    MessageBox.Show("Wrong.\nQuestion: " + Ass + ".\nYour Answer: " + Ans + ".\nCorrect Answer: " + g + ".");
                                    lblAss3.Text = "...";
                                    txtAnswer3.Text = "";
                                    k = q = 0;
                                    g = 0;
                                    break;
                                }
                                await Task.Delay(10);
                            }
                            break;
                        case 4:
                            lblAss4.Text = Ass;
                            txtAnswer4.Focus();
                            h = c;
                            for (q = 12500; q > 0; q = q - 10)
                            {
                                if (k == 1)
                                {
                                    score = score + q;
                                    lblScore.Text = score.ToString();
                                    lblAss4.Text = "...";
                                    txtAnswer4.Text = "";
                                    k = q = 0;
                                    h = 0;
                                    break;
                                }
                                else if (k == 2)
                                {
                                    MessageBox.Show("Wrong.\nQuestion: " + Ass + ".\nYour Answer: " + Ans + ".\nCorrect Answer: " + h + ".");
                                    lblAss4.Text = "...";
                                    txtAnswer4.Text = "";
                                    k = q = 0;
                                    h = 0;
                                    break;
                                }
                                await Task.Delay(10);
                            }
                            break;
                        case 5:
                            lblAss5.Text = Ass;
                            txtAnswer5.Focus();
                            i = c;
                            for (q = 12500; q > 0; q = q - 10)
                            {
                                if (k == 1)
                                {
                                    score = score + q;
                                    lblScore.Text = score.ToString();
                                    lblAss5.Text = "...";
                                    txtAnswer5.Text = "";
                                    k = q = 0;
                                    i = 0;
                                    break;
                                }
                                else if (k == 2)
                                {
                                    MessageBox.Show("Wrong.\nQuestion: " + Ass + ".\nYour Answer: " + Ans + ".\nCorrect Answer: " + i + ".");
                                    lblAss5.Text = "...";
                                    txtAnswer5.Text = "";
                                    k = q = 0;
                                    i = 0;
                                    break;
                                }
                                await Task.Delay(10);
                            }
                            break;
                        case 6:
                            lblAss6.Text = Ass;
                            txtAnswer6.Focus();
                            j = c;
                            for (q = 12500; q > 0; q = q - 10)
                            {
                                if (k == 1)
                                {
                                    score = score + q;
                                    lblScore.Text = score.ToString();
                                    lblAss6.Text = "...";
                                    txtAnswer6.Text = "";
                                    k = q = 0;
                                    j = 0;
                                    break;
                                }
                                else if (k == 2)
                                {
                                    MessageBox.Show("Wrong.\nQuestion: " + Ass + ".\nYour Answer: " + Ans + ".\nCorrect Answer: " + j + ".");
                                    lblAss6.Text = "...";
                                    txtAnswer6.Text = "";
                                    k = q = 0;
                                    j = 0;
                                    break;
                                }
                                await Task.Delay(10);
                            }
                            break;
                    }
                }
            }
            if (x > 30)
            {
                MessageBox.Show("Je hebt " + lblScore.Text + " punten behaald!\n");
                InsertScore();
            }
            this.Close();
        }


        //KeyDown-Events
        private void txtAnswer1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && r == 1)
            {
                Ans = Convert.ToInt32(txtAnswer1.Text);
                if (Ans == d)
                {
                    k = 1;
                }
                else
                {
                    k = 2;
                }
                r = 0;
            }
        }

        private void txtAnswer2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && r == 1)
            {
                Ans = Convert.ToInt32(txtAnswer2.Text);
                if (Ans == f)
                {
                    k = 1;
                }
                else
                {
                    k = 2;
                }
                r = 0;
            }
        }

        private void txtAnswer3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && r == 1)
            {
                Ans = Convert.ToInt32(txtAnswer3.Text);
                if (Ans == g)
                {
                    k = 1;
                }
                else
                {
                    k = 2;
                }
                r = 0;
            }
        }

        private void txtAnswer4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && r == 1)
            {
                Ans = Convert.ToInt32(txtAnswer4.Text);
                if (Ans == h)
                {
                    k = 1;
                }
                else
                {
                    k = 2;
                }
                r = 0;
            }
        }

        private void txtAnswer5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && r == 1)
            {
                Ans = Convert.ToInt32(txtAnswer5.Text);
                if (Ans == i)
                {
                    k = 1;
                }
                else
                {
                    k = 2;
                }
                r = 0;
            }

        }

        private void txtAnswer6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && r == 1)
            {
                Ans = Convert.ToInt32(txtAnswer6.Text);
                if (Ans == j)
                {
                    k = 1;
                }
                else
                {
                    k = 2;
                }
                r = 0;
            }
        }


        //TextChanged-Events
        private void txtAnswer1_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAnswer1.Text, out int x) && txtAnswer1.Text.Length > 1)
            {
                txtAnswer1.Text = txtAnswer1.Text.Substring(0, txtAnswer1.Text.Length - 1);
                txtAnswer1.SelectionStart = txtAnswer1.Text.Length;
                txtAnswer1.SelectionLength = 0;
            }
            else if (string.IsNullOrWhiteSpace(txtAnswer1.Text))
            {
                txtAnswer1.Text = "";
            }
            if (!int.TryParse(txtAnswer1.Text, out int y))
            {
                txtAnswer1.Text = "";
            }
        }

        private void txtAnswer2_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAnswer2.Text, out int x) && txtAnswer2.Text.Length > 1)
            {
                txtAnswer2.Text = txtAnswer2.Text.Substring(0, txtAnswer2.Text.Length - 1);
                txtAnswer2.SelectionStart = txtAnswer2.Text.Length;
                txtAnswer2.SelectionLength = 0;
            }
            else if (!int.TryParse(txtAnswer2.Text, out int y))
            {
                txtAnswer2.Text = "";
            }
        }

        private void txtAnswer3_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAnswer3.Text, out int x) && txtAnswer3.Text.Length > 1)
            {
                txtAnswer3.Text = txtAnswer3.Text.Substring(0, txtAnswer3.Text.Length - 1);
                txtAnswer3.SelectionStart = txtAnswer3.Text.Length;
                txtAnswer3.SelectionLength = 0;
            }
            else if (!int.TryParse(txtAnswer3.Text, out int y))
            {
                txtAnswer3.Text = "";
            }
        }

        private void txtAnswer4_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAnswer4.Text, out int x) && txtAnswer4.Text.Length > 1)
            {
                txtAnswer4.Text = txtAnswer4.Text.Substring(0, txtAnswer4.Text.Length - 1);
                txtAnswer4.SelectionStart = txtAnswer4.Text.Length;
                txtAnswer4.SelectionLength = 0;
            }
            else if (!int.TryParse(txtAnswer4.Text, out int y))
            {
                txtAnswer4.Text = "";
            }
        }

        private void txtAnswer5_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAnswer5.Text, out int x) && txtAnswer5.Text.Length > 1)
            {
                txtAnswer5.Text = txtAnswer5.Text.Substring(0, txtAnswer5.Text.Length - 1);
                txtAnswer5.SelectionStart = txtAnswer5.Text.Length;
                txtAnswer5.SelectionLength = 0;
            }
            else if (!int.TryParse(txtAnswer5.Text, out int y))
            {
                txtAnswer5.Text = "";
            }
        }

        private void txtAnswer6_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAnswer6.Text, out int x) && txtAnswer6.Text.Length > 1)
            {
                txtAnswer6.Text = txtAnswer6.Text.Substring(0, txtAnswer6.Text.Length - 1);
                txtAnswer6.SelectionStart = txtAnswer6.Text.Length;
                txtAnswer6.SelectionLength = 0;
            }
            else if (!int.TryParse(txtAnswer6.Text, out int y))
            {
                txtAnswer6.Text = "";
            }
        }
    }
}