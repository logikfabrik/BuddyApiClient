namespace BuddyApiClient.IntegrationTest
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    public sealed class TestOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            var assemblyName = typeof(TestOrderAttribute).AssemblyQualifiedName;

            var sortedMethods = new SortedDictionary<int, List<TTestCase>>();

            foreach (var testCase in testCases)
            {
                var order = testCase.TestMethod.Method
                    .GetCustomAttributes(assemblyName)
                    .FirstOrDefault()
                    ?.GetNamedArgument<int>(nameof(TestOrderAttribute.Order)) ?? 0;

                GetOrCreate(sortedMethods, order).Add(testCase);
            }

            foreach (var testCase in sortedMethods.Keys.SelectMany(order => sortedMethods[order].OrderBy(testCase => testCase.TestMethod.Method.Name)))
            {
                yield return testCase;
            }
        }

        private static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) where TKey : struct where TValue : new()
        {
            return dictionary.TryGetValue(key, out var result)
                ? result
                : dictionary[key] = new TValue();
        }
    }
}