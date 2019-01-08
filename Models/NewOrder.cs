using Models.DB;

namespace webshop_backend.Models
{
    public class NewOrder
    {
        public int ShoppingCardId {get; set;}
        public string PayMethod {get; set;}
        public Address Address {get; set;}
    }
}