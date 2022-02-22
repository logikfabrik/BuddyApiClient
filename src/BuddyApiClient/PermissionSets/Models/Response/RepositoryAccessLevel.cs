namespace BuddyApiClient.PermissionSets.Models.Response
{
    using System.Runtime.Serialization;

    public enum RepositoryAccessLevel
    {
        [EnumMember(Value = "READ_ONLY")] ReadOnly,

        [EnumMember(Value = "READ_WRITE")] ReadWrite
    }
}