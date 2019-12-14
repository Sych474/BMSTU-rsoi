using CharactersHub.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CharactersHub.Dto.Characters
{
    public class CharacterDto
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "level")]
        public int Level { get; set; }

        [JsonProperty(PropertyName = "race")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Race Race { get; set; }
    }
}
