﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.Email)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.Email)
                .HasMaxLength(500);
            builder.Property(t => t.Phone)
                .HasMaxLength(500);
            builder.HasOne<UserType>(t => t.UserType);

        }
    }
}
