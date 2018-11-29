using Models.DB;

namespace webshop_backend.Models.DB
{
    public class Decks
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public int UserId {get; set;}
        public string Commander {get; set;}
        public string SecondaryCommander {get; set;}
    }
}