using Services.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Domain.Entities
{
    public class Message: Entity
    {
        public string Text { get; set; }
    }
}
