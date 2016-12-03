using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;

namespace Abp.FilterPipeline
{
    public class Pipeline<T> : IFilter<T>
    {
        private readonly List<IFilter<T>> _filters = new List<IFilter<T>>();

        public virtual void Execute(T input)
        {
            var isStopped = new Func<bool>(() => (input is IStopPipeline) && (input as IStopPipeline).StopPipeline);

            _filters.ForEach(f =>
            {
                if (!isStopped()) f.Execute(input);
            });
        }

        // tdgm: filter.execute is still synchronous. Analysis how to make it async.
       // public virtual async Task ExecuteAsync(T input)
       // {
       //     foreach (var f in _filters)
       //     {
       //         var filter = f;
       //         await Task.Run(() => filter.Execute(input)).ConfigureAwait(false);
       //     }
//        }

        /// <summary>
        /// Tdgm - Use IoC to new up filter instances...
        /// Note that Unit test will require Ioc if using this way ???
        ///     - ithink it is not a problem since testing the whole pipeline is an integration test and will use IoC
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        public Pipeline<T> Register<TFilter>() where TFilter : IFilter<T>
        {
            //tdgm do not use the IocContainer directly
            var test = IocManager.Instance.IocContainer.Resolve<TFilter>();
            _filters.Add(test);
            return this;
        }

        public Pipeline<T> Register(IFilter<T> filter)
        {
            _filters.Add(filter);
            return this;
        }
    }

    //tdgm, how to inject this into the pipline ?
    //idea: create an PipeLineFactory that will create the Pipeline.
    //PipeLineFactory would be injected into the ApplicationServices
    public interface IFilterResolver<T>
    {
        IFilter<T> Resolve<TFilter>() where TFilter : IFilter<T>;
    }
}
