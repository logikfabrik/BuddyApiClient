namespace BuddyApiClient.Variables
{
    using BuddyApiClient.Variables.Models;
    using BuddyApiClient.Variables.Models.Request;
    using BuddyApiClient.Variables.Models.Response;

    public interface IVariablesClient
    {
        Task<VariableDetails?> Create(Workspaces.Models.Domain domain, CreateVariable content, CancellationToken cancellationToken = default);

        Task<VariableDetails?> Get(Workspaces.Models.Domain domain, VariableId id, CancellationToken cancellationToken = default);

        Task Delete(Workspaces.Models.Domain domain, VariableId id, CancellationToken cancellationToken = default);
    }
}