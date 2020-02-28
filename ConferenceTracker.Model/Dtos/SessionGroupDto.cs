using System;
using System.Collections.Generic;

namespace ConferenceTracker.Model.Dtos
{
    public class SessionGroupDto
    {
        public DateTime Date { get; set; }
        public List<Session> Sessions { get; set; }
    }
}