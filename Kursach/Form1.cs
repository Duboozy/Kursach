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
            var PasswordUser = Password.Text;

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            MySqlCommand command = new MySqlCommand($"SELECT * FROM `Patient` WHERE number_policy_patient = '{LoginUser}' and password_patient = '{PasswordUser}'", dataBase.getConnection ());

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
