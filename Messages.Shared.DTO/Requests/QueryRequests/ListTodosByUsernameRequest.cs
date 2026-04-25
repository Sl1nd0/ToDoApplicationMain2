using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Shared.DTO.Requests.QueryRequests
{
    public class ListTodosByUsernameRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}
