using Storage.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Commands.Queries
{
    public class StringQueryCommand : IStoreCommand<int>
    {
        public int Value { get; set; }
    }
}
