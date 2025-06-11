using Serilog.Core;
using Serilog.Events;

namespace CarrerBot.Infra.IoC;

class TimestampEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(
            propertyFactory.CreateProperty(
                "Timestamp",
                logEvent.Timestamp.DateTime));
    }
}
