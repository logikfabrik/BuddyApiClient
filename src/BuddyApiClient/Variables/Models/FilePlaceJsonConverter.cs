namespace BuddyApiClient.Variables.Models
{
    internal static class FilePlaceJsonConverter
    {
        public const string NoneAsJson = "NONE";
        public const string ContainerAsJson = "CONTAINER";

        public static FilePlace ConvertFrom(string? json)
        {
            return json switch
            {
                NoneAsJson => FilePlace.None,
                ContainerAsJson => FilePlace.Container,
                _ => throw new NotSupportedException()
            };
        }

        public static string ConvertTo(FilePlace? enumValue)
        {
            return enumValue switch
            {
                FilePlace.None => NoneAsJson,
                FilePlace.Container => ContainerAsJson,
                _ => throw new NotSupportedException()
            };
        }
    }
}