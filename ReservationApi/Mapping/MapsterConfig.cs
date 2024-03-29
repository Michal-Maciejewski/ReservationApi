﻿using Duende.IdentityServer.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReservationApi.Controllers;
using ReservationApi.Data;
using ReservationApi.Models.Sitting;
using ReservationApi.Models.User;
using System;

namespace ReservationApi.Mapping
{
    public class MapsterConfigIRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<(string jwt, IdentityApiUser user, IList<string> roles), ReturnUserModel>.NewConfig()
                .Map(dest => dest.Id, src => src.user.Id)
                .Map(dest => dest.Token, src => src.jwt)
                .Map(dest => dest.Email, src => src.user.Email)
                .Map(dest => dest.FirstName, src => src.user.FirstName)
                .Map(dest => dest.LastName, src => src.user.LastName)
                .Map(dest => dest.Roles, src => src.roles)
            .IgnoreNullValues(true);

            config.NewConfig<Sitting, SittingBaseEventModel>()
                .Map(dest => dest.SittingTypeId, src => src.SittingType.Id)
                .Map(dest => dest.Editable, src => !src.Reservations.Any() && src.End.Date >= DateTime.Now.Date)
                .ConstructUsing(src =>
                    src.GroupSittingId.HasValue
                        ? (SittingBaseEventModel)new SittingGroupEventModel { GroupSittingId = src.GroupSittingId.Value }
                        : new SittingEventModel())
                .IgnoreNullValues(true);
        }
    }
}
