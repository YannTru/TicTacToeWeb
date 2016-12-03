using System.Collections.Generic;
using Kendo.DynamicLinq;

namespace Abp.Application.Services.Dto
{
    public class KendoInputDto
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public IEnumerable<Sort> Sort { get; set; }
        public Filter Filter { get; set; }
    }
}
