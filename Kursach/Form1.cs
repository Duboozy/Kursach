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
using Data;
using MySql.Data.MySqlClient;

namespace Kursach
{
   
    public partial class Aunthefication : Form
    {
        static string sha256(string randomString)
        {
            //Тут происходит криптографическая магия. Смысл данного метода заключается в том, что строка залетает в метод
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
            DataBase dataBase = new DataBase();

        public Aunthefication()
        {
            InitializeComponent();
            StartPosition= FormStartPosition.CenterScreen;
        }

        private void Aunthefication_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var LoginUser = Login.Text;
            var PasswordUser = Aunthefication.sha256(Password.Text);
            

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            MySqlCommand command = new MySqlCommand($"SELECT * FROM `Patient` WHERE fio_patient = '{LoginUser}' and number_policy_patient = '{PasswordUser}'", dataBase.getConnection ());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count == 1)
            {
                MessageBox.Show("Успешная авторизация");
                Form2 frm2 = new Form2();
                this.Hide();
                frm2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не правильный логин или пароль!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login.Text = "";
            Password.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Password.UseSystemPasswordChar = false;
            pictureBox1.Visible = true;
            pictureBox2.Visible = false;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Password.UseSystemPasswordChar = true;
            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
        }
    }
}
