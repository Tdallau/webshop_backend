
using System.Collections.Generic;

namespace Models.DB
{
    public class Order {
        public int id {get; set;}
        public int userId {get; set;}
        public int addressId {get; set;}
        public string status {get; set;}
        public List<OrderProduct> orderProducts {get; set;}
    }
}