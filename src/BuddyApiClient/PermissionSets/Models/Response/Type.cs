namespace BuddyApiClient.PermissionSets.Models.Response
{
    using System.Runtime.Serialization;

    public enum Type
    {
        [EnumMember(Value = "DEVELOPER")] Developer,

        [EnumMember(Value = "READ_ONLY")] ReadOnly,

        [EnumMember(Value = "CUSTOM")] Custom
    }
}