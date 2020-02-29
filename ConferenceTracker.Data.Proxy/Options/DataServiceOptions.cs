namespace ConferenceTracker.Proxy.Options
{
    public class DataServiceOptions : RemotingEndpointOptions
    {
        public string SessionsEndpoint { get; set; }

        public string SpeakersEndpoint { get; set; }
    }
}