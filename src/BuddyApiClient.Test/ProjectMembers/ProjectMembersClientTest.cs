﻿namespace BuddyApiClient.Test.ProjectMembers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using BuddyApiClient.Core;
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Members.Models;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;
    using BuddyApiClient.PermissionSets.Models;
    using BuddyApiClient.ProjectMembers;
    using BuddyApiClient.ProjectMembers.Models.Request;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Test.Testing;
    using BuddyApiClient.Workspaces.Models;
    using FluentAssertions;
    using RichardSzalay.MockHttp;
    using Xunit;

    public sealed class ProjectMembersClientTest
    {
        private static IProjectMembersClient CreateClient(MockHttpMessageHandler handler)
        {
            return new ProjectMembersClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handler.ToHttpClient(), new Uri("https://api.buddy.works"), null)));
        }

        public sealed class Add
        {
            [Theory]
            [FileData(@"ProjectMembers/.testdata/Add_Should_Add_And_Return_The_Project_Member.json")]
            public async Task Should_Add_And_Return_The_Project_Member(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works/workspaces/buddy/projects/company-website/members").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectMember = await sut.Add(new Domain("buddy"), new ProjectName("company-website"), new AddProjectMember(new PermissionSet { Id = new PermissionSetId(2) }) { MemberId = new MemberId(2) });

                projectMember.Should().NotBeNull();
            }
        }

        public sealed class Get
        {
            [Theory]
            [FileData(@"ProjectMembers/.testdata/Get_Should_Return_The_Project_Member_If_It_Exists.json")]
            public async Task Should_Return_The_Project_Member_If_It_Exists(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/members/1").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectMember = await sut.Get(new Domain("buddy"), new ProjectName("company-website"), new MemberId(1));

                projectMember.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_If_The_Project_Member_Does_Not_Exist()
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/members/1").Respond(HttpStatusCode.NotFound);

                var sut = CreateClient(handlerStub);

                var act = FluentActions.Awaiting(() => sut.Get(new Domain("buddy"), new ProjectName("company-website"), new MemberId(1)));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class List
        {
            [Theory]
            [FileData(@"ProjectMembers/.testdata/List_Should_Return_Project_Members_If_Any_Exists.json")]
            public async Task Should_Return_Project_Members_If_Any_Exists(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/members").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectMembers = await sut.List(new Domain("buddy"), new ProjectName("company-website"));

                projectMembers?.Members.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"ProjectMembers/.testdata/List_Should_Not_Return_Project_Members_If_None_Exist.json")]
            public async Task Should_Not_Return_Project_Members_If_None_Exist(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/members").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectMembers = await sut.List(new Domain("buddy"), new ProjectName("company-website"));

                projectMembers?.Members.Should().BeEmpty();
            }
        }

        public sealed class ListAll
        {
            [Theory]
            [FileData(@"ProjectMembers/.testdata/ListAll_Should_Return_Project_Members_If_Any_Exists.json")]
            public async Task Should_Return_Project_Members_If_Any_Exists(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, $"https://api.buddy.works/workspaces/buddy/projects/company-website/members?page={PageIterator.DefaultPageIndex}&per_page={PageIterator.DefaultPageSize}").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectMembers = new List<MemberSummary>();

                var pageQuery = new ListMembersQuery();

                var pageIterator = sut.ListAll(new Domain("buddy"), new ProjectName("company-website"), pageQuery, (_, response, _) =>
                {
                    projectMembers.AddRange(response?.Members ?? Enumerable.Empty<MemberSummary>());

                    return Task.FromResult(true);
                });

                await pageIterator.Iterate();

                projectMembers.Should().NotBeEmpty();
            }
        }

        public sealed class Remove
        {
            [Fact]
            public async Task Should_Remove_The_Project_Member_And_Return_Nothing()
            {
                var handlerMock = new MockHttpMessageHandler();

                handlerMock.Expect(HttpMethod.Delete, "https://api.buddy.works/workspaces/buddy/projects/company-website/members/1").Respond(HttpStatusCode.NoContent);

                var sut = CreateClient(handlerMock);

                await sut.Remove(new Domain("buddy"), new ProjectName("company-website"), new MemberId(1));

                handlerMock.VerifyNoOutstandingExpectation();
            }
        }

        public sealed class Update
        {
            [Theory]
            [FileData(@"ProjectMembers/.testdata/Update_Should_Update_And_Return_The_Project_Member.json")]
            public async Task Should_Update_And_Return_The_Project_Member(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Patch, "https://api.buddy.works/workspaces/buddy/projects/company-website/members/1").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectMember = await sut.Update(new Domain("buddy"), new ProjectName("company-website"), new MemberId(1), new UpdateProjectMember(new PermissionSet { Id = new PermissionSetId(1) }));

                projectMember.Should().NotBeNull();
            }
        }
    }
}