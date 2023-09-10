using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Handlers.DTOs
{
    public class StringDTO
    {
        public int Identifier { get; set; }

        public string StringValue { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastModifiedAt { get; set; }
    }
}
