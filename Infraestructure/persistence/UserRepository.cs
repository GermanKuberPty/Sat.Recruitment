﻿using Application.Interfaces;
using Domain.Entities;
using Domain.Events;
using Infraestructure.Configdb;
using Infraestructure.dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.persistence
{
    public class UserRepository : IUserRepository
    {
        private ApiDbContext _apiDbContext;

        public UserRepository(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }
        public async Task<bool> Create(IUserType user)
        {

            var userDto = new UserDto()
            { Name = user.Name, Email = user.Email, Address = user.Address, Phone = user.Phone, UserType = user.UserType.ToString(), Money = user.Money };

            _apiDbContext.userDto.Add(userDto);
            var insert = await _apiDbContext.SaveChangesAsync();
            if (insert != 0)
            {
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(UserDomain user)
        {
            throw new NotImplementedException();
        }
    }
}
