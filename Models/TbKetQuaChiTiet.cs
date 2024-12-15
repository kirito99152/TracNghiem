using System;
using System.Collections.Generic;

namespace TracNghiem.Models;

public partial class TbKetQuaChiTiet
{
    public int IdKetQuaChiTiet { get; set; }

    public int? IdKetQua { get; set; }

    public int? IdCauHoi { get; set; }

    public string? DapAnChon { get; set; }

    public string? DapAnDienKhuyet { get; set; }

    public string? DapAnSxthuTu { get; set; }

    public bool? IsCorrect { get; set; }

    public int? ThuTuHienThi { get; set; }

    public byte? TonTai { get; set; }

    public virtual TbCauHoi? IdCauHoiNavigation { get; set; }

    public virtual TbKetQua? IdKetQuaNavigation { get; set; }
}
