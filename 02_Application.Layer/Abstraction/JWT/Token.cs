namespace _02_Application.Layer.Abstraction.JWT
{
    public class Token 
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
