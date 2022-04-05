namespace BuddyApiClient.Variables
{
    using BuddyApiClient.Variables.Models.Request;
    using BuddyApiClient.Variables.Models.Response;

    public interface IVariablesClient
    {
        Task<VariableDetails?> Create(Workspaces.Models.Domain domain, CreateVariable content, CancellationToken cancellationToken = default);
    }
}