namespace BuddyApiClient.PermissionSets.Models
{
    internal static class PipelineAccessLevelJsonConverter
    {
        public const string DeniedAsJson = "DENIED";
        public const string ReadOnlyAsJson = "READ_ONLY";
        public const string RunOnlyAsJson = "RUN_ONLY";
        public const string ReadWriteAsJson = "READ_WRITE";

        public static PipelineAccessLevel ConvertFrom(string? json)
        {
            return json switch
            {
                DeniedAsJson => PipelineAccessLevel.Denied,
                ReadOnlyAsJson => PipelineAccessLevel.ReadOnly,
                RunOnlyAsJson => PipelineAccessLevel.RunOnly,
                ReadWriteAsJson => PipelineAccessLevel.ReadWrite,
                _ => throw new NotSupportedException()
            };
        }

        public static string ConvertTo(PipelineAccessLevel? enumValue)
        {
            return enumValue switch
            {
                PipelineAccessLevel.Denied => DeniedAsJson,
                PipelineAccessLevel.ReadOnly => ReadOnlyAsJson,
                PipelineAccessLevel.RunOnly => RunOnlyAsJson,
                PipelineAccessLevel.ReadWrite => ReadWriteAsJson,
                _ => throw new NotSupportedException()
            };
        }
    }
}