using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Glamz.Business.Model
{
    public class AuthenticationResponseDto
    {
        public string Token { get; set; }
        public bool Issuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
        public DateTime exp { get; set; }
        public string ErrorMessage { get; set; }

    }
}
