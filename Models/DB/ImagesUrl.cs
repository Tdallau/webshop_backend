
using System.Collections.Generic;

namespace Models.DB
{
    public class ImagesUrl {
        public int id {get; set;}
        public PrintFace printFace {get; set;}
        public string small {get; set;} 
        public string normal {get; set;} 
        public string large {get; set;} 
        public string png {get; set;} 
        public string art_crop {get; set;} 
        public string border_crop {get; set;} 
    }
}