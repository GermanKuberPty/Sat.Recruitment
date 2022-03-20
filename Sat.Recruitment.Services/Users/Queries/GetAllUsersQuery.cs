﻿using MediatR;
using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Services.Users.Queries
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {
    }
}
