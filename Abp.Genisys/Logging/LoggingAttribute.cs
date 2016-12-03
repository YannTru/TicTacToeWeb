using System;

namespace Abp.Logging
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class LoggingAttribute : Attribute
    {
    }
}
