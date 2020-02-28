using ConferenceTracker.Data.Interfaces;
using ConferenceTracker.Data.Proxy.Services;
using ConferenceTracker.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConferenceTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {
        private readonly ISpeakerDataService _speakerDataService;

        public SpeakersController(IProxyService proxyService)
        {
            _speakerDataService = proxyService.SpeakerDataService();
        }

        // GET: api/Speakers
        [HttpGet]
        public Task<List<Speaker>> Get([FromQuery] int skip = 0, int take = 500)
        {
            return _speakerDataService.GetAll(skip, take);
        }

        // GET: api/Speakers/5
        [HttpGet("{id}", Name = "GetSpeaker")]
        public Task<Speaker> Get(Guid id)
        {
            return _speakerDataService.Get(id);
        }

        // POST: api/Speakers
        [HttpPost]
        public Task Post([FromBody] Speaker speaker)
        {
            return _speakerDataService.Add(speaker);
        }

        // PUT: api/Speakers/5
        [HttpPut("{id}")]
        public Task<Speaker> Put(Guid id, [FromBody] Speaker speaker)
        {
            return _speakerDataService.Update(id, speaker);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public Task<Speaker> Delete(Guid id)
        {
            return _speakerDataService.Remove(id);
        }
    }
}