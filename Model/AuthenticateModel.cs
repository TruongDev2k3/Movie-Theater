using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class AuthenticateModel
    {    
        public string Username { get; set; }    
        public string Password { get; set; }
        public int LoaiTaiKhoan { get; set; }
    }

    public class AppSettings
    {
        public string Secret { get; set; }

    }
}
