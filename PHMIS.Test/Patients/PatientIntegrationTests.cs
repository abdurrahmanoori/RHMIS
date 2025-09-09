using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PHMIS.Application.Common;
using PHMIS.Application.DTO.Patients;
using PHMIS.Infrastructure.Context;
using PHMIS.Test.TestInfrastructure;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace PHMIS.Test.Patients;

public class PatientIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public PatientIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Post_CreatePatient_ReturnsOk_And_CanGetById( )
    {
        // Arrange
        var client = _factory.CreateClient();
        var dto = new PatientBuilder()
              .WithFirstName("Test")
              .WithEmail($"test_{Guid.NewGuid():N}@example.com")
              .BuildCreateDto();
        
        // Act
        var postResponse = await client.PostAsJsonAsync("/api/patient", dto);

        // Assert
        postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var created = await postResponse.Content.ReadFromJsonAsync<PatientCreateDto>();
        created.Should().NotBeNull();
        created!.FirstName.Should().Be(dto.FirstName);

        // Verify list contains at least the seeded patients + new one
        var listResponse = await client.GetAsync("/api/patient?pageNumber=1&pageSize=100");
        listResponse.EnsureSuccessStatusCode();
        var paged = await listResponse.Content.ReadFromJsonAsync<PagedList<PatientDto>>();
        paged.Should().NotBeNull();
        paged!.Items.Should().NotBeNull();
        paged.Items.Should().Contain(x => x.Email == dto.Email);

        // Get by id: the created endpoint does not return id; get last matching email
        var createdItem = paged.Items.Last(x => x.Email == dto.Email);
        var getById = await client.GetAsync($"/api/patient/{createdItem.Id}");
        getById.StatusCode.Should().Be(HttpStatusCode.OK);
        var got = await getById.Content.ReadFromJsonAsync<PatientDto>();
        got!.Email.Should().Be(dto.Email);
    }

    [Fact]
    public async Task Post_CreatePatient_Invalid_ReturnsBadRequest( )
    {
        // Arrange
        var client = _factory.CreateClient();
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
        var response = await client.PostAsJsonAsync("/api/patient", invalid);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    [Retry(3)] // Using Polly or custom retry attribute
    public async Task Get_ListPatients_ReturnsPagedList( )
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/patient?pageNumber=1&pageSize=2");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var paged = await response.Content.ReadFromJsonAsync<PagedList<PatientDto>>();
        paged.Should().NotBeNull();
        paged!.Items.Count.Should().BeGreaterThan(0);
        paged.PageNumber.Should().Be(1);
        paged.PageSize.Should().Be(2);
    }

    [Fact]
    public async Task Get_ById_ReturnsNotFound_ForUnknownId( )
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/patient/99999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Put_UpdatePatient_Works( )
    {
        // Arrange
        var client = _factory.CreateClient();

        // Create a new patient first
        var dto = CreateValidPatient();
        var createResponse = await client.PostAsJsonAsync("/api/patient", dto);
        createResponse.EnsureSuccessStatusCode();

        // Fetch it back to get the id
        var listResponse = await client.GetAsync("/api/patient?pageNumber=1&pageSize=100");
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
        var updateResponse = await client.PutAsJsonAsync($"/api/patient/{existing.Id}", updated);

        // Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var updatedDto = await updateResponse.Content.ReadFromJsonAsync<PatientDto>();
        updatedDto!.LastName.Should().Be("Updated");
    }

    [Fact]
    public async Task Delete_Patient_Works_And_ReturnsNotFound_WhenMissing( )
    {
        // Arrange: ensure an item exists
        var client = _factory.CreateClient();
        var dto = CreateValidPatient();
        var createResponse = await client.PostAsJsonAsync("/api/patient", dto);
        createResponse.EnsureSuccessStatusCode();

        var listResponse = await client.GetAsync("/api/patient?pageNumber=1&pageSize=200");
        var list = await listResponse.Content.ReadFromJsonAsync<PagedList<PatientDto>>();
        var existing = list!.Items.Last(x => x.Email == dto.Email);

        // Act
        var deleteResponse = await client.DeleteAsync($"/api/patient/{existing.Id}");

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // Deleting again should yield NotFound
        var deleteAgain = await client.DeleteAsync($"/api/patient/{existing.Id}");
        deleteAgain.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private static PatientCreateDto CreateValidPatient( )
    {
        return new PatientCreateDto
        {
            FirstName = "Test",
            LastName = "User",
            DateOfBirth = new DateTime(1995, 1, 1),
            Gender = "Male",
            PhoneNumber = "123456789",
            Email = $"user_{Guid.NewGuid():N}@test.local",
            Address = "123 Test Street"
        };
    }

    public async Task CleanupDatabaseAsync( )
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Clear specific tables to avoid FK issues
        db.Patients.RemoveRange(db.Patients);
        await db.SaveChangesAsync();
    }
}
