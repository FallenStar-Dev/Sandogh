using System.Collections.Generic;
using System.Linq;
using Sandogh.DataLayer.Context;
using System.Windows;

namespace Sandogh.App
{
    /// <summary>
    /// Interaction logic for AddOrEditUserWindow.xaml
    /// </summary>
    public partial class AddOrEditUserWindow : Window
    {
        private UserFullView userFullView;
        public AddOrEditUserWindow(int id)
        {
            InitializeComponent();
            TxtId.Text = id.ToString();
            var unitOfWork = new UnitOfWork();
            userFullView = unitOfWork.UserGenericRepository.GetUserFullDetailsByID(id);
            FillForm();
        }
        private void FillForm()
        {
            var unitOfWork = new UnitOfWork();
            StkPanel.DataContext = userFullView;


            //CboJob.ItemsSource = a;
            CboJob.ItemsSource = unitOfWork.JobGenericRepository.GetAll().
                ToDictionary(c => (c.JobID), d => d.JobName);    
            CboJob.DisplayMemberPath = "Value";
            CboJob.SelectedValuePath = "Key";
            CboJob.SelectedValue = userFullView.JobID;
            GrdPhone.ItemsSource = unitOfWork.UserGenericRepository.GetPhones(userFullView.PersonID).Select(c=>new { c.PhoneID, c.PhoneNumber,c.IsDefault}).ToList();

            // TxtName.Text = userFullView.Name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(CboJob.SelectedValue.ToString());
        }

      
    }
}
