using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Glamz.Business.Model
{
    public class AuthenticateRequestDto
    {
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? EditedOn { get; set; }
        public int? EditedBy { get; set; }
    }
}
