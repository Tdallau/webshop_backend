using Models.DB;

namespace webshop_backend.Models.DB
{
    public class CardsDeck
    {
        public int Id {get; set;}
        public Print print {get; set;}
        public string PrintId {get; set;}
        public Decks deck {get; set;}
        public int quantity {get; set;}
        public int DeckId {get; set;}
    }
}