using AutoMapper;
using CharactersHub.Controllers;
using CharactersHub.Dto.Characters;
using CharactersHub.Models.Database.Repositories;
using CharactersHub.Tests.TestUtilits;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CharactersHub.Tests.Controllers
{
    public class CharactersControllerTest : CharacterTestBase
    {
        #region GetCollection tests 
        [Fact]
        public async Task GetCollectionOkTest()
        {
            // Arrange
            var repository = new Mock<ICharactersRepository>();
            var characters = GenerateRandomList(1, 10, GenerateRandomCharacter);
            repository.Setup(e => e.GetCharactersAsync()).ReturnsAsync(characters);
            var controller = new CharactersController(repository.Object, mapper);

            // Act
            var result = await controller.GetCollection();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<CharacterDto>>>(result);
            var returnValue = Assert.IsType<OkObjectResult>(actionResult.Result);
            var chcaracterDtos = Assert.IsAssignableFrom<IEnumerable<CharacterDto>>(returnValue.Value);

            Assert.Equal(characters.Count(), chcaracterDtos.Count());
            AssertEqualLists(characters, chcaracterDtos, AssertEntityEqualsCharacterDto);
        }
        #endregion

        #region Get tests 

        #endregion

        #region Post tests 

        #endregion

    }
}
