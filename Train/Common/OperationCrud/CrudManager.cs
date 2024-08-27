using Common.Entity;
using System.Linq.Expressions;
using AutoMapper;
using Common.Response;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Common.OperationCrud;

public class CrudManager<T, TId, TDatabase> : ICrudManager<T,TId,TDatabase> where TDatabase : DbContext where T : BaseEntity<TId>
{
    private readonly IMapper _mapper;
    private readonly TDatabase _dbContext;

    public CrudManager(IMapper mapper, TDatabase dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }


    public async Task<bool> Create(T entity)
    {
        Util.Utils.NotNull(entity);
        entity.CreateDateTime = DateTime.Now;
        _dbContext.Entry(entity).State = EntityState.Added;
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<T> UpdateByIdObject(TId id, object inputEntity)
    {
        var oldEntity = await GetById(id);
        Util.Utils.NotNull(oldEntity);
        var entity = _mapper.Map(inputEntity, oldEntity);
        entity.UpdateDateTime = DateTime.Now;
        _dbContext.Entry(entity).State = EntityState.Modified;
        var save = await _dbContext.SaveChangesAsync() > 0;
        Util.Utils.StateOperation(save);
        return entity;
    }

    public async Task<bool> UpdateById(TId id, object inputEntity)
    {
        var oldEntity = await GetById(id);
        Util.Utils.NotNull(oldEntity);
        var entity = _mapper.Map(inputEntity, oldEntity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        var save = await _dbContext.SaveChangesAsync() > 0;
        Util.Utils.StateOperation(save);
        return save;
    }

    public async Task<T> Update(object inputEntity)
    {
        _dbContext.Update(inputEntity);
        var save = await _dbContext.SaveChangesAsync()>0;
        Util.Utils.StateOperation(save);
        return (T) inputEntity;
    }

    public async Task<bool> DeleteById(TId id)
    {
        var entity = await _dbContext.Set<T>().Where(q => q.Id!.Equals(id)).FirstOrDefaultAsync();
        Util.Utils.NotNull(entity);
        entity!.IsDeleted = true;
        _dbContext.Entry(entity).State = EntityState.Modified;
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<T> GetById(TId id)
    {
        return await _dbContext.Set<T>().Where(q => q.Id.Equals(id)).AsTracking().FirstOrDefaultAsync();
    }

    public async Task<TR?> FindById<TR>(TId id,Expression<Func<T, TR>> expression) where TR : class
    {
        return await _dbContext.Set<T>().Where(q => q.Id.Equals(id))
            .Select(expression)
            .AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TR>> GetList<TR>(Expression<Func<T, TR>> expression) where TR : class
    {
        return await _dbContext.Set<T>().Select(expression).ToListAsync();
    }

    public async Task<bool> HasRecord(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().AsNoTracking().AnyAsync(predicate);
    }

    public async Task<TResult?> SelectByPredicate<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> predicateSelect)
    {
        return await _dbContext.Set<T>().Where(predicate).Select(predicateSelect).FirstOrDefaultAsync();
    }

    public DbSet<T> GetEntity() => _dbContext.Set<T>();

}

public interface ICrudManager<T, in TId, in TDatabase> where T : BaseEntity<TId>  where TDatabase : DbContext
{
    Task<bool> Create(T entity);
    Task<T> UpdateByIdObject(TId id, object inputEntity);
    Task<bool> UpdateById(TId id, object inputEntity);
    Task<T> Update(object inputEntity);
    Task<bool> DeleteById(TId id);
    Task<T> GetById(TId id);
    Task<TR?> FindById<TR>(TId id, Expression<Func<T, TR>> expression) where TR : class;
    Task<IEnumerable<TR>> GetList<TR>(Expression<Func<T, TR>> expression) where TR : class;


     Task<bool> HasRecord(Expression<Func<T, bool>> predicate);
    Task<TResult?> SelectByPredicate<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> predicateSelect);
    DbSet<T> GetEntity();
}