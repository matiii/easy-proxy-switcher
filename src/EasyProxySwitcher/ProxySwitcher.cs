using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace EasyProxySwitcher
{
    class ProxySwitcher
    {
        private readonly string _proxyHost;
        private readonly string _proxyOverride;
        private const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        private const int INTERNET_OPTION_REFRESH = 37;

        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

        public ProxySwitcher(string proxyHost, string proxyOverride)
        {
            _proxyHost = proxyHost;
            _proxyOverride = proxyOverride;
        }

        public void Process()
        {
            using (var registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true))
            {
                registry.DeleteValue("AutoConfigURL", false);
                registry.SetValue("ProxyEnable", 1);
                registry.SetValue("ProxyServer", _proxyHost);
                registry.SetValue("ProxyOverride", _proxyOverride);
            }

            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }
    }
}
