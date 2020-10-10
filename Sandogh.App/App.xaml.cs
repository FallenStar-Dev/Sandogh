using System.Threading;
using System.Windows;

namespace Sandogh.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Thread.Sleep(4000);
            var main = new MainWindow();
            using var register = new RegisterationWindow();
            using var login = new LoginWindow();
            if (register.ShowDialog().Equals(true))
            {
                register.Dispose();
                register.Close();
                if (login.ShowDialog().Equals(true))
                {
                    login.Dispose();
                    login.Close();
                    // main = new MainWindow();
                    main.ShowDialog();
                    // main.Close();
                }
            }

            register.Dispose();
            register.Close();
            login.Dispose();
            login.Close();
            main.Close();
        }
    }
}
