namespace BuddyApiClient.IntegrationTest.Members
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;
    using FluentAssertions;
    using Xunit;
    using Xunit.Priority;

    [Collection(nameof(BuddyClientCollection))]
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public sealed class MembersClientTest
    {
        private const string Domain = "logikfabrik";

        private const string ProjectName = "testproj";

        private static int? _memberId;

        private readonly BuddyClientFixture _fixture;

        public MembersClientTest(BuddyClientFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Priority(0)]
        public async Task Add_Should_Add_And_Return_The_Added_Member()
        {
            var sut = _fixture.BuddyClient.Members;

            var member = await sut.Add(Domain, new AddMember("john.doe@logikfabrik.se"));

            member.Should().NotBeNull();

            _memberId = member.Id;
        }

        [Fact]
        [Priority(1)]
        public async Task Add_For_Project_Should_Add_And_Return_The_Added_Member()
        {
            var sut = _fixture.BuddyClient.Members;

            var member = await sut.Add(Domain, ProjectName, new AddProjectMember(new PermissionSet { Id = 251343 }) { MemberId = _memberId!.Value });

            member.Should().NotBeNull();
        }

        [Fact]
        [Priority(2)]
        public async Task Get_For_Member_That_Exists_Should_Return_The_Member()
        {
            var sut = _fixture.BuddyClient.Members;

            var member = await sut.Get(Domain, _memberId!.Value);

            member.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_For_Member_That_Does_Not_Exist_Should_Throw()
        {
            var sut = _fixture.BuddyClient.Members;

            var e = await Assert.ThrowsAsync<HttpRequestException>(() => sut.Get(Domain, 1));

            e.Should().NotBeNull();
        }

        [Fact]
        public async Task List_Should_Return_The_Members()
        {
            var sut = _fixture.BuddyClient.Members;

            var members = await sut.List(Domain);

            members?.Members?.Any().Should().BeTrue();
        }

        [Fact]
        public async Task List_For_Project_Should_Return_The_Members()
        {
            var sut = _fixture.BuddyClient.Members;

            var members = await sut.List(Domain, ProjectName);

            members?.Members?.Any().Should().BeTrue();
        }

        [Fact]
        public async Task ListAll_Should_Return_The_Members()
        {
            var sut = _fixture.BuddyClient.Members;

            var members = new List<MemberSummary>();

            var pageQuery = new ListMembersQuery();

            var pageIterator = sut.ListAll(Domain, pageQuery, (_, response, _) =>
            {
                members.AddRange(response?.Members ?? Enumerable.Empty<MemberSummary>());

                return Task.FromResult(true);
            });

            await pageIterator.Iterate();

            members.Any().Should().BeTrue();
        }

        [Fact]
        public async Task ListAll_For_Project_Should_Return_The_Members()
        {
            var sut = _fixture.BuddyClient.Members;

            var members = new List<MemberSummary>();

            var pageQuery = new ListMembersQuery();

            var pageIterator = sut.ListAll(Domain, ProjectName, pageQuery, (_, response, _) =>
            {
                members.AddRange(response?.Members ?? Enumerable.Empty<MemberSummary>());

                return Task.FromResult(true);
            });

            await pageIterator.Iterate();

            members.Any().Should().BeTrue();
        }

        [Fact]
        [Priority(3)]
        public async Task Update_Should_Update_And_Return_The_Member()
        {
            var sut = _fixture.BuddyClient.Members;

            var member = await sut.Update(Domain, _memberId!.Value, new UpdateMember { Admin = true });

            member.Should().NotBeNull();
        }

        [Fact]
        [Priority(4)]
        public async Task Update_For_Project_Should_Update_And_Return_The_Member()
        {
            var sut = _fixture.BuddyClient.Members;

            var member = await sut.Update(Domain, ProjectName, _memberId!.Value, new UpdateProjectMember(new PermissionSet { Id = 251346 }));

            member.Should().NotBeNull();
        }

        [Fact]
        [Priority(5)]
        public async Task Remove_For_Project_Should_Remove_The_Member_And_Return_Nothing()
        {
            var sut = _fixture.BuddyClient.Members;

            await sut.Remove(Domain, ProjectName, _memberId!.Value);
        }

        [Fact]
        [Priority(6)]
        public async Task Remove_Should_Remove_The_Member_And_Return_Nothing()
        {
            var sut = _fixture.BuddyClient.Members;

            await sut.Remove(Domain, _memberId!.Value);

            _memberId = null;
        }
    }
}