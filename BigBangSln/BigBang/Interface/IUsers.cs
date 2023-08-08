using BigBang.Models;

namespace BigBang.Interface
{
    public interface IUsers 
    {
        Task<User> AddUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int userId);
        Task<List<User>> GetActiveUsers();
        Task<List<User>> GetPendingUsers();
        Task<User> GetUserByEmail(string email);
        Task DeleteUser(User user);
        Task<List<User>?> DeleteUserById(int id);
        Task UpdateUser(User user);
    }
}
