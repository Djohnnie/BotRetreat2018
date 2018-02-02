﻿using System;
using System.IO;
using System.Threading.Tasks;
using BotRetreat2017.Wpf.Framework.Services.Interfaces;

namespace BotRetreat2017.Wpf.Framework.Services
{
    public class FileService : IFileService
    {
        public async Task SaveTextFile(String fileName, String contents)
        {
            using (var streamWriter = new StreamWriter(fileName))
            {
                await streamWriter.WriteAsync(contents);
            }
        }

        public async Task<String> LoadTextFile(String fileName)
        {
            using (var streamReader = new StreamReader(fileName))
            {
                return await streamReader.ReadToEndAsync();
            }
        }
    }
}