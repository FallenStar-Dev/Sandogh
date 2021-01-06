using System;
using System.Collections.Generic;
using System.Linq;
using Sandogh.DataLayer.Context;
using System.Windows;

namespace Sandogh.App
{
    /// <summary>
    /// Interaction logic for AddOrEditUserWindow.xaml
    /// </summary>
    public partial class AddOrEditUserWindow : Window, IDisposable
    {
        private UserFullView _userFullView;
        private bool _disposedValue;

        public AddOrEditUserWindow(int id)
        {
            InitializeComponent();
            TxtId.Text = id.ToString();
            var unitOfWork = new UnitOfWork();
            _userFullView = unitOfWork.UserGenericRepository.GetUserFullDetailsById(id);
            FillForm();
        }
        private void FillForm()
        {
            var unitOfWork = new UnitOfWork();
            StkPanel.DataContext = _userFullView;


            //CboJob.ItemsSource = a;
            CboJob.ItemsSource = unitOfWork.JobGenericRepository.GetAll().
                ToDictionary(c => (c.JobID), d => d.JobName);
            CboJob.DisplayMemberPath = "Value";
            CboJob.SelectedValuePath = "Key";
            CboJob.SelectedValue = _userFullView.JobID;
            GrdPhone.ItemsSource = unitOfWork.UserGenericRepository.GetPhones(_userFullView.PersonID).Select(c => new { c.PhoneID, c.PhoneNumber, c.IsDefault }).ToList();

            // TxtName.Text = userFullView.Name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(CboJob.SelectedValue.ToString());
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
        // ~AddOrEditUserWindow()
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

        private void TxtJob_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            UnitOfWork u = new UnitOfWork();
            UserFullView f = u.UserGenericRepository.GetUserFullDetailsByNationalCode(TxtJob.Text);
            TxtName.Text = f?.Name.ToString();
        }
    }
}
