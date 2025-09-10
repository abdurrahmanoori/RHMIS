using System.ComponentModel.DataAnnotations;

namespace PHMIS.Domain.Common
{
    public abstract class BaseEntity 
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
