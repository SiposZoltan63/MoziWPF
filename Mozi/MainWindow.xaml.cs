using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySqlConnector;

namespace Mozi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }
        public string connectionString = "server=localhost;user=root;password=;database=mozi;";

        public void LoadData()
        {

            var conn = new MySqlConnection(connectionString);

            conn.Open();

            string sql = "SELECT * FROM filmek";

            var cmd = new MySqlCommand(sql, conn);

            var adapter = new MySqlDataAdapter(cmd);

            var dt = new DataTable();

            adapter.Fill(dt);

            Filmek.ItemsSource = dt.DefaultView;

            conn.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var row = Filmek.SelectedItem as DataRowView;

            var conn = new MySqlConnection(connectionString);

            conn.Open();

            string sql = "SELECT COUNT(*) FROM `filmek` WHERE `ar`= @ar";

            var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@ar", row["ar"]);

            MessageBox.Show(cmd.ExecuteScalar().ToString());

            conn.Close();
        }
    }
}
