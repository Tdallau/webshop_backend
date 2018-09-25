
using System.Collections.Generic;

namespace Models
{
    public class Product {
        public int Id {get; set;}
        public string Name {get; set;}
        public string MinUnit {get; set;}
        public string Region {get; set;}
        public double UnitPrice {get; set;} 
        public List<ProductDetail> ProductDetail {get; set; }
    }
}