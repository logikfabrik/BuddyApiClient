namespace BuddyApiClient
{
    using BuddyApiClient.Actions;
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

    public interface IBuddyClient
    {
        IActionsClient Actions { get; }

        ICurrentUserClient CurrentUser { get; }

        ICurrentUserEmailsClient CurrentUserEmails { get; }

        IGroupMembersClient GroupMembers { get; }

        IGroupsClient Groups { get; }

        IMembersClient Members { get; }

        IPermissionSetsClient PermissionSets { get; }

        IPipelinesClient Pipelines { get; }

        IProjectGroupsClient ProjectGroups { get; }

        IProjectMembersClient ProjectMembers { get; }

        IProjectsClient Projects { get; }

        IVariablesClient Variables { get; }

        IWorkspacesClient Workspaces { get; }
    }
}