using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace KB.MVCAuthentication.UI.Helpers.Security
{
    public class SHA512
    {
        public static string Hash(string strPass)
        {
            ConvertByte cb = new ConvertByte();
            if (strPass == "" || strPass == null)
            {
                throw new ArgumentNullException("Şifrelenecek veri yok.");
            }
            else
            {
                SHA512Managed sha512 = new SHA512Managed();
                byte[] aryPass = cb.ByteConverter(strPass);
                byte[] aryHash = sha512.ComputeHash(aryPass);
                return BitConverter.ToString(aryHash).Replace("-", "").ToLower();
            }
        }
    }
}