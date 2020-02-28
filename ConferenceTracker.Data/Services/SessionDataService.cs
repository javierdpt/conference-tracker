using ConferenceTracker.Data.Infrastructure;
using ConferenceTracker.Data.Interfaces;
using ConferenceTracker.Model;
using Microsoft.ServiceFabric.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceTracker.Model.Dtos;

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

        public async Task<List<SessionGroupDto>> GetGrouped(int skip, int take)
        {
            var sessions = await GetAll(0, -1);
            var groups = from s in sessions
                         group s by new DateTime(s.Time.Year, s.Time.Month, s.Time.Day, s.Time.Hour, 0, 0)
                         into grp
                         orderby grp.Key
                         select new SessionGroupDto
                         {
                             Date = grp.Key,
                             Sessions = grp.ToList()
                         };

            return groups.Skip(skip).Take(take).ToList();
        }
    }
}