using System;

namespace ConferenceTracker.Model
{
    public abstract class PersonBase : BaseIdEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}