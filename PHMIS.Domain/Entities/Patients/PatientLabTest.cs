using PHMIS.Domain.Entities.Laboratory;
using PHMIS.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PHMIS.Domain.Entities.Patients;

public class PatientLabTest
{

    public int Id { get; set; }


    public int PatientId { get; set; }

    public int LabTestId { get; set; }

    //// Consider adding if tests are performed in different labs/locations
    //public int? PerformingLabId { get; set; }




    public int? LabOrderNumber { get; set; } // Unique identifier for the order, often required
    //public string? SampleId { get; set; } // The physical sample identifier (e.g., barcode)

    // Result data
    public string? ResultValue { get; set; }
    public decimal? NumericResultValue { get; set; } // For calculations, trending, and range checks
    public string? ResultNotes { get; set; }

    // Reference Ranges (Could also be on LabTest, but sometimes overridden per patient/demographic)
    public string? NormalRange { get; set; } // e.g., "<= 5.0", "Negative", "120-450 x10^3/µL"
    public bool? IsAbnormal { get; set; } = false; // Non-nullable. Calculated on result entry.

    // Consider adding who ordered/collected/reported
    public int? OrderedByUserId { get; set; }
    public int? CollectedByUserId { get; set; }
    public int? ReportedByUserId { get; set; }
    // Lifecycle Timestamps
    public DateTime OrderedAt { get; set; } = DateTime.UtcNow;     // Timestamp when the lab test was ordered
    public DateTime? ReceivedByLabAt { get; set; } // When the lab logged receipt of the sample
    public DateTime? CollectedAt { get; set; }     // Timestamp when the sample was collected
    public DateTime? ReportedAt { get; set; }     // Timestamp when the lab test report was generated
    public DateTime? AcknowledgedAt { get; set; } // When the ordering clinician saw the result

    public LabStatus Status { get; set; } = LabStatus.Ordered;

    // Audit Fields
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;

    [ForeignKey(nameof(LabTestId))]
    public virtual LabTest LabTest { get; set; } = null!;

    // Method to update status and timestamps consistently
    public void UpdateStatus(LabStatus newStatus, int userId)
    {
        Status = newStatus;
        LastModifiedDate = DateTime.UtcNow;

        // Optionally set timestamps based on status change
        switch (newStatus)
        {
            case LabStatus.Ordered:
                OrderedAt = DateTime.UtcNow;
                OrderedByUserId = userId;
                break;
            case LabStatus.SampleCollected:
                CollectedAt ??= DateTime.UtcNow; // Set if not already set
                CollectedByUserId = userId;
                break;
            case LabStatus.ReceivedByLab:
                ReceivedByLabAt = DateTime.UtcNow;
                break;
            case LabStatus.Completed:
                ReportedAt ??= DateTime.UtcNow;
                ReportedByUserId = userId;
                break;
            case LabStatus.Acknowledged:
                AcknowledgedAt ??= DateTime.UtcNow;
                break;
                // Add other cases as needed
        }
    }

    // Method to set results and calculate abnormality
    public void SetResult(string value, string? normalRange = null, int? userId = null, string? notes = null)
    {
        ResultValue = value;
        NormalRange = normalRange ??= "this is normal range 15m/c";
        ResultNotes = notes;
        ReportedAt = DateTime.UtcNow;
        LastModifiedDate = DateTime.UtcNow;

        // TODO: Implement more sophisticated logic based on your needs.
        // This is a simple placeholder. Real logic would parse ranges and values.
        // This could also be an external service.
        if (!string.IsNullOrEmpty(normalRange) && decimal.TryParse(value, out decimal numericValue))
        {
            NumericResultValue = numericValue;

            // A very basic check - real logic is complex (e.g., parsing ">=10", "Male: 1-5, Female: 2-6")
            // For now, just mark it. The calculation should happen in a service.
            IsAbnormal = false; // Placeholder
        }
        UpdateStatus(LabStatus.Completed, (int)userId!);
    }
}



//public DateTime? AnalysisStartedAt { get; set; }
//public DateTime? AnalysisCompletedAt { get; set; }