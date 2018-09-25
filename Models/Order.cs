
using System.Collections.Generic;

namespace Models
{
    public class Order {
        public int Id {get; set;}
        public int UserId {get; set;}
        public int AddressId {get; set;}
        public string Status {get; set;}
        public List<OrderProduct> OrderProducts {get; set;}
    }
}