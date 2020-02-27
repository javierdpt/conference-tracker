using ConferenceTracker.Data.Infrastructure;
using ConferenceTracker.Data.Interfaces;
using ConferenceTracker.Model;
using Microsoft.ServiceFabric.Data;
using System.Threading.Tasks;

namespace ConferenceTracker.Data.Services
{
    public class SessionDataService : BaseDataService<Session>, ISessionDataService
    {
        private readonly SpeakerDataService _speakerDataService;

        public SessionDataService(IReliableStateManager stateManager, SpeakerDataService speakerDataService) : base(stateManager)
        {
            _speakerDataService = speakerDataService;
        }

        protected override async Task<Session> GetRandomEntity()
        {
            var speakers = await _speakerDataService.GetAll(0, -1);
            return TestDataHelper.GetSession(speakers);
        }

        protected override string GetCollectionName() => "sessions";
    }
}