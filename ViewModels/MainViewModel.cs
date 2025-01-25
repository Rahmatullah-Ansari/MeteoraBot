using MeteoraBot.PuppeteerBrowser;
using MeteoraBot.ThreadUtility;
using MeteoraBot.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeteoraBot.ViewModels
{
    public class MainViewModel:BindableBase
    {
        private static MainViewModel instance;
        public PuppeteerBrowserActivity browser = new PuppeteerBrowserActivity(isNeedResourceData:true);
        public static MainViewModel Instance => instance ?? (instance = new MainViewModel());
        public MainViewModel()
        {
            InitializeFolder();
            ConnectAccount = new BaseCommand<object>(sender => true, ConnectAccountExecute);
        }

        private void ConnectAccountExecute(object obj)
        {
            ThreadFactory.Instance.Start(async () =>
            {
                await browser.LaunchBrowserAsync(false, AccountUrl, true, new List<string> { Constants.PhantomExtensionPath}, ChromeprofilePath:ChromeProfile);
                await Delay(5);
            });
        }
        public async Task Delay(int delay)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
        }
        private string _url= "https://app.meteora.ag/";
        public string AccountUrl
        {
            get => _url;
            set => SetProperty(ref _url, value,nameof(AccountUrl));
        }
        private string _chromeProfile= $"C:\\Users\\User\\AppData\\Local\\Google\\Chrome\\UserData";
        public string ChromeProfile
        {
            get => _chromeProfile;
            set => SetProperty(ref _chromeProfile, value, nameof(ChromeProfile));
        }
        public ICommand ConnectAccount {  get; set; }
        private void InitializeFolder()
        {
            try
            {
                DirectoryUtility.CreateDirectory(Constants.InstallationFolder);
                DirectoryUtility.CreateDirectory(Constants.PhantomExtensionPath,false);
                ThreadFactory.Instance.Start(async () =>
                {
                    if (!Directory.Exists(Constants.PhantomExtensionPath)
                    || Directory.GetFiles(Constants.PhantomExtensionPath).Count()==0)
                    {
                        await Task.Run(() => System.IO.Compression.ZipFile.ExtractToDirectory(Constants.PhantomExtensionZip, Constants.PhantomExtensionPath));
                    }
                });
            }
            catch { }
        }
    }
}
