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
         // Temp account so don't care this is insecure!
         const string accountSid = "ACba63b5cb2ab44ca65f1466d3db1414f3";
         const string authToken = "36324b4a7767a193d5b2fa52a43dc73b";

         TwilioClient.Init(accountSid, authToken);

         var result = await MessageResource.CreateAsync(
             body: "Join Earth's mightiest heroes. Like Kevin Bacon.",
             from: new Twilio.Types.PhoneNumber("+12057402328"),
             to: new Twilio.Types.PhoneNumber("+13056322230")
         );

         return !string.IsNullOrWhiteSpace(result.Sid);
      }
   }
}
