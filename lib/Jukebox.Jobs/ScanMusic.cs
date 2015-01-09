using System.Diagnostics;
using System.IO;
using Jukebox.Jobs.Extensions;

namespace Jukebox.Jobs
{
    public class ScanMusic
    {
        public void ScanFolder(string folderPath)
        {
            Directory.EnumerateFiles(folderPath, "*.mp3", SearchOption.AllDirectories).ForEach(path => Debug.WriteLine(path));
        }
    }
}
