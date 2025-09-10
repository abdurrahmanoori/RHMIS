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

        // Add more tests for update, delete, invalid, etc.
    }
}
