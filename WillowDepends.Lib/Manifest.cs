using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Setup;
using Microsoft.VisualStudio.Setup.Services;

namespace WillowDepends.Lib {
    public class Manifest {
        private readonly Dictionary<string, IPackage> _packages;
        private Manifest(Dictionary<string, IPackage> dictionary) {
            this._packages = dictionary;
        }

        public IPackage this[string key] => _packages[key.ToLower()];

        public static Manifest FromManifestFile(string pathToManifestFile) {
            var services = new ServiceProvider();
            services.AddService(FileSystem.Default); // Add as extended for our code
            var catalog = Catalog.Parse(services, pathToManifestFile);
            var dictionary = new Dictionary<string, IPackage>();
            foreach (var package in catalog.Packages) {
                if (!dictionary.ContainsKey(package.Id.ToLower())) {
                    dictionary.Add(package.Id.ToLower(), package);
                }
            }

            return new Manifest(dictionary);
        }

        public IDictionary<string, IPackage> Workloads
            => _packages.Values.Where(x => x.Type == PackageType.Workload)
                .Distinct()
                .ToDictionary(p => p.Id.ToLower());

        public IDictionary<string, IPackage> Component
            => _packages.Values.Where(x => x.Type == PackageType.Component)
                .Distinct()
                .ToDictionary(p => p.Id.ToLower());

        public IDictionary<string, IPackage> PackageGroup
            => _packages.Values.Where(x => x.Type == PackageType.Group)
                .Distinct()
                .ToDictionary(p => p.Id.ToLower());
    }
}
