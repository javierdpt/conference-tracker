using ConferenceTracker.Data.Interfaces;

namespace ConferenceTracker.Data.Proxy.Services
{
    public interface IProxyService
    {
        ISessionDataService SessionDataService();
        ISpeakerDataService SpeakerDataService();
    }
}