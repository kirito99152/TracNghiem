using System;
using System.Collections.Generic;

namespace TracNghiem.Models;

public partial class TbVaiTro
{
    public int IdVaiTro { get; set; }

    public string? VaiTro { get; set; }

    public virtual ICollection<TbNguoiDung> TbNguoiDungs { get; set; } = new List<TbNguoiDung>();
}
