
using System.Collections.Generic;

namespace Tabels
{
    public class Product {
        public string id {get; set;}
        public string lang {get; set;}
        public string oracle_id {get; set;}
        public bool foil {get; set;}
        public Legalitie legalities {get; set;}
        public string loyalty {get; set;}
        public string mana_cost {get; set;}
        public string name {get; set;}
        public bool nonfoil {get; set;}
        public string oracle_text {get; set;}
        public string power {get; set;}
        public string reserved {get; set;}
        public string toughness {get; set;}
        public string type_line {get; set;}
        public string price {get; set;}
        public ImagesUrl image_uris {get; set;}
        public string rarity {get; set;}
        public string set {get; set;}
        public string setName {get; set;}
        public List<Parts> all_parts {get; set; } 
        public List<Colors> colors {get; set;}
        public List<ColorIdentity> color_identity {get; set;}
        public List<ColorIndicator> color_indicator {get; set;}
        
    }
}