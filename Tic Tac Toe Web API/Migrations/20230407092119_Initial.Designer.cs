﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tic_Tac_Toe_Web_API;

#nullable disable

namespace Tic_Tac_Toe_Web_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230407092119_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("Tic_Tac_Toe_Web_API.Database_Models.PlayerDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Tic_Tac_Toe_Web_API.Database_Models.ResultsDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("GameName")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Wins")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("Results");
                });

            modelBuilder.Entity("Tic_Tac_Toe_Web_API.Database_Models.ResultsDbModel", b =>
                {
                    b.HasOne("Tic_Tac_Toe_Web_API.Database_Models.PlayerDbModel", "Player")
                        .WithOne("Result")
                        .HasForeignKey("Tic_Tac_Toe_Web_API.Database_Models.ResultsDbModel", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Tic_Tac_Toe_Web_API.Database_Models.PlayerDbModel", b =>
                {
                    b.Navigation("Result")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
