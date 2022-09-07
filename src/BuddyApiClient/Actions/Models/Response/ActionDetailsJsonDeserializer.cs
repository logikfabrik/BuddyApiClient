namespace BuddyApiClient.Actions.Models.Response
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public sealed class ActionDetailsJsonDeserializer : JsonConverter<ActionDetails>
    {
        public override ActionDetails? Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
        {
            using var document = JsonDocument.ParseValue(ref reader);

            var root = document.RootElement;

            var typeJson = root.GetProperty("type").GetString();

            var type = GetActionType(typeJson);

            return (ActionDetails?) JsonSerializer.Deserialize(root.GetRawText(), type, options);
        }

        public override void Write(Utf8JsonWriter writer, ActionDetails value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        private static System.Type GetActionType(string? typeJson)
        {
            Type? type;

            try
            {
                type = TypeJsonConverter.ConvertFrom(typeJson);
            }
            catch (NotSupportedException)
            {
                type = null;
            }

            return type switch
            {
                Type.Sleep => typeof(SleepActionDetails),
                _ => typeof(ActionDetails)
            };
        }
    }
}