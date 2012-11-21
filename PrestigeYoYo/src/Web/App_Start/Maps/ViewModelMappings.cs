using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Prestige.DB.Models;
using Prestige.ViewModels;
using System.Text;

namespace Prestige.Web
{
    public class ViewModelMappings
    {
        public static void Map()
        {
            Mapper.CreateMap<Product, ProductListModel>()
                .ForMember(m => m.Guid, mo => mo.MapFrom(p => p.Id));

            Mapper.CreateMap<ProductFlawType, DefectListModel>()
                .ForMember(m => m.Guid, mo => mo.MapFrom(f => f.Id));

            Mapper.CreateMap<ProductionStation, StationListModel>()
                .ForMember(m => m.Guid, mo => mo.MapFrom(s => s.Id));

            Mapper.CreateMap<User, UserListModel>()
                .ForMember(m => m.Guid, mo => mo.MapFrom(u => u.Id))
                .ForMember(m => m.Password, mo => mo.MapFrom(u => ""))
                .ForMember(m => m.Roles, mo => mo.MapFrom(
                        u => string.Join(", ", u.Roles.Select(r => r.Name))));
        }
    }
}