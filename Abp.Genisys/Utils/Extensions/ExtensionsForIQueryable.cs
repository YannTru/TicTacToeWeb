using System.Linq;
using Abp.Application.Services.Dto;
using Kendo.DynamicLinq;

namespace Abp.Utils.Extensions
{
    public static class ExtensionsForIQueryable
    {
        public static DataSourceResult ToDataSourceResult<T>(this IQueryable<T> query, KendoInputDto input)
        {
            return query.ToDataSourceResult(input.Take, input.Skip, input.Sort, input.Filter);
        }
    }
}
