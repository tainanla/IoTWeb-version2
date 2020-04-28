using System;
using System.Security.Cryptography;
using System.Text;

namespace OneNETDataReceiver.Code
{
    public class ToolKit
    {
        public static string CRC16(string hexStrWithNoSplitChar)
        {
            int crc = 0xFFFF;
            for (int i = 0; i < hexStrWithNoSplitChar.Length; i += 2)
            {
                int a = Convert.ToInt32(hexStrWithNoSplitChar.Substring(i, 2), 16);
                crc ^= a;
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x01) == 1)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            StringBuilder sb = new StringBuilder(hexStrWithNoSplitChar);
            string s = Convert.ToString(crc >> 8 & 0xFF, 16);
            if (s.Length == 1)
            {
                s = "0" + s;
            }
            sb.Append(s);
            s = Convert.ToString(crc & 0xFF, 16);
            if (s.Length == 1)
            {
                s = "0" + s;
            }
            sb.Append(s);
            return sb.ToString().ToUpper();
        }


        public static string GetMD5String(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.Default.GetBytes(str);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < md5data.Length; i++)
            {
                builder.Append(md5data[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}