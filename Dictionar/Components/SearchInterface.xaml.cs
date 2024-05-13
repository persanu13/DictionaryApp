using Dictionar.MyClasses;
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
    /// Interaction logic for SearchInterface.xaml
    /// </summary>
    public partial class SearchInterface : UserControl
    {
        private Words wordsInstance;
        public SearchInterface()
        {
            InitializeComponent();
            DataContext = (Application.Current.MainWindow as MainWindow)?.DataContext;
            wordsInstance = DataContext as Words;
        }
        private void FilterFunction(object sender, TextChangedEventArgs e)
        {
            wordsInstance.filter(MySearchBar.MyText,MyCategory.Text);
        }
        private void ModificateVizualizer(Word word)
        {
            if (word != null)
            {
                w_Name.Text = $"Definition for {word.Name}:";
                w_Description.Text = word.Description;
                if (word.Image != "None")
                {
                    string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    w_Image.Source = new BitmapImage(new Uri(System.IO.Path.Combine(appDirectory, $"Images/{word.Image}"), UriKind.Absolute));
                }
                else
                {
                    w_Image.Source = new BitmapImage(new Uri("/assets/default.jpg", UriKind.Relative));
                }
            }
            else
            {
                w_Name.Text = $"Definition for {MySearchBar.MyText} is not in the dictionary";
                w_Description.Text = String.Empty;
                w_Image.Source = new BitmapImage(new Uri("/assets/default.jpg", UriKind.Relative));
            }
            MySearchBar.MyText = String.Empty;
            MyCategory.SelectedIndex = 0;
        }
        private void SelectedItem(object sender, SelectionChangedEventArgs e)
        {
            Word selectedWord = (sender as ListView).SelectedItem as Word;
            ModificateVizualizer(selectedWord);
        }

        private void MySearchBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Word selectedWord = wordsInstance.GetWord(MySearchBar.MyText);
                ModificateVizualizer(selectedWord);
            }
        }
        private void ButtonSearchPress()
        {
            Word selectedWord = wordsInstance.GetWord(MySearchBar.MyText);
            ModificateVizualizer(selectedWord);
        }
    }
}
