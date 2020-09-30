using System;
using System.Windows;

namespace Sandogh.App
{
    /// <summary>
    /// Interaction logic for W_Transaction.xaml
    /// </summary>
    public partial class TransactionWindow : Window,IDisposable
    {
        public TransactionWindow()
        {
            InitializeComponent();
        }

        public void Dispose() => this.Close();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
