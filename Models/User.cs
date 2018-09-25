
using System.Collections.Generic;

namespace Models
{
    public class User {
        public int Id {get; set;}
        public string Email {get; set;}
        public string Name {get; set;}
        public string Approach {get; set;}
        public string Role {get; set;}
        public string Password {get; set;}
        public string Salt {get; set;}
        public List<Address> Addresses {get; set;}
        public List<Order> Orders {get; set;}

    }
}