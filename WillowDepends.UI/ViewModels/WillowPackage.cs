using Microsoft.VisualStudio.Setup;
using System.Collections.Generic;
using System.Linq;
using WillowDepends.Lib;

namespace WillowDepends.UI.ViewModels
{
    public class WillowPackage
    {
        private Manifest manifest;
        private IPackage package;

        public WillowPackage(IPackage package, Manifest manifest)
        {
            this.package = package;
            this.manifest = manifest;
        }

        public string Name => package.Id;

        public IList<WillowPackage> Dependencies =>
            (package.Dependencies as IEnumerable<KeyValuePair<string, Dependency>>)
                .Select(x => new WillowPackage(manifest[x.Value.Id], manifest))
                .ToList();
    }
}