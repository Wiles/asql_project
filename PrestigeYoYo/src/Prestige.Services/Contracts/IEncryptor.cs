/// String encryptor contract
/// Codeora 2012
///

namespace Prestige.Services
{
    public interface IEncryptor
    {
        string GetHash(string str);
    }
}
