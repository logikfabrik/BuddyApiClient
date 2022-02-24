namespace BuddyApiClient.Core.Models.Request
{
    using System.Collections.Specialized;
    using System.Text;

    public abstract record Query
    {
        internal string Build()
        {
            var parameters = new NameValueCollection();

            AddParameters(parameters);

            var builder = new StringBuilder();

            var keys = parameters.AllKeys.OrderBy(key => key);

            foreach (var key in keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                var values = parameters.GetValues(key);

                if (values == null)
                {
                    continue;
                }

                foreach (var value in values)
                {
                    builder.Append(builder.Length == 0 ? "?" : "&");
                    builder.Append($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}");
                }
            }

            return builder.ToString();
        }

        protected abstract void AddParameters(NameValueCollection parameters);
    }
}