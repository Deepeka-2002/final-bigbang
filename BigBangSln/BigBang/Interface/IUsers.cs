using BigBang.Models;

namespace BigBang.Interface
{
    public interface IUsers 
    {
        Task<User> AddUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByEmail(string email);
    }
}
