using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;

namespace Abp.Logging
{
    /// <summary>
    /// Extend ILogger to be able to log extra properties
    /// </summary>
    public interface ILoggerGenisys : ILogger
    {
        void Debug(string message, IDictionary<string, string> properties);
        void Info(string message, IDictionary<string, string> properties);
        void Warn(string message, IDictionary<string, string> properties);
        void Error(string message, IDictionary<string, string> properties, Exception exception=null);

        void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> measurements = null);
    }
}
