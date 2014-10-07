﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCore.Contract
{
    public interface ILocalStorage
    {
        Task<bool> FileExists(string fileName);
        Task<bool> Copy(string source, string destinationFolder, string newName, bool replace = true);

        Task<byte[]> Load(string fileName);
        Task<Stream> LoadStream(string fileName);
        Task<string> LoadString(string fileName);

        Task Save(string fileName, byte[] data);
        Task SaveStream(string fileName, Stream stream);
        Task<bool> SaveString(string fileName, string data);

        Task<bool> DeleteFile(string fileName);
        Task<bool> EnsureFolderExists(string folderPath);
        Task<List<string>> GetAllFilesInFolder(string folderPath);
        Task<bool> IsZero(string fileName);
    }
}
