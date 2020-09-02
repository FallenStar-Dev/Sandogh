using Sandogh.App.Properties;
using Sandogh.DataLayer.Context;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sandogh.App
{
    /// <summary>
    /// Interaction logic for Window_User.xaml
    /// </summary>
    public partial class Window_User : Window, IDisposable
    {
        public Window_User()
        {
            InitializeComponent();
            /*    برای ذخیره تنظیمات بعد از خروج از برنامه
            Properties.Settings.Default.Hi="hello";           
            Properties.Settings.Default.Save();            
            this.Title=Settings.Default.Hi;
            */
        }

        public void Dispose() { this.Close(); }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            using (UnitOfWork a = new UnitOfWork())
            {
                DgvUsers.ItemsSource = a.UserRepository.GetAllUser();
                a.Dispose();
            }
        }

    }
}
