using System;
using System.Collections.Generic;
using System.Linq;
using Core.Exceptions;
using Newtonsoft.Json;

namespace Core
{
    public class Result
    {
        [JsonIgnore] private bool IsFailed => _errors.IsValueCreated && _errors.Value.Any();
        public bool IsSuccess => !IsFailed;
        public string Message { get; set; }

        private readonly Lazy<List<string>> _errors = new Lazy<List<string>>();

        [JsonIgnore] public IEnumerable<string> Errors => _errors.Value;

        public void AddError(string code)
        {
            _errors.Value.Add(code);
        }

        public Result Combine(params Result[] toCombine)
        {
            return Combine(toCombine.AsEnumerable());
        }

        private Result Combine(IEnumerable<Result> results)
        {
            AddErrors(results
                .Where(x => x.IsFailed)
                .SelectMany(x => x.Errors));

            return this;
        }

        private void AddErrors(IEnumerable<string> errors)
        {
            _errors.Value.AddRange(errors);
        }

        public static Result<TResult> Fail<TResult>(string message = "")
        {
            var result = new Result<TResult>()
            {
                Value = default
            };

            result.AddError(message);

            return result;
        }

        public static Result Fail(string message)
        {
            var result = new Result();

            result.AddError(message);

            return result;
        }

        public static Result<TResult> Ok<TResult>(TResult value)
        {
            return new Result<TResult>() { Value = value };
        }

        public static Result<TResult> Ok<TResult>()
        {
            return new Result<TResult>();
        }

        public static Result Ok()
        {
            return new Result();
        }

        public void ValidateAndThrow()
        {
            if (IsFailed)
                throw new ValidationException(Errors);
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }
    }
}