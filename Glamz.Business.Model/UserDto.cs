using System;
using System.Collections.Generic;
using System.Text;

namespace Glamz.Business.Model
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? EditedOn { get; set; }
        public int? EditedBy { get; set; }
        public string ErrorMessage { get; set; }
    }
}
