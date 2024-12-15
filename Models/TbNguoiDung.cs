﻿using System;
using System.Collections.Generic;

namespace TracNghiem.Models;

public partial class TbNguoiDung
{
    public int IdNguoiDung { get; set; }

    public string? HoTen { get; set; }

    public string? Email { get; set; }

    public string? TenDangNhap { get; set; }

    public string? MatKhau { get; set; }

    public string? Lop { get; set; }

    public int? IdKhoa { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public byte? GioiTinh { get; set; }

    public string? Sdt { get; set; }

    public int? ThuTuHienThi { get; set; }

    public byte? TonTai { get; set; }

    public int? IdVaiTro { get; set; }

    public virtual TbKhoa? IdKhoaNavigation { get; set; }

    public virtual TbVaiTro? IdVaiTroNavigation { get; set; }

    public virtual ICollection<TbKetQua> TbKetQuas { get; set; } = new List<TbKetQua>();
}
