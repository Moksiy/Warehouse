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
    /// Логика взаимодействия для OnWarehauseStatus.xaml
    /// </summary>
    public partial class OnWarehauseStatus : Page
    {
        public OnWarehauseStatus()
        {
            InitializeComponent();

            //Выводим элементы из списка
            Output();
        }

        public static int index { get; set; }

        /// <summary>
        /// Из бд в listview
        /// </summary>
        private async void Output()
        {
            #region CONNECTION
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT ProductName, Orders.StatusID, ProductCost, Quantity, (Quantity * ProductCost) AS Result, StatusDate, OrderID " +
                                      "FROM dbo.Orders " +
                                      "INNER JOIN dbo.Products ON dbo.Orders.ProductID = dbo.Products.ProductID " +
                                      "WHERE Orders.StatusID = 1";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                //Очищаем список
                MainWindow.listOrders.Clear();

                //Чистим listview
                ListViewItems.Items.Clear();

                //Добавляем в список
                while (dataReader.Read())
                {
                    ListViewItem listItem = new ListViewItem();

                    listItem.Content = new Element(Convert.ToString(dataReader[0]),
                                             Convert.ToByte(dataReader[1]), Convert.ToDouble(dataReader[2]),
                                             Convert.ToInt32(dataReader[3]), Convert.ToDouble(dataReader[4]),
                                             Convert.ToDateTime(dataReader[5]));

                    listItem.Tag = dataReader[6];

                    ListViewItems.Items.Add(listItem);
                }
            }
            catch (SqlException ex)
            {
                //Выводим сообщение об ошибке
                MessageBox.Show(Convert.ToString(ex));
            }
            finally
            {
                //В любом случае закрываем подключение
                connection.Close();
            }
            #endregion
        }

        /// <summary>
        /// Назад, в главное меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu(object sender, RoutedEventArgs e)
        {
            MainPage page = new MainPage();
            this.NavigationService.Navigate(page);
        }

        /// <summary>
        /// Обработчик на нажатие правой кнопки мыши по элементу listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItem_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            object obj = item.Tag;            
            ContextMenu cm = this.FindName("CONTEXT") as ContextMenu;
            cm.IsOpen = true;
            index = Convert.ToInt32(obj);
        }

        private async void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "UPDATE Orders SET StatusID = 2 WHERE OrderID = "+$"{index}" +
                    " UPDATE Orders SET StatusDate = '"+$"{DateTime.Now}"+"' WHERE OrderID = "+ $"{index}";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();
            }
            catch (SqlException ex)
            {
                //Выводим сообщение об ошибке
                MessageBox.Show(Convert.ToString(ex));
            }
            finally
            {
                //В любом случае закрываем подключение
                connection.Close();
            }

            //Обновляем listview
            Output();
        }
    }
}
