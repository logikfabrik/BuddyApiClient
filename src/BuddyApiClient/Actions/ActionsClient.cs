﻿namespace BuddyApiClient.Actions
{
    using System.Text.Json;
    using BuddyApiClient.Actions.Models;
    using BuddyApiClient.Actions.Models.Request;
    using BuddyApiClient.Actions.Models.Response;
    using BuddyApiClient.Core;
    using BuddyApiClient.Pipelines.Models;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class ActionsClient : ClientBase, IActionsClient
    {
        private readonly JsonSerializerOptions _deserializationOptions;

        public ActionsClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
            _deserializationOptions = new JsonSerializerOptions { Converters = { new ActionDetailsJsonDeserializer() } };
        }

        public async Task<ActionDetails?> Add(Domain domain, ProjectName projectName, PipelineId pipelineId, AddAction content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/pipelines/{pipelineId}/actions";

            return await HttpClientFacade.Post<ActionDetails>(url, content, _deserializationOptions, cancellationToken);
        }

        public async Task<ActionDetails?> Get(Domain domain, ProjectName projectName, PipelineId pipelineId, ActionId id, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/pipelines/{pipelineId}/actions/{id}";

            return await HttpClientFacade.Get<ActionDetails>(url, _deserializationOptions, cancellationToken);
        }

        public async Task<ActionList?> List(Domain domain, ProjectName projectName, PipelineId pipelineId, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/pipelines/{pipelineId}/actions";

            return await HttpClientFacade.Get<ActionList>(url, _deserializationOptions, cancellationToken);
        }

        public async Task<ActionDetails?> Update(Domain domain, ProjectName projectName, PipelineId pipelineId, ActionId id, UpdateAction content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/pipelines/{pipelineId}/actions/{id}";

            return await HttpClientFacade.Patch<ActionDetails>(url, content, _deserializationOptions, cancellationToken);
        }

        public async Task Remove(Domain domain, ProjectName projectName, PipelineId pipelineId, ActionId id, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/pipelines/{pipelineId}/actions/{id}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }
    }
}