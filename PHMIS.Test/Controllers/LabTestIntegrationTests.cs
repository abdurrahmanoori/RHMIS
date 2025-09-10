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
            // Clean up groups (and cascading lab tests) for isolation
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
        public async Task Get_ListLabTests_ReturnsPagedList()
        {
            // Prepare a group and one test
            var groupDto = new LabTestGroupCreateDto { Name = "Hematology" };
            var groupResp = await _client.PostAsJsonAsync("/api/labtestgroup", groupDto);
            groupResp.EnsureSuccessStatusCode();
            var groupList = await _client.GetFromJsonAsync<PagedList<LabTestGroupDto>>("/api/labtestgroup?pageNumber=1&pageSize=10");
            var group = groupList!.Items.First(x => x.Name == groupDto.Name);

            var create = new LabTestCreateDto { Name = "CBC", Description = "Complete Blood Count", Price = 20, IsActive = true, LabTestGroupId = group.Id };
            var post = await _client.PostAsJsonAsync("/api/labtest", create);
            post.EnsureSuccessStatusCode();

            var response = await _client.GetAsync("/api/labtest?pageNumber=1&pageSize=2");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var paged = await response.Content.ReadFromJsonAsync<PagedList<LabTestDto>>();
            paged.Should().NotBeNull();
            paged!.Items.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Get_ById_ReturnsNotFound_ForUnknownId()
        {
            var response = await _client.GetAsync("/api/labtest/999999");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Put_UpdateLabTest_Works()
        {
            // Create group and test
            var groupDto = new LabTestGroupCreateDto { Name = "Microbiology" };
            var groupResp = await _client.PostAsJsonAsync("/api/labtestgroup", groupDto);
            groupResp.EnsureSuccessStatusCode();
            var groupList = await _client.GetFromJsonAsync<PagedList<LabTestGroupDto>>("/api/labtestgroup?pageNumber=1&pageSize=10");
            var group = groupList!.Items.First(x => x.Name == groupDto.Name);

            var create = new LabTestCreateDto { Name = "Culture", Description = "Urine culture", Price = 50, IsActive = true, LabTestGroupId = group.Id };
            var post = await _client.PostAsJsonAsync("/api/labtest", create);
            post.EnsureSuccessStatusCode();

            var listResponse = await _client.GetAsync("/api/labtest?pageNumber=1&pageSize=50");
            var list = await listResponse.Content.ReadFromJsonAsync<PagedList<LabTestDto>>();
            var existing = list!.Items.First(x => x.Name == create.Name);

            var updated = new LabTestCreateDto { Name = existing.Name, Description = existing.Description, Price = 60, IsActive = false, LabTestGroupId = group.Id };
            var updateResponse = await _client.PutAsJsonAsync($"/api/labtest/{existing.Id}", updated);
            updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var updatedDto = await updateResponse.Content.ReadFromJsonAsync<LabTestDto>();
            updatedDto!.Price.Should().Be(60);
            updatedDto.IsActive.Should().BeFalse();
        }

        [Fact]
        public async Task Delete_LabTest_Works_And_ReturnsNotFound_WhenMissing()
        {
            // Create group and test
            var groupDto = new LabTestGroupCreateDto { Name = "Serology" };
            var groupResp = await _client.PostAsJsonAsync("/api/labtestgroup", groupDto);
            groupResp.EnsureSuccessStatusCode();
            var groupList = await _client.GetFromJsonAsync<PagedList<LabTestGroupDto>>("/api/labtestgroup?pageNumber=1&pageSize=10");
            var group = groupList!.Items.First(x => x.Name == groupDto.Name);

            var create = new LabTestCreateDto { Name = "HIV", Description = "Rapid", Price = 15, IsActive = true, LabTestGroupId = group.Id };
            var post = await _client.PostAsJsonAsync("/api/labtest", create);
            post.EnsureSuccessStatusCode();

            var listResponse = await _client.GetAsync("/api/labtest?pageNumber=1&pageSize=50");
            var list = await listResponse.Content.ReadFromJsonAsync<PagedList<LabTestDto>>();
            var existing = list!.Items.First(x => x.Name == create.Name);

            var deleteResponse = await _client.DeleteAsync($"/api/labtest/{existing.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var deleteAgain = await _client.DeleteAsync($"/api/labtest/{existing.Id}");
            deleteAgain.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
