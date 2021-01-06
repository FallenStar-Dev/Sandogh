using Sandogh.DataLayer.Context;

using System;
using System.Windows;

namespace Sandogh.App
{
    /// <summary>
    /// Interaction logic for Window_User.xaml
    /// </summary>
    public partial class UsersWindow : Window,IDisposable
    {
        private bool _disposedValue;

        public UsersWindow()
        {
            InitializeComponent();
            RefreshDataGrid();
            /*    برای ذخیره تنظیمات بعد از خروج از برنامه
            Properties.Settings.Default.Hi="hello";           
            Properties.Settings.Default.Save();            
            this.Title=Settings.Default.Hi;
            */
        }

       

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            using var a = new UnitOfWork();
            // DgvUsers.ItemsSource = a.UserRepository.GetAllUserWithJobDetails();
            DgvUsers.ItemsSource = a.UserGenericRepository.GetAllUserSimpleDetails();
            a.Dispose();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var a = new UnitOfWork();
            var selectedUserRow = (UserSimpleView)DgvUsers.SelectedItem;
            var id = selectedUserRow.UserID;
            var window = new AddOrEditUserWindow(id);
            window.ShowDialog();
            /*a.UserGenericRepository.Insert(new User()
            {
                UserName = "c",
                Password = "c",
                UserImage = null,
                Activity = true,
                CreateDate = null,
                PersonID=2,
                UsersJobID=3,
                
            }) ;  */


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
            DgvUsers.ItemsSource = a.UserGenericRepository.GetAllUserSimpleDetails();
            //List<Tbl_Users>  tbls=a.UserGenericRepository.GetAll().ToList();
            //var row=DgvUsers..Columns[0].GetValue
            /* a.UserRepository.DeleteUser(a.UserRepository.GetUserByID(DgvUsers.Items.IndexOf(DgvUsers.CurrentItem)));
             DgvUsers.ItemsSource = a.UserRepository.GetAllUserWithJobDetails();   */
            a.Dispose();
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
        // ~UsersWindow()
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
        #endregion
    }
}
