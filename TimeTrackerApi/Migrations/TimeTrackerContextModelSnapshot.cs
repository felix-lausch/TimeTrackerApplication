﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeTrackerApi;

#nullable disable

namespace TimeTrackerApi.Migrations
{
    [DbContext(typeof(TimeTrackerContext))]
    partial class TimeTrackerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TimeTrackerApi.Models.TimeEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("EndHours")
                        .HasColumnType("int");

                    b.Property<int>("EndMinutes")
                        .HasColumnType("int");

                    b.Property<double>("EndMinutesAlt")
                        .HasColumnType("float");

                    b.Property<double>("PauseHours")
                        .HasColumnType("float");

                    b.Property<int>("StartHours")
                        .HasColumnType("int");

                    b.Property<int>("StartMinutes")
                        .HasColumnType("int");

                    b.Property<double>("StartMinutesAlt")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("TimeEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
