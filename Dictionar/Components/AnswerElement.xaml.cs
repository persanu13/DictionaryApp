using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for AnswerElement.xaml
    /// </summary>
    public partial class AnswerElement : UserControl, INotifyPropertyChanged
    {
       
        public AnswerElement()
        {
            InitializeComponent();
        }
        public string myQuestText;
        public string myAnswerText;
        public string myCorectText;


        public string MyQuestText
        {
            get { return myQuestText; }
            set { myQuestText = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MyQuestText"));}
        }
        public string MyAnswerText
        {
            get { return myAnswerText; }
            set { myAnswerText = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MyAnswerText")); }
        }
        public string MyCorectText
        {
            get { return myCorectText; }
            set { myCorectText = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MyCorectText")); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
