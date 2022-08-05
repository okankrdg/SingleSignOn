using Okan.IdentityServer.Models;
using Okan.IdentityServer.Services;
using System.Security.Claims;

namespace MvcApp.Services
{
    public class ClientService : IClientService
    {
        public ClientDetail GetClientDetail()
        {
            return new ClientDetail
            {
                Authority = "",
                BaseAddress = "",
                ClientId = "",
                ClientSecret = "",
                LogOutAddress = ""
            };
        }

        public IEnumerable<Claim> GetCustomClaims()
        {
            throw new NotImplementedException();
        }
    }
}
