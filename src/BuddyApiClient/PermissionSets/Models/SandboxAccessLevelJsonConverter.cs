namespace BuddyApiClient.PermissionSets.Models
{
    public static class SandboxAccessLevelJsonConverter
    {
        public const string DeniedAsJson = "DENIED";
        public const string ReadOnlyAsJson = "READ_ONLY";
        public const string ReadWriteAsJson = "READ_WRITE";

        public static SandboxAccessLevel ConvertFrom(string? json)
        {
            return json switch
            {
                DeniedAsJson => SandboxAccessLevel.Denied,
                ReadOnlyAsJson => SandboxAccessLevel.ReadOnly,
                ReadWriteAsJson => SandboxAccessLevel.ReadWrite,
                _ => throw new NotSupportedException()
            };
        }

        public static string ConvertTo(SandboxAccessLevel? enumValue)
        {
            return enumValue switch
            {
                SandboxAccessLevel.Denied => DeniedAsJson,
                SandboxAccessLevel.ReadOnly => ReadOnlyAsJson,
                SandboxAccessLevel.ReadWrite => ReadWriteAsJson,
                _ => throw new NotSupportedException()
            };
        }
    }
}