﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TracNghiem.Models;

public partial class TracNghiemContext : DbContext
{
    public TracNghiemContext()
    {
    }

    public TracNghiemContext(DbContextOptions<TracNghiemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbBaiThi> TbBaiThis { get; set; }

    public virtual DbSet<TbBaiThiCauHoi> TbBaiThiCauHois { get; set; }

    public virtual DbSet<TbBaiThiChuDe> TbBaiThiChuDes { get; set; }

    public virtual DbSet<TbCauHoi> TbCauHois { get; set; }

    public virtual DbSet<TbChuDe> TbChuDes { get; set; }

    public virtual DbSet<TbDapAn> TbDapAns { get; set; }

    public virtual DbSet<TbKetQua> TbKetQuas { get; set; }

    public virtual DbSet<TbKetQuaChiTiet> TbKetQuaChiTiets { get; set; }

    public virtual DbSet<TbKhoa> TbKhoas { get; set; }

    public virtual DbSet<TbLoaiCauHoi> TbLoaiCauHois { get; set; }

    public virtual DbSet<TbMonHoc> TbMonHocs { get; set; }

    public virtual DbSet<TbMucDoKho> TbMucDoKhos { get; set; }

    public virtual DbSet<TbNguoiDung> TbNguoiDungs { get; set; }

    public virtual DbSet<TbVaiTro> TbVaiTros { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbBaiThi>(entity =>
        {
            entity.HasKey(e => e.IdBaiThi);

            entity.ToTable("tb.BaiThi");

            entity.Property(e => e.IdBaiThi).ValueGeneratedNever();
            entity.Property(e => e.TenBaiThi).HasMaxLength(500);
            entity.Property(e => e.ThoiGian).HasPrecision(6);
            entity.Property(e => e.ThoiGianBatDau).HasColumnType("datetime");

            entity.HasOne(d => d.SoLanToiDaNavigation).WithMany(p => p.TbBaiThis)
                .HasForeignKey(d => d.SoLanToiDa)
                .HasConstraintName("FK_tb.BaiThi_tb.LoaiCauHoi");
        });

        modelBuilder.Entity<TbBaiThiCauHoi>(entity =>
        {
            entity.HasKey(e => e.IdBaiThiCauHoi);

            entity.ToTable("tb.BaiThi_CauHoi");

            entity.Property(e => e.IdBaiThiCauHoi)
                .ValueGeneratedNever()
                .HasColumnName("IdBaiThi_CauHoi");

            entity.HasOne(d => d.IdBaiThiNavigation).WithMany(p => p.TbBaiThiCauHois)
                .HasForeignKey(d => d.IdBaiThi)
                .HasConstraintName("FK_tb.BaiThi_CauHoi_tb.BaiThi");

            entity.HasOne(d => d.IdCauHoiNavigation).WithMany(p => p.TbBaiThiCauHois)
                .HasForeignKey(d => d.IdCauHoi)
                .HasConstraintName("FK_tb.BaiThi_CauHoi_tb.CauHoi");
        });

        modelBuilder.Entity<TbBaiThiChuDe>(entity =>
        {
            entity.HasKey(e => e.IdBaiThiChuDe);

            entity.ToTable("tb.BaiThi_ChuDe");

            entity.Property(e => e.IdBaiThiChuDe)
                .ValueGeneratedNever()
                .HasColumnName("IdBaiThi_ChuDe");

            entity.HasOne(d => d.IdBaiThiNavigation).WithMany(p => p.TbBaiThiChuDes)
                .HasForeignKey(d => d.IdBaiThi)
                .HasConstraintName("FK_tb.BaiThi_ChuDe_tb.BaiThi");

            entity.HasOne(d => d.IdChuDeNavigation).WithMany(p => p.TbBaiThiChuDes)
                .HasForeignKey(d => d.IdChuDe)
                .HasConstraintName("FK_tb.BaiThi_ChuDe_tb.ChuDe");
        });

        modelBuilder.Entity<TbCauHoi>(entity =>
        {
            entity.HasKey(e => e.IdCauHoi);

            entity.ToTable("tb.CauHoi");

            entity.Property(e => e.IdCauHoi).ValueGeneratedNever();

            entity.HasOne(d => d.IdChuDeNavigation).WithMany(p => p.TbCauHois)
                .HasForeignKey(d => d.IdChuDe)
                .HasConstraintName("FK_tb.CauHoi_tb.ChuDe");

            entity.HasOne(d => d.IdLoaiCauHoiNavigation).WithMany(p => p.TbCauHois)
                .HasForeignKey(d => d.IdLoaiCauHoi)
                .HasConstraintName("FK_tb.CauHoi_tb.LoaiCauHoi");

            entity.HasOne(d => d.IdMucDoKhoNavigation).WithMany(p => p.TbCauHois)
                .HasForeignKey(d => d.IdMucDoKho)
                .HasConstraintName("FK_tb.CauHoi_tb.MucDoKho");
        });

        modelBuilder.Entity<TbChuDe>(entity =>
        {
            entity.HasKey(e => e.IdChuDe);

            entity.ToTable("tb.ChuDe");

            entity.Property(e => e.IdChuDe).ValueGeneratedNever();
            entity.Property(e => e.TenChuDe).HasMaxLength(500);

            entity.HasOne(d => d.IdMonHocNavigation).WithMany(p => p.TbChuDes)
                .HasForeignKey(d => d.IdMonHoc)
                .HasConstraintName("FK_tb.ChuDe_tb.MonHoc");
        });

        modelBuilder.Entity<TbDapAn>(entity =>
        {
            entity.HasKey(e => e.IdDapAn);

            entity.ToTable("tb.DapAn");

            entity.Property(e => e.IdDapAn).ValueGeneratedNever();
            entity.Property(e => e.NoiDung).HasMaxLength(255);

            entity.HasOne(d => d.IdCauHoiNavigation).WithMany(p => p.TbDapAns)
                .HasForeignKey(d => d.IdCauHoi)
                .HasConstraintName("FK_tb.DapAn_tb.CauHoi");
        });

        modelBuilder.Entity<TbKetQua>(entity =>
        {
            entity.HasKey(e => e.IdKetQua).HasName("PK_tb.KetQua");

            entity.ToTable("Tb.KetQua");

            entity.Property(e => e.IdKetQua).ValueGeneratedNever();
            entity.Property(e => e.ThoiGianNop).HasColumnType("datetime");

            entity.HasOne(d => d.IdBaiThiNavigation).WithMany(p => p.TbKetQuas)
                .HasForeignKey(d => d.IdBaiThi)
                .HasConstraintName("FK_tb.KetQua_tb.BaiThi");

            entity.HasOne(d => d.IdNguoiDungNavigation).WithMany(p => p.TbKetQuas)
                .HasForeignKey(d => d.IdNguoiDung)
                .HasConstraintName("FK_Tb.KetQua_tb.NguoiDung");
        });

        modelBuilder.Entity<TbKetQuaChiTiet>(entity =>
        {
            entity.HasKey(e => e.IdKetQuaChiTiet).HasName("PK_tb.KetQuaChiTiet");

            entity.ToTable("Tb.KetQuaChiTiet");

            entity.Property(e => e.IdKetQuaChiTiet).ValueGeneratedNever();
            entity.Property(e => e.DapAnChon).HasMaxLength(500);
            entity.Property(e => e.DapAnDienKhuyet).HasColumnType("text");
            entity.Property(e => e.DapAnSxthuTu)
                .HasColumnType("text")
                .HasColumnName("DapAnSXThuTu");
            entity.Property(e => e.IsCorrect).HasColumnName("Is_Correct");

            entity.HasOne(d => d.IdCauHoiNavigation).WithMany(p => p.TbKetQuaChiTiets)
                .HasForeignKey(d => d.IdCauHoi)
                .HasConstraintName("FK_tb.KetQuaChiTiet_tb.CauHoi");

            entity.HasOne(d => d.IdKetQuaNavigation).WithMany(p => p.TbKetQuaChiTiets)
                .HasForeignKey(d => d.IdKetQua)
                .HasConstraintName("FK_tb.KetQuaChiTiet_tb.KetQua");
        });

        modelBuilder.Entity<TbKhoa>(entity =>
        {
            entity.HasKey(e => e.IdKhoa).HasName("PK_Id.Khoa");

            entity.ToTable("Tb.Khoa");

            entity.Property(e => e.IdKhoa).ValueGeneratedNever();
            entity.Property(e => e.TenKhoa).HasMaxLength(1000);
        });

        modelBuilder.Entity<TbLoaiCauHoi>(entity =>
        {
            entity.HasKey(e => e.IdLoaiCauHoi).HasName("PK_tb.LoaiCauHoi");

            entity.ToTable("Tb.LoaiCauHoi");

            entity.Property(e => e.IdLoaiCauHoi).ValueGeneratedNever();
            entity.Property(e => e.LoaiCauHoi).HasMaxLength(300);
        });

        modelBuilder.Entity<TbMonHoc>(entity =>
        {
            entity.HasKey(e => e.IdMonHoc).HasName("PK_tb.MonHoc");

            entity.ToTable("Tb.MonHoc");

            entity.Property(e => e.IdMonHoc).ValueGeneratedNever();
            entity.Property(e => e.TenMonHoc).HasMaxLength(500);

            entity.HasOne(d => d.IdKhoaNavigation).WithMany(p => p.TbMonHocs)
                .HasForeignKey(d => d.IdKhoa)
                .HasConstraintName("FK_tb.MonHoc_tb.Khoa");
        });

        modelBuilder.Entity<TbMucDoKho>(entity =>
        {
            entity.HasKey(e => e.IdMucDoKho).HasName("PK_tb.MucDoKho");

            entity.ToTable("Tb.MucDoKho");

            entity.Property(e => e.IdMucDoKho).ValueGeneratedNever();
            entity.Property(e => e.TenMucDo).HasMaxLength(500);
        });

        modelBuilder.Entity<TbNguoiDung>(entity =>
        {
            entity.HasKey(e => e.IdNguoiDung);

            entity.ToTable("tb.NguoiDung");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.GioiTinh).HasColumnName("GIoiTinh");
            entity.Property(e => e.HoTen).HasMaxLength(255);
            entity.Property(e => e.Lop).HasMaxLength(255);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");
            entity.Property(e => e.TenDangNhap).HasMaxLength(100);

            entity.HasOne(d => d.IdKhoaNavigation).WithMany(p => p.TbNguoiDungs)
                .HasForeignKey(d => d.IdKhoa)
                .HasConstraintName("FK_tb.NguoiDung_tb.Khoa");

            entity.HasOne(d => d.IdVaiTroNavigation).WithMany(p => p.TbNguoiDungs)
                .HasForeignKey(d => d.IdVaiTro)
                .HasConstraintName("FK_tb.NguoiDung_VaiTro");
        });

        modelBuilder.Entity<TbVaiTro>(entity =>
        {
            entity.HasKey(e => e.IdVaiTro).HasName("PK_VaiTro");

            entity.ToTable("TbVaiTro");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
