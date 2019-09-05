using System.Collections.Generic;
using System.Threading.Tasks;

namespace CharactersHub.Models.Database.Repositories
{
    public interface ICharactersRepository
    {
        Task<Character> FindCharacterByIdAsync(long id);

        Task<List<Character>> GetCharactersAsync();

        Task<bool> AddCharacterAsync(Character character);
    }
}
