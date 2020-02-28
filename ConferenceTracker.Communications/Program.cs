using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;
using Microsoft.ApplicationInsights.ServiceFabric;
using Microsoft.ApplicationInsights.ServiceFabric.Module;
using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;

namespace ConferenceTracker.Communications
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            try
            {
                // The ServiceManifest.XML file defines one or more service type names.
                // Registering a service maps a service type name to a .NET type.
                // When Service Fabric creates an instance of this service type,
                // an instance of the class is created in this host process.

                ServiceRuntime.RegisterServiceAsync("ConferenceTracker.CommunicationsType",
                    context =>
                    {
                        InitTelemetry(context);
                        return new Communications(context);
                    }).GetAwaiter().GetResult();

                ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(Communications).Name);

                // Prevents this host process from terminating so services keep running.
                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
                throw;
            }
        }

        private static void InitTelemetry(ServiceContext context)
        {
            var config = TelemetryConfiguration.CreateDefault();
            config.TelemetryInitializers.Add(
                FabricTelemetryInitializerExtension.CreateFabricTelemetryInitializer(context)
            );
            config.InstrumentationKey = "ad0fbd1f-f4d0-4480-bd92-3522b29144e9";
            config.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());
            config.TelemetryInitializers.Add(new HttpDependenciesParsingTelemetryInitializer());
            new DependencyTrackingTelemetryModule().Initialize(config);
            new ServiceRemotingRequestTrackingTelemetryModule().Initialize(config);
            new ServiceRemotingDependencyTrackingTelemetryModule().Initialize(config);
            new QuickPulseTelemetryModule().Initialize(config);
        }
    }
}