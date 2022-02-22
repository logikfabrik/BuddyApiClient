namespace BuddyApiClient.CurrentUserEmails
{
    using BuddyApiClient.CurrentUserEmails.Models.Request;
    using BuddyApiClient.CurrentUserEmails.Models.Response;

    public interface ICurrentUserEmailsClient
    {
        Task<EmailDetails?> Add(AddEmail content, CancellationToken cancellationToken = default);

        Task<EmailList?> List(CancellationToken cancellationToken = default);

        Task Remove(string email, CancellationToken cancellationToken = default);
    }
}