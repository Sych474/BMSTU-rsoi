using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CharactersHub.Dto.Characters;
using CharactersHub.Models;
using CharactersHub.Models.Database.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CharactersHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharactersRepository charactersRepository;

        private readonly IMapper mapper;

        public CharactersController(ICharactersRepository charactersRepository, IMapper mapper)
        {
            this.charactersRepository = charactersRepository;
            this.mapper = mapper;
        }

        [SwaggerOperation(Summary = "Get all characters")]
        [ProducesResponseType(typeof(IEnumerable<CharacterDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetCollection()
        {
            var characters = await charactersRepository.GetCharactersAsync();
            return Ok(mapper.Map<IEnumerable<CharacterDto>>(characters));
        }

        [SwaggerOperation(Summary = "Get character by id")]
        [ProducesResponseType(typeof(CharacterDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDto>> Get([FromRoute] long id)
        {
            ActionResult result;
            if (ModelState.IsValid)
            {
                var character = await charactersRepository.FindCharacterByIdAsync(id);
                if (character == null)
                    result = NotFound(id);
                else
                    result = Ok(mapper.Map<CharacterDto>(character));
            }
            else
                result = BadRequest(ModelState);

            return result;
        }

        [SwaggerOperation(Summary = "Post new character")]
        [ProducesResponseType(typeof(CharacterDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        public async Task<ActionResult<CharacterDto>> Post([FromBody] CharacterPostDto postDto)
        {
            ActionResult result;
            if (ModelState.IsValid)
            {
                var character = mapper.Map<Character>(postDto);

                if (await charactersRepository.AddCharacterAsync(character))
                    result = CreatedAtAction(nameof(Post), mapper.Map<CharacterDto>(character));
                else
                    result = Conflict();
            }
            else
                result = BadRequest(ModelState);
            
            return result;
        }
    }
}
