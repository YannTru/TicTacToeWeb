using Abp.Web.Mvc.Views;

namespace TicTacToeWeb.Web.Views
{
    public abstract class TicTacToeWebWebViewPageBase : TicTacToeWebWebViewPageBase<dynamic>
    {

    }

    public abstract class TicTacToeWebWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected TicTacToeWebWebViewPageBase()
        {
            LocalizationSourceName = TicTacToeWebConsts.LocalizationSourceName;
        }
    }
}