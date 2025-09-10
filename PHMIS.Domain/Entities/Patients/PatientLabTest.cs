using PHMIS.Domain.Entities.Laboratory;
using PHMIS.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PHMIS.Domain.Entities.Patients;

public class PatientLabTest 
{
    public int Id { get; set; } 

    public int PatientId { get; set; } 

    public int LabTestId { get; set; } 

    // Result data
    public string? ResultValue { get; set; } // The value of the lab test result
    public string? ResultNotes { get; set; } // Additional notes or observations about the result
    public bool? IsAbnormal { get; set; } // Indicates if the result is abnormal

    // Lifecycle
    public DateTime OrderedAt { get; set; } = DateTime.UtcNow; // Timestamp when the lab test was ordered
    public DateTime? CollectedAt { get; set; } // Timestamp when the sample was collected
    public DateTime? ReportedAt { get; set; } // Timestamp when the lab test report was generated

    [Column(TypeName = "nvarchar(24)")]
    public LabStatus LabStatus { get; set; } = LabStatus.PendingToLab; // Current status of the lab test

    [ForeignKey(nameof(LabTestId))]
    public LabTest LabTest { get; set; } = null!; 

    [ForeignKey(nameof(PatientId))]
    public Patient Patient { get; set; } = null!; 

}

