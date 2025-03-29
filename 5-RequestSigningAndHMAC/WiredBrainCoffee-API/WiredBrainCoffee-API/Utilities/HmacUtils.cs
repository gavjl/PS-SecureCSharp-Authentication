using System.Security.Cryptography;
using System.Text;

namespace WiredBrainCoffee_API.Utilities
{
    public static class HmacUtils
    {

        public static bool VerifyHmac(string secretKey, string data, string providedSignature)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                var computedSignature = BitConverter.ToString(hash).Replace("-", "").ToLower();
                return computedSignature == providedSignature.ToLower();
            }
        }

        public static bool ValidNonce(Guid nonce)
        {
            //lookup the nonce in the database for previously used nonces
            //if it exists then reject this request
            return true;
        }

    }
}
