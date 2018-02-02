using System;

namespace BotRetreat2018.Wpf.Framework.Services.Interfaces
{
    public interface IFileExplorerService
    {
        String LoadFile(String filter);

        String SaveFile(String filter);
    }
}