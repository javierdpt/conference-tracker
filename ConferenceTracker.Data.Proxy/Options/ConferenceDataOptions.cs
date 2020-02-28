namespace ConferenceTracker.Data.Proxy.Options
{
    public class ConferenceDataOptions
    {
        public string Uri { get; set; }

        public long PartitionKey { get; set; }

        public string SessionsEndpoint { get; set; }

        public string SpeakersEndpoint { get; set; }
    }
}