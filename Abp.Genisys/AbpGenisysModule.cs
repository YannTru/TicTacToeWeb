using System.Reflection;
using Abp.Logging;
using Abp.Modules;

namespace Abp
{
    public class AbpGenisysModule : AbpModule
    {
        public override void PreInitialize()
        {
            base.PreInitialize();
            //TODO LoggingInterceptorRegistrer.Initialize(IocManager);
        }

        public override void Initialize()
        {
            base.Initialize();
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
