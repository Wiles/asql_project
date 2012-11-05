using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prestige.EF;

namespace Prestige.DB.Models
{
    public class InspectionStation : IModelBase
    {
        public Guid Id { get; set; }

        public int StationNumber { get; set; }

        public string Description { get; set; }
    }
}
