using Abp.Application.Services;
using Abp.Dependency;
using Castle.Core;

namespace Abp.Logging
{
    public static class LoggingInterceptorRegistrer
    {
        public static void Initialize(IIocManager iocManager)
        {
            iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private static void Kernel_ComponentRegistered(string key, Castle.MicroKernel.IHandler handler)
        {
            if (typeof(IApplicationService).IsAssignableFrom(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(LoggingInterceptor)));
            }
        }
    }
}
