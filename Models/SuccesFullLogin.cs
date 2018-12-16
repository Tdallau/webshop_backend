namespace Models {
    public class SucccessFullyLoggedIn
    {
        public string Token {get; set;}
        public string RefreshToken {get; set;}
        public UserData User {get; set;}
    }
}