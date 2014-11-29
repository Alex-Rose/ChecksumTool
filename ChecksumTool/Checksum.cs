using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChecksumTool
{
    public enum HashAlgorithm
    {
        SHA1 = 0,
        MD5,
    }

    public static class Checksum
    {
        public static string GetChecksum(HashAlgorithm algo, String[] files)
        {
            byte[] hash = new byte[]{};
            switch(algo)
            {
                case HashAlgorithm.SHA1:
                    hash = ComputeSha1(files);
                    break;
                case HashAlgorithm.MD5:

                    break;
            }

            var strHash = BitConverter.ToString(hash);
            return strHash.Replace("-", "").ToLower();
        }

        private static byte[] ComputeSha1(String[] files)
        {
            var file = files[0];
            file = file.Trim('"');
            var buffer = File.ReadAllBytes(file);
            var hash = SHA1.Create().ComputeHash(buffer);

            return hash;
        }
    }
}
