using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Setup;

namespace WillowDepends.Lib {
    public class WillowPackage {
        private readonly Manifest _manifest;
        private IPackage _package;

        public WillowPackage(IPackage package, Manifest manifest) {
            _package = package;
            _manifest = manifest;
        }

        public string Name => _package.Id;

        public IList<WillowPackage> Dependencies =>
            (_package.Dependencies as IEnumerable<KeyValuePair<string, Dependency>>)
                .Select(x => new WillowPackage(_manifest[x.Value.Id], _manifest))
                .ToList();

        public override string ToString() => Name;
    }
}
