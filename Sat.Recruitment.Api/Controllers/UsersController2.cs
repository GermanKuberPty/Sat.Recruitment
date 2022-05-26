﻿using System;
using System.IO;

namespace Sat.Recruitment.Api.Controllers
{
    public partial class UsersController
    {
        private StreamReader ReadUsersFromFile()
        {
            const string filesUsersTxt = "/Files/Users.txt";
            var path = $"{Directory.GetCurrentDirectory()}{filesUsersTxt}";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
