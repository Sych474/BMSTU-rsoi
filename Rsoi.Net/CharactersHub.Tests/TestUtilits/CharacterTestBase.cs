using AutoMapper;
using CharactersHub.Dto.Characters;
using CharactersHub.Models;
using Xunit;

namespace CharactersHub.Tests.TestUtilits
{
    public class CharacterTestBase : TestBase
    {
        protected readonly IMapper mapper;

        public CharacterTestBase()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CharacterDtoMappingProfile());
                cfg.AddProfile(new CharacterPostDtoMappingProfile());
            });

            mapper = config.CreateMapper();
        }

        protected void AssertCharacterDtoEqualsEntity(CharacterDto dto, Character entity)
        {
            Assert.Equal(dto.Id, entity.Id);
            Assert.Equal(dto.Level, entity.Level);
            Assert.Equal(dto.Name, entity.Name);
            Assert.Equal(dto.Race, entity.Race);
        }

        protected void AssertEntityEqualsCharacterDto(Character entity, CharacterDto dto)
        {
            Assert.Equal(entity.Id, dto.Id);
            Assert.Equal(entity.Level, dto.Level);
            Assert.Equal(entity.Name, dto.Name);
            Assert.Equal(entity.Race, dto.Race);
        }

        protected void AssertCharacterPostDtoEqualsCharacterDto(CharacterPostDto dto, CharacterDto dto2)
        {
            Assert.Equal(dto.Level, dto2.Level);
            Assert.Equal(dto.Name, dto2.Name);
            Assert.Equal(dto.Race, dto2.Race);
        }

        protected void AssertCharacterPostDtoEqualsCharacter(CharacterPostDto dto, Character entity)
        {
            Assert.Equal(dto.Level, entity.Level);
            Assert.Equal(dto.Name, entity.Name);
            Assert.Equal(dto.Race, entity.Race);
        }

        protected Character GenerateRandomCharacter()
        {
            return new Character()
            {
                Id = random.Next(),
                Level = random.Next(),
                Name = GenerateRandomString(),
                Race = GenerateRandomEnumValue<Race>()
            };
        }

        protected CharacterDto GenerateRandomCharacterDto()
        {
            return new CharacterDto()
            {
                Id = random.Next(),
                Level = random.Next(),
                Name = GenerateRandomString(),
                Race = GenerateRandomEnumValue<Race>()
            };
        }

        protected CharacterPostDto GenerateRandomCharacterPostDto()
        {
            return new CharacterPostDto()
            {
                Level = random.Next(),
                Name = GenerateRandomString(),
                Race = GenerateRandomEnumValue<Race>()
            };
        }
    }
}
