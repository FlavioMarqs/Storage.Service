using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAOs
{
    public class IdentifiableDAO
    {
        public int Identifier { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
