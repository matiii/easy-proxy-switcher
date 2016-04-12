using System;
using System.Linq;

namespace EasyProxySwitcher
{
    class Program
    {
        private static string proxy= "proxy.hyper:3128";

        static void Main(string[] args)
        {
            var conf = new OverConfiguration();
            string over = conf.Overs();
            Console.WriteLine("Start switching");
            Console.WriteLine("Proxy: " + proxy);
            Console.WriteLine("Exceptions: ");
            conf.OversArray.ToList().ForEach(Console.WriteLine);

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
