﻿///
///
///

namespace Prestige.Web
{
    using Ninject.Modules;
    using Ninject.Web.Common;
    using Prestige.Services;
    using Prestige.Web.Providers;

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
            Bind<IProductFlawService>().To<ProductFlawService>();
            Bind<IProductionStationService>().To<ProductionStationService>();

            Bind<IUserService>().To<UserService>().InRequestScope();
            Bind<IUserServiceFactory>().To<UserServiceFactory>().InSingletonScope();
        }
    }
}