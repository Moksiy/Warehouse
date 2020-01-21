using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;

namespace RSS_DB
{   
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //===========================   Строка подключения БД   ================================
        public static string ConnectionSrting { get; } = @"Data Source=DESKTOP-SJE2N6P\SQLEXPRESS;Initial Catalog=Warehouse_RSS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //======================================================================================

        public static List<Element> listOrders = new List<Element>();

        public MainWindow()
        {
            InitializeComponent();            
            MainPage mainPage = new MainPage();
            MainPage.NavigationService.Navigate(mainPage);
        }
    }
}
