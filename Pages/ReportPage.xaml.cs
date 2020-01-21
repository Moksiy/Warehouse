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

namespace RSS_DB
{
    /// <summary>
    /// Логика взаимодействия для ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public string FilterValue { get; set; } = "";

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
    }
}
