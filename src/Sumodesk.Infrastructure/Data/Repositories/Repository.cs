using Microsoft.EntityFrameworkCore;
using Sumodesk.Domain.Abstractions;
using Sumodesk.Domain.Interfaces;
using Throw;

namespace Sumodesk.Infrastructure.Data.Repositories;

public class Repository<TEntity> : ReadOnlyRepository<TEntity>, IRepository<TEntity>, IReadUpdateRepository<TEntity>
	where TEntity : class, IEntityRoot
{
    public Repository(DomainDbContext dbContext)
		: base(dbContext)
    {
        
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		entity.ThrowIfNull();

		DbSet.Add(entity);

		if (AutoSaveChanges)
		{
			await SaveChangesAsync(cancellationToken);
		}

		return entity;
	}

	public async Task<TEntity?> GetByIdAsync(int id, IncludeProps<TEntity>? includeProps = default, CancellationToken token = default)
	{
		id.Throw().IfLessThanOrEqualTo(0);

		var query = includeProps != null
			? IncludeProps(DbSet, includeProps.Props)
			: DbSet;

		query = query.Where(e => e.Id == id);

		return EnableTracking
			? await query.FirstOrDefaultAsync()
			: await query.AsNoTracking().FirstOrDefaultAsync();
	}

	public async Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		entity.ThrowIfNull();

		DbSet.Remove(entity);

		if (AutoSaveChanges) 
		{
			await SaveChangesAsync(cancellationToken);
		}
		
		return entity;
			
	}

	public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		entity.ThrowIfNull();

		DbSet.Update(entity);

		if (AutoSaveChanges)
		{
			await SaveChangesAsync(cancellationToken);
		}

		return entity;
	}
}
