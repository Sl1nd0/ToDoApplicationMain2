using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Shared.DTO.Results.QueryResults
{
    public class ListConnectionsByUserIdResult
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public int ConnectionID { get; set; }
    }
}
