using Models.DB;

namespace webshop_backend.Models
{
    public class OrderReturn : Order
    {
        public string StatusString {get; set;}
    }
}