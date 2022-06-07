namespace BuddyApiClient.PermissionSets.Models
{
    internal static class RepositoryAccessLevelJsonConverter
    {
        public const string ReadOnlyAsJson = "READ_ONLY";
        public const string ReadWriteAsJson = "READ_WRITE";

        public static RepositoryAccessLevel ConvertFrom(string? json)
        {
            return json switch
            {
                ReadOnlyAsJson => RepositoryAccessLevel.ReadOnly,
                ReadWriteAsJson => RepositoryAccessLevel.ReadWrite,
                _ => throw new NotSupportedException()
            };
        }

        public static string ConvertTo(RepositoryAccessLevel? enumValue)
        {
            return enumValue switch
            {
                RepositoryAccessLevel.ReadOnly => ReadOnlyAsJson,
                RepositoryAccessLevel.ReadWrite => ReadWriteAsJson,
                _ => throw new NotSupportedException()
            };
        }
    }
}