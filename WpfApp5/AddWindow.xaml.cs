using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
            loadComboBox();
            TypeComboBOx.SelectedIndex = 0;

        }
        void loadComboBox()
        {
            List<string> strings = new List<string>();
                
            DB dB = new DB();
            dB.openConnection();
            SqlCommand sqlCommand = new SqlCommand("Select Name From MaterialType", dB.GetSqlConnection());
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                strings.Add(reader["Name"].ToString());
            }
            TypeComboBOx.ItemsSource = strings;
        
        }
        int GetType(string Name)
        {
            if (Name == "Дерево")
            {
                return 1;
            }
            else if (Name == "Древесная плита")
            {
                return 2;
            }
            else if (Name == "Текстиль")
            {
                return 3;
            }
            else if (Name == "Стекло")
            {
                return 4;
            }
            else if (Name == "Металл")
            {
                return 5;
            }
            else if (Name == "Пластик")
            {
                return 6;
            }
            else
            {
                return -1;
            }

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string selectedType = TypeComboBOx.SelectedItem.ToString();
            int TypeName = GetType(selectedType);

            DB dB = new DB();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand("Insert Into MaterialsImport (Name, MaterialType, Price, Count, MinCount, CountInPack, Metric) VALUES" +
                "(@Name, @MaterialType, @Price, @Count, @MinCount, @CountInPack, @Metric)", dB.GetSqlConnection());
            sqlCommand.Parameters.Add("@Name", SqlDbType.VarChar).Value = NameBox.Text;
            sqlCommand.Parameters.Add("@MaterialType", SqlDbType.Int).Value = TypeName;
            sqlCommand.Parameters.Add("@Price", SqlDbType.Float).Value = PriceBox.Text;
            sqlCommand.Parameters.Add("@Count", SqlDbType.Float).Value = CountBox.Text;
            sqlCommand.Parameters.Add("@MinCount", SqlDbType.Float).Value = MinBox.Text;
            sqlCommand.Parameters.Add("@CountInPack", SqlDbType.Float).Value = CountInPackBox.Text;
            sqlCommand.Parameters.Add("@Metric", SqlDbType.VarChar).Value = MetricBox.Text;
            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(dataTable);
            this.Close();

        }
    }
}

