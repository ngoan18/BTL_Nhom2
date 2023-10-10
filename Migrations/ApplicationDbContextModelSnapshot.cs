﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcMovie.Data;

#nullable disable

namespace qlnv.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Mapb")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Tencv")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Macv");

                    b.HasIndex("Mapb");

                    b.ToTable("Chucvu");
                });

            modelBuilder.Entity("qlnv.Models.HopDong", b =>
                {
                    b.Property<string>("Mahd")
                        .HasColumnType("TEXT");

                    b.Property<string>("Manv")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NgayBatDau")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NgayKetThuc")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Tenhd")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ThoiHan")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Mahd");

                    b.HasIndex("Manv");

                    b.ToTable("HopDong");
                });

            modelBuilder.Entity("qlnv.Models.KhenThuong", b =>
                {
                    b.Property<string>("MaKT")
                        .HasColumnType("TEXT");

                    b.Property<string>("Manv")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TenKT")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MaKT");

                    b.HasIndex("Manv");

                    b.ToTable("KhenThuong");
                });

            modelBuilder.Entity("qlnv.Models.KyLuat", b =>
                {
                    b.Property<string>("MaKL")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenKL")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MaKL");

                    b.ToTable("KyLuat");
                });

            modelBuilder.Entity("qlnv.Models.Luong", b =>
                {
                    b.Property<string>("LuongCB")
                        .HasColumnType("TEXT");

                    b.Property<string>("Macv")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LuongCB");

                    b.HasIndex("Macv");

                    b.ToTable("Luong");
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

                    b.Property<string>("Mabp")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Macv")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Mapc")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Matd")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Ngaysinh")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Manv");

                    b.HasIndex("Mabp");

                    b.HasIndex("Macv");

                    b.HasIndex("Mapc");

                    b.HasIndex("Matd");

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

            modelBuilder.Entity("qlnv.Models.PhuCap", b =>
                {
                    b.Property<string>("Mapc")
                        .HasColumnType("TEXT");

                    b.Property<string>("SoTien")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Tenpc")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Mapc");

                    b.ToTable("PhuCap");
                });

            modelBuilder.Entity("qlnv.Models.Trinhdo", b =>
                {
                    b.Property<string>("Matd")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tentd")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Matd");

                    b.ToTable("Trinhdo");
                });

            modelBuilder.Entity("qlnv.Models.Chucvu", b =>
                {
                    b.HasOne("qlnv.Models.Phongban", "phongban")
                        .WithMany()
                        .HasForeignKey("Mapb")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("phongban");
                });

            modelBuilder.Entity("qlnv.Models.HopDong", b =>
                {
                    b.HasOne("qlnv.Models.Nhanvien", "Nhanvien")
                        .WithMany()
                        .HasForeignKey("Manv")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nhanvien");
                });

            modelBuilder.Entity("qlnv.Models.KhenThuong", b =>
                {
                    b.HasOne("qlnv.Models.Nhanvien", "Nhanvien")
                        .WithMany()
                        .HasForeignKey("Manv")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nhanvien");
                });

            modelBuilder.Entity("qlnv.Models.Luong", b =>
                {
                    b.HasOne("qlnv.Models.Chucvu", "Chucvu")
                        .WithMany()
                        .HasForeignKey("Macv")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chucvu");
                });

            modelBuilder.Entity("qlnv.Models.Nhanvien", b =>
                {
                    b.HasOne("qlnv.Models.Bophan", "bophan")
                        .WithMany()
                        .HasForeignKey("Mabp")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("qlnv.Models.Chucvu", "chucvu")
                        .WithMany()
                        .HasForeignKey("Macv")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("qlnv.Models.PhuCap", "phucap")
                        .WithMany()
                        .HasForeignKey("Mapc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("qlnv.Models.Trinhdo", "trinhdo")
                        .WithMany()
                        .HasForeignKey("Matd")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("bophan");

                    b.Navigation("chucvu");

                    b.Navigation("phucap");

                    b.Navigation("trinhdo");
                });
#pragma warning restore 612, 618
        }
    }
}
