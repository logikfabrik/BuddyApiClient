namespace BuddyApiClient.Pipelines.Models
{
    internal static class EventTypeJsonConverter
    {
        public const string PushAsJson = "PUSH";
        public const string CreateReferenceAsJson = "CREATE_REF";
        public const string DeleteReferenceAsJson = "DELETE_REF";

        public static EventType ConvertFrom(string? json)
        {
            return json switch
            {
                PushAsJson => EventType.Push,
                CreateReferenceAsJson => EventType.CreateReference,
                DeleteReferenceAsJson => EventType.DeleteReference,
                _ => throw new NotSupportedException()
            };
        }

        public static string ConvertTo(EventType? enumValue)
        {
            return enumValue switch
            {
                EventType.Push => PushAsJson,
                EventType.CreateReference => CreateReferenceAsJson,
                EventType.DeleteReference => DeleteReferenceAsJson,
                _ => throw new NotSupportedException()
            };
        }
    }
}