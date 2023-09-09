using Storage.Commands;
using Storage.Handlers.Interfaces;
using Storage.Repositories.Interfaces;
using System.Windows.Input;

namespace Storage.Handlers
{
    public class StringCommandHandler : ICommandHandler<StringStoreCommand>
    {
        private IRepository<string> _repository;

        public StringCommandHandler(IRepository<string> repository) 
        { 
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task HandleAsync(StringStoreCommand command)
        {
            await _repository.CreateAsync(command.Value);
        }
    }
}