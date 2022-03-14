namespace BuddyApiClient.Projects
{
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Projects.Models.Request;
    using BuddyApiClient.Projects.Models.Response;

    public interface IProjectsClient
    {
        Task<ProjectDetails?> Create(string domain, CreateProject content, CancellationToken cancellationToken = default);

        Task<ProjectDetails?> Get(string domain, string name, CancellationToken cancellationToken = default);

        Task<ProjectList?> List(string domain, ListProjectsQuery? query = default, CancellationToken cancellationToken = default);

        IPageIterator ListAll(string domain, ListProjectsQuery pageQuery, PageResponseHandler<ListProjectsQuery, ProjectList> pageResponseHandler);

        Task Delete(string domain, int name, CancellationToken cancellationToken = default);

        Task<ProjectDetails?> Update(string domain, string name, UpdateProject content, CancellationToken cancellationToken = default);
    }
}