namespace BuddyApiClient.IntegrationTest.CurrentUserEmails
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using AutoFixture;
    using AutoFixture.Xunit2;
    using BuddyApiClient.CurrentUserEmails.Models.Request;
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

            [Theory]
            [AutoData]
            public async Task Should_Add_And_Return_The_Email(MailAddress address)
            {
                var sut = Fixture.BuddyClient.CurrentUserEmails;

                var email = await sut.Add(new AddEmail(address.Address));

                _email = email?.Email;

                email.Should().NotBeNull();
            }

            [Theory]
            [AutoData]
            public async Task Should_Throw_If_The_Email_Is_Invalid(string invalidEmail)
            {
                var sut = Fixture.BuddyClient.CurrentUserEmails;

                var act = FluentActions.Awaiting(() => sut.Add(new AddEmail(invalidEmail)));

                await act.Should().ThrowAsync<HttpRequestException>();
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

                var address = new Fixture().Create<MailAddress>();

                var email = await client.Add(new AddEmail(address.Address));

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