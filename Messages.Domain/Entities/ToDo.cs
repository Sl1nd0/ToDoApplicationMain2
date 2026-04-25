using Services.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Messages.Domain.Entities
{
    public class ToDo: Entity
    {
        [Required]
        [ForeignKey("User_FK")]
        public int UsersID { get; set; }
        public string _ToDo { get; set; }
        public string ToDoTitle { get; set; }

    }
}
