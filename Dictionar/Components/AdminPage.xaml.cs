using Microsoft.Win32;
using System;
using System.IO;
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
using Dictionar.MyClasses;
using System.Xml.Serialization;
using System.Collections.ObjectModel;


namespace Dictionar.Components
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private Words wordsInstance;

        private string imagePath;
        public AdminPage()
        {
            InitializeComponent();
            DataContext = (Application.Current.MainWindow as MainWindow)?.DataContext;
            wordsInstance = DataContext as Words;
            wordsInstance.InitialState();
        }

        private void copyImage(string imageName)
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string destinationFolder = System.IO.Path.Combine(appDirectory, "Images");
            string destinationPath = System.IO.Path.Combine(destinationFolder, imageName);
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }
            try
            {
                if (!File.Exists(destinationPath))
                {
                    File.Copy(imagePath, destinationPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la accesarea imagini: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Logout(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.mainFrame.Navigate(new Uri("/Components/SearchPage.xaml", UriKind.Relative));
            }
        }

        private void Image_Add(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                imagePath = openFileDialog.FileName;
                string fileName = System.IO.Path.GetFileName(imagePath);
                button_image.Content = fileName;
                MessageBox.Show("Imaginea a fost selectata cu succes!");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            bool validate = true;
            if(Input_Name.Text == String.Empty)
            {
                validate = false;
                Input_Name.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4538A"));
            }
            if (Input_Description.Text == String.Empty)
            {
                validate = false;
                Input_Description.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4538A"));
            }
            if (Input_Category.Text == String.Empty)
            {
                validate = false;
                Input_Category.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4538A"));
            }
            if (!validate) return;

            Word word = new Word() {
                Name = Input_Name.Text,
                Description = Input_Description.Text,
                Category = Input_Category.Text,
                Image = button_image.Content.ToString()
            };
            Input_Name.Text = string.Empty;
            Input_Description.Text = string.Empty;
            Input_Category.Text = string.Empty;
            button_image.Content = "None";
            searchBar.Text = string.Empty;

            if (wordsInstance.Have(word.Name))
            {
                wordsInstance.RemoveWord(word.Name);
                wordsInstance.AddWord(word);
                MessageBox.Show("Word updated!");
            }
            else
            {
                wordsInstance.AddWord(word);
                MessageBox.Show("Word added!");
            }
            if(word.Image != "None")
            {
                copyImage(word.Image);
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            wordsInstance.filter(searchBar.Text);
        }

        private void DeleteWord(object sender, MouseButtonEventArgs e)
        {
           wordsInstance.RemoveWord((sender as wordElement).MyText);
           MessageBox.Show("Word Deleted!");
        }
        private void UpdateWord(object sender, MouseButtonEventArgs e)
        {
            Word updated_word = wordsInstance.GetWord((sender as wordElement).MyText);
            if (updated_word != null)
            {
                Input_Name.Text = updated_word.Name;
                Input_Description.Text = updated_word.Description;
                Input_Category.Text = updated_word.Category;
                button_image.Content = updated_word.Image;
            }
        }

        private void Input_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).BorderBrush = new SolidColorBrush(Colors.Black);
        }
    }
}
