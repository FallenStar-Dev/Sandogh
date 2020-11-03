using SmsIrRestful;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandogh.Bussiness.SendMessage
{
    public class SendSms : ISendMessage
    {
        private readonly string UserApiKey;
        private readonly string SecretKey;
        public SendSms(string apiKey, string secretKey)
        {
            UserApiKey = apiKey;
            SecretKey = secretKey;
        }
        private string GetNewToken()
        {
            SmsIrRestful.Token tk = new SmsIrRestful.Token();
            return tk.GetToken(UserApiKey, SecretKey);
        }
        public void Send(string Reciver, string Message)
        {

            var token = GetNewToken();

            var messageSendObject = new MessageSendObject()
            {
                Messages = new List<string> { Message }.ToArray(),
                MobileNumbers = new List<string> { Reciver }.ToArray(),
                LineNumber = "30006442226442",
                SendDateTime = null,
                CanContinueInCaseOfError = true
            };

            MessageSendResponseObject messageSendResponseObject = new MessageSend().Send(token, messageSendObject);

            if (messageSendResponseObject.IsSuccessful)
            {
                MessageBox.Show("OK");
            }
            else
            {

            }
        }
    }
}
