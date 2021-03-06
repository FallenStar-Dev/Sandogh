﻿using System;
using System.Windows;

using Sandogh.Bussiness;
using Sandogh.Utility.Cryptography;

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
            var activationKey = Aes.Encrypt(TxtSerial.Text.Trim(), TxtHardwareSerial.Text, 256);
            TxtActivation.Text = activationKey;
            if (TxtActivation.Text.Equals(activationKey))
            {               
                RegistryOperator.CreateKey("SerialNumber", TxtSerial.Text.Trim());
                RegistryOperator.CreateKey("ActivationKey", activationKey);
                DialogResult = true;
            }
            Window_Reset();
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            DialogResult = false;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TxtSerial.Focus();
        }

        private void Window_Reset()
        {
            TxtSerial.Clear();
            TxtHardwareSerial.Clear();
            TxtActivation.Clear();
            TxtSerial.Focus();
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

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton.Equals(true)) DragMove();
        }

        private void TxtSerial_LostFocus(object sender, RoutedEventArgs e)
        {
            TxtHardwareSerial.Text = HardwareInfo.GetHddSerialNo();
        }
    }
}
