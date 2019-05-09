using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataConverter.Abstraction
{
    public interface IFileData
    {
        byte[] ToByteArray(string plainText);

        MemoryStream ToStream(string plainText);
    }
}
