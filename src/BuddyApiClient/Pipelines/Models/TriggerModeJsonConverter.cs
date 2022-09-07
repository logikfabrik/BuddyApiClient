namespace BuddyApiClient.Pipelines.Models
{
    internal static class TriggerModeJsonConverter
    {
        public const string ClickAsJson = "CLICK";
        public const string EventAsJson = "EVENT";
        public const string ScheduleAsJson = "SCHEDULE";

        public static TriggerMode ConvertFrom(string? json)
        {
            return json switch
            {
                ClickAsJson => TriggerMode.Click,
                EventAsJson => TriggerMode.Event,
                ScheduleAsJson => TriggerMode.Schedule,
                _ => throw new NotSupportedException()
            };
        }

        public static string ConvertTo(TriggerMode? enumValue)
        {
            return enumValue switch
            {
                TriggerMode.Click => ClickAsJson,
                TriggerMode.Event => EventAsJson,
                TriggerMode.Schedule => ScheduleAsJson,
                _ => throw new NotSupportedException()
            };
        }
    }
}