namespace BuddyApiClient.IntegrationTest
{
    using System;

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestOrderAttribute : Attribute
    {
        public TestOrderAttribute(int order)
        {
            Order = order;
        }

        public int Order { get; }
    }
}