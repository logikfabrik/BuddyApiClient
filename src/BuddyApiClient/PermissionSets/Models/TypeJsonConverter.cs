namespace BuddyApiClient.PermissionSets.Models
{
    internal static class TypeJsonConverter
    {
        public const string DeveloperAsJson = "DEVELOPER";
        public const string ReadOnlyAsJson = "READ_ONLY";
        public const string CustomAsJson = "CUSTOM";

        public static Type ConvertFrom(string? json)
        {
            return json switch
            {
                DeveloperAsJson => Type.Developer,
                ReadOnlyAsJson => Type.ReadOnly,
                CustomAsJson => Type.Custom,
                _ => throw new NotSupportedException()
            };
        }
    }
}