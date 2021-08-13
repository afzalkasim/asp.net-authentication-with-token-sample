using System;
using System.Collections.Generic;
using System.Text;

namespace Glamz.Business.Service
{
    public interface IEncryptionService
    {
        string GetKey();
        string EncryptString(string text, string keyString);
        string DecryptString(string cipherText, string keyString);
    }
}
