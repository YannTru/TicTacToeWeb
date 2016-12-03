using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.Web
{
    public interface IUrlProvider
    {
        string GetApplicationPathAbsoluteUrl();
    }
}
