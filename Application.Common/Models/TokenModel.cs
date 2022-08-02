namespace Application.Common.Models
{
    public class TokenModel
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public UserModel User { get; set; }
        public string Username { get; set; }
        public DateTime ValidTo { get; set; }
        public DateTime ValidFrom { get; set; }
    }
}
