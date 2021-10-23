﻿public class BaseRepository<TEntity>
    : IBaseRepository<TEntity>
    where TEntity : BaseEntity, new()
{
    private readonly DbSet<TEntity> entities;

    public BaseRepository(DataContext dataContext)
    {
        entities = dataContext.Set<TEntity>();
    }

    public async Task<IQueryable<TEntity>> GetAll() =>
        await Task.FromResult(entities.AsNoTracking());

    public async Task<TEntity> GetById(Guid id) =>
        await entities.AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Id == id);

    public async Task Insert(TEntity entity) =>
        await entities.AddAsync(entity);

    public async Task Update(TEntity entity) =>
        entities.Update(entity);

    public async Task<bool> Delete(Guid id)
    {
        TEntity entity = await entities.FirstOrDefaultAsync(entity => entity.Id == id);
        if (entity is not null)
        {
            entities.Remove(entity);
            return true;
        }
        return false;
    }
}