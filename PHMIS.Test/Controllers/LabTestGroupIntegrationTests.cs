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
    public class LabTestGroupIntegrationTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public LabTestGroupIntegrationTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        public async Task InitializeAsync()
        {
            await _factory.CleanupLabTestGroupsAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Post_CreateLabTestGroup_ReturnsOk_And_CanGetById()
        {
            var dto = new LabTestGroupCreateDto { Name = "Chemistry", Description = "Chemistry tests", SortOrder = 1 };
            var postResponse = await _client.PostAsJsonAsync("/api/labtestgroup", dto);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var created = await postResponse.Content.ReadFromJsonAsync<LabTestGroupCreateDto>();
            created.Should().NotBeNull();
            created!.Name.Should().Be(dto.Name);

            var listResponse = await _client.GetAsync("/api/labtestgroup?pageNumber=1&pageSize=100");
            listResponse.EnsureSuccessStatusCode();
            var paged = await listResponse.Content.ReadFromJsonAsync<PagedList<LabTestGroupDto>>();
            paged.Should().NotBeNull();
            paged!.Items.Should().Contain(x => x.Name == dto.Name);

            var createdItem = paged.Items.Last(x => x.Name == dto.Name);
            var getById = await _client.GetAsync($"/api/labtestgroup/{createdItem.Id}");
            getById.StatusCode.Should().Be(HttpStatusCode.OK);
            var got = await getById.Content.ReadFromJsonAsync<LabTestGroupDto>();
            got!.Name.Should().Be(dto.Name);
        }

        [Fact]
        public async Task Get_ListLabTestGroups_ReturnsPagedList()
        {
            var dto = new LabTestGroupCreateDto { Name = "Hematology", Description = "Blood tests", SortOrder = 2 };
            var postResponse = await _client.PostAsJsonAsync("/api/labtestgroup", dto);
            postResponse.EnsureSuccessStatusCode();

            var response = await _client.GetAsync("/api/labtestgroup?pageNumber=1&pageSize=2");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var paged = await response.Content.ReadFromJsonAsync<PagedList<LabTestGroupDto>>();
            paged.Should().NotBeNull();
            paged!.Items.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Get_ById_ReturnsNotFound_ForUnknownId()
        {
            var response = await _client.GetAsync("/api/labtestgroup/999999");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Put_UpdateLabTestGroup_Works()
        {
            var dto = new LabTestGroupCreateDto { Name = "Microbiology", Description = "Micro tests", SortOrder = 3 };
            var createResponse = await _client.PostAsJsonAsync("/api/labtestgroup", dto);
            createResponse.EnsureSuccessStatusCode();

            var listResponse = await _client.GetAsync("/api/labtestgroup?pageNumber=1&pageSize=100");
            var list = await listResponse.Content.ReadFromJsonAsync<PagedList<LabTestGroupDto>>();
            var existing = list!.Items.Last(x => x.Name == dto.Name);

            var updated = new LabTestGroupCreateDto { Name = "Microbiology Updated", Description = dto.Description, SortOrder = dto.SortOrder };
            var updateResponse = await _client.PutAsJsonAsync($"/api/labtestgroup/{existing.Id}", updated);
            updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var updatedDto = await updateResponse.Content.ReadFromJsonAsync<LabTestGroupDto>();
            updatedDto!.Name.Should().Be("Microbiology Updated");
        }

        [Fact]
        public async Task Delete_LabTestGroup_Works_And_ReturnsNotFound_WhenMissing()
        {
            var dto = new LabTestGroupCreateDto { Name = "Serology", Description = "Serology tests", SortOrder = 4 };
            var createResponse = await _client.PostAsJsonAsync("/api/labtestgroup", dto);
            createResponse.EnsureSuccessStatusCode();

            var listResponse = await _client.GetAsync("/api/labtestgroup?pageNumber=1&pageSize=200");
            var list = await listResponse.Content.ReadFromJsonAsync<PagedList<LabTestGroupDto>>();
            var existing = list!.Items.Last(x => x.Name == dto.Name);

            var deleteResponse = await _client.DeleteAsync($"/api/labtestgroup/{existing.Id}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var deleteAgain = await _client.DeleteAsync($"/api/labtestgroup/{existing.Id}");
            deleteAgain.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
