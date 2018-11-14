using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.DB
{
    public class Card {
        [MaxLength(36)]
        public string Id {get; set;}
        public Legalitie legalities {get; set;}
        public virtual List<CardInSet> availableSets {get; set;}
		public int? EdhrecRank {get; set;}
        public List<Parts> allParts {get; set; }
        public ColorCombinations colorIdentity {get; set;}
    }
}
