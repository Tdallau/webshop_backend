
using System.Collections.Generic;

namespace Models
{
    public class User {
        public int id {get; set;}
        public string email {get; set;}
        public string name {get; set;}
        public string approach {get; set;}
        public string role {get; set;}
        public string password {get; set;}
        public string salt {get; set;}
        public List<Address> addresses {get; set;}
        public List<Order> orders {get; set;}

    }
}