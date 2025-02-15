using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.App.Authentication.DTOs
{
    public class LoginResponseDTO
    {
        //public readonly static LoginResponseDTO Empty = new LoginResponseDTO();
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public DateTime? ExpiredOn { get; set; }
        public DateTime? RefreshTokenExpiredOn { get; set; }
    }
}
