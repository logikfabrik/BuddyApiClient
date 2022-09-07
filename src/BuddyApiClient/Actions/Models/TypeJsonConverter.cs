namespace BuddyApiClient.Actions.Models
{
    internal static class TypeJsonConverter
    {
        public const string SleepAsJson = "SLEEP";

        public static Type ConvertFrom(string? json)
        {
            return json switch
            {
                SleepAsJson => Type.Sleep,
                _ => throw new NotSupportedException()
            };
        }

        public static string ConvertTo(Type? enumValue)
        {
            return enumValue switch
            {
                Type.Sleep => SleepAsJson,
                _ => throw new NotSupportedException()
            };
        }
    }
}