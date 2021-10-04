using System;
using System.Linq.Expressions;
using Moq;

namespace Core.Tests.Extensions
{
    public static class MyIt
    {
        public static Expression<Func<TValue, bool>> IsAnyExp<TValue>()
        {
            return It.IsAny<Expression<Func<TValue, bool>>>();
        }
    }
}