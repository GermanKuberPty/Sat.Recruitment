﻿using Sat.Recruitment.Business.Contracts;
using Sat.Recruitment.Business.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Sat.Recruitment.Business.Services
{
    public class UserService : IUserService
    {
        private readonly List<UserModel> _users = new List<UserModel>();

        #region Methods

        /// <summary>
        /// Create a new user.
        /// </summary>        
        /// <param name="user">The <see cref="UserModel"/> model.</param>
        /// <returns>The <see cref="ResultModel"/>.</returns>
        public ResultModel CreateUser(UserModel user)
        {
            var errors = "";

            ValidateErrors(user, ref errors);

            if (errors != null && errors != "")
                return new ResultModel()
                {
                    Data = user,
                    IsSuccess = false,
                    Errors = errors
                };

            user.Money = AssignMoney(user);
            var reader = ReadUsersFromFile();

            user.Email = NormalizeEmail(user.Email);

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var userModel = new UserModel
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                _users.Add(userModel);
            }
            reader.Close();

            IsDuplicated(_users, user, ref errors);

            if(errors == "")
            {
                return new ResultModel()
                {
                    Data = user,
                    IsSuccess = true,
                    Errors = "User Created"
                };
            }
            else
            {
                return new ResultModel()
                {
                    Data = null,
                    IsSuccess = false,
                    Errors = errors
                };
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Validate required properties
        /// </summary>
        /// <param name="user">The <see cref="UserModel"/> model.</param>
        /// <param name="errors">The error variable.</param>        
        public void ValidateErrors(UserModel user, ref string errors)
        {
            if (user.Name == null)
                //Validate if Name is null
                errors = "The name is required";
            if (user.Email == null)
                //Validate if Email is null
                errors += " The email is required";
            if (user.Address == null)
                //Validate if Address is null
                errors += " The address is required";
            if (user.Phone == null)
                //Validate if Phone is null
                errors += " The phone is required";
        }

        /// <summary>
        /// Read users file from file
        /// </summary>        
        /// <returns>The <see cref="StreamReader"/> of users.</returns>
        public StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }

        /// <summary>
        /// Validate if a user already exists.
        /// </summary>
        /// <param name="_users">List of users <see cref="UserModel"/>.</param>
        /// <param name="errors">The error variable.</param>        
        public void IsDuplicated(List<UserModel> _users, UserModel userModel ,ref string errors)
        {
            var isDuplicated = false;
            foreach (var user in _users)
            {
                if ((user.Email == userModel.Email || user.Phone == userModel.Phone) || (user.Name == userModel.Name && user.Address == userModel.Address)) isDuplicated = true;

            }
            if (isDuplicated) errors += "The user is duplicated";
        }

        /// <summary>
        /// Assign Money depending on user type
        /// </summary>
        /// <param name="user">The <see cref="UserModel"/> model.</param>
        /// <returns>The assigned money.</returns>
        public decimal AssignMoney(UserModel user)
        {
            switch (user.UserType)
            {
                case "Normal":
                    if (user.Money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.12);
                        //If new user is normal and has more than USD100
                        var gif = user.Money * percentage;
                        user.Money += gif;
                    }
                    if (user.Money < 100 && user.Money > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = user.Money * percentage;
                        user.Money += gif;
                    }                    
                    break;
                case "SuperUser":
                    if (user.Money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.20);
                        var gif = user.Money * percentage;
                        user.Money += gif;
                    }
                    break;
                case "Premium":
                    if (user.Money > 100)
                    {
                        var gif = user.Money * 2;
                        user.Money += gif;
                    }
                    break;
                default:
                    break;
            }

            return user.Money;
        }

        /// <summary>
        /// Normalize Email.
        /// </summary>
        /// <param name="email">The user email.</param>
        /// <returns>The normalize email.</returns>
        private string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            email = string.Join("@", new string[] { aux[0], aux[1] });
            return email;
        }
        #endregion
    }
}
