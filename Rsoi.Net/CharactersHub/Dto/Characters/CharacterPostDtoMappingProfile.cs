using AutoMapper;
using CharactersHub.Models;

namespace CharactersHub.Dto.Characters
{
    public class CharacterPostDtoMappingProfile : Profile
    {
        public CharacterPostDtoMappingProfile()
        {
            CreateMap<CharacterDto, Character>();
        }
    }
}
