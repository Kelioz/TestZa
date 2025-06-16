using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            initTable();
        }
        public class Product
        {
            public int id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string MinCount { get; set; }
            public string Count { get; set; }
            public string Price { get; set; }
            public string CountInPack { get; set; }
            public string TypeAndName { get; set; }
            public string Metric {  get; set; }
            public string MinCountWithTitle { get; set; }
            public string CountWithTitle { get; set; }
            public string PriceAndCountInPackWithTitle { get; set; }
            public string NeedsCountWithTitle { get; set; }
            public double Procent { get; set; }
            public int MaterialTypeID { get; set; }

        }
        public double NeedsMaterial { get; set; }
        double NeedsMaterialRas( int MaterialID)
        {
            DB dB = new DB();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand cmd = new SqlCommand("Select SUM(NeedsMaterial) as NeedsCount from MaterialProductImport Where NameMaterial = @id", dB.GetSqlConnection());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = MaterialID;
            sqlDataAdapter.SelectCommand = cmd;
            sqlDataAdapter.Fill(dataTable);
            double result = Convert.ToDouble(dataTable.Rows[0]["NeedsCount"]);
            return result;
        }
        void initTable()
        {
            List<Product> products = new List<Product>();
            DB dB = new DB();
            dB.openConnection();
            SqlCommand sqlCommand = new SqlCommand("Select * From Material_View", dB.GetSqlConnection());
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                products.Add(new Product
                {
                    id = Convert.ToInt32(sqlDataReader["id"]),
                    Name = sqlDataReader["Name"].ToString(),
                    Type = sqlDataReader["NameMaterial"].ToString(),
                    MinCount = sqlDataReader["MinCount"].ToString(),
                    Count = sqlDataReader["Count"].ToString(),
                    Price = sqlDataReader["Price"].ToString(),
                    Procent = Convert.ToDouble(sqlDataReader["Procent"]),
                    CountInPack = sqlDataReader["CountInPack"].ToString(),
                    Metric = sqlDataReader["Metric"].ToString(),
                    TypeAndName = $"{sqlDataReader["NameMaterial"]} | Количество в упаковке {sqlDataReader["CountInPack"]}",
                    CountWithTitle = $"Количество на складе {sqlDataReader["Count"]}",
                    MinCountWithTitle = $"Минимальное Количество на складе {sqlDataReader["MinCount"]}",
                    PriceAndCountInPackWithTitle = $"Цена{sqlDataReader["Price"]}р/{sqlDataReader["Metric"]} | {sqlDataReader["CountInPack"]}",
                    NeedsCountWithTitle = $"Требуемое Количество {NeedsMaterialRas(Convert.ToInt32(sqlDataReader["MaterialType"]))}",
                    MaterialTypeID = Convert.ToInt32(sqlDataReader["MaterialType"])
                });

            }
            MainListView.ItemsSource = products;


        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.ShowDialog();
            initTable();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainListView.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись для редактирования");
                return;
            }
            else
            {
                Product product = MainListView.SelectedItem as Product;
                EditWindow editWindow = new EditWindow(product);
                editWindow.ShowDialog();
                initTable();
            }
        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)

        {
            if (MainListView.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись для редактирования");
                return;
            }
            else
            {
                Product product = MainListView.SelectedItem as Product;
                InfoPage InfoPage = new InfoPage(product);
                NavigationService?.Navigate(InfoPage);
                initTable();
            }
        }
    }
}
