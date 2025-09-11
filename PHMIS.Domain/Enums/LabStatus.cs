namespace PHMIS.Domain.Enums
{

    public enum LabStatus
    {
        Ordered,          // Test has been requested
        SampleCollected,  // Sample has been taken from the patient
        InTransit,        // Sample is being transported to the lab
        ReceivedByLab,    // Lab has received and logged the sample
        InAnalysis,       // Test is being processed/analyzed
        Completed,        // Analysis is done, result is available
        Acknowledged,     // Result has been seen by the ordering clinician
        Cancelled,        // Order was cancelled
        Rejected          // Sample was rejected (e.g., insufficient quantity, hemolyzed)
    }

}
