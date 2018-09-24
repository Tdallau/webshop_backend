
using System.Collections.Generic;

namespace Models
{
    public class User {
        public int Id {get; set;}
        public string Email {get; set;}
        public string Name {get; set;}
        public string Gender {get; set;}
        public string Role {get; set;}
        public string Password {get; set;}
        public string Salt {get; set;}
        public List<Addresses> Addresses {get; set;}

    }
}