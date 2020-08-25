namespace Application.DTOs.Authentication
{
    public class TokenRequest
    {
        public string IdentificationId { get; set; }
        public string Password { get; set; }
    }

    public class TokenRequestMapTest
    {
        public string IdentificationId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
