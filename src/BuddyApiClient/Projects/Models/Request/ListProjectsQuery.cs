namespace BuddyApiClient.Projects.Models.Request
{
    using BuddyApiClient.Core.Models.Request;

    public sealed record ListProjectsQuery : SortQuery
    {
        public bool? Membership { get; set; }

        public Status? Status { get; set; }

        public SortProjectsBy? SortBy { get; set; }

        protected override string? GetSortBy()
        {
            return SortBy switch
            {
                SortProjectsBy.Name => "name",
                SortProjectsBy.CreateDate => "create_date",
                SortProjectsBy.RepositorySize => "repository_size",
                _ => null
            };
        }

        private string? GetMembership()
        {
            return Membership?.ToString();
        }

        private string? GetStatus()
        {
            return Status is null ? null : StatusJsonConverter.ConvertTo(Status);
        }

        private void AddMembership(QueryStringParameters parameters)
        {
            parameters.Add("membership", GetMembership);
        }

        private void AddStatus(QueryStringParameters parameters)
        {
            parameters.Add("status", GetStatus);
        }

        protected override void AddParameters(QueryStringParameters parameters)
        {
            base.AddParameters(parameters);

            AddMembership(parameters);
            AddStatus(parameters);
        }
    }
}