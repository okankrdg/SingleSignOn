using BookApi.Models;
using System.Security.Claims;

namespace BookApi.Configurations
{
    public class MainAuthSettings : IAuthSettings
    {
        public IConfiguration _configuration { get; }
        public MainAuthSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public ClientDetail GetClientDetail()
        {
            return new ClientDetail
            {
                ClientId = _configuration["ClientId"],
                ClientSecret = _configuration["ClientSecret"]
            };
        }

        public ClientUrlModel GetClientUrl()
        {
            return new ClientUrlModel
            {
                Authority = "https://localhost:7267/",
                BaseAddress = "https://localhost:7267/",
                LogOutAddress = "/Home/Index"
            };
        }

        public IEnumerable<Claim> GetCustomClaims()
        {
            var model = new[]
            {
                new Claim("CustomKey","Custom")
            };
            return model;
        }
    }
}
