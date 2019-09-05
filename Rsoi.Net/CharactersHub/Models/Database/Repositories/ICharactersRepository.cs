using System.Collections.Generic;
using System.Threading.Tasks;

namespace CharactersHub.Models.Database.Repositories
{
    public interface ICharactersRepository
    {
        Task<Character> FindCharacterById(long id);

        Task<List<Character>> GetCharacters();

        Task<bool> AddCharacter(Character character);
    }
}
