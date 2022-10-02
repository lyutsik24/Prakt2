using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prakt2
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void clearbutton_Click(object sender, EventArgs e)
        {
            LoginBox.Clear();
            PasswordBox.Clear();
        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            //данные для входа login=admin; password=admin
            using (MySqlConnection Connection = new MySqlConnection(Properties.Settings.Default.KostyaConnectionString))
            {
                try
                {
                    String loginUser = LoginBox.Text;
                    String loginPassword = PasswordBox.Text;
                    String quary = "SELECT * FROM `Kostya`.`users` WHERE `login` = @uL AND `password` = @uP";

                    DataTable table = new DataTable();

                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand command = new MySqlCommand(quary, Connection);
                    command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
                    command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = loginPassword;
                    Connection.Open();
                    command.ExecuteNonQuery();

                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        BooksForm main = new BooksForm();
                        this.Hide();
                        main.Show();
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден");
                        return;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
