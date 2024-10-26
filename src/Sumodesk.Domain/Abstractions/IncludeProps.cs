using System.Linq.Expressions;

namespace Sumodesk.Domain.Abstractions;

public abstract class IncludeProps<T>
{
	private readonly ICollection<Expression<Func<T, object>>> _includedProps = new List<Expression<Func<T, object>>>();

	protected IncludeProps()
	{

	}

	public IEnumerable<Expression<Func<T, object>>> Props { get => _includedProps; }

	protected void Include(Expression<Func<T, object>> include)
	{
		_includedProps.Add(include);
	}
}
