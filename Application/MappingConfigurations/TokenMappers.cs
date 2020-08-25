using Application.DTOs.Authentication;
using AutoMapper;

namespace Application.MappingConfigurations
{
    public class TokenMappers : Profile
    {
        public TokenMappers()
        {
            CreateMap<TokenRequest, TokenRequestMapTest>();
        }
    }
}
