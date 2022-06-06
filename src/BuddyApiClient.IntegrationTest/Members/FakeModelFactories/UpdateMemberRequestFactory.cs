namespace BuddyApiClient.IntegrationTest.Members.FakeModelFactories
{
    using BuddyApiClient.Members.Models.Request;

    internal static class UpdateMemberRequestFactory
    {
        public static UpdateMember Create()
        {
            return new UpdateMember { Admin = true };
        }
    }
}