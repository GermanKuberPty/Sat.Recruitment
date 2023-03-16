﻿using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Dto
{
    public class UserDto 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public decimal Money { get; set; }
    }
}
