namespace BuddyApiClient.IntegrationTest.CurrentUserEmails
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using BuddyApiClient.CurrentUserEmails.Models.Request;
    using Shouldly;
    using Xunit;
    using Xunit.Priority;

    [Collection(nameof(BuddyClientCollection))]
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public sealed class CurrentUserEmailsClientTest
    {
        private readonly BuddyClientFixture _fixture;

        public CurrentUserEmailsClientTest(BuddyClientFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Priority(0)]
        public async Task Add_Should_Add_And_Return_The_Added_Email()
        {
            var sut = _fixture.BuddyClient.CurrentUserEmails;

            var email = await sut.Add(new AddEmail("jane.doe@logikfabrik.se"));

            email.ShouldNotBeNull();
        }

        [Fact]
        public async Task Add_For_Invalid_Email_Should_Throw()
        {
            var sut = _fixture.BuddyClient.CurrentUserEmails;

            var e = await Assert.ThrowsAsync<HttpRequestException>(() => sut.Add(new AddEmail("INVALID_EMAIL")));

            e.ShouldNotBeNull();
        }

        [Fact]
        [Priority(1)]
        public async Task List_Should_Return_The_Emails()
        {
            var sut = _fixture.BuddyClient.CurrentUserEmails;

            var emails = await sut.List();

            emails.ShouldNotBeNull();
        }

        [Fact]
        [Priority(2)]
        public async Task Remove_Should_Remove_The_Email_And_Return_Nothing()
        {
            var sut = _fixture.BuddyClient.CurrentUserEmails;

            await sut.Remove("jane.doe@logikfabrik.se");
        }
    }
}