﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcMovie.Data;

#nullable disable

namespace qlnv.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231002031530_Table_Phongban")]
    partial class Table_Phongban
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("qlnv.Models.Bophan", b =>
                {
                    b.Property<string>("Mabp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tenbp")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Mabp");

                    b.ToTable("Bophan");
                });

            modelBuilder.Entity("qlnv.Models.Chucvu", b =>
                {
                    b.Property<string>("Macv")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tencv")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Macv");

                    b.ToTable("Chucvu");
                });

            modelBuilder.Entity("qlnv.Models.Nhanvien", b =>
                {
                    b.Property<string>("Manv")
                        .HasColumnType("TEXT");

                    b.Property<string>("CCCD")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Diachi")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Gioitinh")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Hoten")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Ngaysinh")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Manv");

                    b.ToTable("Nhanvien");
                });

            modelBuilder.Entity("qlnv.Models.Phongban", b =>
                {
                    b.Property<string>("Mapb")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tenpb")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Mapb");

                    b.ToTable("Phongban");
                });
#pragma warning restore 612, 618
        }
    }
}
