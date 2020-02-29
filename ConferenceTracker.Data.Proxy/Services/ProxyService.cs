using System;
using ConferenceTracker.Communications.Interfaces;
using ConferenceTracker.Data.Interfaces;
using ConferenceTracker.Proxy.Options;
using Microsoft.Extensions.Options;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace ConferenceTracker.Proxy.Services
{
    public class ProxyService : IProxyService
    {
        private readonly DataServiceOptions _dataServiceOptions;
        private readonly CommunicationServiceOptions _communicationServiceOptions;

        public ProxyService(
            IOptions<DataServiceOptions> dataServiceOptions,
            IOptions<CommunicationServiceOptions> communicationServiceOptions)
        {
            _dataServiceOptions = dataServiceOptions.Value;
            _communicationServiceOptions = communicationServiceOptions.Value;
        }

        public ISessionDataService SessionDataService() => ServiceProxy.Create<ISessionDataService>(
            new Uri(_dataServiceOptions.Uri),
            new ServicePartitionKey(_dataServiceOptions.PartitionKey),
            listenerName: _dataServiceOptions.SessionsEndpoint
        );

        public ISpeakerDataService SpeakerDataService() => ServiceProxy.Create<ISpeakerDataService>(
            new Uri(_dataServiceOptions.Uri),
            new ServicePartitionKey(_dataServiceOptions.PartitionKey),
            listenerName: _dataServiceOptions.SpeakersEndpoint
        );

        public ITwilioSmsService TwilioSmsService() => ServiceProxy.Create<ITwilioSmsService>(
            new Uri(_communicationServiceOptions.Uri),
            listenerName: _communicationServiceOptions.Endpoint
        );
    }
}