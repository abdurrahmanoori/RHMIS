using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PHMIS.Application.Identity.Models;
using PHMIS.Test.TestInfrastructure;
using Xunit;

namespace PHMIS.Test.Controllers
{
    public class UsersIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public UsersIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task CreateUser_Should_Create_And_Return_Id()
        {
            var dto = new UserCreateDto
            {
                UserName = $"testuser_{Guid.NewGuid():N}",
                Email = $"test_{Guid.NewGuid():N}@local",
                Password = "Pass@123",
                FirstName = "Test",
                LastName = "User",
                Roles = new[] { "User" },
                HospitalId = 1
            };

            var response = await _client.PostAsJsonAsync("/api/users", dto);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var id = await response.Content.ReadFromJsonAsync<int>();
            id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetUsers_Should_Return_List()
        {
            var response = await _client.GetAsync("/api/users");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
