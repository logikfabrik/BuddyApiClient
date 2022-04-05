namespace BuddyApiClient.Variables.Models
{
    internal static class TypeJsonConverter
    {
        public const string VariableAsJson = "VAR";
        public const string SshKeyAsJson = "SSH_KEY";

        public static Type ConvertFrom(string? json)
        {
            return json switch
            {
                VariableAsJson => Type.Variable,
                SshKeyAsJson => Type.SshKey,
                _ => throw new NotSupportedException()
            };
        }

        public static string ConvertTo(Type? enumValue)
        {
            return enumValue switch
            {
                Type.Variable => VariableAsJson,
                Type.SshKey => SshKeyAsJson,
                _ => throw new NotSupportedException()
            };
        }
    }
}