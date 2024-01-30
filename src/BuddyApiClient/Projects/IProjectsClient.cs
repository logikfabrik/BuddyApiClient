namespace BuddyApiClient.Projects
{
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Projects.Models.Request;
    using BuddyApiClient.Projects.Models.Response;
    using BuddyApiClient.Workspaces.Models;

    public interface IProjectsClient
    {
        Task<ProjectDetails?> Create(Domain domain, CreateProject content, CancellationToken cancellationToken = default);

        Task<ProjectDetails?> Get(Domain domain, ProjectName name, CancellationToken cancellationToken = default);

        Task<ProjectList?> List(Domain domain, ListProjectsQuery? query = null, CancellationToken cancellationToken = default);

        ICollectionIterator ListAll(Domain domain, ListProjectsQuery collectionQuery, CollectionPageResponseHandler<ListProjectsQuery, ProjectList> collectionPageResponseHandler);

        Task Delete(Domain domain, ProjectName name, CancellationToken cancellationToken = default);

        Task<ProjectDetails?> Update(Domain domain, ProjectName name, UpdateProject content, CancellationToken cancellationToken = default);
    }
}