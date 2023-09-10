using Storage.Commands.Interfaces;

namespace Storage.Commands.Commands
{
    public class StringStoreCommand : IStoreCommand<string>
    {
        public string Value { get; set; }
    }
}