using System;
using System.Windows;

using Sandogh.Bussiness;

namespace Sandogh.App
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegisterationWindow : Window, IDisposable
    {
        private bool _disposedValue;

        public RegisterationWindow()
        {
            InitializeComponent();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            string alldata = null;
            alldata += HardwareInfo.GetBoardProductId() + '|';
            alldata += HardwareInfo.GetHddSerialNo() + '|';
            alldata += HardwareInfo.GetMacAddress() + '|';
            alldata += HardwareInfo.GetProcessorId();
            DialogResult = true;
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {

            DialogResult = false;

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
        // ~RegisterationWindow()
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
