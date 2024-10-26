using Microsoft.EntityFrameworkCore;
using Sumodesk.Domain;
using Sumodesk.Domain.Abstractions;
using Sumodesk.Domain.Interfaces;
using Throw;

namespace Sumodesk.Infrastructure.Data.Repositories;

public class ReadUpdateRepository<TEntity> : BaseRepository<TEntity>, IReadUpdateRepository<TEntity>
	where TEntity : class, IEntityRoot
{
	public ReadUpdateRepository(DomainDbContext dbContext)
		: base(dbContext)
	{

	}

    public bool AutoSaveChanges { get; set; } = true;

	protected async Task SaveChangesAsync(CancellationToken cancellationToken)
	{
		try
		{
			await DbContext.SaveChangesAsync(cancellationToken);
		}
		catch (Exception ex)
		{

			throw new RepositoryException("An exception occurred in the database while trying to save the data.", ex);
		}
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

