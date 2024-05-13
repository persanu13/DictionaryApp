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
    /// Interaction logic for SearchBar.xaml
    /// </summary>
    public partial class SearchBar : UserControl, INotifyPropertyChanged
    {
        public SearchBar()
        {
            InitializeComponent();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public string myText = "";

        public string MyText
        {
            get { return myText; }
            set { myText = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MyText")); }
        }

        public static readonly DependencyProperty TextChangeProperty =
            DependencyProperty.Register("TextChange", typeof(Action<object, TextChangedEventArgs>), typeof(SearchBar));

        public static readonly DependencyProperty SelectionChangedProperty =
           DependencyProperty.Register("SelectionChanged", typeof(Action<object, SelectionChangedEventArgs>), typeof(SearchBar));

        public static readonly DependencyProperty SearchPressProperty =
            DependencyProperty.Register("SearchPress", typeof(Action), typeof(SearchBar));


        public Action<object, TextChangedEventArgs> TextChange
        {
            get { return (Action<object, TextChangedEventArgs>)GetValue(TextChangeProperty); }
            set { SetValue(TextChangeProperty, value); }
        }
        public Action<object, SelectionChangedEventArgs> SelectionChanged
        {
            get { return (Action<object, SelectionChangedEventArgs>)GetValue(SelectionChangedProperty); }
            set { SetValue(SelectionChangedProperty, value); }
        }
        public Action SearchPress
        {
            get { return (Action)GetValue(SearchPressProperty); }
            set { SetValue(SearchPressProperty, value); }
        }
        private void txtInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if (view_list.Items.Count > 0 && txtInput.Text.Length > 1)
            {
                MyPopup.IsOpen = true;
            }
        }
        private void txtInput_LostFocus(object sender, RoutedEventArgs e)
        {
            MyPopup.IsOpen = false;
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            myText = txtInput.Text;
            TextChange?.Invoke(this, e);
            if (txtInput.Text.Length > 0)
            {
                tbPlaceHolder.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbPlaceHolder.Visibility = Visibility.Visible;
            }
            if (view_list.Items.Count > 0 && txtInput.Text.Length > 0)
            {
                MyPopup.IsOpen = true;
            }
            else
            {
                MyPopup.IsOpen = false;
            }
         
        }

        private void view_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                SelectionChanged?.Invoke(sender, e);
            }
        }

        private void btnSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SearchPress?.Invoke();
        }
    }
}
