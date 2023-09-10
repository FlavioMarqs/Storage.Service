using AutoMapper;
using Storage.Commands.Commands;
using Storage.Commands.Queries;
using Storage.Service.Requests;

namespace Storage.Service.MappingProfiles
{
    public class StorageMappingProfiles : Profile
    {
        public StorageMappingProfiles() 
        {
            CreateMap<StringCreationRequest, StringStoreCommand>();
            CreateMap<StringQueryRequest, StringQueryCommand>();
        }
    }
}
