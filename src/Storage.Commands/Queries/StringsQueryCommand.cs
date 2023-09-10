using Storage.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Commands.Queries
{
    public class StringsQueryCommand : IStoreCommand<bool>
    {
        public bool Identifier { get; set; }
    }
}
