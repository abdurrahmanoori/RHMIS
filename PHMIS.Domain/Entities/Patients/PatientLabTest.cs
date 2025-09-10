using PHMIS.Domain.Entities.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHMIS.Domain.Entities
{
   
    namespace PHMIS.Domain.Entities.Laboratory
    {
        public class PatientLabTest
        {
            public int Id { get; set; }

            // Relationships
            public int PatientId { get; set; }
            public Patient Patient { get; set; } = null!;

            public int LabTestId { get; set; }
            public LabTest LabTest { get; set; } = null!;

            // Lifecycle
            public DateTime OrderedAt { get; set; } = DateTime.UtcNow;
            public DateTime? CollectedAt { get; set; }
            public DateTime? ReportedAt { get; set; }

            // Result data
            public string? ResultValue { get; set; }
            public string? ResultNotes { get; set; }
            public bool? IsAbnormal { get; set; }

            // Snapshot of LabTest properties at the time of order/result
            public string? UnitOrMeasurment { get; set; }
            public string? NormalRange { get; set; }
            public string? Abbreviation { get; set; }

            // Status: e.g. Ordered, Collected, Completed, Canceled
            public string? Status { get; set; }
        }
    }
}
