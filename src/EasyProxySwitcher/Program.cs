using System;

namespace EasyProxySwitcher
{
    class Program
    {
        private static string proxy= "proxy.host:port";
        private static string over = "don't use this sites;<local>";

        static void Main(string[] args)
        {
            Console.WriteLine("Start switching");
            Console.WriteLine("Proxy: " + proxy);
            Console.WriteLine("Wyjątki " + over);

            try
            {
                var switcher = new ProxySwitcher(proxy, over);
                switcher.Process();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR " + ex.Message);
            }

            Console.WriteLine("Switcher end.");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
