namespace MovieCollectionAPI.Messages
{
    public class LoginResponse : BaseResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
