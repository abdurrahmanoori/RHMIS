
namespace PHMIS.Domain.Entities.Laboratory
{
    public class LabTestGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public short? SortOrder { get; set; }
        //public ICollection<LabTest> LabTests { get; set; }
    }
}
