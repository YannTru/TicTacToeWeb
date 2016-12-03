using Abp.Application.Services;

namespace TicTacToeWeb
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class TicTacToeWebAppServiceBase : ApplicationService
    {
        protected TicTacToeWebAppServiceBase()
        {
            LocalizationSourceName = TicTacToeWebConsts.LocalizationSourceName;
        }
    }
}