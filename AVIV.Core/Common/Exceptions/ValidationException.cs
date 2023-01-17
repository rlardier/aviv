using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace AVIV.Core.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("Une ou plusieurs erreurs sont apparues.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(string propertyOnError, string error)
            : base("Une ou plusieurs erreurs sont apparues.")
        {
            Errors = new Dictionary<string, string[]>()
            {
                {propertyOnError, new string[1] { error } }
            };
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}