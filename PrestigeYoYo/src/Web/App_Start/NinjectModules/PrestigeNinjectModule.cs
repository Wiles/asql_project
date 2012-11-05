using System.Data.Entity;
using AutoMapper;
using Ninject.Modules;
using Ninject.Web.Common;
using Prestige.DB;

namespace Prestige.Web
{
    public class PrestigeNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<PrestigeContext>()
                .InRequestScope()
                .WithConstructorArgument("connectionString", "Prestige");

            Bind<IMappingEngine>().ToMethod(m => Mapper.Engine)
                .InSingletonScope();

            ProductMappings.Map();
        }
    }
}