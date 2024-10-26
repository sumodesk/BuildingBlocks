using Microsoft.EntityFrameworkCore;
using Sumodesk.Domain.Abstractions;
using Sumodesk.Domain.Interfaces;
using Throw;

namespace Sumodesk.Infrastructure.Data.Repositories;

public class ReadOnlyRepository<TEntity> : BaseRepository<TEntity>, IReadOnlyRespository<TEntity>
	where TEntity : class
{
	public ReadOnlyRepository(DomainDbContext dbContext)
		: base(dbContext)
	{

	}

	public async Task<bool> AnyAsync(QueryFilter<TEntity>? filter = null, CancellationToken cancellationToken = default)
	{
		var query = filter != null
			? IncludeProps(DbSet, filter.Props)
			: DbSet;

		return filter != null
			? await query.Where(filter.Filter).AsNoTracking().AnyAsync(cancellationToken)
			: await query.AsNoTracking().AnyAsync(cancellationToken);
	}

	public async Task<int> CountAsync(QueryFilter<TEntity>? filter = null, CancellationToken cancellationToken = default)
	{
		var query = filter != null
			? IncludeProps(DbSet, filter.Props)
			: DbSet;

		return filter != null
			? await query.Where(filter.Filter).AsNoTracking().CountAsync(cancellationToken)
			: await query.AsNoTracking().CountAsync(cancellationToken);
	}

	public async Task<IReadOnlyList<TEntity>> GetAllAsync(IncludeProps<TEntity>? includeProps = null, CancellationToken cancellationToken = default)
	{
		var query = includeProps != null
			? IncludeProps(DbSet, includeProps.Props)
			: DbSet;

		return EnableTracking
			? await query.ToListAsync(cancellationToken)
			: await query.AsNoTracking().ToListAsync(cancellationToken);
	}

	public async Task<IReadOnlyList<TEntity>> GetFilteredAsync(QueryFilter<TEntity> filter, CancellationToken cancellationToken = default)
	{
		filter.ThrowIfNull();

		var query = IncludeProps(DbSet, filter.Props)
			.Where(filter.Filter);

		return EnableTracking
			? await query.ToListAsync(cancellationToken)
			: await query.AsNoTracking().ToListAsync(cancellationToken);
	}

	public async Task<TEntity?> GetOneAsync(QueryFilter<TEntity> filter, CancellationToken cancellationToken = default)
	{
		filter.ThrowIfNull();

		var query = IncludeProps(DbSet, filter.Props)
			.Where(filter.Filter);

		return EnableTracking
			? await query.FirstOrDefaultAsync(cancellationToken)
			: await query.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
}

