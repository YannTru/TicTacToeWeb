using System;
using System.Threading;

namespace Abp.FilterPipeline.PipelineDecorators
{
    public class RetryDecorator<T>: Pipeline<T>
    {
        private readonly Pipeline<T> _pipeline;
        private readonly int _maxRetries;
        private readonly int _slideTime;

        private int _currentRetry = 0;


        public RetryDecorator(Pipeline<T> pipeline, int maxRetries=3, int slideTime=0)
        {
            _pipeline = pipeline;

            _maxRetries = maxRetries;
            _slideTime = slideTime;
        }


        public override void Execute(T input)
        {
            try
            {
                _pipeline.Execute(input);
            }
            catch (Exception)
            {
                _currentRetry++;
                if (_currentRetry <= _maxRetries)
                {
                    if (_slideTime > 0)
                    {
                        Thread.Sleep(_slideTime * _currentRetry);
                    }
                    Execute(input);
                }
                else
                {
                    throw;
                }
            }
        }
        
    }
}
