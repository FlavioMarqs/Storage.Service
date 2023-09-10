using AutoMapper;
using Storage.Commands.Commands;
using Storage.Commands.Queries;
using Storage.DTOs.Requests;
using Storage.DTOs.Responses;
using Storage.Handlers.DTOs;

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

            CreateMap<StringDTO, StringResponse>()
                // The following lines can be commented-out as AutoMapper automagically maps fields with the same name/type
                //.ForMember(d => d.Identifier, s => s.MapFrom(o => o.Identifier))
                //.ForMember(d => d.StringValue, s => s.MapFrom(o => o.StringValue))
                //.ForMember(d => d.CreatedAt, s => s.MapFrom(o => o.CreatedAt))
                //.ForMember(d => d.LastModifiedAt, s => s.MapFrom(o => o.LastModifiedAt))
                ;
                
        }
    }
}
