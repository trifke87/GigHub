﻿using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //MapperConfiguration config = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<ApplicationUser, UserDto>();
            //    cfg.CreateMap<Gig, GigDto>();
            //    cfg.CreateMap<Notification, NotificationDto>();
            //});

            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Gig, GigDto>();
            CreateMap<Notification, NotificationDto>();
        }

    }
}