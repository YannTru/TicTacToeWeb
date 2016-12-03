using System.Linq;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Utils.Extensions;
using Kendo.DynamicLinq;

namespace Abp.Application.CRUD
{
    public class CrudAppService<TRepository, TEntity, TEntityDto, TPrimaryKey, TSelectRequestInput, TCreateInput, TUpdateInput>
        : ApplicationServiceBase, ICrudAppService<TEntityDto, TPrimaryKey, TSelectRequestInput, TCreateInput, TUpdateInput>
        where TSelectRequestInput : KendoInputDto
        where TRepository : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TUpdateInput : EntityRequestInput<TPrimaryKey>
    {
        protected readonly TRepository Repository;

        static CrudAppService()
        {
        }

        public CrudAppService(TRepository repository)
        {
            Repository = repository;
        }

        public TEntityDto Get(IdInput<TPrimaryKey> input)
        {
            return Repository.Get(input.Id).MapTo<TEntityDto>();
        }

        public virtual DataSourceResult GetAll(TSelectRequestInput input)
        {
            var query = CreateQueryable(input);
            return query.ToDataSourceResult(input);
        }

        public virtual TPrimaryKey Create(TCreateInput input)
        {
            return Repository.InsertOrUpdateAndGetId(input.MapTo<TEntity>());
        }

        public virtual void Update(TUpdateInput input)
        {
            var entity = Repository.Get(input.Id);
            input.MapTo(entity);
        }

        public virtual void Delete(IdInput<TPrimaryKey> input)
        {
            Repository.Delete(input.Id);
        }

        protected virtual IQueryable<TEntity> CreateQueryable(TSelectRequestInput input)
        {
            return Repository.GetAll().OrderBy(x => x.Id);
        }
    }

    public class CrudAppService<TRepository, TEntity, TEntityDto, TPrimaryKey, TSelectRequestInput>
        : CrudAppService<TRepository, TEntity, TEntityDto, TPrimaryKey, TSelectRequestInput, TEntityDto, TEntityDto>
        where TSelectRequestInput : KendoInputDto
        where TRepository : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : EntityRequestInput<TPrimaryKey>
    {
        public CrudAppService(TRepository repository)
            : base(repository)
        {
        }
    }

    public class CrudAppService<TRepository, TEntity, TEntityDto, TPrimaryKey>
        : CrudAppService<TRepository, TEntity, TEntityDto, TPrimaryKey, KendoInputDto>
        where TRepository : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : EntityRequestInput<TPrimaryKey>
    {
        public CrudAppService(TRepository repository)
            : base(repository)
        {
        }
    }

    public class CrudAppService<TEntity, TEntityDto, TPrimaryKey> : CrudAppService<IRepository<TEntity, TPrimaryKey>, TEntity, TEntityDto, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : EntityRequestInput<TPrimaryKey>
    {
        public CrudAppService(IRepository<TEntity, TPrimaryKey> repository)
            : base(repository)
        {
        }
    }

    public class CrudAppService<TEntity, TEntityDto> : CrudAppService<TEntity, TEntityDto, int>
        where TEntity : class, IEntity<int>
        where TEntityDto : EntityRequestInput<int>
    {
        public CrudAppService(IRepository<TEntity, int> repository)
            : base(repository)
        {
        }
    }
}
