using Fleet.Modules.Patients.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace Fleet.Modules.Patients.Tests.Integration;

[TestClass]
public sealed class PatientsEndpointIntegrationTests
{
    [TestMethod]
    public async Task GetPatientsById_WhenPatientExists_ReturnsOkWithPatientDetail()
    {
        await using var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.UseEnvironment("Testing"));

        using var client = factory.CreateClient();

        var response = await client.GetAsync("/patients/1");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var patient = await response.Content.ReadFromJsonAsync<PatientDetail>();

        Assert.IsNotNull(patient);
        Assert.AreEqual(1, patient.Id);
        Assert.AreEqual("485 210 9932", patient.NHSNumber);
        Assert.AreEqual("Tyrone Parsons", patient.Name);
        Assert.AreEqual("Swarland Avenue Surgery", patient.GPPractice);
    }

    [TestMethod]
    public async Task GetPatientsById_WhenPatientDoesNotExist_ReturnsNotFoundWithErrorDetails()
    {
        await using var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.UseEnvironment("Testing"));

        using var client = factory.CreateClient();

        var response = await client.GetAsync("/patients/-999");

        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        var error = await response.Content.ReadFromJsonAsync<ErrorDetails>();

        Assert.IsNotNull(error);
        Assert.AreEqual("Patient -999 not found.", error.Message);
    }
}

