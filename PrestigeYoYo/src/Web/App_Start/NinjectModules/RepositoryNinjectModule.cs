///
///
///

namespace Prestige.Web
{
    using Ninject.Modules;
    using Prestige.Repositories;

    /// <summary>
    /// Module for injecting repository classes.
    /// </summary>
    public class RepositoryNinjectModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IProductRepository>().To<ProductRepository>();
            Bind<IScheduleRepository>().To<ScheduleRepository>();
            Bind<IProductionEntryRepository>().To<ProductionEntryRepository>();
            Bind<IUserRepository>().To<UserRepository>();
        }
    }
}