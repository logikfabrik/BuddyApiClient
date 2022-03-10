namespace BuddyApiClient.IntegrationTest.PermissionSets
{
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using BuddyApiClient.PermissionSets.Models.Request;
    using Shouldly;
    using Xunit;
    using Xunit.Priority;

    [Collection(nameof(BuddyClientCollection))]
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public sealed class PermissionSetsClientTest
    {
        private const string Domain = "logikfabrik";

        private static int? _permissionSetId;

        private readonly BuddyClientFixture _fixture;

        public PermissionSetsClientTest(BuddyClientFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [AutoData]
        [Priority(0)]
        public async Task Create_Should_Create_And_Return_The_Created_Permission_Set(string name, string description)
        {
            var sut = _fixture.BuddyClient.PermissionSets;

            var permissionSet = await sut.Create(Domain, new CreatePermissionSet(name) { Description = description });

            permissionSet.ShouldNotBeNull();

            _permissionSetId = permissionSet.Id;
        }

        [Fact]
        [Priority(1)]
        public async Task Get_For_Permission_Set_That_Exists_Should_Return_The_Permission_Set()
        {
            var sut = _fixture.BuddyClient.PermissionSets;

            var permissionSet = await sut.Get(Domain, _permissionSetId!.Value);

            permissionSet.ShouldNotBeNull();
        }

        [Fact]
        public async Task Get_For_Permission_Set_That_Does_Not_Exist_Should_Throw()
        {
            var sut = _fixture.BuddyClient.PermissionSets;

            var e = await Assert.ThrowsAsync<HttpRequestException>(() => sut.Get(Domain, 1));

            e.ShouldNotBeNull();
        }

        [Fact]
        public async Task List_Should_Return_The_Permission_Sets()
        {
            var sut = _fixture.BuddyClient.PermissionSets;

            var members = await sut.List(Domain);

            members?.PermissionSets?.Any().ShouldBeTrue();
        }

        [Theory]
        [AutoData]
        [Priority(2)]
        public async Task Update_Should_Update_And_Return_The_Permission_Set(string name)
        {
            var sut = _fixture.BuddyClient.PermissionSets;

            var permissionSet = await sut.Update(Domain, _permissionSetId!.Value, new UpdatePermissionSet { Name = name });

            permissionSet.ShouldNotBeNull();
        }

        [Fact]
        [Priority(3)]
        public async Task Delete_Should_Delete_The_Permission_Set_And_Return_Nothing()
        {
            var sut = _fixture.BuddyClient.PermissionSets;

            await sut.Delete(Domain, _permissionSetId!.Value);

            _permissionSetId = null;
        }
    }
}