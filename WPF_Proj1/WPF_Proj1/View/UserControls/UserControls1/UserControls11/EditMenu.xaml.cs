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

namespace WPF_Proj1.View.UserControls.UserControls1.UserControls11
{
    /// <summary>
    /// Interaction logic for EditMenu.xaml
    /// </summary>
    public partial class EditMenu : UserControl
    {
        public EditMenu()
        {
            InitializeComponent();
        }

        private void SaveChanges(object sender, EventArgs e)
        {
            MessageBox.Show("Are you sure about these changes?", "Confirm changes", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        /*public event EventHandler? CloseRequested;

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }*/

    }
}
