
using System.Collections.Generic;
using webshop_backend.Enum;

namespace Models.DB
{
    public class Order {
        public int Id {get; set;}
        public int UserId {get; set;}
        public int AddressId {get; set;}
        public string PayMethod {get; set;}
        public OrderStatus Status {get; set;}
        public List<OrderProduct> OrderProducts {get; set;}
    }
}