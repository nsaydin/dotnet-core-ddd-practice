using System;
using System.Collections.Generic;

namespace Core.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<string> Errors { get; set; }

        public ValidationException(string error) : base(BuildErrorMessage(new List<string> { error }))
        {
            Errors = new List<string> { error };
        }

        public ValidationException(IEnumerable<string> errors) : base(BuildErrorMessage(errors))
        {
            Errors = errors;
        }

        private static string BuildErrorMessage(IEnumerable<string> errors)
        {
            return "Validation failed: " + string.Join(string.Empty, errors);
        }
    }
}