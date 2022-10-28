using OtpNet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer4.Shared.Totp
{

    //For more info refer to https://github.com/kspearrin/Otp.NET
    public class Otp : IOtp
    {
        private static readonly string base32Secret = "6L4OH6DDC4PLNQBA5422GM67KXRDIQQP";
        byte[] secret = null;
        private static OtpNet.Totp totp =null;

        public Otp()
        {
            secret = Base32Encoding.ToBytes(base32Secret);
            totp = new OtpNet.Totp(secret, step:60,  mode: OtpHashMode.Sha256, totpSize: 4);

        }
        public Task<string> GenerateOtp()
        {

            var t= Task.Run(() => totp.ComputeTotp(DateTime.UtcNow));

            //var code = totp.ComputeTotp();

            //return code;

            return t;
        }

        public Task<bool> ValidateOtp(string inputCode)
        {
            //var window = new VerificationWindow(previous: 1, future: 1);

            //bool valid = totp.VerifyTotp(inputCode, out long timeStepMatched, window);
            //return valid;

            var isValid = Task.Run(() => { 
                return totp.VerifyTotp(inputCode, out long timeStepMatched, VerificationWindow.RfcSpecifiedNetworkDelay); 
            });

            return isValid;

        }
    }
}
