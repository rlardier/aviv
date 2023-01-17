using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVIV.Domain.Extensions
{
    public static class CustomGuardClause
    {
        public static byte Negative(this IGuardClause guardClause, byte input, string parameterName, string? message = null)
        {
            if (input.CompareTo(default(byte)) < 0)
            {
                throw new ArgumentException(message ?? $"Le champs {parameterName} ne peut pas être négatif", parameterName);
            }

            return input;
        }
    }
}
