namespace BuddyApiClient.Pipelines.Models
{
    internal static class WorkerAssignmentMethodJsonConverter
    {
        public const string FixedAsJson = "FIXED";
        public const string TagsAsJson = "TAGS";

        public static bool TryConvertTo(WorkerAssignmentMethod? enumValue, out string? json)
        {
            json = enumValue switch
            {
                WorkerAssignmentMethod.Fixed => FixedAsJson,
                WorkerAssignmentMethod.Tags => TagsAsJson,
                _ => null
            };

            return json is not null;
        }
    }
}