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
            CreateMap<StringCreationRequest, StringStoreCommand>()
                .ForMember(d => d.Identifier, s => s.MapFrom(o => o.Value));

            CreateMap<StringQueryRequest, StringQueryCommand>();

            CreateMap<StringsQueryRequest, StringsQueryCommand>()
                .ForMember(d => d.Identifier, s => s.MapFrom(o => o.IncludeDeleted));
        }
    }
}
