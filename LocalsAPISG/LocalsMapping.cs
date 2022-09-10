using AutoMapper;
using LocalsAPISG.Entities;
using LocalsAPISG.Models;

namespace ProjectApp
{
    public class LocalsMapping : Profile
    {
        public LocalsMapping()
        {
            CreateMap<Locals, LocalsDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));


            CreateMap<Menu, MenuDto>();
            CreateMap<CreateLocalDto, Locals>()
                .ForMember(r => r.Address, c => c.MapFrom(dto => new Address()
                {
                    City = dto.City,
                    PostalCode = dto.PostalCode,
                    Street = dto.Street
                }));
        }
    }
}
