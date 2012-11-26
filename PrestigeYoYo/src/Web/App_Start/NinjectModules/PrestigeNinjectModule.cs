///
///
///

namespace Prestige.Web
{
    using System.Data.Entity;
    using AutoMapper;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using Prestige.DB;

    public class PrestigeNinjectModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            // bind our specific database context
            Bind<DbContext>().To<PrestigeContext>()
                .InRequestScope()
                .WithConstructorArgument("connectionString", "Prestige");

            // bind automapper
            Bind<IMappingEngine>().ToMethod(m => Mapper.Engine)
                .InSingletonScope();

            // map automapper mappings
            ViewModelMappings.Map();
        }
    }
}