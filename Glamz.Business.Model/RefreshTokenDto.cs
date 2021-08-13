using System;
using System.Collections.Generic;
using System.Text;

namespace Glamz.Business.Model
{
    public class RefreshTokenDto
    {
        public long Id { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiredOn { get; set; }
        public bool IsUsed { get; set; }
        public bool InValidated { get; set; }
        public int EmployeeId { get; set; }
        public string IpAddress { get; set; }
    }
}
