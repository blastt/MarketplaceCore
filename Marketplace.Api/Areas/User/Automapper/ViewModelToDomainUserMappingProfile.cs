﻿using AutoMapper;
using Marketplace.Api.Areas.User.ViewModels;
using Marketplace.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Api.Areas.User.Automapper
{
    public partial class ViewModelToDomainUserMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        public ViewModelToDomainUserMappingProfile()
        {
            CreateMap<MessageViewModel, Message>()
               .ForMember(o => o.Id, map => map.MapFrom(vm => vm.Id))
               .ForMember(o => o.ToViewed, map => map.MapFrom(vm => vm.ToViewed))
                .ForMember(o => o.FromViewed, map => map.MapFrom(vm => vm.FromViewed))
               .ForMember(o => o.MessageBody, map => map.MapFrom(vm => vm.MessageBody))

               .ForMember(o => o.ReceiverDeleted, map => map.MapFrom(vm => vm.ReceiverDeleted))
               .ForMember(o => o.SenderDeleted, map => map.MapFrom(vm => vm.SenderDeleted))
               .ForMember(o => o.ReceiverId, map => map.MapFrom(vm => vm.ReceiverId))
               .ForMember(o => o.SenderId, map => map.MapFrom(vm => vm.SenderId))
               .ReverseMap()
               .ForPath(o => o.ReceiverName, map => map.MapFrom(vm => vm.Receiver.UserName))
               .ForPath(o => o.SenderImage, map => map.MapFrom(vm => vm.Sender.Avatar32))
               .ForPath(o => o.SenderName, map => map.MapFrom(vm => vm.Sender.UserName))
               .ReverseMap()
               .ForMember(o => o.CreatedDate, map => map.MapFrom(vm => vm.CreatedDate));
        }
    }
}