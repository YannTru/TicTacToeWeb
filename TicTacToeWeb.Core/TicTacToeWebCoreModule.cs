using System.Reflection;
using Abp.Modules;

namespace TicTacToeWeb
{
    public class TicTacToeWebCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
