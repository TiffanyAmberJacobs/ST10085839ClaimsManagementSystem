using System;

public class Claim
{
    public int ClaimId { get; set; }
    public int LecturerId { get; set; }
    public decimal HoursWorked { get; set; }
    public decimal HourlyRate { get; set; }
    public required string AdditionalNotes { get; set; }
    public required string DocumentPath { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime SubmittedAt { get; set; } = DateTime.Now;
}
