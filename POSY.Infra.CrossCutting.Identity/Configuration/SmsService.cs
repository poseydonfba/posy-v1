using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Twilio;

namespace POSY.Infra.CrossCutting.Identity.Configuration
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            if (ConfigurationManager.AppSettings["Internet"] == "true")
            {
                // Utilizando TWILIO como SMS Provider.
                // https://www.twilio.com/docs/quickstart/csharp/sms/sending-via-rest

                const string accountSid = "AC80f3b49d90619bb6c232330bc0bc23a7";
                const string authToken = "2498607386c0e1a81a0568d15fa53ee2";

                var client = new TwilioRestClient(accountSid, authToken);

                client.SendMessage("+12566662708", message.Destination, message.Body);
            }

            return Task.FromResult(0);
        }
    }
}