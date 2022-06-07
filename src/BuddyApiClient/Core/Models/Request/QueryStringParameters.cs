namespace BuddyApiClient.Core.Models.Request
{
    using System.Collections.Specialized;
    using System.Text;
    using EnsureThat;

    public sealed class QueryStringParameters
    {
        private readonly NameValueCollection _parameters;

        public QueryStringParameters()
        {
            _parameters = new NameValueCollection();
        }

        private void Add(string name, string? value)
        {
            Ensure.String.IsNotNullOrEmpty(name, nameof(name));

            if (value is null)
            {
                return;
            }

            _parameters.Add(name, value);
        }

        public void Add(string name, Func<string?> value)
        {
            Add(name, value());
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            var keys = _parameters.AllKeys;

            if (!keys.Any())
            {
                return string.Empty;
            }

            // For predictability (testability) we put the keys in alphabetical ascending order.
            foreach (var key in keys.OrderBy(key => key))
            {
                if (string.IsNullOrEmpty(key))
                {
                    continue;
                }

                // For predictability (testability) we put the values in alphabetical ascending order.
                var values = _parameters.GetValues(key)?.OrderBy(value => value).ToArray() ?? Array.Empty<string>();

                foreach (var value in values)
                {
                    builder.Append(builder.Length == 0 ? "?" : "&");
                    builder.Append($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}");
                }
            }

            return builder.ToString();
        }
    }
}