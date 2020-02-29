using ConferenceTracker.Data.Interfaces;
using ConferenceTracker.Model;
using ConferenceTracker.Model.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceTracker.Proxy.Services;

namespace ConferenceTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionDataService _sessionDataService;

        public SessionsController(IProxyService proxyService)
        {
            _sessionDataService = proxyService.SessionDataService();
        }

        // GET: api/Sessions
        [HttpGet]
        public Task<List<Session>> Get([FromQuery] int skip = 0, [FromQuery] int take = 500)
        {
            return _sessionDataService.GetAll(skip, take);
        }

        // GET: api/Sessions/Groups?skip=1&take=2
        [HttpGet("[action]")]
        public Task<List<SessionGroupDto>> Groups(
            [FromQuery] int skip = 0, [FromQuery] int take = 500)
        {
            return _sessionDataService.GetGrouped(skip, take);
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