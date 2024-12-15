using System;
using System.Collections.Generic;

namespace TracNghiem.Models;

public partial class TbKhoa
{
    public int IdKhoa { get; set; }

    public string? TenKhoa { get; set; }

    public virtual ICollection<TbMonHoc> TbMonHocs { get; set; } = new List<TbMonHoc>();

    public virtual ICollection<TbNguoiDung> TbNguoiDungs { get; set; } = new List<TbNguoiDung>();
}
