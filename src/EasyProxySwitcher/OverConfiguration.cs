using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace EasyProxySwitcher
{
    class OverConfiguration
    {
        private string[] overs;
        private bool isLocal;
        private const string overLocal = "<local>";
        private const string confPath = "over.config";

        public string[] OversArray => overs;

        public string Overs()
        {
            if (overs != null)
                return Join();

            var list = GetOvers().ToList();

            if (isLocal)
                list.Add(overLocal);

            overs = list.ToArray();

            return Join();
        }

        private IEnumerable<string> GetOvers()
        {
            var xml = XDocument.Load(confPath);
            var oversRoot = xml.Descendants("overs").First();
            var isLocalAttribute = oversRoot.Attribute("isLocal");

            if (isLocalAttribute?.Value != null)
                isLocal = bool.Parse(isLocalAttribute.Value);

            return oversRoot.Descendants().Select(x => x.Value);
        }


        private string Join() => String.Join(";", overs);
    }
}
