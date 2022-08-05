namespace BookApi.Services
{
    public interface ITokenService
    {
        Task<string> GetToken(); 
    }
}
