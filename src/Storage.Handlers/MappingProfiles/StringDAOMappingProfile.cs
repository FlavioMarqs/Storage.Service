using AutoMapper;
using Storage.Commands.Commands;
using Storage.DAOs;
using Storage.Handlers.DTOs;

namespace Storage.Handlers.MappingProfiles
{
    public class StringDAOMappingProfile : Profile
    {
        public StringDAOMappingProfile()
        {
            CreateMap<StringStoreCommand, StringDTO>()
                .ForMember(d => d.StringValue, s => s.MapFrom(o => o.Identifier))
                .ForMember(d => d.CreatedAt, s => s.MapFrom(o => DateTime.UtcNow));
        }
    }
}
