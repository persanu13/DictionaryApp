using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        private string _username;
        private string _password;
        public Login()
        {
            InitializeComponent();
            string relativePath = "user.txt";
            string workingDirectory = Environment.CurrentDirectory;
            string filePath = System.IO.Path.Combine(workingDirectory, relativePath);
            using (StreamReader reader = new StreamReader(filePath))
            {
                _username = reader.ReadLine();
                _password = reader.ReadLine();
            }
        }
        private void X_Button_Enter(object sender, MouseEventArgs e)
        {
            ((Image)sender).Source = new BitmapImage(new Uri("/Assets/x_button_hover.png", UriKind.Relative));
        }

        private void X_Button_Leave(object sender, MouseEventArgs e)
        {
            ((Image)sender).Source = new BitmapImage(new Uri("/Assets/x_button.png", UriKind.Relative));
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LoginInterface.Visibility = Visibility.Collapsed;
            gresit.Visibility = Visibility.Hidden;
            userName.Clear();
        }

        private void Login_Enter(object sender, MouseEventArgs e)
        {
            ((Image)sender).Source = new BitmapImage(new Uri("/Assets/login_red.png", UriKind.Relative));
        }
        private void Login_Leave(object sender, MouseEventArgs e)
        {
            ((Image)sender).Source = new BitmapImage(new Uri("/Assets/login_blue.png", UriKind.Relative));
        }
        private void LoginUp()
        {
            if (userName.Text == _username && password.Password == _password)
            {
                var mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.mainFrame.Navigate(new Uri("/Components/AdminPage.xaml", UriKind.Relative));
                }
            }
            else
            {
                gresit.Visibility = Visibility.Visible;
                password.Clear();
            }
        }
        private void Login_Down(object sender, MouseEventArgs e)
        {
           LoginUp();
        }
        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { LoginUp(); }
        }
    }
}
