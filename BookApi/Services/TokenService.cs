using BookApi.Configurations;
using IdentityModel.Client;

namespace BookApi.Services;

public class TokenService : ITokenService
{
    private readonly IAuthSettings _authSettings;

    public TokenService(IAuthSettings authSettings)
    {
        _authSettings = authSettings;
    }
    public async Task<string> GetToken()
    {
        //identity-model required
        var clientInfo = _authSettings.GetClientDetail();
        var urls = _authSettings.GetClientUrl();
        var client = new HttpClient();
        var response = await client.RequestTokenAsync(new TokenRequest
        {
            Address = urls.Authority,
            ClientId = clientInfo.ClientId,
            ClientSecret = clientInfo.ClientSecret,
        });
        if (response.IsError)
        {
            throw new HttpRequestException(response.Error);
        }
        return response.AccessToken;
    }
}

