using System.Security.Cryptography;
using System.Text;
using UnityEngine;

class HashTool
{
    public static string EncodePassword(string password, string salt)
    {
        SHA512Managed crypt = new SHA512Managed();
        StringBuilder hash = new StringBuilder();
        string saltedPassword = salt + password;
        byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword), 0, Encoding.UTF8.GetByteCount(saltedPassword));
        foreach (byte bit in crypto)
        {
            hash.Append(bit.ToString("x2"));
        }
        return hash.ToString().ToLower();
    }
}
