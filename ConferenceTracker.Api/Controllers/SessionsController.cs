using ConferenceTracker.Data.Interfaces;
using ConferenceTracker.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConferenceTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionDataService _sessionDataService;

        public SessionsController()
        {
            _sessionDataService = ServiceProxy.Create<ISessionDataService>(
                new Uri("fabric:/ConferenceTracker/ConferenceTracker.Data"),
                new ServicePartitionKey(0),
                TargetReplicaSelector.Default,
                "SessionDataEndpoint"
            );
        }

        // GET: api/Sessions
        [HttpGet]
        public Task<List<Session>> Get([FromQuery] int skip = 0, int take = 500)
        {
            return _sessionDataService.GetAll(skip, take);
        }

        // GET: api/Sessions/5
        [HttpGet("{id}", Name = "GetSession")]
        public Task<Session> Get(Guid id)
        {
            return _sessionDataService.Get(id);
        }

        // POST: api/Sessions
        [HttpPost]
        public Task Post([FromBody] Session session)
        {
            return _sessionDataService.Add(session);
        }

        // PUT: api/Sessions/5
        [HttpPut("{id}")]
        public Task<Session> Put(Guid id, [FromBody] Session session)
        {
            return _sessionDataService.Update(id, session);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public Task<Session> Delete(Guid id)
        {
            return _sessionDataService.Remove(id);
        }
    }
}