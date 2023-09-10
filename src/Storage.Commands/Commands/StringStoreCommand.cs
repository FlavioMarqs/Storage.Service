using Storage.Commands.Interfaces;

namespace Storage.Commands.Commands
{
    public class StringStoreCommand : IStoreCommand<string>
    {
        public string Identifier { get; set; }
    }
}