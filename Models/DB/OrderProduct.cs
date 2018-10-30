
namespace Models.DB
{
    public class OrderProduct {
        public int id {get; set;}
        public int orderId {get;set;}
        public Card card {get; set;}
        public double price {get; set;}
        public int quantity {get; set;}
    }
}