using OtpNet;

namespace Core.Security.OtpAuthenticator.OtpNet;

public class OtpNetOtpAuthenticatorHelper : IOtpAuthenticatorHelper
{
    public Task<byte[]> GenerateSecretKey()
    {
        var key = KeyGeneration.GenerateRandomKey(20);

        var base32String = Base32Encoding.ToString(key);
        var base32Bytes = Base32Encoding.ToBytes(base32String);

        return Task.FromResult(base32Bytes);
    }

    public Task<string> ConvertSecretKeyToString(byte[] secretKey)
    {
        var base32String = Base32Encoding.ToString(secretKey);
        return Task.FromResult(base32String);
    }

    public Task<bool> VerifyCode(byte[] secretKey, string code)
    {
        Totp totp = new(secretKey);

        var totpCode = totp.ComputeTotp(DateTime.UtcNow);

        var result = totpCode == code;

        return Task.FromResult(result);
    }
}