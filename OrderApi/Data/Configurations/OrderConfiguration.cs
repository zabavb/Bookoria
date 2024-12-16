﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderApi.Models;

namespace OrderApi.Data.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.OrderId)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(o => o.Address)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)");

            builder.Property(o => o.Region)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            builder.Property(o => o.City)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            builder.Property(o => o.Price)
                .IsRequired()
                .HasColumnType("float");

            // Will be moved into DB
            builder.Property(o => o.Delivery)
                .HasConversion<string>();

            builder.Property(o => o.Status)
                .HasConversion<string>();

        }
    }
}