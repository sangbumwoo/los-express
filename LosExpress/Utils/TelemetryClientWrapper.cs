using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;

namespace LosExpress.Utils
{
    public class TelemetryClientWrapper
    {
        private TelemetryClient tClient = null;
        public TelemetryContext Context = null;

        public TelemetryClientWrapper()
        {
            tClient = new TelemetryClient();
        }

        public TelemetryClientWrapper(TelemetryConfiguration configuration)
        {
            tClient = new TelemetryClient(configuration);
        }

        public TelemetryContext ContextGet()
        {
            return tClient.Context;
        }

        public bool isEnabled()
        {
            return tClient.IsEnabled();
        }

        public virtual void TrackEvent(EventTelemetry telemetry)
        {
            tClient.TrackEvent(telemetry);
        }

        public virtual void TrackEvent(string eventName)
        {
            tClient.TrackEvent(eventName);
        }

        public virtual void TrackEvent(string eventName, IDictionary<string, string> properties, IDictionary<string, double> metrics)
        {
            tClient.TrackEvent(eventName, properties, metrics);
        }

        public virtual void TrackException(Exception exception)
        {
            tClient.TrackException(exception);
        }

        public virtual void TrackException(ExceptionTelemetry telemetry)
        {
            tClient.TrackException(telemetry);
        }

        public virtual void TrackException(Exception exception, IDictionary<string, string> properties, IDictionary<string, double> metrics)
        {
            tClient.TrackException(exception, properties, metrics);
        }

        public virtual void TrackMetric(MetricTelemetry telemetry)
        {
            tClient.TrackMetric(telemetry);
        }

        public virtual void TrackMetric(string name, double value)
        {
            tClient.TrackMetric(name, value);
        }

        public virtual void TrackMetric(string name, double value, IDictionary<string, string> properties)
        {
            tClient.TrackMetric(name, value, properties);
        }

        public virtual void TrackPageView(PageViewTelemetry telemetry)
        {
            tClient.TrackPageView(telemetry);
        }

        public virtual void TrackPageView(string name)
        {
            tClient.TrackPageView(name);
        }

        public virtual void TrackRequest(RequestTelemetry request)
        {
            tClient.TrackRequest(request);
        }

        public virtual void TrackRequest(string name, DateTimeOffset timestamp, TimeSpan duration, string responseCode, bool success)
        {
            tClient.TrackRequest(name, timestamp, duration, responseCode, success);
        }

        public virtual void TrackTrace(string message)
        {
            tClient.TrackTrace(message);
        }

        public virtual void TrackTrace(TraceTelemetry telemetry)
        {
            tClient.TrackTrace(telemetry);
        }

        public virtual void TrackTrace(string message, IDictionary<string, string> properties)
        {
            tClient.TrackTrace(message, properties);
        }

        public virtual void TrackTrace(string message, SeverityLevel severityLevel)
        {
            tClient.TrackTrace(message, severityLevel);
        }

        public virtual void TrackTrace(string message, SeverityLevel severityLevel, IDictionary<string, string> properties)
        {
            tClient.TrackTrace(message, severityLevel, properties);
        }
    }
}