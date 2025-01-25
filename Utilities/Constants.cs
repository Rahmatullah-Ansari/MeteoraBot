using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoraBot.Utilities
{
    public class Constants
    {
        public static string ApplicationName => "MeteoraBot";
        public static string InstallationFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ApplicationName);
        public static string PhantomExtensionPath => Path.Combine(InstallationFolder, "Phantom");
        public static string PhantomExtensionZip => Path.Combine(AppContext.BaseDirectory, "Phantom.zip");
    }
}
