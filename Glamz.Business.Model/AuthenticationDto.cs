using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Glamz.Business.Model
{
    public class AuthenticationDto
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
