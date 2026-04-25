using Services.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Messages.Domain.Entities
{
    public class Comment: Entity
    {
        public string _Comment { get; set; }

        [Required]
        [ForeignKey("User_FK")]
        public int UserID { get; set; }

        [Required]

        [ForeignKey("ToDo_FK")]
        public int ToDoID { get; set; }
    }
}
