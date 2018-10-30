
using System.Collections.Generic;

namespace Models.DB
{
    public class Legalitie {
        public int id {get; set;}
        public Legalities standard {get; set;}
        public Legalities future {get; set;}
        public Legalities frontier {get; set;}
        public Legalities modern {get; set;}
        public Legalities legacy {get; set;}
        public Legalities pauper {get; set;}
        public Legalities vintage {get; set;}
        public Legalities penny {get; set;}
        public Legalities commander {get; set;}
        public Legalities one_v_one {get; set;}
        public Legalities duel {get; set;}
        public Legalities brawl {get; set;}

    }
}