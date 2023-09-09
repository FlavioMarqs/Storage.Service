using Storage.Commands.Interfaces;

namespace Storage.Commands
{
    public class StringStoreCommand : IStoreCommand<string>
    {
        public StringStoreCommand(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}