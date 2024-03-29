﻿using Microsoft.EntityFrameworkCore;
using web_api.Models;

namespace web_api.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) :base(options)
        {

        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
    }
}
