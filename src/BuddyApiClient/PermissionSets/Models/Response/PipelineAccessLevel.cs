namespace BuddyApiClient.PermissionSets.Models.Response
{
    public enum PipelineAccessLevel
    {
        Denied,
        ReadOnly,
        RunOnly,
        ReadWrite
    }

    public static class PipelineAccessLevelJsonConverter
    {
        public static PipelineAccessLevel ConvertFrom(string json)
        {
            return json switch
            {
                "DENIED" => PipelineAccessLevel.Denied,
                "READ_ONLY" => PipelineAccessLevel.ReadOnly,
                "RUN_ONLY" => PipelineAccessLevel.RunOnly,
                "READ_WRITE" => PipelineAccessLevel.ReadWrite,
                _ => throw new NotSupportedException()
            };
        }

        public static string ConvertTo(PipelineAccessLevel pipelineAccessLevel)
        {
            return pipelineAccessLevel switch
            {
                PipelineAccessLevel.Denied => "DENIED",
                PipelineAccessLevel.ReadOnly => "READ_ONLY",
                PipelineAccessLevel.RunOnly => "RUN_ONLY",
                PipelineAccessLevel.ReadWrite => "READ_WRITE",
                _ => throw new NotSupportedException()
            };
        }
    }
}