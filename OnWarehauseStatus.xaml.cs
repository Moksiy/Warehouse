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
    /// Логика взаимодействия для OnWarehauseStatus.xaml
    /// </summary>
    public partial class OnWarehauseStatus : Page
    {
        public OnWarehauseStatus()
        {
            InitializeComponent();
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
    }
}
