﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace CoE.Ideas.Shared.Extensions
{
    public static class SecureStringExtensions
    {
        // from https://stackoverflow.com/questions/4502676/c-sharp-compare-two-securestrings-for-equality,
        // answer by Nikola Novak

        public static bool IsEqualTo(this SecureString ss1, SecureString ss2)
        {
            IntPtr bstr1 = IntPtr.Zero;
            IntPtr bstr2 = IntPtr.Zero;
            try
            {
                bstr1 = Marshal.SecureStringToBSTR(ss1);
                bstr2 = Marshal.SecureStringToBSTR(ss2);
                int length1 = Marshal.ReadInt32(bstr1, -4);
                int length2 = Marshal.ReadInt32(bstr2, -4);
                if (length1 == length2)
                {
                    for (int x = 0; x < length1; ++x)
                    {
                        byte b1 = Marshal.ReadByte(bstr1, x);
                        byte b2 = Marshal.ReadByte(bstr2, x);
                        if (b1 != b2) return false;
                    }
                }
                else return false;
                return true;
            }
            finally
            {
                if (bstr2 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr2);
                if (bstr1 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr1);
            }
        }

        // from https://stackoverflow.com/questions/9887996/how-to-convert-a-string-to-securestring-explicitly,
        // answer by Colin Gardner
        /// <summary>
        /// Returns a Secure string from the source string
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static SecureString ToSecureString(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return null;
            else
            {
                SecureString result = new SecureString();
                foreach (char c in source.ToCharArray())
                    result.AppendChar(c);
                return result;
            }
        }
    }
}
