using System.Linq.Expressions;

namespace Sumodesk.Domain.Abstractions;

public abstract class QueryFilter<T> : IncludeProps<T>
	where T : class
{
	private Expression<Func<T, bool>>? _filter;

	protected QueryFilter()
	{

	}

	public Expression<Func<T, bool>> Filter { get => _filter ?? throw new ArgumentNullException(nameof(Filter)); }

	protected void SetQueryFilter(Expression<Func<T, bool>> filter)
	{
		_filter = filter;
	}
}
