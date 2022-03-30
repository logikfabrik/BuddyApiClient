namespace BuddyApiClient.Projects.Models.Request
{
    using System.Collections.Specialized;
    using BuddyApiClient.Core.Models.Request;

    public sealed record ListProjectsQuery : SortQuery
    {
        public bool? Membership { get; set; }

        public Status? Status { get; set; }

        public SortProjectsBy? SortBy { get; set; }

        protected override string? GetSortBy()
        {
            if (!SortBy.HasValue)
            {
                return null;
            }

            throw new NotImplementedException();
        }

        private string? GetMembership()
        {
            return Membership?.ToString();
        }

        private string? GetStatus()
        {
            return Status is null ? null : StatusJsonConverter.ConvertTo(Status);
        }

        private void AddMembership(NameValueCollection parameters)
        {
            var membership = GetMembership();

            if (membership is null)
            {
                return;
            }

            parameters.Add("membership", membership);
        }

        private void AddStatus(NameValueCollection parameters)
        {
            var status = GetStatus();

            if (status is null)
            {
                return;
            }

            parameters.Add("status", status);
        }

        protected override void AddParameters(NameValueCollection parameters)
        {
            base.AddParameters(parameters);

            AddMembership(parameters);
            AddStatus(parameters);
        }
    }
}