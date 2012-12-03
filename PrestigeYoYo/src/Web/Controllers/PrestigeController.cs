/// Base application controller
/// Codeora 2012
///

namespace Prestige.Controllers
{
    using System;
    using System.Web.Mvc;
    using AutoMapper;

    [PrestigeAuthorize]
    public class PrestigeController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="PrestigeController" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        public PrestigeController(IMappingEngine mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }

            this.Mapper = mapper;
        }

        /// <summary>
        /// Gets or sets the mapper.
        /// </summary>
        /// <value>
        /// The mapper.
        /// </value>
        protected IMappingEngine Mapper { get; set; }
    }
}
