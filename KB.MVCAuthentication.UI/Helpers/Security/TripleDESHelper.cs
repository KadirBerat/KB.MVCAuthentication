using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Text;
using KB.MVCAuthentication.UI.Models;

namespace KB.MVCAuthentication.UI.Helpers.Security
{
    public class TripleDESHelper
    {
        private ConvertByte cb = new ConvertByte();
        private TripleDES tripleDES = TripleDES.Create();
        private MVCAuthenticationContext db = new MVCAuthenticationContext();
        private string CreateTripleDESKey()
        {
            tripleDES.GenerateKey();
            return cb.ByteToString(tripleDES.Key);
        }
        private string CreateTripleDESIV()
        {
            tripleDES.GenerateIV();
            return cb.ByteToString(tripleDES.IV);
        }
        public void SaveTripleDESData()
        {
            db.SessionSecurityKeys.Add(new SessionSecurityKey
            {
                TripleDESKey = CreateTripleDESKey(),
                TripleDesIV = CreateTripleDESIV()
            });
            db.SaveChanges();
        }
        public string TripleDESEncrypt(string strInput)
        {
            string output = "";

            if (strInput == "" || strInput == null)
            {
                throw new ArgumentNullException("No Input!");
            }
            else
            {
                byte[] aryKey = cb.Byte8(db.SessionSecurityKeys.FirstOrDefault().TripleDESKey);
                byte[] aryIV = cb.Byte8(db.SessionSecurityKeys.FirstOrDefault().TripleDesIV);
                TripleDESCryptoServiceProvider dec = new TripleDESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, dec.CreateEncryptor(aryKey, aryIV), CryptoStreamMode.Write);
                StreamWriter writer = new StreamWriter(cs);
                writer.Write(strInput);
                writer.Flush();
                cs.FlushFinalBlock();
                writer.Flush();
                output = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
                writer.Dispose();
                cs.Dispose();
                ms.Dispose();
            }

            return output;
        }
        public string TripleDESDecrypt(string strInput)
        {
            string strOutput = "";
            if (strInput == "" || strInput == null)
            {
                throw new ArgumentNullException("No Input!");
            }
            else
            {
                byte[] aryKey = cb.Byte8(db.SessionSecurityKeys.FirstOrDefault().TripleDESKey);
                byte[] aryIV = cb.Byte8(db.SessionSecurityKeys.FirstOrDefault().TripleDesIV);
                TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream(Convert.FromBase64String(strInput));
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(aryKey, aryIV), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cs);
                strOutput = reader.ReadToEnd();
                reader.Dispose();
                cs.Dispose();
                ms.Dispose();
            }
            return strOutput;
        }
    }
}