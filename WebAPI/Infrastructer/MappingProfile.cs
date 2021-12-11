using AutoMapper;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Model.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Infrastructer
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, User>();
            CreateMap<User,UserModel>();

            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel,Category>();
            CreateMap<ProductModel,Product>();
            CreateMap<Product,ProductModel>();

        }
    }
}
