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

        public DbSet<Item> item { get; set; }

        public DbSet<Customer> customer { get; set; }

        public DbSet<Discount> discount { get; set; }

        public DbSet<LoyaltyPoints> loyaltyPoints { get; set; }
        public DbSet<Order> order { get; set; }
        public DbSet<OrderItem> orderItem { get; set; }
        public DbSet<OrderBooking> orderBooking { get; set; }
        public DbSet<Tip> tip { get; set; }
        public DbSet<Transaction> transactions { get; set; }
        public DbSet<PaymentTransaction> paymentTransaction { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderBooking>()
                .HasKey(ob => new { ob.OrderId, ob.BookingId });

            modelBuilder.Entity<OrderItem>()
                .HasKey(ob => new { ob.OrderId, ob.ItemId });
        
        }

    }
}
