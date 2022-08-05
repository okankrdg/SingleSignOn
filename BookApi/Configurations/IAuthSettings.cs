using BookApi.Models;
using System.Security.Claims;

namespace BookApi.Configurations;

public interface IAuthSettings
{
    ClientDetail GetClientDetail();
    ClientUrlModel GetClientUrl();
    IEnumerable<Claim> GetCustomClaims();
}

