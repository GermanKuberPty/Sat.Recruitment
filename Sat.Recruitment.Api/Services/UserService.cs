﻿using Sat.Recruitment.Api.Utils;
using System;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Repositories;
using Sat.Recruitment.Api.Strategies;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Create(User user)
        {
            IGiftStrategy giftStrategy = GetGiftStrategy(user);

            var userGiftContext = new GiftContext(giftStrategy);
            userGiftContext.ApllyGift(user);

            user.Email = Helper.NormalizeMail(user.Email);

            var result = await _userRepository.Create(user);

            return result;
        }

        private static IGiftStrategy GetGiftStrategy(User user)
        {
            return user.UserType switch
            {
                UserType.Normal => new NormalGiftStrategy(),
                UserType.SuperUser => new SuperUserStrategy(),
                UserType.Premium => new PremiumGiftStrategy(),
                _ => throw new Exception("Error in userType")
            };
        }
    }
}
