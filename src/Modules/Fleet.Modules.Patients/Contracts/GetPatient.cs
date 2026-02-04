namespace Fleet.Modules.Patients.Contracts;

public record GetPatient
{
    public int PatientId { get; init; }
}