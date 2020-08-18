
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
            this.GetUsers();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(this.GetUsers);
        }
        private void GetUsers()
        {            
            using (UnitOfWork a = new UnitOfWork())
                DgvUsers.ItemsSource = a.UserRepository.GetAllUser();
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



        private void UserTab_GotFocus(object sender, RoutedEventArgs e)
        {
            GetUsers();
        }

        private void DatePicker_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {


        }

        private void dp_CalendarClosed(object sender, RoutedEventArgs e)
        {


            //System.Globalization.PersianCalendar Pc = new System.Globalization.PersianCalendar();
            //DateTime dt=dp.SelectedDate.Value;
            //  DateTime Date = dt.ToIranTimeZoneDateTime();
            //string Time = $" {Pc.GetHour(dt)}:{Pc.GetMinute(dt)} ";
            dp.DisplayDate = DateTime.Now;

        }
    }
}
