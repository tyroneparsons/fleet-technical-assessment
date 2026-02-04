using Fleet.Modules.Patients.Contracts;
using Fleet.Modules.Patients.Features;
using Fleet.Modules.Patients.Infrastructure;
using MassTransit.Testing;

namespace Fleet.Modules.Patients.Tests.Unit;

[TestClass]
public sealed class PatientsConsumerTests
{
    [TestMethod]
    public async Task GetPatientConsumer_WhenPatientExists_RespondsWithPatientDetail()
    {
        var repository = new InMemoryPatientRepository();

        using var harness = new InMemoryTestHarness();
        var consumer = harness.Consumer(() => new GetPatientConsumer(repository));

        await harness.Start();
            
        try
        {
            var requestClient = await harness.ConnectRequestClient<GetPatient>();
            var response = await requestClient.GetResponse<PatientDetail>(new { PatientId = 1 });

            Assert.IsTrue(await consumer.Consumed.Any<GetPatient>());
            Assert.AreEqual(1, response.Message.Id);
            Assert.AreEqual("485 210 9932", response.Message.NHSNumber);
            Assert.AreEqual("Tyrone Parsons", response.Message.Name);
            Assert.AreEqual("Swarland Avenue Surgery", response.Message.GPPractice);
        }
        finally
        {
            await harness.Stop();
        }
    }

    [TestMethod]
    public async Task GetPatientConsumer_WhenPatientDoesNotExist_RespondsWithPatientNotFound()
    {
        var repository = new InMemoryPatientRepository();

        using var harness = new InMemoryTestHarness();
        var consumer = harness.Consumer(() => new GetPatientConsumer(repository));

        await harness.Start();
            
        try
        {
            var requestClient = await harness.ConnectRequestClient<GetPatient>();
            var response = await requestClient.GetResponse<PatientNotFound>(new { PatientId = -999 });

            Assert.IsTrue(await consumer.Consumed.Any<GetPatient>());
            Assert.AreEqual(-999, response.Message.PatientId);
        }
        finally
        {
            await harness.Stop();
        }
    }
}
