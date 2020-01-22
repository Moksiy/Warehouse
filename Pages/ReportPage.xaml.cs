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
    /// Логика взаимодействия для ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public string FilterValue { get; set; } = "";

        public double Summ { get; set; } = 0;

        public ReportPage()
        {
            InitializeComponent();
            allFilter.IsChecked = true;
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
        /// Обработчик выбора фильтра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton item)
            {
                FilterValue = item.Tag.ToString();
            }
        }

        /// <summary>
        /// Вывод в listview с учетом фильтров
        /// </summary>
        async private void Output()
        {
            if (FirstDate.SelectedDate.ToString() != "" && SecondDate.SelectedDate.ToString() != "")
            {
                SqlConnection connection = new SqlConnection();

                try
                {
                    connection.ConnectionString = MainWindow.ConnectionSrting;

                    //Открываем подключение
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand();

                    //Обнуляем сумму
                    Sum.Text = "0";

                    //Запрос
                    command.CommandText = "SELECT ProductName, Orders.StatusID, ProductCost, Quantity, (Quantity * ProductCost) AS Result, StatusDate " +
                                          "FROM dbo.Orders " +
                                          "INNER JOIN dbo.Products ON dbo.Orders.ProductID = dbo.Products.ProductID " +
                                          "WHERE StatusDate >= '"+$"{FirstDate.SelectedDate.ToString()}"+"' AND StatusDate <= '"+ $"{SecondDate.SelectedDate.ToString()}"+"' "+
                                          $"{(FilterValue == "" ? "": " AND Orders.StatusID = ")}" + $"{FilterValue}";

                    command.Connection = connection;

                    SqlDataReader dataReader = command.ExecuteReader();

                    //Чистим listview
                    List.Items.Clear();

                    //Добавляем в список
                    while (dataReader.Read())
                    {
                        ListViewItem listItem = new ListViewItem();

                        listItem.Content = new Element(Convert.ToString(dataReader[0]),
                                                 Convert.ToByte(dataReader[1]), Convert.ToDouble(dataReader[2]),
                                                 Convert.ToInt32(dataReader[3]), Convert.ToDouble(dataReader[4]),
                                                 Convert.ToDateTime(dataReader[5]));

                        //Суммируем
                        Summ += Convert.ToDouble(dataReader[4]);

                        List.Items.Add(listItem);
                    }

                    //Обновляем сумму
                    Sum.Text = Summ.ToString();
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
            }
            else
            {
                MessageBox.Show("Выберите диапазон дат");
            }
        }

        /// <summary>
        /// Обновление listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Output();
        }
    }
}
