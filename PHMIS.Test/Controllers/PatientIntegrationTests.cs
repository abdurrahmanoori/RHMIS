using FluentAssertions;
using PHMIS.Application.Common;
using PHMIS.Application.DTO.Patients;
using PHMIS.Test.Builders;
using PHMIS.Test.TestInfrastructure;
using PHMIS.Test.Extensions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace PHMIS.Test.Controllers
{
    public class PatientIntegrationTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public PatientIntegrationTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        public async Task InitializeAsync()
        {
            // Clean up before each test
            await _factory.CleanupPatientsAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Post_CreatePatient_ReturnsOk_And_CanGetById()
        {
            // Arrange
            var dto = PatientBuilder.CreateValidPatient();

            // Act
            var postResponse = await _client.PostAsJsonAsync("/api/patient", dto);

            // Assert
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var created = await postResponse.Content.ReadFromJsonAsync<PatientCreateDto>();
            created.Should().NotBeNull();
            created!.FirstName.Should().Be(dto.FirstName);

            // Verify list contains the new patient
            var listResponse = await _client.GetAsync("/api/patient?pageNumber=1&pageSize=100");
            listResponse.EnsureSuccessStatusCode();
            var paged = await listResponse.Content.ReadFromJsonAsync<PagedList<PatientDto>>();
            paged.Should().NotBeNull();
            paged!.Items.Should().NotBeNull();
            paged.Items.Should().Contain(x => x.Email == dto.Email);

            // Get by id
            var createdItem = paged.Items.Last(x => x.Email == dto.Email);
            var getById = await _client.GetAsync($"/api/patient/{createdItem.Id}");
            getById.StatusCode.Should().Be(HttpStatusCode.OK);
            var got = await getById.Content.ReadFromJsonAsync<PatientDto>();
            got!.Email.Should().Be(dto.Email);
        }

        [Fact]
        public async Task Post_CreatePatient_Invalid_ReturnsBadRequest()
        {
            // Arrange
            var invalid = new PatientCreateDto
            {
                FirstName = "", // invalid
                LastName = "",
                DateOfBirth = DateTime.UtcNow.AddDays(1), // future date invalid
                Gender = "",
                PhoneNumber = "",
                Email = "not-an-email",
                Address = ""
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/patient", invalid);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_ListPatients_ReturnsPagedList()
        {
            // Arrange - Create some patients first
            for (int i = 0; i < 3; i++)
            {
                var dto = new PatientBuilder()
                    .WithEmail($"user{i}_{Guid.NewGuid():N}@test.local")
                    .BuildCreateDto();
                await _client.PostAsJsonAsync("/api/patient", dto);
            }

            // Act
            var response = await _client.GetAsync("/api/patient?pageNumber=1&pageSize=2");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var paged = await response.Content.ReadFromJsonAsync<PagedList<PatientDto>>();
            paged.Should().NotBeNull();
            paged!.Items.Count.Should().Be(2);
            paged.PageNumber.Should().Be(1);
            paged.PageSize.Should().Be(2);
            paged.TotalCount.Should().Be(3);
        }

        [Fact]
        public async Task Get_ById_ReturnsNotFound_ForUnknownId()
        {
            // Act
            var response = await _client.GetAsync("/api/patient/99999");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Put_UpdatePatient_Works()
        {
            // Arrange - Create a patient first
            var dto = PatientBuilder.CreateValidPatient();
            var createResponse = await _client.PostAsJsonAsync("/api/patient", dto);
            createResponse.EnsureSuccessStatusCode();

            // Get the created patient
            var listResponse = await _client.GetAsync("/api/patient?pageNumber=1&pageSize=100");
            var list = await listResponse.Content.ReadFromJsonAsync<PagedList<PatientDto>>();
            var existing = list!.Items.Last(x => x.Email == dto.Email);

            var updated = new PatientCreateDto
            {
                FirstName = existing.FirstName,
                LastName = "Updated",
                DateOfBirth = existing.DateOfBirth,
                Gender = existing.Gender,
                PhoneNumber = existing.PhoneNumber,
                Email = existing.Email,
                Address = existing.Address
            };

            // Act
            var updateResponse = await _client.PutAsJsonAsync($"/api/patient/{existing.Id}", updated);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var updatedDto = await updateResponse.Content.ReadFromJsonAsync<PatientDto>();
            updatedDto!.LastName.Should().Be("Updated");
        }

        [Fact]
        public async Task Delete_Patient_Works_And_ReturnsNotFound_WhenMissing()
        {
            // Arrange: Create a patient
            var dto = PatientBuilder.CreateValidPatient();
            var createResponse = await _client.PostAsJsonAsync("/api/patient", dto);
            createResponse.EnsureSuccessStatusCode();

            var listResponse = await _client.GetAsync("/api/patient?pageNumber=1&pageSize=200");
            var list = await listResponse.Content.ReadFromJsonAsync<PagedList<PatientDto>>();
            var existing = list!.Items.Last(x => x.Email == dto.Email);

            // Act
            var deleteResponse = await _client.DeleteAsync($"/api/patient/{existing.Id}");

            // Assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // Deleting again should yield NotFound
            var deleteAgain = await _client.DeleteAsync($"/api/patient/{existing.Id}");
            deleteAgain.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
