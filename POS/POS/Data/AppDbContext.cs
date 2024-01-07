﻿using POS.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace POS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Role> role { get; set; }
        public DbSet<Service> service { get; set; }
        public DbSet<TimeSlot> timeSlot { get; set; }
        public DbSet<Staff> staff { get; set; }
        public DbSet<Booking> booking { get; set; }

    }
}
