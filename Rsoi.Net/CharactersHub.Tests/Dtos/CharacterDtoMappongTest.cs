using CharactersHub.Dto.Characters;
using CharactersHub.Models;
using CharactersHub.Tests.TestUtilits;
using Xunit;

namespace CharactersHub.Tests.Dtos
{
    public class CharacterDtoMappongTest : CharacterTestBase
    {
        [Fact]
        public void CharacterDtoToEntityTest()
        {
            //Arrange
            var dto = GenerateRandomCharacterDto();

            //Act
            var entity = mapper.Map<Character>(dto);

            //Assert
            AssertCharacterDtoEqualsEntity(dto, entity);
        }

        [Fact]
        public void EntityToCharacterDtoTest()
        {
            //Arrange
            var entity = GenerateRandomCharacter();

            //Act
            var dto = mapper.Map<CharacterDto>(entity);

            //Assert
            AssertEntityEqualsCharacterDto(entity, dto);
        }

        [Fact]
        public void CharacterPostDtoToEntityTest()
        {
            //Arrange
            var postDto = GenerateRandomCharacterPostDto();

            //Act
            var entity = mapper.Map<Character>(postDto);

            //Assert
            AssertCharacterPostDtoEqualsCharacter(postDto, entity);
        }
    }
}
