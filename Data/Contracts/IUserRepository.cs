using Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> GetUserAndPassword(string username, string password, CancellationToken cancelationtoken);
        Task AddAsync(User user, string password, CancellationToken cancellationToken);
    }
}