using FluentAssertions;
using PHMIS.Application.Common;
using PHMIS.Application.DTO.Laboratory;
using PHMIS.Test.TestInfrastructure;
using PHMIS.Test.Extensions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace PHMIS.Test.Controllers
{
    public class LabTestIntegrationTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public LabTestIntegrationTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        public async Task InitializeAsync()
        {
            // Clean up groups and tests for isolation
            await _factory.CleanupLabTestGroupsAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Post_CreateLabTest_Works_And_Can_Get()
        {
            // Create group first
            var groupDto = new LabTestGroupCreateDto { Name = "Chemistry" };
            var groupResp = await _client.PostAsJsonAsync("/api/labtestgroup", groupDto);
            groupResp.EnsureSuccessStatusCode();
            var groupList = await _client.GetFromJsonAsync<PagedList<LabTestGroupDto>>("/api/labtestgroup?pageNumber=1&pageSize=10");
            var group = groupList!.Items.First(x => x.Name == groupDto.Name);

            var create = new LabTestCreateDto { Name = "Glucose", Description = "FBS", Price = 11, IsActive = true, LabTestGroupId = group.Id };
            var postResponse = await _client.PostAsJsonAsync("/api/labtest", create);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var listResponse = await _client.GetAsync("/api/labtest?pageNumber=1&pageSize=50");
            listResponse.EnsureSuccessStatusCode();
            var paged = await listResponse.Content.ReadFromJsonAsync<PagedList<LabTestDto>>();
            paged!.Items.Should().Contain(x => x.Name == create.Name && x.Price == create.Price && x.LabTestGroupId == group.Id);

            var item = paged.Items.First(x => x.Name == create.Name);
            var byId = await _client.GetAsync($"/api/labtest/{item.Id}");
            byId.StatusCode.Should().Be(HttpStatusCode.OK);
            var got = await byId.Content.ReadFromJsonAsync<LabTestDto>();
            got!.Name.Should().Be(create.Name);
            got.Price.Should().Be(create.Price);
            got.LabTestGroupId.Should().Be(group.Id);
        }

        [Fact]
        public async Task Post_CreateLabTest_InvalidGroup_ReturnsBadRequest()
        {
            var create = new LabTestCreateDto { Name = "Invalid", LabTestGroupId = 999999 };
            var resp = await _client.PostAsJsonAsync("/api/labtest", create);
            resp.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
