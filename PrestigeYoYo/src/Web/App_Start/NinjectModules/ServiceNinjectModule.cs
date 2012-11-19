///
///
///

namespace Prestige.Web
{
    using Ninject.Modules;
    using Prestige.Services;

    /// <summary>
    /// Module for injecting service classes.
    /// </summary>
    public class ServiceNinjectModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IProductService>().To<ProductService>();
            Bind<IScheduleService>().To<ScheduleService>();
            Bind<IProductionEntryService>().To<ProductionEntryService>();
        }
    }
}