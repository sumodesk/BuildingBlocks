namespace Sumodesk.Domain.Abstractions;

public abstract class ValueObject : IEquatable<ValueObject>
{
	protected static bool EqualOperator(ValueObject left, ValueObject right)
	{
		if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
		{
			return false;
		}
		return ReferenceEquals(left, right) || left!.Equals(right);
	}

	protected static bool NotEqualOperator(ValueObject left, ValueObject right)
	{
		return !(EqualOperator(left, right));
	}

	protected virtual IEnumerable<object> GetEqualityComponents()
	{
		var components = new List<object>
			{
				Guid.NewGuid().ToString()
			};

		return components;
	}

	public override bool Equals(object obj)
	{
		if (obj == null || obj.GetType() != GetType())
		{
			return false;
		}

		var other = (ValueObject)obj;

		return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
	}

	public override int GetHashCode()
	{
		return GetEqualityComponents()
			.Select(x => x != null ? x.GetHashCode() : 0)
			.Aggregate((x, y) => x ^ y);
	}

	public static bool operator ==(ValueObject one, ValueObject two)
	{
		return EqualOperator(one, two);
	}

	public static bool operator !=(ValueObject one, ValueObject two)
	{
		return NotEqualOperator(one, two);
	}

	public bool Equals(ValueObject? other)
	{
		return other != null && EqualOperator(this, other);
	}
}

