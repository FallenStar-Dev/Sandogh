
using Microsoft.Win32;

using Sandogh.Bussiness;
using Sandogh.DataLayer.Context;
using Sandogh.Utility.Cryptography;

using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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
        }
        /// <summary>
        /// how many time tried with incorrect username and password
        /// </summary>

        private int _tryToLogin = 0;
        private bool _disposedValue;

        private void BtnLogin_click(object sender, RoutedEventArgs e)
        {
            if (RegistryConnectionChecker())
            {
                try
                {
                    using var db = new UnitOfWork();
                    {
                        //  Sp_Login_Result user = db.UserRepository.Login(TxtUsername.Text, TxtPassword.Password);
                        UserFullView user = db.UserGenericRepository.Login(TxtUsername.Text, TxtPassword.Password);

                        if (user is not null)
                        {
                            if (user.TActivity.Equals("فعال"))
                            {
                                Global_variable.ActiveUser = user;
                                DialogResult = true;
                            }
                            else if (user.TActivity.Equals("غیر فعال"))
                            {
                                MessageBox.Show("این حساب کاربری غیر فعال است");
                                TxtsResetter();
                            }
                        }
                        else
                        {
                            TxtsResetter();
                            _tryToLogin++;
                            if (_tryToLogin >= 3) ExitFromApplication();
                        }
                    }
                }
                catch (EntityException)
                {
                    MessageBox.Show("خطا در برقراری ارتباط با پایگاه داده");
                    TxtsResetter();
                }
            }
        }

        private void TxtsResetter()
        {
            TxtUsername.Clear();
            TxtPassword.Clear();
            TxtUsername.Focus();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ExitFromApplication();
        }
        private static void ExitFromApplication()
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
                TxtPassword.Focus();
            }
        }
        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnLogin.Focus();
            }
        }



        private void BtnOpenConnectionWindow_Click(object sender, RoutedEventArgs e)
        {
            using var connectionWindow = new SetConnectionWindow() { Owner = this };
            if (connectionWindow.ShowDialog().Equals(true))
            {
                using var registryKey = Registry.CurrentUser.CreateSubKey(@"software\\Sandogh");
                using var aes = new Aes();
                GlobalVariables.MainConnectionString = aes.Decrypt(registryKey?.GetValue("ConnectionString").ToString(), "password", 256);
                registryKey?.Close();
                registryKey?.Dispose();
                aes.Dispose();
            }
            connectionWindow.Dispose();
            connectionWindow.Close();
        }

        private bool RegistryConnectionChecker()
        {
            using (var registryKey = Registry.CurrentUser.CreateSubKey(@"software\\Sandogh"))
            {
                if (registryKey?.GetValueNames().Contains("ConnectionString") is not null)
                {
                    using (var aes = new Aes())
                    {
                        GlobalVariables.MainConnectionString = aes.Decrypt(registryKey.GetValue("ConnectionString").ToString(), "password", 256);
                    }

                    registryKey.Close();
                    registryKey.Dispose();
                    return true;
                }
            }

            MessageBox.Show("رشته اتصالی وجود ندارد");
            BtnOpenConnectionWindow_Click(null, null);
            return false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RegistryConnectionChecker();
            TxtsResetter();
        }

        #region Disposing
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~LoginWindow()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion Disposing
    }
}
