using AutoMapper;
using CharactersHub.Models;

namespace CharactersHub.Dto.Characters
{
    public class CharacterDtoMappingProfile : Profile
    {
        public CharacterDtoMappingProfile()
        {
            CreateMap<Character, CharacterDto>();

            CreateMap<CharacterDto, Character>();
        }
    }
}
