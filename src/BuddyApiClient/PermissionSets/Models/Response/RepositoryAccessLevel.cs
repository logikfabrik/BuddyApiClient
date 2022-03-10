namespace BuddyApiClient.PermissionSets.Models.Response
{
    public enum RepositoryAccessLevel
    {
        ReadOnly,
        ReadWrite
    }

    public static class RepositoryAccessLevelJsonConverter
    {
        public static RepositoryAccessLevel ConvertFrom(string json)
        {
            return json switch
            {
                "READ_ONLY" => RepositoryAccessLevel.ReadOnly,
                "READ_WRITE" => RepositoryAccessLevel.ReadWrite,
                _ => throw new NotSupportedException()
            };
        }

        public static string ConvertTo(RepositoryAccessLevel repositoryAccessLevel)
        {
            return repositoryAccessLevel switch
            {
                RepositoryAccessLevel.ReadOnly => "READ_ONLY",
                RepositoryAccessLevel.ReadWrite => "READ_WRITE",
                _ => throw new NotSupportedException()
            };
        }
    }
}