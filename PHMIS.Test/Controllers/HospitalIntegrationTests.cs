using FluentAssertions;
using PHMIS.Application.Common;
using PHMIS.Application.DTO.Hospitals;
using PHMIS.Test.TestInfrastructure;
using PHMIS.Test.Extensions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace PHMIS.Test.Controllers
{
    public class HospitalIntegrationTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public HospitalIntegrationTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        public async Task InitializeAsync()
        {
            await _factory.CleanupHospitalsAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Post_CreateHospital_ReturnsOk_And_ListContainsItem()
        {
            var dto = new HospitalCreateDto
            {
                Code = $"H-{Guid.NewGuid():N}",
                Phone = "111-222-3333",
                Email = $"h_{Guid.NewGuid():N}@test.local",
                Address = "Somewhere",
                IsActive = true
            };

            var post = await _client.PostAsJsonAsync("/api/hospital", dto);
            post.StatusCode.Should().Be(HttpStatusCode.OK);

            var listResponse = await _client.GetAsync("/api/hospital?pageNumber=1&pageSize=100");
            listResponse.EnsureSuccessStatusCode();
            var paged = await listResponse.Content.ReadFromJsonAsync<PagedList<HospitalDto>>();
            paged!.Items.Should().Contain(x => x.Code == dto.Code);
        }

        [Fact]
        public async Task Get_ById_ReturnsNotFound_ForUnknownId()
        {
            var response = await _client.GetAsync("/api/hospital/999999");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Put_UpdateHospital_Works()
        {
            var dto = new HospitalCreateDto
            {
                Code = $"H-{Guid.NewGuid():N}",
                Phone = "111-222-3333",
                Email = $"h_{Guid.NewGuid():N}@test.local",
                Address = "Somewhere",
                IsActive = true
            };

            var post = await _client.PostAsJsonAsync("/api/hospital", dto);
            post.EnsureSuccessStatusCode();

            var listResponse = await _client.GetAsync("/api/hospital?pageNumber=1&pageSize=200");
            var list = await listResponse.Content.ReadFromJsonAsync<PagedList<HospitalDto>>();
            var existing = list!.Items.Last(x => x.Code == dto.Code);

            var updated = new HospitalCreateDto
            {
                Code = existing.Code,
                Phone = existing.Phone,
                Email = existing.Email,
                Address = "Updated Address",
                IsActive = existing.IsActive
            };

            var put = await _client.PutAsJsonAsync($"/api/hospital/{existing.Id}", updated);
            put.StatusCode.Should().Be(HttpStatusCode.OK);
            var got = await put.Content.ReadFromJsonAsync<HospitalDto>();
            got!.Address.Should().Be("Updated Address");
        }

        [Fact]
        public async Task Delete_Hospital_Works_And_ThenNotFound()
        {
            var dto = new HospitalCreateDto
            {
                Code = $"H-{Guid.NewGuid():N}",
                Phone = "111-222-3333",
                Email = $"h_{Guid.NewGuid():N}@test.local",
                Address = "Somewhere",
                IsActive = true
            };

            var post = await _client.PostAsJsonAsync("/api/hospital", dto);
            post.EnsureSuccessStatusCode();

            var listResponse = await _client.GetAsync("/api/hospital?pageNumber=1&pageSize=200");
            var list = await listResponse.Content.ReadFromJsonAsync<PagedList<HospitalDto>>();
            var existing = list!.Items.Last(x => x.Code == dto.Code);

            var delete = await _client.DeleteAsync($"/api/hospital/{existing.Id}");
            delete.StatusCode.Should().Be(HttpStatusCode.OK);

            var deleteAgain = await _client.DeleteAsync($"/api/hospital/{existing.Id}");
            deleteAgain.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
