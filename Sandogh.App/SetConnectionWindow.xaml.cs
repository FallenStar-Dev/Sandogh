using Microsoft.Win32;

using Sandogh.Bussiness;

using System;
using System.Data.Entity.Core;
using System.Data.Entity.Core.EntityClient;
using System.Windows;
using System.Windows.Input;
using Sandogh.DataLayer;
using Sandogh.Utility.Cryptography;

namespace Sandogh.App
{
    /// <summary>
    /// Interaction logic for SetConnectionWindow.xaml
    /// </summary>
    public partial class SetConnectionWindow : Window, IDisposable
    {
        public SetConnectionWindow()
        {
            InitializeComponent();
        }

        public void Dispose()
        {
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void TxtIpAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
                TxtDatabaseName.Focus();
        }

        private void TxtDatabaseName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
                TxtUsername.Focus();
        }
        private void TxtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
                TxtPassword.Focus();
        }
        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
                BtnSave.Focus();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            string EntityConnectionString = BuildConnectionString(ProviderConnectionString());

            if (IsServerConnected(EntityConnectionString))
            {
                using (var registryKey = Registry.CurrentUser.CreateSubKey(@"software\Sandogh"))
                {
                    using (var aes = new Aes())
                    {
                        string encryptedConnectionString = aes.Encrypt(EntityConnectionString, "password", 256);
                        registryKey?.SetValue("ConnectionString", encryptedConnectionString);
                    }
              
                    DataBaseConnection.MainConnectionString = EntityConnectionString;
                    DialogResult = true;
                    registryKey?.Close();
                    registryKey?.Dispose();
                }
            }

            else
                TxtsResetter();
        }
        private string ProviderConnectionString() =>
            (
            $"data source = {TxtIpAddress.Text.Trim()};" +
            $"initial catalog= {TxtDatabaseName.Text.Trim()};" +
            $"user id={TxtUsername.Text.Trim()};" +
            $"password={TxtPassword.Password.Trim()};" +
            " MultipleActiveResultSets =true ;" +
            "Integrated Security=false"
            );
        private void TxtsResetter()
        {
            TxtIpAddress.Clear();
            TxtDatabaseName.Clear();
            TxtUsername.Clear();
            TxtPassword.Clear();
            TxtIpAddress.Focus();
        }

        private static bool IsServerConnected(string connectionString)
        {
            using var connection = new EntityConnection(connectionString);
            try
            {
                connection.Open();
                connection.Close();
                connection.Dispose();
                return true;
            }
            catch (EntityException)
            {
                connection.Close();
                connection.Dispose();
                return false;
            }
        }

        private static string BuildConnectionString(string dynamicProviderConnectionString)
        {
            EntityConnectionStringBuilder entityConnectionString = new EntityConnectionStringBuilder()
            {
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = dynamicProviderConnectionString,
                Metadata = "res://*",
            };
            return entityConnectionString.ToString();
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;

        private void Window_Loaded(object sender, RoutedEventArgs e) => TxtIpAddress.Focus();

    }
}
