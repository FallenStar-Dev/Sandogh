using Sandogh.Bussiness;
using Sandogh.Utility.Cryptography;

using System.Windows;
using System.Windows.Threading;

namespace Sandogh.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            // Thread.Sleep(4000);
            var main = new MainWindow();
            using var register = new RegisterationWindow();
            using var login = new LoginWindow();
            if (LicenceIsValid() || (register.ShowDialog().Equals(true)))
            {
                if (login.ShowDialog().Equals(true))
                {
                    login.Dispose();
                    login.Close();
                    main.ShowDialog();
                }
            }

            register.Dispose();
            register.Close();
            login.Dispose();
            login.Close();
            main.Close();
        }

        private static bool LicenceIsValid()
        {
            return RegistryOperator.IsKeyExist("ActivationKey") &&
                   RegistryOperator.IsKeyExist("SerialNumber") &&
                   Aes.Decrypt(RegistryOperator.GetKey("ActivationKey"),
                   HardwareInfo.GetHddSerialNo(), 256)
                   .Equals(RegistryOperator.GetKey("SerialNumber"));
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString());
            e.Handled = true;
        }
    }
}
