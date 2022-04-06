namespace BuddyApiClient
{
    using BuddyApiClient.Core;
    using BuddyApiClient.CurrentUser;
    using BuddyApiClient.CurrentUserEmails;
    using BuddyApiClient.GroupMembers;
    using BuddyApiClient.Groups;
    using BuddyApiClient.Members;
    using BuddyApiClient.PermissionSets;
    using BuddyApiClient.ProjectGroups;
    using BuddyApiClient.ProjectMembers;
    using BuddyApiClient.Projects;
    using BuddyApiClient.Variables;
    using BuddyApiClient.Workspaces;
    using Microsoft.Extensions.Options;

    public sealed class BuddyClient : IBuddyClient
    {
        public BuddyClient(IHttpClientFactory httpClientFactory, IOptions<BuddyClientOptions> options)
            : this(() => HttpClientFacadeFactory.Create(httpClientFactory.CreateClient(), options.Value.BaseUrl, options.Value.AccessToken))
        {
        }

        public BuddyClient(HttpClient httpClient, IOptions<BuddyClientOptions> options)
            : this(() => HttpClientFacadeFactory.Create(httpClient, options.Value.BaseUrl, options.Value.AccessToken))
        {
        }

        private BuddyClient(Func<HttpClientFacade> factory)
        {
            var httpClientFacade = new Lazy<HttpClientFacade>(factory);

            CurrentUser = new CurrentUserClient(httpClientFacade);
            CurrentUserEmails = new CurrentUserEmailsClient(httpClientFacade);
            GroupMembers = new GroupMembersClient(httpClientFacade);
            Groups = new GroupsClient(httpClientFacade);
            Members = new MembersClient(httpClientFacade);
            PermissionSets = new PermissionSetsClient(httpClientFacade);
            ProjectGroups = new ProjectGroupsClient(httpClientFacade);
            ProjectMembers = new ProjectMembersClient(httpClientFacade);
            Projects = new ProjectsClient(httpClientFacade);
            Variables = new VariablesClient(httpClientFacade);
            Workspaces = new WorkspacesClient(httpClientFacade);
        }

        public ICurrentUserClient CurrentUser { get; }

        public ICurrentUserEmailsClient CurrentUserEmails { get; }

        public IGroupMembersClient GroupMembers { get; }

        public IGroupsClient Groups { get; }

        public IMembersClient Members { get; }

        public IPermissionSetsClient PermissionSets { get; }

        public IProjectGroupsClient ProjectGroups { get; }

        public IProjectMembersClient ProjectMembers { get; }

        public IProjectsClient Projects { get; }

        public IVariablesClient Variables { get; }

        public IWorkspacesClient Workspaces { get; }
    }
}