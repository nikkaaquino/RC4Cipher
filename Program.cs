using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;

class Program
{
    static void Main()
    {
        string plaintext = "RAEVA20";
        byte[] key = Encoding.UTF8.GetBytes("password");

        byte[] encrypted = RC4Encrypt(plaintext, key);
        Console.WriteLine("Encrypted: " + BitConverter.ToString(encrypted).Replace("-", ""));

        string decrypted = RC4Decrypt(encrypted, key);
        Console.WriteLine("Decrypted: " + decrypted);
    }

    static byte[] RC4Encrypt(string plaintext, byte[] key)
    {
        IStreamCipher cipher = new RC4Engine();
        cipher.Init(true, new KeyParameter(key));

        byte[] input = Encoding.UTF8.GetBytes(plaintext);
        byte[] output = new byte[input.Length];

        cipher.ProcessBytes(input, 0, input.Length, output, 0);

        return output;
    }

    static string RC4Decrypt(byte[] encrypted, byte[] key)
    {
        IStreamCipher cipher = new RC4Engine();
        cipher.Init(false, new KeyParameter(key));

        byte[] output = new byte[encrypted.Length];

        cipher.ProcessBytes(encrypted, 0, encrypted.Length, output, 0);

        return Encoding.UTF8.GetString(output);
    }
}
