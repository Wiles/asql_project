
namespace Prestige.Services
{
    using System;
    using System.Linq;
    using Prestige.DB.Models;

    public interface IScheduleService
    {
        /// <summary>
        /// Gets the schedule entry by timestamp.
        /// </summary>
        /// <param name="timestamp">The timestamp.</param>
        /// <returns>The schedule entry.</returns>
        Schedule GetByTimestamp(DateTime timestamp);

        IQueryable<Schedule> List();

        void Add(string day, int start, int finish, Product product);
    }
}
