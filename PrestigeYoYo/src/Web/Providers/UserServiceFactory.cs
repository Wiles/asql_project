using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Prestige.Services;
using Ninject;

namespace Prestige.Web.Providers
{
    public interface IUserServiceFactory
    {
        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <returns>The service.</returns>
        IUserService GetService();
    }

    public class UserServiceFactory : IUserServiceFactory
    {
        private readonly IKernel Kernel;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="UserServiceFactory" /> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public UserServiceFactory(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }

            this.Kernel = kernel;
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <returns>The service.</returns>
        public IUserService GetService()
        {
            return this.Kernel.Get<IUserService>();
        }
    }
}