using Newtonsoft.Json;

namespace CharactersHub.Models
{
    public class Character
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        public Race Race { get; set; }
    }
}
