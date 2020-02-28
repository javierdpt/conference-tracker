using Microsoft.ServiceFabric.Services.Remoting;
using System.Threading.Tasks;

namespace ConferenceTracker.Communications.Interfaces
{
	public interface ITwilioSmsService : IService
	{
		Task<bool> SendText(string phoneNumber, string message);

	}

}
