using Models.DB;

namespace webshop_backend.Models.DB
{
    public class CardsDeck
    {
        public int Id {get; set;}
        public Print print {get; set;}
        public Decks deck {get; set;}
    }
}