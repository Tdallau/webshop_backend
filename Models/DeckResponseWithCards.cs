using System.Collections.Generic;
using Models;

namespace webshop_backend.Models
{
    public class DeckResponseWithCards : DeckResponse
    {
        public string CommanderName {get; set;}
        public List<CardResponse> Cards { get; set; }
    }
}