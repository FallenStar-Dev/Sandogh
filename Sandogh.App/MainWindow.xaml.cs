
using MaterialDesignThemes.Wpf;

using Sandogh.DataLayer.Context;
using Sandogh.App;
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
            /*UnitOfWork unit = new UnitOfWork();
            unit.PersonRepository.Insert(new Person
            {
                Name = "محمد",
                Family = "دلاوری",
                Gender = true,
                Email = "abc@gmail.com",
                NationalCode = "1900145265",
                Phones = {
                    new Phone()
                {

                      PhoneNumber="0916011022",
                      IsDefault=true
                }
                }
            });
            unit.Save();   */
            PeopleWindow window = new PeopleWindow();
            window.ShowDialog();
        }
    }
}
