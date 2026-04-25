using Services.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Messages.Domain.Entities
{
    public class Like : Entity
    {
        [Required]
        [ForeignKey("ToDo_FK")]
        public int ToDoID { get; set; }

        [Required]

        [ForeignKey("user_FK")]
        public int UserID { get; set; }
    }
}
