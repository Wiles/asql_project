/// User service factory to facilitate proper injection
/// of the user service into the role provider.
/// Codeora 2012
///

namespace Prestige.Web.Providers
{
    using System;
    using Ninject;
    using Prestige.Services;

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