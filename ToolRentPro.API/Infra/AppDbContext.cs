﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToolRentPro.API.Model.User;

namespace ToolRentPro.API.Infra;

public class AppDbContext : IdentityDbContext<UserModel>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}
