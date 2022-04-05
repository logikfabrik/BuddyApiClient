namespace BuddyApiClient.Projects.Models
{
    internal static class StatusJsonConverter
    {
        public const string ActiveAsJson = "ACTIVE";
        public const string ClosedAsJson = "CLOSED";

        public static Status ConvertFrom(string? json)
        {
            return json switch
            {
                ActiveAsJson => Status.Active,
                ClosedAsJson => Status.Closed,
                _ => throw new NotSupportedException()
            };
        }

        public static string ConvertTo(Status? enumValue)
        {
            return enumValue switch
            {
                Status.Active => ActiveAsJson,
                Status.Closed => ClosedAsJson,
                _ => throw new NotSupportedException()
            };
        }
    }
}