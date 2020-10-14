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
        private bool _disposedValue;

        public SetConnectionWindow()
        {
            InitializeComponent();
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
            var entityConnectionString = BuildConnectionString(ProviderConnectionString());

            if (IsServerConnected(entityConnectionString))
            {
                var encryptedConnectionString = Aes.Encrypt(entityConnectionString, HardwareInfo.GetProcessorId(), 256);
                RegistryOperator.CreateKey("ConnectionString",encryptedConnectionString);
                DataBaseConnection.MainConnectionString = entityConnectionString;
                DialogResult = true;
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
            var entityConnectionString = new EntityConnectionStringBuilder()
            {
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = dynamicProviderConnectionString,
                Metadata = "res://*",
            };
            return entityConnectionString.ToString();
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;

        private void Window_Loaded(object sender, RoutedEventArgs e) => TxtIpAddress.Focus();

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
        // ~SetConnectionWindow()
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
