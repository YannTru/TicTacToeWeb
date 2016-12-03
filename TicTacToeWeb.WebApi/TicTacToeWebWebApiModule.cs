using System.Reflection;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;

namespace TicTacToeWeb
{
    [DependsOn(typeof(AbpWebApiModule), typeof(TicTacToeWebApplicationModule))]
    public class TicTacToeWebWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(TicTacToeWebApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
