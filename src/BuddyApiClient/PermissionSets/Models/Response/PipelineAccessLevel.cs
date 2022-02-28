namespace BuddyApiClient.PermissionSets.Models.Response
{
    using System.Runtime.Serialization;

    public enum PipelineAccessLevel
    {
        [EnumMember(Value = "DENIED")] Denied,

        [EnumMember(Value = "READ_ONLY")] ReadOnly,

        [EnumMember(Value = "RUN_ONLY")] RunOnly,

        [EnumMember(Value = "READ_WRITE")] ReadWrite
    }
}