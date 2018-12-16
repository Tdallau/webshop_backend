using System;

namespace webshop_backend.Models.DB
{
    public class Tokens
    {
        public int Id {get; set;}
        public int UserId {get; set;}
        public string Token {get; set;}
        public DateTime Time {get; set;}
        public DateTime ExpireDate {get; set;}
    }
}