using Microsoft.Extensions.Options;

namespace Storage.Commands
{
    public class StoreCommand<T>
    {
        public Options Value { get; set; }
    }
}