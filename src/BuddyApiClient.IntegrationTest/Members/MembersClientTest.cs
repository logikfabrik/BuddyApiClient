namespace BuddyApiClient.IntegrationTest.Members
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Bogus;
    using BuddyApiClient.Members.Models;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;
    using BuddyApiClient.PermissionSets.Models;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Workspaces.Models;
    using FluentAssertions;
    using Xunit;
    using Xunit.Priority;

    // TODO: Rewrite using preconditions

    [Collection(nameof(BuddyClientCollection))]
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public sealed class MembersClientTest
    {
        private static readonly Domain Domain = new("logikfabrik");

        private static readonly ProjectName ProjectName = new("424dc608-b925-4389-9e0b-893a9a06c2b2");

        private static MemberId? _memberId;

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

            _memberId = member?.Id;
        }

        [Fact]
        [Priority(1)]
        public async Task Add_For_Project_Should_Add_And_Return_The_Added_Member()
        {
            var sut = _fixture.BuddyClient.Members;

            var member = await sut.Add(Domain, ProjectName, new AddProjectMember(new PermissionSet { Id = new PermissionSetId(251343) }) { MemberId = _memberId!.Value });

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

            var e = await Assert.ThrowsAsync<HttpRequestException>(() => sut.Get(Domain, new MemberId(10000000)));

            e.Should().NotBeNull();
        }

        [Fact]
        public async Task List_Should_Return_The_Members()
        {
            var sut = _fixture.BuddyClient.Members;

            var members = await sut.List(Domain);

            members?.Members.Any().Should().BeTrue();
        }

        [Fact]
        public async Task List_For_Project_Should_Return_The_Members()
        {
            var sut = _fixture.BuddyClient.Members;

            var members = await sut.List(Domain, ProjectName);

            members?.Members.Any().Should().BeTrue();
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

            var member = await sut.Update(Domain, ProjectName, _memberId!.Value, new UpdateProjectMember(new PermissionSet { Id = new PermissionSetId(251346) }));

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

        public sealed class Add : BuddyClientTest
        {
            private readonly Precondition<Domain> _domainExistsPrecondition;
            private readonly Precondition<MemberId> _memberExistsPrecondition;
            private readonly Precondition<PermissionSetId> _permissionSetExistsPrecondition;
            private readonly Precondition<ProjectName> _projectExistsPrecondition;

            public Add(BuddyClientFixture fixture) : base(fixture)
            {
                var faker = new Faker();

                _domainExistsPrecondition = new DomainExistsPrecondition(fixture.BuddyClient.Workspaces);
                _memberExistsPrecondition = new MemberExistsPrecondition(fixture.BuddyClient.Members, _domainExistsPrecondition, faker.Internet.ExampleEmail());
                _permissionSetExistsPrecondition = new PermissionSetExistsPrecondition(fixture.BuddyClient.PermissionSets, _domainExistsPrecondition, faker.Lorem.Word());
                _projectExistsPrecondition = new ProjectExistsPrecondition(fixture.BuddyClient.Projects, _domainExistsPrecondition, faker.Lorem.Word());
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await _domainExistsPrecondition.TearDown();
                await _memberExistsPrecondition.TearDown();
                await _permissionSetExistsPrecondition.TearDown();
                await _projectExistsPrecondition.TearDown();
            }

            [Fact]
            public async Task Should_Add_And_Return_The_Member()
            {
                var domain = await _domainExistsPrecondition.SetUp();

                var sut = Fixture.BuddyClient.Members;

                var member = await sut.Add(domain, new AddMember(new Faker().Internet.ExampleEmail()));

                member.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Add_And_Return_The_Project_Member()
            {
                var domain = await _domainExistsPrecondition.SetUp();
                var projectName = await _projectExistsPrecondition.SetUp();
                var memberId = await _memberExistsPrecondition.SetUp();
                var permissionSetId = await _permissionSetExistsPrecondition.SetUp();

                var sut = Fixture.BuddyClient.Members;

                var member = await sut.Add(domain, projectName, new AddProjectMember(new PermissionSet { Id = permissionSetId }) { MemberId = memberId });

                member.Should().NotBeNull();
            }
        }
    }
}