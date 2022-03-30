﻿namespace BuddyApiClient
{
    using BuddyApiClient.CurrentUser;
    using BuddyApiClient.CurrentUserEmails;
    using BuddyApiClient.Members;
    using BuddyApiClient.PermissionSets;
    using BuddyApiClient.Projects;
    using BuddyApiClient.Workspaces;

    public interface IBuddyClient
    {
        ICurrentUserClient CurrentUser { get; }

        ICurrentUserEmailsClient CurrentUserEmails { get; }

        IMembersClient Members { get; }

        IPermissionSetsClient PermissionSets { get; }

        IProjectsClient Projects { get; }

        IWorkspacesClient Workspaces { get; }
    }
}