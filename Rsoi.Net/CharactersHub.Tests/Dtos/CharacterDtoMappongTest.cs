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
    }
}
