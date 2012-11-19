
namespace Prestige.Services
{
    using System;
    using System.Linq;
    using Prestige.DB.Models;

    public interface IScheduleService
    {
        IQueryable<Schedule> List();

        void Add(string day, int start, int finish, Product product);
    }
}
