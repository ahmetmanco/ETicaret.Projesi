using _02_Application.Layer.Abstraction.JWT;

namespace _02_Application.Layer.Abstraction
{
    public interface ITokenHandler
    {
        Token CreatedAccessToken(int minute);
    }
}
