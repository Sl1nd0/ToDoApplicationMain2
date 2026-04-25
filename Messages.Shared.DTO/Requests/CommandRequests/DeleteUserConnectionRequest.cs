using Services.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Shared.DTO.Requests.CommandRequests
{
    public class DeleteUserConnectionRequest: ICommandModel
    {
        public int Id { get; set; }
    }
}
