using System.Reflection;
using Abp.Modules;

namespace TicTacToeWeb
{
    [DependsOn(typeof(TicTacToeWebCoreModule))]
    public class TicTacToeWebApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
