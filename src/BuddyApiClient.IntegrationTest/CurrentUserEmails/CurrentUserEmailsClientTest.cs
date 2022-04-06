namespace BuddyApiClient.IntegrationTest.CurrentUserEmails
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Bogus.DataSets;
    using BuddyApiClient.CurrentUserEmails.Models.Request;
    using BuddyApiClient.IntegrationTest.CurrentUserEmails.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.Testing;
    using FluentAssertions;
    using Xunit;

    public sealed class CurrentUserEmailsClientTest
    {
        public sealed class Add : BuddyClientTest
        {
            private string? _email;

            public Add(BuddyClientFixture fixture) : base(fixture)
            {
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                if (_email is null)
                {
                    return;
                }

                var client = Fixture.BuddyClient.CurrentUserEmails;

                await client.Remove(_email);

                _email = null;
            }

            [Fact]
            public async Task Should_Add_And_Return_The_Email()
            {
                var sut = Fixture.BuddyClient.CurrentUserEmails;

                var email = await sut.Add(AddEmailFactory.Create());

                _email = email?.Email;

                email.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_If_The_Email_Is_Invalid()
            {
                var sut = Fixture.BuddyClient.CurrentUserEmails;

                var act = FluentActions.Awaiting(() => sut.Add(new AddEmail(new Lorem().Word())));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }

        public sealed class List : BuddyClientTest
        {
            public List(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_Return_Emails()
            {
                var sut = Fixture.BuddyClient.CurrentUserEmails;

                var emails = await sut.List();

                emails?.Emails.Should().NotBeEmpty();
            }
        }

        public sealed class Remove : BuddyClientTest
        {
            private string? _email;

            public Remove(BuddyClientFixture fixture) : base(fixture)
            {
            }

            public override async Task InitializeAsync()
            {
                await base.InitializeAsync();

                var client = Fixture.BuddyClient.CurrentUserEmails;

                var email = await client.Add(AddEmailFactory.Create());

                _email = email?.Email;
            }

            [Fact]
            public async Task Should_Remove_The_Email_And_Return_Nothing()
            {
                if (_email is null)
                {
                    throw new InvalidOperationException();
                }

                var sut = Fixture.BuddyClient.CurrentUserEmails;

                await sut.Remove(_email);

                var emails = (await sut.List())?.Emails.Select(summary => summary.Email);

                emails.Should().NotContain(_email);
            }
        }
    }
}