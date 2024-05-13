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
    /// Interaction logic for wordElement.xaml
    /// </summary>
    
    public partial class wordElement : UserControl, INotifyPropertyChanged
    {
        public wordElement()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty MyTextProperty =
        DependencyProperty.Register("MyText", typeof(string), typeof(wordElement), new PropertyMetadata(""));

        public static readonly DependencyProperty DeleteFunctionProperty =
            DependencyProperty.Register("DeleteFunction", typeof(Action<object,MouseButtonEventArgs>), typeof(wordElement));

        public static readonly DependencyProperty UpdateFunctionProperty =
            DependencyProperty.Register("UpdateFunction", typeof(Action<object, MouseButtonEventArgs>), typeof(wordElement));

        public event PropertyChangedEventHandler? PropertyChanged;
        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        public Action<object, MouseButtonEventArgs> DeleteFunction
        {
            get { return (Action<object, MouseButtonEventArgs>)GetValue(DeleteFunctionProperty); }
            set { SetValue(DeleteFunctionProperty, value); }
        }
        public Action<object, MouseButtonEventArgs> UpdateFunction
        {
            get { return (Action<object, MouseButtonEventArgs>)GetValue(UpdateFunctionProperty); }
            set { SetValue(UpdateFunctionProperty, value); }
        }

        private void Delete_Down(object sender, MouseButtonEventArgs e)
        {
            DeleteFunction?.Invoke(this,e);
        }
        private void Update_Down(object sender, MouseButtonEventArgs e)
        {
            UpdateFunction?.Invoke(this, e);
        }
    }
}
