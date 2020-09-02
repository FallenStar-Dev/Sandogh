
using MaterialDesignThemes.Wpf;

using Sandogh.DataLayer.Context;

using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Sandogh.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Thread thread = new Thread(this.GetUsers);

        }

        private void MainWindow_Initialized(object sender, EventArgs e)
        {
            this.Dispatcher.InvokeAsync(() =>
            {
                LoginWindow L = new LoginWindow();
                {
                    L.ShowDialog();
                    Visibility = Visibility.Visible;
                }
            });
        }

        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
            using (Window_User W_user = new Window_User())
            {
                W_user.Owner = this;
                W_user.ShowDialog();
                W_user.Dispose();
            }
        }

        private void BtnTransaction_Click(object sender, RoutedEventArgs e)
        {
            using (W_Transaction W_tranaction = new W_Transaction())
            {
                W_tranaction.Owner = this;
                W_tranaction.ShowDialog();
                W_tranaction.Dispose();
            }
        }
    }
}
