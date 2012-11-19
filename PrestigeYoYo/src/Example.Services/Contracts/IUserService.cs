///
///
///

namespace Prestige.Services
{
    using System.Linq;
    using Prestige.DB.Models;

    public interface IUserService
    {

        bool Authenticate(string username, string password);

        void Add(User user);

        void Delete(User user);

        IQueryable<User> List();

        void Update(User user);
    }
}
