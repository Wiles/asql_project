///
///
///

namespace Prestige.Services
{
    using System;
    using System.Linq;
    using Prestige.DB.Models;
    using Prestige.Repositories;

    public class ScheduleService : IScheduleService
    {
        /// <summary>
        /// The schedulable days.
        /// </summary>
        private static string[] Days = new string[]
        {
            "MONDAY",
            "TUESDAY",
            "WEDNESDAY",
            "THURSDAY",
            "FRIDAY"
        };

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ScheduleService" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ScheduleService(IScheduleRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.Repository = repository;
        }

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        private IScheduleRepository Repository { get; set; }

        /// <summary>
        /// Returns a queryable list of schedule objects.
        /// </summary>
        /// <returns>Queryable of the schedule.</returns>
        public IQueryable<Schedule> List()
        {
            return this.Repository;
        }

        /// <summary>
        /// Adds the specified start.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="finish">The finish.</param>
        /// <param name="product">The product.</param>
        public void Add(string day, int start, int finish, Product product)
        {
            if (string.IsNullOrWhiteSpace(day))
            {
                throw new ArgumentException("day cannot be null or blank");
            }
            else if (!Days.Contains(day.ToUpper()))
            {
                throw new ArgumentException("invalid day specified");
            }
            else if (start >= finish)
            {
                throw new ArgumentException("start must come before finish.");
            }
            else if (start < 0 || start > 23)
            {
                throw new ArgumentException("invalid start specified");
            }
            else if (finish < 1 || finish > 24)
            {
                throw new ArgumentException("invalid finish specified");
            }
            
            day = day.ToUpper();

            for (int i = start; i < finish; i++)
            {
                var schedule = this.Repository.FirstOrDefault(s => s.Day == day && s.Hour == i);

                if (schedule == null)
                {
                    this.Repository.Add(new Schedule()
                    {
                        Day = day.ToUpper(),
                        Hour = i,
                        Product = product
                    });
                }
                else
                {
                    schedule.Product = product;
                }
            }

            this.Repository.SaveChanges();
        }
    }
}
