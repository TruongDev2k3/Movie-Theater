using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_NguyenVanTruong_
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int LoaiTaiKhoan { get; set; }
    }

    public class AppSettings
    {
        public string Secret { get; set; }

    }
}
