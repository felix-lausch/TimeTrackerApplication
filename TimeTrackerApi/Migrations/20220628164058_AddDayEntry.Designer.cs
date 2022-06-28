﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeTrackerApi;

#nullable disable

namespace TimeTrackerApi.Migrations
{
    [DbContext(typeof(TimeTrackerContext))]
    [Migration("20220628164058_AddDayEntry")]
    partial class AddDayEntry
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TimeTrackerApi.Models.DayEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("DayEntries");
                });

            modelBuilder.Entity("TimeTrackerApi.Models.TimeEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DayEntryId")
                        .HasColumnType("uniqueidentifier");

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

                    b.HasIndex("DayEntryId");

                    b.ToTable("TimeEntries");
                });

            modelBuilder.Entity("TimeTrackerApi.Models.TimeEntry", b =>
                {
                    b.HasOne("TimeTrackerApi.Models.DayEntry", null)
                        .WithMany("TimeEntries")
                        .HasForeignKey("DayEntryId");
                });

            modelBuilder.Entity("TimeTrackerApi.Models.DayEntry", b =>
                {
                    b.Navigation("TimeEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
