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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для InfoPage.xaml
    /// </summary>
    public partial class InfoPage : Page
    {
        public int MaterialId {  get; set; }
        public InfoPage(MainPage.Product product)
        {
            InitializeComponent();
            MaterialLabel.Content = product.Name;
            NeedsMaterialLabel.Content = product.NeedsCountWithTitle;

            this.MaterialId = product.MaterialTypeID;
            GetProductByMaterial();

        }
        void GetProductByMaterial()
        {
            DB dB = new DB();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            SqlCommand sqlCommand = new SqlCommand("Select * From MaterialProductImport Where NameMaterial = @NameMaterial", dB.GetSqlConnection());
            sqlCommand.Parameters.Add("@NameMaterial", System.Data.SqlDbType.Int).Value = this.MaterialId;
            sqlDataAdapter.SelectCommand = sqlCommand;
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            ProductDataGrid.ItemsSource = dataTable.DefaultView;
        }
       
    }
}
