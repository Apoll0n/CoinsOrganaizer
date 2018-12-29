using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using CoinsOrganizerDesktop.ViewModels;

namespace CoinsOrganizerDesktop.Views
{
    /// <summary>
    /// Interaction logic for OrdersView.xaml
    /// </summary>
    public partial class OrdersView : UserControl
    {
        public OrdersView()
        {
            InitializeComponent();
            DataContext = new OrdersViewModel();
        }

        private void OnHyperlinkClick(object sender, RoutedEventArgs e)
        {
            var destination = ((Hyperlink) e.OriginalSource).NavigateUri;

            if (destination != null && destination.IsAbsoluteUri)
            {
                using (Process browser = new Process())
                {
                    browser.StartInfo = new ProcessStartInfo
                    {
                        FileName = destination.ToString(),
                        UseShellExecute = true,
                        ErrorDialog = true
                    };
                    browser.Start();
                }
            }
        }
    }
}
