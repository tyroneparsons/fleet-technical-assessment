namespace Fleet.Modules.Patients.Contracts;

public record ErrorDetails
{
    public string Message { get; init; } = string.Empty;
}