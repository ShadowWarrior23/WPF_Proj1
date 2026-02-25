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

namespace WPF_Proj1.View.UserControls.UserControls1.UserControls11.UserControls111
{
    /// <summary>
    /// Interaction logic for EditMenuClearableTextbox.xaml
    /// </summary>
    public partial class EditMenuClearableTextbox : UserControl
    {
        public EditMenuClearableTextbox()
        {
            InitializeComponent();
        }
        private string placeholder;

        public string Placeholder
        {
            get { return placeholder; }
            set
            {
                placeholder = value;
                TextBlockPlaceholder.Text = placeholder;
            }
        }

        public string Text
        {
            get { return Input.Text; }
            set { Input.Text = value; }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            Input.Clear();
            Input.Focus();
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(Input.Text)) TextBlockPlaceholder.Visibility = Visibility.Visible;
            else TextBlockPlaceholder.Visibility = Visibility.Hidden;
        }
    }
}
