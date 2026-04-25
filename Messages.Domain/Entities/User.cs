using Services.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Messages.Domain.Entities
{
    public class User: Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Identifier { get; set; }
        public string DateOfBirth { get; set; }
        public string Password { get; set; }
        public string CellNumber { get; set; }

        [ForeignKey("UserRole_FK")]
        public int UserRoleID { get; set; }
    }
}
