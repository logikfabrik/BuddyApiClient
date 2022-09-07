namespace BuddyApiClient.Pipelines.Models
{
    internal static class WorkerAssignmentMethodJsonConverter
    {
        public const string FixedAsJson = "FIXED";
        public const string TagsAsJson = "TAGS";

        public static WorkerAssignmentMethod ConvertFrom(string? json)
        {
            return json switch
            {
                FixedAsJson => WorkerAssignmentMethod.Fixed,
                TagsAsJson => WorkerAssignmentMethod.Tags,
                _ => throw new NotSupportedException()
            };
        }

        public static string ConvertTo(WorkerAssignmentMethod? enumValue)
        {
            return enumValue switch
            {
                WorkerAssignmentMethod.Fixed => FixedAsJson,
                WorkerAssignmentMethod.Tags => TagsAsJson,
                _ => throw new NotSupportedException()
            };
        }

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