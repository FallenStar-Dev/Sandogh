
using Sandogh.Bussiness;
using Sandogh.DataLayer;
using Sandogh.DataLayer.Context;
using Sandogh.Utility.Cryptography;

using System;
using System.Data.Entity.Core;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
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

        private int _tryToLogin;
        private bool _disposedValue;

        private void BtnLogin_click(object sender, RoutedEventArgs e)
        {
            if (RegistryConnectionStringChecker())
            {
                try
                {
                    using UnitOfWork db = new UnitOfWork();
                    {

                        UserFullView user = db.UserGenericRepository.Login(TxtUsername.Text, TxtPassword.Password);

                        if (user != null)
                        {
                            if (user.Activity)
                            {
                                GlobalVariables.ActiveUser = user;
                                db.Dispose();
                                DialogResult = true;
                            }
                            else
                            {
                                MessageBox.Show("این حساب کاربری غیر فعال است");
                                TxtsResetter();
                            }
                        }
                        else
                        {
                            db.Dispose();
                            TxtsResetter();
                            _tryToLogin++;
                            if (_tryToLogin >= 3)
                            {
                                ExitFromApplication();
                            }
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
           // DialogResult = false;
            ExitFromApplication();
        }
        private static void ExitFromApplication()
        {
            Application.Current.Shutdown();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton.Equals(MouseButtonState.Pressed))
            {
                DragMove();
            }
        }



        private void CheckTxtInput()
        {
            BtnLogin.IsEnabled=InputValidator.ValidateInput.IsLoginInputValid(TxtUsername.Text,TxtPassword.Password);
            //if (string.IsNullOrWhiteSpace(TxtUsername.Text) || string.IsNullOrWhiteSpace(TxtPassword.Password))
            //{
            //    BtnLogin.IsEnabled = false;
            //}
            //else
            //{
            //    BtnLogin.IsEnabled = true;
            //}
        }


        private void BtnOpenConnectionWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowConnectionWindow();
        }

        private bool RegistryConnectionStringChecker()
        {
            if (RegistryOperator.IsKeyExist("ConnectionString"))
            {
                DataBaseConnection.MainConnectionString = Aes.Decrypt(RegistryOperator.GetKey("ConnectionString"), HardwareInfo.GetProcessorId(), 256);
                return true;
            }

            MessageBox.Show("رشته اتصالی وجود ندارد");
            ShowConnectionWindow();
           
            return false;
        }

        private void ShowConnectionWindow()
        {
            using SetConnectionWindow connectionWindow = new SetConnectionWindow() { Owner = this };
            if (connectionWindow.ShowDialog().Equals(true))
            {
                DataBaseConnection.MainConnectionString = Aes.Decrypt(RegistryOperator.GetKey("ConnectionString"), HardwareInfo.GetProcessorId(), 256);
            }

            connectionWindow.Dispose();
            connectionWindow.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RegistryConnectionStringChecker();
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


        private void TxtUsername_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CheckTxtInput();
        }

        private void TxtPassword_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
             CheckTxtInput();
        }


    }
}
