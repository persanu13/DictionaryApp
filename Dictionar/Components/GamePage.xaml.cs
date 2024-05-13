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
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private Words wordsInstance;

        private List<Word> randomWords;
        private List<bool> image_description;
        private List<string> answers = Enumerable.Repeat("", 5).ToList();
        private int questNumber;
        public GamePage()
        {
            InitializeComponent();
            DataContext = (Application.Current.MainWindow as MainWindow)?.DataContext;
            wordsInstance = DataContext as Words;
            InitializateDates();
        }

        private void GameOut(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.mainFrame.Navigate(new Uri("/Components/SearchPage.xaml", UriKind.Relative));
            }
        }
        private void InitializateDates()
        {
            Random random = new Random();
            randomWords = wordsInstance.list_Words.OrderBy(x => random.Next()).Take(5).ToList();
            answers = Enumerable.Repeat("", 5).ToList();
            my_Answer.Text = String.Empty;
            PanelParinte.Children.Clear();
            image_description = new List<bool> {};
            foreach (var word in randomWords)
            {
                if (word.Image == "None")
                {
                    image_description.Add(true);
                }
                else
                {
                    double sansa = random.NextDouble();
                    if (sansa < 0.5)
                    {
                        image_description.Add(false);
                    }
                    else
                    {
                        image_description.Add(true);
                    }

                }
            }
            questNumber = 0;
            setUi(0);
        }
        private void setUi(int counter)
        {
            if (questNumber == 0 && counter == -1) return;
            answers[questNumber] = my_Answer.Text;
            if(questNumber == 4 && counter == 1){
                int raspunsuriCorecte = 0;
                for(int i = 0; i < 5; i++)
                {
                    AnswerElement textBlock = new AnswerElement();
                    textBlock.MyQuestText = $"Question {i+1}";
                    textBlock.MyAnswerText = answers[i];
                    if(answers[i] == randomWords[i].Name) raspunsuriCorecte++;
                    textBlock.MyCorectText = randomWords[i].Name;
                    PanelParinte.Children.Add(textBlock);
                }
                CorectAnswerNumber.Text = $"Congratulations, you have {raspunsuriCorecte} correct answers!";
                Panel1.Visibility = Visibility.Collapsed;
                Panel2.Visibility = Visibility.Visible;
                return;
            }
            questNumber += counter;
            AnswerNumber.Text = $"Question {questNumber+1}";
            my_Answer.Text = answers[questNumber];
            if (image_description[questNumber])
            {
                QuesText.Text = "What is the described word ?";
                DescriptionText.Text = randomWords[questNumber].Description;
                ImageBorder.Visibility = Visibility.Collapsed;
                DescriptionText.Visibility = Visibility.Visible;
            }
            else
            {
                QuesText.Text = "What is the word in the picture ?";
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                ImageBox.Source = new BitmapImage(new Uri(System.IO.Path.Combine(appDirectory, $"Images/{randomWords[questNumber].Image}"), UriKind.Absolute));
                DescriptionText.Visibility = Visibility.Collapsed;
                ImageBorder.Visibility = Visibility.Visible;
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if((sender as Image).Name == "left_Arrow")
            {
                setUi(-1);
            }
            else
            {
                setUi(1);
            }
        }

        private void RestartBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            InitializateDates();
            Panel2.Visibility = Visibility.Collapsed;
            Panel1.Visibility = Visibility.Visible;
        }
    }
}
