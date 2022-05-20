namespace BuddyApiClient.Core.Models.Request
{
    public abstract record Query
    {
        internal string Build()
        {
            var parameters = new QueryStringParameters();

            AddParameters(parameters);

            return parameters.ToString();
        }

        protected abstract void AddParameters(QueryStringParameters parameters);
    }
}