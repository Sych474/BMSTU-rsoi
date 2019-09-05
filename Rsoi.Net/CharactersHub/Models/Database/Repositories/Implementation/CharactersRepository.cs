using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CharactersHub.Models.Database.Repositories
{
    public class CharactersRepository : ICharactersRepository
    {
        private readonly CharactersHubDbContext dbContext;

        public CharactersRepository(CharactersHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Character> FindCharacterById(long id)
        {
            return dbContext.Characters.FindAsync(id);
        }

        public Task<List<Character>> GetCharacters()
        {
            return dbContext.Characters.ToListAsync();
        }

        public async Task<bool> AddCharacter(Character character)
        {
            bool result = true;
            try
            {
                dbContext.Characters.Add(character);

                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                result = false;
            }
            return result;
        }
    }
}
