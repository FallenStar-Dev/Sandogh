using MaterialDesignThemes.Wpf;

using Sandogh.DataLayer.Context;

using System;
using System.Windows;

using System.Windows.Input;
using System.Windows.Controls;


namespace Sandogh.App
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, IDisposable
    {


        public LoginWindow()
        {
            InitializeComponent();
            Resetter();
        }
        /// <summary>
        /// how many time tried with incurrect username and password
        /// </summary>
        private int TryToLogin = 0;
        private void BtnLogin_click(object sender, RoutedEventArgs e)
        {
            try
            {
                using UnitOfWork db = new UnitOfWork();
                {
                    Sp_Login_Result user = db.UserRepository.Login(TxtUsername.Text, TxtPassword.Password);
                    if (user is null)
                    {
                        Resetter();
                        TryToLogin++;
                        if (TryToLogin >= 3) ExitFromApplication();
                    }
                    else if (user?.TActivity == "فعال")
                    {
                        Global_variable.ActiveUser = user;
                        this.DialogResult = true;

                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Resetter();
            }

        }
        private void ShowMainWindow()
        {


        }

        private void Resetter()
        {
            TxtUsername.Clear();
            TxtPassword.Clear();
            TxtUsername.Focus();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            ExitFromApplication();
        }
        private void ExitFromApplication()
        {
            Application.Current.Shutdown();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void TxtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                TxtPassword.Focus();
            }
        }
        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                BtnLogin_click(null, null);
            }
        }

        public void Dispose() { this.Close(); }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Resetter();
        }
    }
}
