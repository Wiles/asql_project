using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prestige.EF;

namespace Prestige.DB.Models
{
    public class ProductFlawType : IModelBase
    {
        public Guid Id { get; set; }

        public InspectionStation InspectionLocation { get; set; }

        public string Decision { get; set; }

        public string Reason { get; set; }
    }
}
