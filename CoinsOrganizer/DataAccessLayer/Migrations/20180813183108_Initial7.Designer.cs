﻿// <auto-generated />
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20180813183108_Initial7")]
    partial class Initial7
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccessLayer.Coin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AversFotoLink");

                    b.Property<int>("CoinId");

                    b.Property<double>("Cost");

                    b.Property<double>("DollarPrice");

                    b.Property<string>("EnglishName");

                    b.Property<bool>("IsInStock");

                    b.Property<bool>("IsSold");

                    b.Property<string>("Link")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("OrderId");

                    b.Property<string>("PolishName");

                    b.Property<string>("ReversFotoLink");

                    b.Property<double>("SalePrice");

                    b.Property<double>("SoldPrice");

                    b.Property<double>("ZlotyPrice");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DataAccessLayer.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("CoinCost");

                    b.Property<int>("CoinId");

                    b.Property<string>("Email");

                    b.Property<bool>("IsPaid");

                    b.Property<string>("Link");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NickName")
                        .IsRequired();

                    b.Property<string>("OrderDetails");

                    b.Property<string>("SaleCurrency");

                    b.Property<double>("SalePrice");

                    b.Property<string>("TrackNumber");

                    b.Property<string>("WhereSold")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Coins");
                });
#pragma warning restore 612, 618
        }
    }
}
