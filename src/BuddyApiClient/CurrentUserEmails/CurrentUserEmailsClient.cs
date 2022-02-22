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
            return await HttpClientFacade.Post<EmailDetails>("user/emails", Ensure.Any.HasValue(content, nameof(content)), cancellationToken);
        }

        public async Task<EmailList?> List(CancellationToken cancellationToken = default)
        {
            return await HttpClientFacade.Get<EmailList>("user/emails", cancellationToken);
        }

        public async Task Remove(string email, CancellationToken cancellationToken = default)
        {
            await HttpClientFacade.Delete($"user/emails/{Uri.EscapeDataString(Ensure.String.IsNotNullOrWhiteSpace(email, nameof(email)))}", cancellationToken);
        }
    }
}