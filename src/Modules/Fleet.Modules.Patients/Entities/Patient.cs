namespace Fleet.Modules.Patients.Entities;

public class Patient
{
    public int Id { get; set; }

    public int PracticeId { get; set; }

    public string NHSNumber { get; set; } = string.Empty;
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }
}