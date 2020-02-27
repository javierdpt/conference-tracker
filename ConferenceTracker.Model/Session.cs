using System;
using System.Collections.Generic;

namespace ConferenceTracker.Model
{
    public class Session : BaseIdEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Speaker> Speakers { get; set; }
        public List<Attendee> Attendees { get; set; }
    }
}