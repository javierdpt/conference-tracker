using ConferenceTracker.Model;
using ConferenceTracker.Model.Dtos;
using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConferenceTracker.Data.Interfaces
{
    public interface ISessionDataService : IService
    {
        Task<List<SessionGroupDto>> GetGrouped(int skip, int take);

        Task<List<Session>> GetAll(int skip, int take);

        Task<Session> Get(Guid id);

        Task<Session> Add(Session entity);

        Task<Session> Update(Guid id, Session entity);

        Task<Session> Remove(Guid id);

        Task<long> Count();
    }
}