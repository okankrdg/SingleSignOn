using SingleSignOn.Models;

namespace SingleSignOn.Services;


public class UserService : IUserService
{
    private List<UserModel> users => new()
    {
        new UserModel { Id = 1, Name = "Okan Karadag", BirthDate = new DateTime(1905, 10, 10), City = "Ankara", Email = "okan@okan.com", Gender = "Man", Username = "okan", Roles = { "Admin" } },
        new UserModel { Id = 2, Name = "Albert Camus", BirthDate = new DateTime(1899, 10, 10), City = "Ankara", Email = "albert@okan.com", Gender = "Man", Username = "albert", Roles = { "Author" } },
        new UserModel { Id = 3, Name = "Frank Herbert", BirthDate = new DateTime(1930, 10, 10), City = "Ankara", Email = "frank@okan.com", Gender = "Man", Username = "frank", Roles = { "Editor" } },
        new UserModel { Id = 4, Name = "Isaac Asimov", BirthDate = new DateTime(1940, 10, 10), City = "Ankara", Email = "asimov@okan.com", Gender = "Man", Username = "asimov", Roles = { "Editor", "Author" } },
        new UserModel { Id = 5, Name = "Tolstoy", BirthDate = new DateTime(1936, 10, 10), City = "Ankara", Email = "tolstoy@okan.com", Gender = "Man", Username = "tolstoy", Roles = { "Editor", "Reader" } },
        new UserModel { Id = 6, Name = "Ursula", BirthDate = new DateTime(1945, 10, 10), City = "Ankara", Email = "ursula@okan.com", Gender = "Woman", Username = "ursula", Roles = { "Editor", "Master" } },
    };
    public UserModel? GetById(int id)
    {
        return users.FirstOrDefault(u => u.Id == id);
    }
    public UserModel? GetByUsername(string username)
    {
        return users.FirstOrDefault(u => u.Username == username);
    }
}
public interface IUserService
{
    UserModel? GetById(int id);
    UserModel? GetByUsername(string username);
}
