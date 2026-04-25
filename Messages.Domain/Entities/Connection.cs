using Services.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Messages.Domain.Entities
{
    public class Connection: Entity
    {
        [Required]
        [ForeignKey("User_FK")]
        public int UserID { get; set; }
        public int ConnectionUserID { get; set; }

    }
}
