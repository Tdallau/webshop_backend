
namespace Models.DB
{
    public class OrderProduct {
        public int id {get; set;}
        public int orderId {get;set;}
        public string PrintId {get; set;}
        public int price {get; set;}
        public int quantity {get; set;}
    }
}