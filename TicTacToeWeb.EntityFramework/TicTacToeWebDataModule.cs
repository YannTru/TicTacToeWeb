using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using TicTacToeWeb.EntityFramework;

namespace TicTacToeWeb
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(TicTacToeWebCoreModule))]
    public class TicTacToeWebDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<TicTacToeWebDbContext>(null);
        }
    }
}
