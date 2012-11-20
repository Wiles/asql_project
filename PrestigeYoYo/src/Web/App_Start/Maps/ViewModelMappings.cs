using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Prestige.DB.Models;
using Prestige.ViewModels;

namespace Prestige.Web
{
    public class ProductMappings
    {
        public static void Map()
        {
            Mapper.CreateMap<Product, ProductListModel>()
                .ForMember(m => m.Guid, mo => mo.MapFrom(p => p.Id));

            Mapper.CreateMap<ProductFlawType, DefectListModel>()
                .ForMember(m => m.Guid, mo => mo.MapFrom(f => f.Id));
        }
    }
}