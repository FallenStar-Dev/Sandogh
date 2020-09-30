using Sandogh.DataLayer.Context;
using System;
using System.Windows;

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

        

       

        private void MainWindow_Initialized(object sender, EventArgs e)
        {
            /*this.Dispatcher.InvokeAsync(() =>
            {
                LoginWindow L = new LoginWindow();
                {
                    L.ShowDialog();
                    Visibility = Visibility.Visible;
                }
            });*/
        }

        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
            using (UsersWindow W_user = new UsersWindow())
            {
                W_user.Owner = this;
                W_user.ShowDialog();
                W_user.Dispose();
            }
        }

        private void BtnTransaction_Click(object sender, RoutedEventArgs e)
        {
            using (TransactionWindow W_tranaction = new TransactionWindow())
            {
                W_tranaction.Owner = this;
                W_tranaction.ShowDialog();
                W_tranaction.Dispose();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
        //    var a=unitOfWork.UserRepository.GetUserWithJobDetailsByID(2);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
