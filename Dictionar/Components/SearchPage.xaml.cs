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

namespace Dictionar.Components
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();
            DataContext = (Application.Current.MainWindow as MainWindow)?.DataContext;
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginInterface.Visibility = Visibility.Visible;
        }
        private void Game_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.mainFrame.Navigate(new Uri("/Components/GamePage.xaml", UriKind.Relative));
            }
        }
    }
}
