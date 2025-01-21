using System.Collections.Generic;

namespace ModsProcessor.Models
{
    public class Game
    {
        public string Name { get; set; }
        public List<ModConfig> Profiles{ get; set; }
    }
}
