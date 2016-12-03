using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.Logging
{
    /// <summary>
    /// Add this attribute to your application service interface, and the request raw content will be logged.
    /// This is useful when exposing a public api and you want to validate exactly what the external client sent, to troubleshoot web api parameter binding problem.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class RequestContentRawAuditAttribute : Attribute
    {
    }
}
