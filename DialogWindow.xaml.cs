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
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace RSS_DB
{
    /// <summary>
    /// Логика взаимодействия для DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        //Список товаров
        public List<string> products = new List<string>();

        public DialogWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Добавление нового товара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            double cost;
            int quantity;
            //Проверка всех полей
            if (ProductName.Text.Trim() != "" && Cost.Text.Trim() != "" && Double.TryParse(Cost.Text, out cost) && Int32.TryParse(Quantity.Text, out quantity) && Quantity.Text.Trim() != "")
            {
                SqlConnection connection = new SqlConnection();

                try
                {
                    connection.ConnectionString = MainWindow.ConnectionSrting;

                    //Открываем подключение
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand();

                    command.CommandText = " DECLARE @id int, @orderid int " +
                        "SELECT @id = (SELECT COUNT(Products.ProductID) FROM Products) + 1 " +
                        "IF (SELECT COUNT(1) FROM Products where ProductName='"+$"{ProductName.Text}"+"')=0 " +
                        "BEGIN " +
                        "INSERT INTO Products VALUES (@id,'"+$"{ProductName.Text}"+"',"+$"{Cost.Text}"+") " +
                        "END " +
                        "ELSE " +
                        "BEGIN " +
                        "SELECT @id = (SELECT Products.ProductID FROM Products WHERE ProductName = '"+$"{ProductName.Text}"+"') " +
                        "END " +
                        "SELECT @orderid = (SELECT COUNT(Orders.OrderID) FROM Orders) + 1 " +
                        "INSERT INTO Orders VALUES (@orderid, @id, "+$"{Quantity.Text}"+", '"+$"{DateTime.Now}"+"', 0)";

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

                //Закрываем окно
                this.Close();
            }
            else
            {
                MessageBox.Show("Некорректный ввод");
            }
        }
    }
}
