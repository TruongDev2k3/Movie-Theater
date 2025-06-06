using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class CommentModel
    {
        public int CommentId { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TenTaiKhoan { get; set; }
    }
}


