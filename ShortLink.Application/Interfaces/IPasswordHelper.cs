namespace ShortLink.Application.Interfaces
{
    public interface IPasswordHelper
    {
        string EncodePasswordMd5(string pass);
    }
}