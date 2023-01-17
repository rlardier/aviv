namespace AVIV.SharedKernel.Models
{
    public abstract class AppValueObject
        {
            protected static bool EqualOperator(AppValueObject left, AppValueObject right)
            {
                if (left is null ^ right is null)
                {
                    return false;
                }

                return left?.Equals(right) != false;
            }

            protected static bool NotEqualOperator(AppValueObject left, AppValueObject right)
            {
                return !(EqualOperator(left, right));
            }

            protected abstract IEnumerable<object> GetEqualityComponents();

            public override bool Equals(object obj)
            {
                if (obj == null || obj.GetType() != GetType())
                {
                    return false;
                }

                var other = (AppValueObject)obj;
                return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
            }

            public override int GetHashCode()
            {
                return GetEqualityComponents()
                    .Select(x => x != null ? x.GetHashCode() : 0)
                    .Aggregate((x, y) => x ^ y);
            }
        }
}