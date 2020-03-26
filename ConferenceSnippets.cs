protected override async Task RunAsync(CancellationToken cancellationToken)
{
    var proxy = new ProxyService(
        new OptionsWrapper<DataServiceOptions>(new DataServiceOptions
        {
            Uri = "fabric:/ConferenceTracker/ConferenceTracker.Data",
            PartitionKey = 0,
            SessionsEndpoint = "SessionDataEndpoint",
            SpeakersEndpoint = "SpeakerDataEndpoint"
        }),
        new OptionsWrapper<CommunicationServiceOptions>(new CommunicationServiceOptions
        {
            Uri = "fabric:/ConferenceTracker/ConferenceTracker.Communications",
            Endpoint = "CommunicationsServiceEndpoint"
        }));
    var sessionDataService = proxy.SessionDataService();
    var twilioSmsService = proxy.TwilioSmsService();

    while (true)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var sessions = await sessionDataService.GetAll(0, -1);
            foreach (var session in sessions.Where(s => s.Time > DateTime.Now))
            {
                if ((int)session.Time.Subtract(DateTime.Now).TotalMinutes == 15)
                {
                    await Task.WhenAll(
                        session.Attendees
                            .Where(a => a.PhoneNumber != "5555555555")
                            .Select(a => twilioSmsService.SendText(
                                a.PhoneNumber, $"The session \"{session.Title}\" will start in {session.Time.Subtract(DateTime.Now):g} min"))
                    );
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(65), cancellationToken);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
    }
}

/*
<ItemGroup>
    <PackageReference Include="Microsoft.ServiceFabric.Services" Version="4.0.466" />
    <PackageReference Include="Microsoft.ApplicationInsights.PerfCounterCollector" Version="2.13.1" />
    <PackageReference Include="Microsoft.ApplicationInsights.DependencyCollector" Version="2.13.1" />
    <PackageReference Include="Microsoft.ApplicationInsights.ServiceFabric.Native" Version="2.3.1" />
  </ItemGroup>
*/

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

 ServiceRuntime.RegisterServiceAsync("ConferenceTracker.WorkerType", context =>
{
    InitTelemetry(context);
    return new Worker(context);
}).GetAwaiter().GetResult();