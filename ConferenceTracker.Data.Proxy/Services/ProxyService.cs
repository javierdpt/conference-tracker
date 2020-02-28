using ConferenceTracker.Data.Interfaces;
using ConferenceTracker.Data.Proxy.Options;
using Microsoft.Extensions.Options;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;

namespace ConferenceTracker.Data.Proxy.Services
{
    public class ProxyService : IProxyService
    {
        private readonly ConferenceDataOptions _options;

        public ProxyService(IOptions<ConferenceDataOptions> options)
        {
            _options = options.Value;
        }

        public ISessionDataService SessionDataService() => ServiceProxy.Create<ISessionDataService>(
            new Uri(_options.Uri),
            new ServicePartitionKey(_options.PartitionKey),
            TargetReplicaSelector.Default,
            _options.SessionsEndpoint
        );

        public ISpeakerDataService SpeakerDataService() => ServiceProxy.Create<ISpeakerDataService>(
            new Uri(_options.Uri),
            new ServicePartitionKey(_options.PartitionKey),
            TargetReplicaSelector.Default,
            _options.SpeakersEndpoint
        );
    }
}