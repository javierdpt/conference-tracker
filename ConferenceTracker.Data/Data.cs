using ConferenceTracker.Data.Services;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;

namespace ConferenceTracker.Data
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class Data : StatefulService, IService
    {
        private readonly SpeakerDataService _speakerDataService;
        private readonly SessionDataService _sessionDataService;

        public Data(StatefulServiceContext context)
            : base(context)
        {
            _speakerDataService = new SpeakerDataService(StateManager);
            _sessionDataService = new SessionDataService(StateManager, _speakerDataService);
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
            {
                new ServiceReplicaListener(c =>
                {
                    var settings = new FabricTransportRemotingListenerSettings { UseWrappedMessage = true };
                    return new FabricTransportServiceRemotingListener(c, _sessionDataService, settings);
                }, "SessionDataEndpoint"),
                new ServiceReplicaListener((c) =>
                {
                    var settings = new FabricTransportRemotingListenerSettings { UseWrappedMessage = true };
                    return new FabricTransportServiceRemotingListener(c, _speakerDataService, settings);
                }, "SpeakerDataEndpoint")
            };
        }

        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            var random = new Random();
            await _speakerDataService.Seed(random.Next(10, 20));
            await _sessionDataService.Seed(random.Next(15, 25));
        }
    }
}