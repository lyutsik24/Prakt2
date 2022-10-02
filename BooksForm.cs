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
    public partial class BooksForm : Form
    {
        int Bookid;
        bool BookSelected;

        public BooksForm()
        {
            InitializeComponent();
        }

        private void BooksForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void BooksForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kostyaDataSet1.books". При необходимости она может быть перемещена или удалена.
            this.booksTableAdapter1.Fill(this.kostyaDataSet1.books);

            MySqlConnection conn = DBUtills.GetDBConnection();

            String query1 = "SELECT * FROM Kostya.themes;";

            //Заполнение комбобокса Темами из БД
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query1, conn);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "theme");
            cBoxTheme.ValueMember = "theme";
            cBoxTheme.DataSource = dataSet.Tables["theme"];
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAuthor.Clear();
            txtName.Clear();
            txtPublisher.Clear();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

            String query = "INSERT INTO `Kostya`.`books` (`theme`, `author`, `name`, `publisher`, `year`) VALUES (@theme, @author, @name, @publisher, @year);";

            MySqlConnection conn = DBUtills.GetDBConnection();
            conn.Open();
            MySqlCommand command = new MySqlCommand(query, conn);

            var dtp = dtYear.Value.Date.ToString("yyyy-MM-dd");

            command.Parameters.AddWithValue("@theme", cBoxTheme.SelectedValue.ToString());
            command.Parameters.AddWithValue("@author", txtAuthor.Text);
            command.Parameters.AddWithValue("@name", txtName.Text);
            command.Parameters.AddWithValue("@publisher", txtPublisher.Text);
            command.Parameters.AddWithValue("@year", dtp);

            command.ExecuteNonQuery();
            conn.Close();

            this.booksTableAdapter1.Fill(this.kostyaDataSet1.books);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы уверены что хотите удалить данный материал?", "Вы уверены?", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {

                string query = "DELETE FROM `Kostya`.`books` WHERE (`id` = @id)";

                MySqlConnection conn = DBUtills.GetDBConnection();
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@id", Bookid);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                this.booksTableAdapter1.Fill(this.kostyaDataSet1.books);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                Bookid = Convert.ToInt32(selectedRow.Cells[0].Value);
                BookSelected = true;
            }
        }
    }
}
