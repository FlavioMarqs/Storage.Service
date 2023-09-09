using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Storage.Handlers.Interfaces
{
    public interface ICommandHandler
    {
        Task HandleAsync(ICommand command);
    }
}
