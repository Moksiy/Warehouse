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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Принят
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Accepted(object sender, RoutedEventArgs e)
        {
            AcceptedStatus page = new AcceptedStatus();
            this.NavigationService.Navigate(page);
        }

        /// <summary>
        /// Склад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Warehouse(object sender, RoutedEventArgs e)
        {
            OnWarehauseStatus page = new OnWarehauseStatus();
            this.NavigationService.Navigate(page);
        }

        /// <summary>
        /// Продано
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Solded(object sender, RoutedEventArgs e)
        {
            SoldedStatus page = new SoldedStatus();
            this.NavigationService.Navigate(page);
        }

        /// <summary>
        /// Отчет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Report(object sender, RoutedEventArgs e)
        {
            ReportPage page = new ReportPage();
            this.NavigationService.Navigate(page);
        }
    }
}
