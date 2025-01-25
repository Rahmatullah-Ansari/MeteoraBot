using System.IO;

namespace MeteoraBot.Utilities
{
    public class DirectoryUtility
    {
        public static bool CreateDirectory(string Path, bool Overwrite = true)
        {
            try
            {
                if (Overwrite && Directory.Exists(Path))
                {
                    Directory.Delete(Path, true);
                }
                else
                {
                    if (!Directory.Exists(Path))
                        Directory.CreateDirectory(Path);
                }
                return true;
            }
            catch { return false; }
        }

    }
}
