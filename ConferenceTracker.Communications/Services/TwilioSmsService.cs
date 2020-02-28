using ConferenceTracker.Communications.Interfaces;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ConferenceTracker.Communications.Services
{
    public class TwilioSmsService : ITwilioSmsService
    {
        public async Task<bool> SendText(string phoneNumber, string message)
        {
            TwilioClient.Init(AccountSid, AuthToken);

            var result = await MessageResource.CreateAsync(
                body: message,
                from: new Twilio.Types.PhoneNumber("+12057402328"),
                to: new Twilio.Types.PhoneNumber("+1" + phoneNumber)
            );

            return !string.IsNullOrWhiteSpace(result.Sid);
        }

        // Temp account so don't care this is insecure!

        private const string AccountSid = "ACba63b5cb2ab44ca65f1466d3db1414f3";

        private const string AuthToken = "36324b4a7767a193d5b2fa52a43dc73b";
    }
}