namespace PHMIS.Application.DTO.Laboratory
{
    public class LabTestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }
        public bool IsActive { get; set; }
        public int LabTestGroupId { get; set; }

        // New fields
        public string? UnitOfMeasurment { get; set; }
        public string? NormalRange { get; set; }
        public string? Abbreviation { get; set; }
    }
}
