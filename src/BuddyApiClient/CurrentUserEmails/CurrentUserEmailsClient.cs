namespace BuddyApiClient.CurrentUserEmails
{
    using BuddyApiClient.Core;
    using BuddyApiClient.CurrentUserEmails.Models.Request;
    using BuddyApiClient.CurrentUserEmails.Models.Response;
    using EnsureThat;

    internal sealed class CurrentUserEmailsClient : ClientBase, ICurrentUserEmailsClient
    {
        public CurrentUserEmailsClient(Lazy<HttpClientFacade> httpClientFacade) : base(
            httpClientFacade)
        {
        }

        public async Task<EmailDetails?> Add(AddEmail content, CancellationToken cancellationToken = default)
        {
            const string url = "user/emails";

            return await HttpClientFacade.Post<EmailDetails>(url, content, cancellationToken);
        }

        public async Task<EmailList?> List(CancellationToken cancellationToken = default)
        {
            const string url = "user/emails";

            return await HttpClientFacade.Get<EmailList>(url, cancellationToken);
        }

        public async Task Remove(string email, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrWhiteSpace(email, nameof(email));

            var url = $"user/emails/{email}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }
    }
}