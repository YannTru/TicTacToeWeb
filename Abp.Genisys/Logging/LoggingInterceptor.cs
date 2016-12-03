using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Abp.Application.Services;
using Abp.Collections.Extensions;
using Abp.Utils.Extensions;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace Abp.Logging
{
    public class LoggingInterceptor : IInterceptor
    {
        private readonly ILoggerGenisys _logger;

        // List of param names that will be logged to application insights in a standalone property, so it is easier to search
        private readonly List<String> _commonParamNames = new List<string>()
        {
            "taskId", "projectId"
        };

        public LoggingInterceptor(
            ILoggerGenisys logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            
            bool doLog = 
                invocation.MethodInvocationTarget.IsDefined(typeof (LoggingAttribute), true)
                || typeof(ICommandApplicationService).IsAssignableFrom(invocation.TargetType);

            if (!doLog)
            {
                invocation.Proceed();
                return;
            }

            var log = new
            {
                Method = invocation.TargetType.FullName + "." + invocation.MethodInvocationTarget.Name,
                Args = invocation.Arguments,

                ServiceName = invocation.MethodInvocationTarget.DeclaringType != null
                    ? invocation.MethodInvocationTarget.DeclaringType.FullName
                    : "",
                MethodName = invocation.MethodInvocationTarget.Name,
                Parameters = ConvertArgumentsToJson(invocation),
                TimeStamp = DateTime.Now

            };

            var propertyDict = new Dictionary<string, string>
            {
                {"ggcServiceName", log.ServiceName},
                {"ggcMethodName", log.MethodName},
                {"ggcMethodDirection", "IN"},
                {"ggcParameters", log.Parameters}
            };

            var commonParams = GetCommonParametersAsProperties(invocation);
            foreach (var commonParam in commonParams)
            {
                propertyDict.Add(commonParam.Key, commonParam.Value);
            }

            _logger.Info("IN - " + log.ServiceName, propertyDict);


            var stopwatch = Stopwatch.StartNew();
            try
            {
                invocation.Proceed();
            }
            finally
            {
                stopwatch.Stop();
            }


            _logger.Info("OUT - " + log.ServiceName, new Dictionary<string, string>
            {
                {"ggcServiceName", log.ServiceName},
                {"ggcMethodName", log.MethodName },
                {"ggcMethodDirection","OUT"},
                {"ggcReturnValue", invocation.ReturnValue.ToJson(formatted:false) },
                {"ggcDuration", Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds).ToString(CultureInfo.InvariantCulture)}
            }); 
        }


        private string ConvertArgumentsToJson(IInvocation invocation)
        {
            try
            {
                var parameters = invocation.MethodInvocationTarget.GetParameters();
                if (parameters.IsNullOrEmpty())
                {
                    return "{}";
                }

                var dictionary = new Dictionary<string, object>();
                for (int i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    var argument = invocation.Arguments[i];
                    dictionary[parameter.Name] = argument;
                    
                }

                var jsonResult = "";
                try
                {
                    jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(dictionary,
                        new JsonSerializerSettings()
                        {
                            ContractResolver = new JsonLimitLengthContractResolver(maxPropertyLength:50),
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                            Formatting = Formatting.None
                        });
                }
                catch (Exception ex)
                {
                    jsonResult = "errorToJson = " + ex.Message;
                }
                return jsonResult;
            }
            catch (Exception ex)
            {
                _logger.Warn("Could not serialize parameters for method: " + invocation.MethodInvocationTarget.Name);
                _logger.Warn(ex.ToString(), ex);
                return "{}";
            }
        }

        private Dictionary<string, string> GetCommonParametersAsProperties(IInvocation invocation)
        {
            try
            {
                var parameters = invocation.MethodInvocationTarget.GetParameters();
                if (parameters.IsNullOrEmpty())
                {
                    return new Dictionary<string, string>();
                }

                var dictionary = new Dictionary<string, string>();
                for (int i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    var argument = invocation.Arguments[i];

                    var paramTypeProps = argument.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    

                    // We are limited to 200 unique properties for the whole app, so that may not be a good idea to use a property for each dto properties.
                    foreach (var prop in paramTypeProps
                        .Where(x => _commonParamNames.Contains(x.Name, StringComparer.CurrentCultureIgnoreCase)))
                    {
                        var value = prop.GetValue(argument);
                        var strValue = "";
                        if (value != null)
                            strValue = value.ToString();
                        dictionary["ggc" + prop.Name] = strValue;
                    }
                }

                return dictionary;
            }
            catch (Exception ex)
            {
                _logger.Warn("Could not serialize arguments for method: " + invocation.MethodInvocationTarget.Name);
                _logger.Warn(ex.ToString(), ex);
                return new Dictionary<string, string>();
            }
        }
    }
}
