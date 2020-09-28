using Sandogh.Bussiness.Properties;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sandogh.App
{
    /// <summary>
    /// Interaction logic for Window_User.xaml
    /// </summary>
    public partial class UsersWindow : Window, IDisposable
    {
        public UsersWindow()
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
                // DgvUsers.ItemsSource = a.UserRepository.GetAllUserWithJobDetails();
                DgvUsers.ItemsSource = a.UserRepository.GetAllUserSimpleDetails();
                a.Dispose();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UnitOfWork a = new UnitOfWork();
            UserSimpleView current = (UserSimpleView)DgvUsers.SelectedItem;
            int id = current.UserID;
            
            /*a.UserRepository.Insert(new User()
            {
                UserName = "c",
                Password = "c",
                UserImage = null,
                Activity = true,
                CreateDate = null,
                PersonID=2,
                UsersJobID=3,                
            }) ;    */


            // a.UserGenericRepository.Insert(new Tbl_Users()
            /*a.UserGenericRepository.Delete(new Tbl_Users() {
                UserID = 3,
                Activity = false,
                CreateDate = null,                
                Password = "cccc",
                UserImage = null,
                UserName = "cccc",
                UsersJobID = 1
            }) ;    */

            // a.Save();
            // DgvUsers.ItemsSource = a.UserRepository.GetAllUserFullDetails();
            //List<Tbl_Users>  tbls=a.UserGenericRepository.GetAll().ToList();
            //var row=DgvUsers..Columns[0].GetValue
            /* a.UserRepository.DeleteUser(a.UserRepository.GetUserByID(DgvUsers.Items.IndexOf(DgvUsers.CurrentItem)));
             DgvUsers.ItemsSource = a.UserRepository.GetAllUserWithJobDetails();   */
            a.Dispose();
        }
    }
}
