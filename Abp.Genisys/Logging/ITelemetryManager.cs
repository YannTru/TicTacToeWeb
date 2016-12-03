using System.Collections.Generic;

namespace Abp.Logging
{
    public interface ITelemetryManager
    {
        void TrackTrace(string message, IDictionary<string, string> properties = null);
    }
}