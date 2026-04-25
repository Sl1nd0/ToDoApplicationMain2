using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Core.Entities
{
    public class Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Id")]
        public int Id { get; set; }
        public DateTime DateEntered { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateDeleted { get; set; }
    }
}
