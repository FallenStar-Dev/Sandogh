
using Microsoft.Win32;

using Sandogh.Bussiness;
using Sandogh.DataLayer.Context;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
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

        private int TryToLogin = 0;
        private void BtnLogin_click(object sender, RoutedEventArgs e)
        {
            if (RegistryConnectionChecker())
            {
                try
                {
                    using UnitOfWork db = new UnitOfWork();
                    {
                        //  Sp_Login_Result user = db.UserRepository.Login(TxtUsername.Text, TxtPassword.Password);
                        UserFullView user = db.UserRepository.Login(TxtUsername.Text, TxtPassword.Password);

                        if (user is not null)
                        {
                            if (user.Activity.Equals(true))
                            {
                                Global_variable.ActiveUser = user;
                                DialogResult = true;
                            }
                            else if (user.Activity.Equals(false))
                            {
                                MessageBox.Show("این حساب کاربری غیر فعال است");
                                TxtsResetter();
                            }
                        }
                        else
                        {
                            TxtsResetter();
                            TryToLogin++;
                            if (TryToLogin >= 3) ExitFromApplication();
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

        public void Dispose() { }

        private void BtnOpenConnectionWindow_Click(object sender, RoutedEventArgs e)
        {
            using (SetConnectionWindow connectionWindow = new SetConnectionWindow() { Owner = this })
            {
                if (connectionWindow.ShowDialog().Equals(true))
                {
                    using RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(@"software\\Sandogh");
                    GlobalVariables.MainConnectionString = registryKey.GetValue("ConnectionString").ToString();
                    registryKey.Close();
                    registryKey.Dispose();
                }
                connectionWindow.Dispose();
            }
        }

        private bool RegistryConnectionChecker()
        {
            using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(@"software\\Sandogh"))
            {
                if (registryKey.GetValueNames().Contains("ConnectionString"))
                {
                    GlobalVariables.MainConnectionString = registryKey.GetValue("ConnectionString").ToString();
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
    }
}
