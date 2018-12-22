using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using CoinsOrganizerDesktop.ViewModels;

namespace CoinsOrganizerDesktop.Views
{
    /// <summary>
    /// Interaction logic for CoinsView.xaml
    /// </summary>
    public partial class CoinsView : UserControl
    {
        private readonly Regex _regex = new Regex("[^0-9.,]+"); //regex that matches disallowed text

        public CoinsView()
        {
            InitializeComponent();
            DataContext = new CoinsViewModel();
        }

        private bool IsTextAllowed(string text)
        {
            return _regex.IsMatch(text);
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextAllowed(e.Text);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            if (e.Uri.IsAbsoluteUri)
            {
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                e.Handled = true;
            }
        }
    }
}
