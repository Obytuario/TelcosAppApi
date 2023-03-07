﻿using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.Application.Contracts.User
{
    public interface IUserServices
    {

        Task<RequestResult<List<PostUserDto>>> GetAllUsers();
        Task<RequestResult<PostUserDto>> SaveUser(PostUserDto userDto);
    }
}
