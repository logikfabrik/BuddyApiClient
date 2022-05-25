namespace BuddyApiClient.Test.Core.Models.Request
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BuddyApiClient.Core.Models.Request;
    using FluentAssertions;
    using Xunit;

    public sealed class QueryStringParametersTest
    {
        public new sealed class ToString
        {
            [Theory]
            [MemberData(nameof(GetParameters), 1)]
            [MemberData(nameof(GetParameters), 3)]
            public void Should_Return_Query(IEnumerable<(string, Func<string?>)> parameters, string expected)
            {
                var sut = new QueryStringParameters();

                foreach (var (name, value) in parameters)
                {
                    sut.Add(name, value);
                }

                sut.ToString().Should().Be(expected);
            }

            public static IEnumerable<object[]> GetParameters(int count)
            {
                var parameters = new List<(string, Func<string?>)>();

                // Parameters are added in alphabetical descending order, and expected query will be in alphabetical ascending order - to test/verify the parameter order.
                for (var i = count; i > 0; i--)
                {
                    var key = $"key{i}";
                    var value = $"value{i}";

                    parameters.Add(new ValueTuple<string, Func<string?>>(key, () => value));
                }

                return new List<object[]> { new object[] { parameters, $"?{string.Join("&", Enumerable.Range(1, count).Select(i => $"key{i}=value{i}"))}" } };
            }
        }
    }
}