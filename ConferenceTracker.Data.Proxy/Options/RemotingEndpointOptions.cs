namespace ConferenceTracker.Proxy.Options
{
    public abstract class RemotingEndpointOptions
    {
        public string Uri { get; set; }

        public long PartitionKey { get; set; }
    }
}