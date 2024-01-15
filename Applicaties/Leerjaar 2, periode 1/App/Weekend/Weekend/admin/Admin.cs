using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weekend.admin
{
    public partial class Admin : Form
    {
        MySqlConnection connection = new MySqlConnection("Server=127.0.0.1;Database=reken-app;Uid=root;Pwd=;");
        private string Gebruikersnaam, Email, Voornaam, Tussenvoegsel, Achternaam;
        private int AccountID, RolID;
        public Admin()
        {
            InitializeComponent();
        }

        private void lblChangePass_OnClick(object sender, EventArgs e)
        {
            pnlPassReset.Visible = true;
            pnlPassReset.Location = new Point(29, 123);
        }

        static string ComputeSHA256Hash(string unhashedpass)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(unhashedpass);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return hashedPassword;
        }

        private void clearVars()
        {
            Gebruikersnaam = Email = Voornaam = Tussenvoegsel = Achternaam = "";
            AccountID = RolID = 0;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearVars();
            enableInputs();
            txtSearch.Clear();
            txtNewPass.Clear();
            pnlDeleteConfirm.Visible = false;
            pnlPassReset.Visible = false;
            pnlDeleteConfirm.Visible = false;
            pnlGegevens.Visible = false;

        }

        private void clearFields()
        {
            txtGebruikersnaam.Clear();
            txtEmail.Clear();
            txtRolID.Clear();
            txtVoornaam.Clear();
            txtTussenvoegsel.Clear();
            txtAchternaam.Clear();
        }

        private void disableInputs()
        {
            pnlGegevens.Enabled = false;
            btnSearch.Enabled = false;
            txtSearch.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            connection.Open();
            string stmt = "UPDATE `account` SET `RolID`= @NewRolID,`Gebruikersnaam`= @NewGebruikersnaam ,`email`= @NewEmail,`voornaam`= @NewVoornaam,`Tussenvoegsel`= @NewTussenvoegsel,`achternaam`= @NewAchternaam WHERE `Gebruikersnaam` = @gebruikersnaam;";
            MySqlCommand SQL = new MySqlCommand(stmt, connection);
            SQL.Parameters.AddWithValue("@NewRolID", txtRolID.Text);
            SQL.Parameters.AddWithValue("@NewGebruikersnaam", txtGebruikersnaam.Text);
            SQL.Parameters.AddWithValue("@NewEmail", txtEmail.Text);
            SQL.Parameters.AddWithValue("@NewVoornaam", txtVoornaam.Text);
            SQL.Parameters.AddWithValue("@NewTussenvoegsel", txtTussenvoegsel.Text);
            SQL.Parameters.AddWithValue("@NewAchternaam", txtAchternaam.Text);
            SQL.Parameters.AddWithValue("@Gebruikersnaam",lblTarget.Text);
            SQL.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("User Info Succesfully Updated!");
            btnReset_Click(sender, e);
        }

        private void btnPassResetCancel_Click(object sender, EventArgs e)
        {
            pnlDeleteConfirm.Visible = false;
            enableInputs();
        }

        private void btnResetPassConfirm_Click(object sender, EventArgs e)
        {
            var NewPass = ComputeSHA256Hash(txtNewPass.Text);  
            connection.Open();
            string stmt = "Update 'account' SET `wachtwoord` = @NewPass WHERE Gebruikersnaam = @gebruikersnaam;";
            MySqlCommand SQL = new MySqlCommand(stmt, connection);
            SQL.Parameters.AddWithValue("@NewPass", NewPass);
            SQL.Parameters.AddWithValue("@gebruikersnaam",lblTarget);
            SQL.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Password Succesfully Updated!");
            btnReset_Click(sender, e);
        }

        private void enableInputs()
        {
            pnlGegevens.Enabled = true;
            btnSearch.Enabled = true;
            txtSearch.Enabled = true;
        }
        
        private void btnDeleteCancel_Click(object sender, EventArgs e)
        {
            pnlDeleteConfirm.Visible = false;
            enableInputs();
        }

        private void btnDeleteConfirm_Click(object sender, EventArgs e)
        {
            connection.Open();
            string stmt = "DELETE FROM `account` WHERE `AccountID` = @AccountID";
            MySqlCommand Sql = new MySqlCommand(stmt, connection);
            Sql.Parameters.AddWithValue("@AccountID", AccountID);
            Sql.ExecuteNonQuery();
            connection.Close();
            btnReset_Click(sender, e);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            disableInputs();
            pnlDeleteConfirm.Visible = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            clearFields();
            lblTarget.Text = txtSearch.Text;
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                string stmt = "SELECT `Gebruikersnaam`,`Email`,`RolID`,`Voornaam`,`Tussenvoegsel`,`Achternaam`,`AccountID` FROM account WHERE `Gebruikersnaam`=@Gebruiker;";
                MySqlCommand Sql = new MySqlCommand(stmt, connection);
                Sql.Parameters.AddWithValue("@Gebruiker", txtSearch.Text);
                MySqlDataReader Reader = Sql.ExecuteReader();
                while (Reader.Read())
                {
                    Gebruikersnaam = Reader.GetString(0);
                    Email = Reader.GetString(1);
                    RolID = Reader.GetInt32(2);
                    Voornaam = Reader.GetString(3);
                    if (!Reader.IsDBNull(4))
                    {
                        Tussenvoegsel = Reader.GetString(4);
                    }
                    Achternaam = Reader.GetString(5);
                    AccountID = Reader.GetInt32(6);
                }
                Reader.Close();
                connection.Close();
                txtGebruikersnaam.Text = Gebruikersnaam;
                txtEmail.Text = Email;
                txtRolID.Text = RolID.ToString();
                txtVoornaam.Text = Voornaam;
                txtTussenvoegsel.Text = Tussenvoegsel;
                txtAchternaam.Text = Achternaam;
                pnlGegevens.Visible = true;
            }
        }
    }
}
