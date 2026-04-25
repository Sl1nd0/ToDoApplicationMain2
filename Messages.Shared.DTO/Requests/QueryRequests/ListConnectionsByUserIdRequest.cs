using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Shared.DTO.Requests.QueryRequests
{
    public class ListConnectionsByUserIdRequest
    {
        public int UserID { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
    }
}
