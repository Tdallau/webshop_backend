using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DB
{
    public class ShoppingCard {
        public int Id {get; set;}
        public int UserId {get; set; }
        public string Status {get; set;}
        public List<ShoppingCardItem> ShoppingCardItems {get; set;}
    }
}