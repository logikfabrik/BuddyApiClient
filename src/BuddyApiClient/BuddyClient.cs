namespace BuddyApiClient
{
    using BuddyApiClient.Actions;
    using BuddyApiClient.Core;
    using BuddyApiClient.CurrentUser;
    using BuddyApiClient.CurrentUserEmails;
    using BuddyApiClient.GroupMembers;
    using BuddyApiClient.Groups;
    using BuddyApiClient.Members;
    using BuddyApiClient.PermissionSets;
    using BuddyApiClient.Pipelines;
    using BuddyApiClient.ProjectGroups;
    using BuddyApiClient.ProjectMembers;
    using BuddyApiClient.Projects;
    using BuddyApiClient.Variables;
    using BuddyApiClient.Workspaces;

    public sealed class BuddyClient : IBuddyClient
    {
        public BuddyClient(string accessToken, Uri? baseUrl = null, HttpClient? httpClient = null)
            : this(() => HttpClientFacadeFactory.Create(accessToken, baseUrl ?? new Uri("https://api.buddy.works"), httpClient ?? new HttpClient()))
        {
        }

        private BuddyClient(Func<HttpClientFacade> factory)
        {
            var httpClientFacade = new Lazy<HttpClientFacade>(factory);

            Actions = new ActionsClient(httpClientFacade);
            CurrentUser = new CurrentUserClient(httpClientFacade);
            CurrentUserEmails = new CurrentUserEmailsClient(httpClientFacade);
            GroupMembers = new GroupMembersClient(httpClientFacade);
            Groups = new GroupsClient(httpClientFacade);
            Members = new MembersClient(httpClientFacade);
            PermissionSets = new PermissionSetsClient(httpClientFacade);
            Pipelines = new PipelinesClient(httpClientFacade);
            ProjectGroups = new ProjectGroupsClient(httpClientFacade);
            ProjectMembers = new ProjectMembersClient(httpClientFacade);
            Projects = new ProjectsClient(httpClientFacade);
            Variables = new VariablesClient(httpClientFacade);
            Workspaces = new WorkspacesClient(httpClientFacade);
        }

        public IActionsClient Actions { get; }

        public ICurrentUserClient CurrentUser { get; }

        public ICurrentUserEmailsClient CurrentUserEmails { get; }

        public IGroupMembersClient GroupMembers { get; }

        public IGroupsClient Groups { get; }

        public IMembersClient Members { get; }

        public IPermissionSetsClient PermissionSets { get; }

        public IPipelinesClient Pipelines { get; }

        public IProjectGroupsClient ProjectGroups { get; }

        public IProjectMembersClient ProjectMembers { get; }

        public IProjectsClient Projects { get; }

        public IVariablesClient Variables { get; }

        public IWorkspacesClient Workspaces { get; }
    }
}