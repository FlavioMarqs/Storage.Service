using Storage.Handlers.Interfaces;
using System.Windows.Input;

namespace Storage.Handlers
{
    public class StringCommandHandler : ICommandHandler
    {
        public StringCommandHandler(IRepository<string> repo) 
        { 
        }

        public Task HandleAsync(ICommand command)
        {
            throw new NotImplementedException();
        }
    }
}