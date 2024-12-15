using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TracNghiem.PageModels
{
    public class SecurityModel
    {
        public int id{ get; set; }
        public string username{ get; set; }

        [DataType(DataType.Password)]
        public string? current_password{ get; set; }
        
        [DataType(DataType.Password)]
        public string? password{ get; set; }
        
        [DataType(DataType.Password)]
        public string? confirm_password{ get; set; }
        
        [DataType(DataType.Password)]
        public string? delete_password {get; set;} 
    }
}