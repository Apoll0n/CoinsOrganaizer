﻿// <auto-generated />
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccessLayer.Coin", b =>
                {
                    b.Property<int>("CoinId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.HasKey("CoinId");

                    b.HasIndex("OrderForeignKey");

                    b.ToTable("Coins");
                });

            modelBuilder.Entity("DataAccessLayer.Order", b =>
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

                    b.HasIndex("CoinForeignKey");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DataAccessLayer.Coin", b =>
                {
                    b.HasOne("DataAccessLayer.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderForeignKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccessLayer.Order", b =>
                {
                    b.HasOne("DataAccessLayer.Coin", "Coin")
                        .WithMany()
                        .HasForeignKey("CoinForeignKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
