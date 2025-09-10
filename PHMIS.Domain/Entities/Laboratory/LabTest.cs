namespace PHMIS.Domain.Entities.Laboratory
{
    public class LabTest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? Price { get; set; }
        public bool IsActive { get; set; } = true;
        public int LabTestGroupId { get; set; }
        public LabTestGroup LabTestGroup { get; set; }
    }
}
