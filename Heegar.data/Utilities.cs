using System;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Xml;
using System.Net;

namespace Heegar.data
{
    public static class Utilities
    {
        private const int BlockSize = 128;
        private const string ConVal = "ME#";
        private const string CryptKeyHash = "X+RflqBmLGd6mFaLh8Hi1D13vK2+h5+Daa2zSFr7CGNNR9PtArRPtUdV1n4SJ5RBaXP2KZALDBK74k1Nuw+VPg==";
        private static readonly byte[] InitializationVector = new byte[16];
        private static readonly CipherMode CipherMode = CipherMode.CBC;
        private const PaddingMode Pad = PaddingMode.PKCS7;

        /// <summary>
        /// Return an XML Serialized string of the object.  Object must have a class attribute of DataContract
        /// </summary>
        /// <typeparam name="T">Any class type decorated with DataContract</typeparam>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
        private static string DataContractSerialize<T>(this T objectToSerialize)
        {
            var serializer = new DataContractSerializer(typeof(T));
            string xmlString;
            using (var sw = new StringWriter())
            {
                using (var writer = new XmlTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented; // indent the Xml so it's human readable
                    serializer.WriteObject(writer, objectToSerialize);
                    writer.Flush();
                    xmlString = sw.ToString();

                    return xmlString;
                }
            }
        }
        public static string Encrypt(string value, byte[] key = null)
        {
            return Heegar_Encrypt(value, key);
        }
        public static byte[] Encrypt(byte[] value, byte[] key = null)
        {
            return Heegar_Encrypt(value, key);
        }

        public static string Decrypt(string value, byte[] key = null)
        {
            return Heegar_Decrypt(value, key);
        }

        public static byte[] Decrypt(byte[] value, byte[] key = null)
        {
            return Heegar_Decrypt(value, key);
        }

        internal static byte[] GetKey(string keyValue, bool isSecure = true)
        {
            var enc = Encoding.GetEncoding(1252);
            if (isSecure)
            {
                using (var sha1 = new SHA1Managed())
                {

                    using (var stream = new MemoryStream(enc.GetBytes(keyValue)))
                    {
                        var hash = sha1.ComputeHash(stream);
                        keyValue = hash.ToHex();

                        using (var stream2 = new MemoryStream(enc.GetBytes(keyValue)))
                        {
                            var hash2 = sha1.ComputeHash(stream2);

                            keyValue = hash2.ToHex();

                        }
                    }
                }
            }
            var asciiBytes = Encoding.ASCII.GetBytes(keyValue);

            using (var md5Hash = MD5.Create())
            {
                var hash = md5Hash.ComputeHash(asciiBytes);
                return hash;
            }
        }

        internal static string Heegar_Decrypt(string textToDecrypt, byte[] keyValue = null)
        {
            if (string.IsNullOrWhiteSpace(textToDecrypt))
                return string.Empty;

            var encryptedBytes = Convert.FromBase64String(textToDecrypt);
            var decryptedBytes = Heegar_Decrypt(encryptedBytes, keyValue);
            var output = Encoding.Unicode.GetString(decryptedBytes);

            return output;
        }

        internal static byte[] Heegar_Decrypt(byte[] encryptedBytes, byte[] keyValue = null)
        {
            var secret = Heegar_DecryptSecureItem(CryptKeyHash);
            var key = keyValue ?? GetKey(secret, false);

            using (var rijAlg = Rijndael.Create())
            {
                const int byteBlockSize = 128;

                rijAlg.BlockSize = byteBlockSize;
                rijAlg.Key = key;
                rijAlg.IV = InitializationVector;
                rijAlg.Mode = CipherMode;
                rijAlg.Padding = Pad;

                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);


                using (var msDecrypt = new MemoryStream(encryptedBytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var memo = new MemoryStream())
                        {
                            csDecrypt.CopyTo(memo);

                            var decryptedBytes = memo.ToArray();
                            return decryptedBytes;
                        }
                    }
                }
            }
        }

        internal static string Heegar_DecryptSecureItem(string textToDecrypt, byte[] keyValue = null)
        {
            string output;
            var loops = 46;
            var sup1 = "Ac";
            var key = GetKey(ConVal + loops + sup1 + "#dd3ASD");

            using (var rijAlg = Rijndael.Create())
            {
                var encryptedBytes = Convert.FromBase64String(CryptKeyHash);

                rijAlg.BlockSize = BlockSize;
                rijAlg.Key = key;
                rijAlg.IV = InitializationVector;
                rijAlg.Mode = CipherMode;
                rijAlg.Padding = Pad;

                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);


                using (var msDecrypt = new MemoryStream(encryptedBytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var memo = new MemoryStream())
                        {
                            csDecrypt.CopyTo(memo);

                            var b = memo.ToArray();

                            output = Encoding.Unicode.GetString(b);
                        }
                    }
                }
            }

            return output;
        }

        internal static string Heegar_Encrypt(string textToEncrypt, byte[] keyValue = null)
        {
            if (string.IsNullOrWhiteSpace(textToEncrypt))
                return string.Empty;


            var plainTextBytes = Encoding.Unicode.GetBytes(textToEncrypt);

            var cipherTextBytes = Heegar_Encrypt(plainTextBytes, keyValue);

            var cipherText = Convert.ToBase64String(cipherTextBytes);

            return cipherText;
        }

        internal static byte[] Heegar_Encrypt(byte[] plainTextBytes, byte[] keyValue = null)
        {
            var secret = Heegar_DecryptSecureItem(CryptKeyHash);

            var key = keyValue ?? GetKey(secret, false);

            using (var rijAlg = Rijndael.Create())
            {
                rijAlg.BlockSize = BlockSize;
                rijAlg.Key = key;
                rijAlg.IV = InitializationVector;
                rijAlg.Mode = CipherMode;
                rijAlg.Padding = Pad;

                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
                using (var msDecrypt = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(msDecrypt, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        var cipherTextBytes = msDecrypt.ToArray();

                        return cipherTextBytes;
                    }
                }

            }
        }
        /// <summary>
        /// Returns the Sha1 Hash for the contents of the file supplied.  Returns null if there is no file.  Returns null if there is an error.
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <returns></returns>
        public static string GetSHA1HashForFile(string fullFilePath)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(fullFilePath))
                    return null;

                return !File.Exists(fullFilePath) ? null : GetSHA1Hash(File.ReadAllBytes(fullFilePath));
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the Sha1 Hash for the string supplied.  Returns null if the string is empty or null, or if there was an error.
        /// </summary>
        /// <param name="stringToHash"></param>
        /// <returns></returns>
        public static string GetSHA1HashForString(string stringToHash)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(stringToHash))
                    return null;
                return GetSHA1Hash(Encoding.UTF8.GetBytes(stringToHash));
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the Hex value of the hash of the bytes sent in.  Primary use is for File Hahses, hence the parameter name.
        /// </summary>
        /// <param name="bytesToHash"></param>
        /// <returns></returns>
        public static string GetSHA1Hash(byte[] bytesToHash)
        {
            try
            {

                using (var sha1 = new SHA1Managed())
                {
                    using (var stream = new MemoryStream(bytesToHash))
                    {
                        var hash = sha1.ComputeHash(stream);
                        return hash.ToHex();
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal static bool IsValidBlockSize(byte[] keyValue = null)
        {
            var length = keyValue?.Length * 8 ?? 0;
            return length == 0 || length >= 128 && length <= 256 && length % 32 == 0;
        }

        /// <summary>
        /// Get's a new GUID.  Pass in a string to get a specific format.  Default is no formatting
        /// </summary>
        /// <param name="formatString">N = no formatting.   D = Dashes.   B = Dashes with Braces around ends.   P = Dashes with Parentheses around ends.   X = weird.</param>
        /// <returns></returns>
        internal static string GetAGUID(string formatString = "N")
        {
            return System.Guid.NewGuid().ToString(formatString).ToUpper();
        }
        #region Unused

        public static string EncryptString(this string value, byte[] key = null)
        {
            return Heegar_Encrypt(value, key);
        }

        public static string DecryptString(this string value, byte[] key = null)
        {
            return Heegar_Decrypt(value, key);
        }

        public static byte[] EncryptBytes(this byte[] value, byte[] key = null)
        {
            return Heegar_Encrypt(value, key);
        }

        public static byte[] DecryptBytes(this byte[] value, byte[] key = null)
        {
            return Heegar_Decrypt(value, key);
        }

        public static byte[] CompressData(byte[] dataToCompress)
        {
            using (var compressStream = new MemoryStream())
            {
                using (var compressor = new DeflateStream(compressStream, CompressionMode.Compress, true))
                {
                    compressor.Write(dataToCompress, 0, dataToCompress.Length);
                    compressor.Close();
                    dataToCompress = compressStream.ToArray();
                }
            }
            return dataToCompress;
        }

        public static byte[] DecompressData(byte[] dataToDecompress)
        {

            var output = new MemoryStream();
            using (var stream = new MemoryStream(dataToDecompress))
            {
                using (var decompress = new DeflateStream(stream, CompressionMode.Decompress))
                {
                    decompress.CopyTo(output);
                }
            }
            output.Position = 0;
            dataToDecompress = output.ToArray();
            output.Close();
            output.Dispose();


            return dataToDecompress;

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToHex(this byte[] data)
        {
            return ToHex(data, "");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string ToHex(this byte[] data, string prefix)
        {
            char[] lookup = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            int i = 0, p = prefix.Length, l = data.Length;
            char[] c = new char[l * 2 + p];
            byte d;
            for (; i < p; ++i) c[i] = prefix[i];
            i = -1;
            --l;
            --p;
            while (i < l)
            {
                d = data[++i];
                c[++p] = lookup[d >> 4];
                c[++p] = lookup[d & 0xF];
            }
            return new string(c, 0, c.Length);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] FromHex(this string str)
        {
            return FromHex(str, 0, 0, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="offset"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static byte[] FromHex(this string str, int offset, int step)
        {
            return FromHex(str, offset, step, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="offset"></param>
        /// <param name="step"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static byte[] FromHex(this string str, int offset, int step, int tail)
        {
            byte[] b = new byte[(str.Length - offset - tail + step) / (2 + step)];
            byte c1, c2;
            int l = str.Length - tail;
            int s = step + 1;
            for (int y = 0, x = offset; x < l; ++y, x += s)
            {
                c1 = (byte)str[x];
                if (c1 > 0x60) c1 -= 0x57;
                else if (c1 > 0x40) c1 -= 0x37;
                else c1 -= 0x30;
                c2 = (byte)str[++x];
                if (c2 > 0x60) c2 -= 0x57;
                else if (c2 > 0x40) c2 -= 0x37;
                else c2 -= 0x30;
                b[y] = (byte)((c1 << 4) + c2);
            }

            return b;
        }

        public static string SendURLAndGetResponse(string url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            string result = "Error";
            using (WebResponse myResponse = myRequest.GetResponse())
            {
                using (StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    result = sr.ReadToEnd();
                    sr.Close();
                    myResponse.Close();
                }
            }
            return result;
        }

        private static readonly byte[] machineTest = { 9, 82, 132, 56, 112, 73, 203, 255, 3, 208, 92, 8, 61, 14, 201, 59 };

        private static int GetIntForString(string value)
        {

            int i = 0;
            try
            {
                foreach (char c in value.ToCharArray())
                {
                    i += (int)c;
                }
            }
            catch { }
            return i;
        }


        private static byte[] GetCustomKey(string value, bool reverse = false)
        {
            byte[] temp = BitConverter.GetBytes(GetIntForString(value));

            byte[] enc = new byte[16];
            machineTest.CopyTo(enc, 0);
            if (!reverse)
            {
                for (int i = 15; i > 15 - temp.Length; i--)
                {
                    enc[i] = temp[15 - i];
                }
            }
            else
            {
                int ndex = 0;
                for (int i = 15; i > 15 - temp.Length; i--)
                {
                    enc[ndex++] = temp[15 - i];
                }
            }
            return enc;
        }

        #endregion
    }
}
