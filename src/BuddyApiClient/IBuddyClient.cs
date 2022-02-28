namespace BuddyApiClient
{
    using BuddyApiClient.CurrentUser;
    using BuddyApiClient.CurrentUserEmails;
    using BuddyApiClient.Workspaces;

    public interface IBuddyClient
    {
        ICurrentUserClient CurrentUser { get; }

        ICurrentUserEmailsClient CurrentUserEmails { get; }

        IWorkspacesClient Workspaces { get; }
    }
}