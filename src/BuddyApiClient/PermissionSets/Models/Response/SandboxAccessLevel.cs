namespace BuddyApiClient.PermissionSets.Models.Response
{
    using System.Runtime.Serialization;

    public enum SandboxAccessLevel
    {
        [EnumMember(Value = "DENIED")] Denied,

        [EnumMember(Value = "READ_ONLY")] ReadOnly,

        [EnumMember(Value = "READ_WRITE")] ReadWrite
    }
}