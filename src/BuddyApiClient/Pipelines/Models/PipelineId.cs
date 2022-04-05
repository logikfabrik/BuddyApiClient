namespace BuddyApiClient.Pipelines.Models
{
    [StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
    public partial struct PipelineId
    {
    }
}