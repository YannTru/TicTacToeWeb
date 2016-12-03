using Abp.Web.Mvc.Controllers;

namespace TicTacToeWeb.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class TicTacToeWebControllerBase : AbpController
    {
        protected TicTacToeWebControllerBase()
        {
            LocalizationSourceName = TicTacToeWebConsts.LocalizationSourceName;
        }
    }
}