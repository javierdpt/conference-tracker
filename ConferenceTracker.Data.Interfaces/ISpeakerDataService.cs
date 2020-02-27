using ConferenceTracker.Model;
using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConferenceTracker.Data.Interfaces
{
    public interface ISpeakerDataService : IService
    {
        Task<List<Speaker>> GetAll(int skip, int take);

        Task<Speaker> Get(Guid id);

        Task<Speaker> Add(Speaker entity);

        Task<Speaker> Update(Guid id, Speaker entity);

        Task<Speaker> Remove(Guid id);

        Task<long> Count();
    }
}