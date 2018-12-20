using webshop_backend.Enum;

namespace webshop_backend.Models
{
    public class AdminUsersList
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Email {get; set;}
        public string Approach {get; set;}
        public Roles Role {get; set;}
        public bool Active {get; set;}
    }
}