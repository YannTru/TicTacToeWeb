using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Kendo.DynamicLinq;

namespace Abp.Application.CRUD
{
    public interface ICrudAppService<out TEntityDto, TPrimaryKey, in TSelectRequestInput, in TCreateInput, in TUpdateInput> 
        where TSelectRequestInput : class
    {
        TEntityDto Get(IdInput<TPrimaryKey> input);
        DataSourceResult GetAll(TSelectRequestInput input);
        TPrimaryKey Create(TCreateInput input);
        void Update(TUpdateInput input);
        void Delete(IdInput<TPrimaryKey> input);
    }

    public interface ICrudAppService<TEntityDto, TPrimaryKey, in TSelectRequestInput> : ICrudAppService<TEntityDto, TPrimaryKey, TSelectRequestInput, TEntityDto, TEntityDto>
        where TSelectRequestInput : class
    {
    }

    public interface ICrudAppService<TEntityDto, TPrimaryKey> : ICrudAppService<TEntityDto, TPrimaryKey, KendoInputDto>
    {
    }

    public interface ICrudAppService<TEntityDto> : ICrudAppService<TEntityDto, int>
    {
    }
}
