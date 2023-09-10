using AutoMapper;
using Storage.Commands.Commands;
using Storage.Commands.Queries;
using Storage.DAOs;
using Storage.Handlers.DTOs;
using Storage.Handlers.Interfaces;
using Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Handlers
{
    public class StringsQueryHandler : ICommandHandler<StringsQueryCommand, IEnumerable<StringDTO>>
    {
        private readonly IStoreRepository<StringDAO> _repository;
        private readonly IMapper _mapper;

        public StringsQueryHandler(IStoreRepository<StringDAO> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<StringDTO>> HandleAsync(StringsQueryCommand command)
        {
            var result = await _repository.GetAllAsync(command.Identifier);
            return _mapper.Map<IEnumerable<StringDTO>>(result);
        }
    }
}
