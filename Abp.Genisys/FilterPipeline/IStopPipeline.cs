using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.FilterPipeline
{
    /// <summary>
    /// Used to stop the execution of following filters in the pipeline.
    /// This should be implemented by your Pipeline message class
    /// **Note that the UoW will still be commited
    /// </summary>
    public interface IStopPipeline
    {
        bool StopPipeline { get; set; }
    }
}
