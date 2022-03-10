namespace BuddyApiClient.PermissionSets.Models.Response
{
    public enum Type
    {
        Developer,
        ReadOnly,
        Custom
    }

    public static class TypeJsonConverter
    {
        public static Type ConvertFrom(string json)
        {
            return json switch
            {
                "DEVELOPER" => Type.Developer,
                "READ_ONLY" => Type.ReadOnly,
                "CUSTOM" => Type.Custom,
                _ => throw new NotSupportedException()
            };
        }

        public static string ConvertTo(Type type)
        {
            return type switch
            {
                Type.Developer => "DEVELOPER",
                Type.ReadOnly => "READ_ONLY",
                Type.Custom => "CUSTOM",
                _ => throw new NotSupportedException()
            };
        }
    }
}