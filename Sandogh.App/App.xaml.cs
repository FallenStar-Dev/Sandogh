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
            MainWindow main = new MainWindow();
            using (LoginWindow login = new LoginWindow())
            {
                if (login.ShowDialog().Equals(true))
                {
                    // main = new MainWindow();
                    main.ShowDialog();
                    // main.Close();
                }

                login.Dispose();
            }
            main.Close();
        }
    }
}
