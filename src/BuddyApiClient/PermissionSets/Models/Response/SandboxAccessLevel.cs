namespace BuddyApiClient.PermissionSets.Models.Response
{
    public enum SandboxAccessLevel
    {
        Denied,
        ReadOnly,
        ReadWrite
    }

    public static class SandboxAccessLevelJsonConverter
    {
        public static SandboxAccessLevel ConvertFrom(string json)
        {
            return json switch
            {
                "DENIED" => SandboxAccessLevel.Denied,
                "READ_ONLY" => SandboxAccessLevel.ReadOnly,
                "READ_WRITE" => SandboxAccessLevel.ReadWrite,
                _ => throw new NotSupportedException()
            };
        }

        public static string ConvertTo(SandboxAccessLevel sandboxAccessLevel)
        {
            return sandboxAccessLevel switch
            {
                SandboxAccessLevel.Denied => "DENIED",
                SandboxAccessLevel.ReadOnly => "READ_ONLY",
                SandboxAccessLevel.ReadWrite => "READ_WRITE",
                _ => throw new NotSupportedException()
            };
        }
    }
}