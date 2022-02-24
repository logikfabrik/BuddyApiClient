namespace BuddyApiClient.Projects
{
    using BuddyApiClient.Projects.Models.Response;

    public interface IProjectsClient
    {
        public Task<ProjectDetails?> Get(string domain, string name, CancellationToken cancellationToken = default);
    }
}