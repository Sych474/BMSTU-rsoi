using CharactersHub.Controllers;
using CharactersHub.Dto.Characters;
using CharactersHub.Models;
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
            var characterDtos = Assert.IsAssignableFrom<IEnumerable<CharacterDto>>(returnValue.Value);

            Assert.Equal(characters.Count(), characterDtos.Count());
            AssertEqualLists(characters, characterDtos, AssertEntityEqualsCharacterDto);
        }
        #endregion

        #region Get tests 
        [Fact]
        public async Task GetOkTest()
        {
            // Arrange
            var repository = new Mock<ICharactersRepository>();
            var character = GenerateRandomCharacter();
            repository.Setup(e => e.FindCharacterByIdAsync(It.IsAny<long>())).ReturnsAsync(character);
            var controller = new CharactersController(repository.Object, mapper);

            // Act
            var result = await controller.Get(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<CharacterDto>>(result);
            var returnValue = Assert.IsType<OkObjectResult>(actionResult.Result);
            var characterDto = Assert.IsAssignableFrom<CharacterDto>(returnValue.Value);

            AssertEntityEqualsCharacterDto(character, characterDto);
        }

        [Fact]
        public async Task GetNotFoundTest()
        {
            // Arrange
            var repository = new Mock<ICharactersRepository>();
            Character character = null;
            repository.Setup(e => e.FindCharacterByIdAsync(It.IsAny<long>())).ReturnsAsync(character);
            var controller = new CharactersController(repository.Object, mapper);
            long badId = 1;
            // Act
            var result = await controller.Get(badId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<CharacterDto>>(result);
            var returnValue = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var resultValue = Assert.IsAssignableFrom<long>(returnValue.Value);

            Assert.Equal(badId, resultValue);
        }

        [Fact]
        public async Task GetBadRequestTest()
        {
            // Arrange
            CharactersController controller = new CharactersController(Mock.Of<ICharactersRepository>(), mapper);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.Get(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<CharacterDto>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }
        #endregion

        #region Post tests 
        [Fact]
        public async Task PostOkTest()
        {
            // Arrange
            var postCharacterDto = GenerateRandomCharacterPostDto();
            var repository = new Mock<ICharactersRepository>();
            repository.Setup(e => e.AddCharacterAsync(It.IsAny<Character>())).ReturnsAsync(true);

            var controller = new CharactersController(repository.Object, mapper);

            // Act
            var result = await controller.Post(postCharacterDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<CharacterDto>>(result);
            var returnValue = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var characterDto = Assert.IsAssignableFrom<CharacterDto>(returnValue.Value);

            AssertCharacterPostDtoEqualsCharacterDto(postCharacterDto, characterDto);
        }

        [Fact]
        public async Task PostConflictTest()
        {
            // Arrange
            var repository = new Mock<ICharactersRepository>();
            repository.Setup(e => e.AddCharacterAsync(It.IsAny<Character>())).ReturnsAsync(false);
            var controller = new CharactersController(repository.Object, mapper);

            // Act
            var result = await controller.Post(GenerateRandomCharacterPostDto());

            // Assert
            var actionResult = Assert.IsType<ActionResult<CharacterDto>>(result);
            var returnValue = Assert.IsType<ConflictResult>(actionResult.Result);
        }

        [Fact]
        public async Task PostBadRequestTest()
        {
            // Arrange
            CharactersController controller = new CharactersController(Mock.Of<ICharactersRepository>(), mapper);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.Post(null);

            // Assert
            var actionResult = Assert.IsType<ActionResult<CharacterDto>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }
        #endregion

    }
}
