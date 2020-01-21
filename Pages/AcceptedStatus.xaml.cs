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
    /// Логика взаимодействия для AcceptedStatus.xaml
    /// </summary>
    public partial class AcceptedStatus : Page
    {
        public AcceptedStatus()
        {
            InitializeComponent();

            //Выводим данные из БД
            Output();
        }

        /// <summary>
        /// Назад, на главное меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu(object sender, RoutedEventArgs e)
        {
            MainPage page = new MainPage();
            this.NavigationService.Navigate(page);
        }

        async private void Output()
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
                command.CommandText = "SELECT ProductName, Orders.StatusID, ProductCost, Quantity, (Quantity * ProductCost) AS Result, StatusDate " +
                                      "FROM dbo.Orders " +
                                      "INNER JOIN dbo.Products ON dbo.Orders.ProductID = dbo.Products.ProductID " +
                                      "WHERE Orders.StatusID = 0";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                //Очищаем список
                MainWindow.listOrders.Clear();

                //Чистим ListView
                AcceptedList.Items.Clear();

                //Добавляем в список
                while (dataReader.Read())
                {
                    MainWindow.listOrders.Add(new Element(Convert.ToString(dataReader[0]),
                                             Convert.ToByte(dataReader[1]), Convert.ToDouble(dataReader[2]),
                                             Convert.ToInt32(dataReader[3]), Convert.ToDouble(dataReader[4]),
                                             Convert.ToDateTime(dataReader[5])));
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

            #region LISTVIEW
            //Выводим в listview элементы списка
            foreach (var item in MainWindow.listOrders)
            {
                ListViewItem listItem = new ListViewItem();

                listItem.Content = item;

                AcceptedList.Items.Add(listItem);
            }
            #endregion
        }

        /// <summary>
        /// Вызов нового окна добавления нового элемента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputWindow(object sender, RoutedEventArgs e)
        {
            DialogWindow dialog = new DialogWindow();
            dialog.Show();           
        }

        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Output();
        }
    }
}
