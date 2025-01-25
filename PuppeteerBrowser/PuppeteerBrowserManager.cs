using PuppeteerSharp;

namespace MeteoraBot.PuppeteerBrowser
{
    public class PuppeteerBrowserManager : Dictionary<string, IBrowser>, IPuppeteerBrowserManager
    {
        public static PuppeteerBrowserManager Instance;
        public PuppeteerBrowserManager()
        {
            Instance = this;
        }
        public async Task<IBrowser> GetBrowserAsync(string KeyName)
        {
            try
            {
                var browser = Instance[KeyName];
                return browser != null ? browser : null;
            }
            catch { return null; }
        }

        public async Task<bool> RemoveAllBrowser()
        {
            var removed = true;
            try
            {
                foreach (var value in Instance.ToList())
                {
                    try
                    {
                        await RemoveBrowser(value.Key.ToString());
                    }
                    catch
                    {
                    }
                }
                Instance.Clear();
            }
            catch(Exception ex) { removed = false; }
            return removed;
        }

        public async Task<bool> RemoveBrowser(string KeyName)
        {
            var IsRemoved = true;
            try
            {
                Instance.TryGetValue(KeyName, out IBrowser currentBrowser);
                if (currentBrowser != null)
                {
                    if(!currentBrowser.IsClosed)
                    {
                        try
                        {
                            await currentBrowser.CloseAsync();
                        }
                        catch { }
                    }
                    Instance.Remove(KeyName);
                }
            }
            catch { IsRemoved = false; }
            return IsRemoved;
        }

        public async Task<bool> SaveBrowserAsync(string KeyName, IBrowser browser)
        {
            var IsSaved = true;
            try
            {
                Instance.TryGetValue(KeyName,out IBrowser browser1);
                if (browser1 != null)
                    Instance[KeyName] = browser;
                else
                    Instance.Add(KeyName, browser);
            }
            catch { IsSaved = false;}
            return IsSaved;
        }
    }
    public interface IPuppeteerBrowserManager
    {
        Task<IBrowser> GetBrowserAsync(string KeyName);
        Task<bool> SaveBrowserAsync(string KeyName, IBrowser browser);
        Task<bool> RemoveBrowser(string KeyName);
        Task<bool> RemoveAllBrowser();
    }
}
