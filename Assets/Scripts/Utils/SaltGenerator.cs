using System;
using System.Text;

class SaltGenerator
{
    private const int DEFAULT_SALT_SIZE = 40;
    private const int MAX_SALT_SIZE = 60;
    private const string input = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+./,";
    private int m_saltSize;

    private static SaltGenerator m_instance = null;

    private SaltGenerator(){}

    public static SaltGenerator instance()
    {
        if(m_instance == null)
        {
            m_instance = new SaltGenerator();
        }
        return m_instance;
    }

    public string generateSalt()
    {
        Random random = new Random();
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            result.Append(input[random.Next(input.Length)]);
        }
        return result.ToString();
    }
}
