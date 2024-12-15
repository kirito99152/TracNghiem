using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TracNghiem.PageModels
{
    public class LoginModel
    {
        [RegularExpression(@"^[\w]{1,20}$", ErrorMessage = "Chứa ít nhấ 1 ký tự, nhiều nhất 20 ký tự bao gồm các ký tự latin")]
        public string username{ get; set; }
        [DataType(DataType.Password)]
        [RegularExpression(@"^[\w!@#$%\^&*()_?+\-=]{8,30}$", ErrorMessage = "Mật khẩu phải gồm ít nhất 8 ký tự và nhiều nhất 30 ký tự bao gồm các ký tự từ latin, số và các ký tự đặc biệt !@#$%^&*()_?+-=")]
        public string password{ get; set; }
        public string? rememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}