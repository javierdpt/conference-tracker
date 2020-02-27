using ConferenceTracker.Data.Infrastructure;
using ConferenceTracker.Data.Interfaces;
using ConferenceTracker.Model;
using Microsoft.ServiceFabric.Data;
using System.Threading.Tasks;

namespace ConferenceTracker.Data.Services
{
    public class SpeakerDataService : BaseDataService<Speaker>, ISpeakerDataService
    {
        public SpeakerDataService(IReliableStateManager stateManager) : base(stateManager)
        {
        }

        protected override Task<Speaker> GetRandomEntity()
        {
            return Task.FromResult(TestDataHelper.GetRandomSpeaker());
        }

        protected override string GetCollectionName() => "speakers";
    }
}