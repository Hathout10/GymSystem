using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL.Interfaces.Auth
{
    public interface IOtpService
    {
        public string GenerateOtp(string email);
        public bool IsValidOtp(string email, string otp);
    }
}
