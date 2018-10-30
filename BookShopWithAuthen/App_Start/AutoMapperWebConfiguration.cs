using AutoMapper;
using BookShopWithAuthen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookShopWithAuthen.Models;
using BookShopWithAuthen.ViewModel;

namespace BookShopWithAuthen.App_Start
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<OrderDetailProfile>();
            });
        }
    }

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, ShippingDetailViewModel>();
            CreateMap<ApplicationUser, UserViewModel>();
        }
    }
    public class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetail, OrderDetailViewModel>();
        }
    }
}