using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace KB.MVCAuthentication.UI.Helpers.Security
{
    public class ConvertByte
    {
        private UnicodeEncoding ByteConverterUE = new UnicodeEncoding();

        public byte[] ByteConverter(string value)
        {
            return ByteConverterUE.GetBytes(value);
        }

        public byte[] Byte8(string value)
        {
            char[] arrayChar = value.ToCharArray();
            byte[] arrayByte = new byte[arrayChar.Length];
            for (int i = 0; i < arrayByte.Length; i++)
            {
                arrayByte[i] = Convert.ToByte(arrayChar[i]);
            }
            return arrayByte;
        }

        public string ByteToString(byte[] value)
        {
            return ByteConverterUE.GetString(value);
        }
    }
}