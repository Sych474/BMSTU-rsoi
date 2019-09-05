using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CharactersHub.Dto.Characters;
using CharactersHub.Models;
using CharactersHub.Models.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetCollection()
        {
            var characters = await charactersRepository.GetCharacters();
            return Ok(mapper.Map<IEnumerable<CharacterDto>>(characters));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDto>> Get([FromRoute] long id)
        {
            ActionResult result;
            if (ModelState.IsValid)
            {
                var character = await charactersRepository.FindCharacterById(id);
                if (character == null)
                    result = NotFound(id);
                else
                    result = Ok(mapper.Map<CharacterDto>(character));
            }
            else
                result = BadRequest(ModelState);

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<CharacterDto>> Post([FromBody] CharacterPostDto entity)
        {
            ActionResult result;
            if (ModelState.IsValid)
            {
                var character = mapper.Map<Character>(entity);

                if (await charactersRepository.AddCharacter(character))
                    result = CreatedAtAction(nameof(Post), character);
                else
                    result = Conflict(entity);
            }
            else
                result = BadRequest(ModelState);
            
            return result;
        }
    }
}
