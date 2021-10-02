﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Entities.Migrations
{
    [DbContext(typeof(CoffeeClubContext))]
    [Migration("20210929110109_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Models.Coffee", b =>
                {
                    b.Property<int>("CoffeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoffeeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("CoffeePrice")
                        .HasColumnType("float");

                    b.Property<string>("CountryOfOrigin")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CoffeeId");

                    b.ToTable("Coffees");
                });

            modelBuilder.Entity("Entities.Models.Comments", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CoffeeId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("CoffeeId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Entities.Models.Comments", b =>
                {
                    b.HasOne("Entities.Models.Coffee", "Coffee")
                        .WithMany("Comments")
                        .HasForeignKey("CoffeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coffee");
                });

            modelBuilder.Entity("Entities.Models.Coffee", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
