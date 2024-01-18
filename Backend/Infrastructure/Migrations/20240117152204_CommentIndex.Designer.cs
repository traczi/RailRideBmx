﻿// <auto-generated />
using System;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240117152204_CommentIndex")]
    partial class CommentIndex
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.Domain.Entity.Address", b =>
                {
                    b.Property<Guid>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Line1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Line2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    b.HasIndex("CartId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Core.Domain.Entity.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsPayd")
                        .HasColumnType("bit");

                    b.Property<string>("SessionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Core.Domain.Entity.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsReported")
                        .HasColumnType("bit");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId", "ProductId")
                        .IsUnique();

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Core.Domain.Entity.ConfigurationBMX", b =>
                {
                    b.Property<Guid>("ConfigurationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AssemblyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ChainsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CrankSetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DiskId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ForkId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FrameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FrontBrakesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GallowsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("HandlebarCapId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("HandlebarCuffId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("HandlebarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("HeadsetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("HubsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NameConfiguration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PedalArmsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PedalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PegsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RearBrakesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RimId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RotorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SaddleClampId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SaddleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SaddleStemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SpokesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TireId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WheelId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ConfigurationId");

                    b.HasIndex("UserId");

                    b.ToTable("ConfigurationsBMX");
                });

            modelBuilder.Entity("Core.Domain.Entity.Like", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Core.Domain.Entity.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfigCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("FrameSize")
                        .HasColumnType("real");

                    b.Property<string>("Geometry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("HandlebarSize")
                        .HasColumnType("real");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("SubCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("WheelSize")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Core.Domain.Entity.ProductCart", b =>
                {
                    b.Property<Guid>("ProductCartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductCartId");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCarts");
                });

            modelBuilder.Entity("Core.Domain.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResetPassWordToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetPasswordTokenExpiration")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Core.Domain.Entity.Address", b =>
                {
                    b.HasOne("Core.Domain.Entity.Cart", "Cart")
                        .WithOne("Address")
                        .HasForeignKey("Core.Domain.Entity.Address", "CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("Core.Domain.Entity.Comment", b =>
                {
                    b.HasOne("Core.Domain.Entity.Product", "Product")
                        .WithMany("Comment")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Domain.Entity.ConfigurationBMX", b =>
                {
                    b.HasOne("Core.Domain.Entity.User", "User")
                        .WithMany("Configurations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Domain.Entity.Like", b =>
                {
                    b.HasOne("Core.Domain.Entity.Product", "Product")
                        .WithMany("Like")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entity.User", "User")
                        .WithMany("Like")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Domain.Entity.ProductCart", b =>
                {
                    b.HasOne("Core.Domain.Entity.Cart", "Cart")
                        .WithMany("ProductCarts")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entity.Product", "Product")
                        .WithMany("ProductCarts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Core.Domain.Entity.Cart", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("ProductCarts");
                });

            modelBuilder.Entity("Core.Domain.Entity.Product", b =>
                {
                    b.Navigation("Comment");

                    b.Navigation("Like");

                    b.Navigation("ProductCarts");
                });

            modelBuilder.Entity("Core.Domain.Entity.User", b =>
                {
                    b.Navigation("Configurations");

                    b.Navigation("Like");
                });
#pragma warning restore 612, 618
        }
    }
}
