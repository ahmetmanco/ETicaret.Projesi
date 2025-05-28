namespace _02_Application.Layer.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException(): base("Kullanıcı Adı veya şifre hatalıdır")
        {
            
        }
        public NotFoundUserException(string? message ) : base( message ) 
        {
            
        }
        public NotFoundUserException(string? message , Exception? innerException): base( message , innerException ) 
        {
            
        }
    }
}
