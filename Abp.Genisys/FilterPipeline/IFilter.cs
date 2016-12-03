namespace Abp.FilterPipeline
{
    public interface IFilter<in T>
    {
        void Execute(T msg);
    }
}
