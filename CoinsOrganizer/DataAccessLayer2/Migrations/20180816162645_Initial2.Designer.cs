﻿// <auto-generated />
using DataAccessLayer2.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer2.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20180816162645_Initial2")]
    partial class Initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccessLayer2.Coin", b =>
                {
                    b.Property<int>("CoinerId");

                    b.Property<string>("AversFotoLink");

                    b.Property<double>("Cost");

                    b.Property<double>("DollarPrice");

                    b.Property<string>("EnglishName");

                    b.Property<bool>("IsInStock");

                    b.Property<bool>("IsSold");

                    b.Property<string>("Link")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("OrderForeignKey");

                    b.Property<string>("PolishName");

                    b.Property<string>("ReversFotoLink");

                    b.Property<double>("ZlotyPrice");

                    b.HasKey("CoinerId");

                    b.HasIndex("OrderForeignKey")
                        .IsUnique();

                    b.ToTable("Coins");
                });

            modelBuilder.Entity("DataAccessLayer2.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CoinForeignKey");

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

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DataAccessLayer2.Coin", b =>
                {
                    b.HasOne("DataAccessLayer2.Order", "Order")
                        .WithOne("Coin")
                        .HasForeignKey("DataAccessLayer2.Coin", "OrderForeignKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
