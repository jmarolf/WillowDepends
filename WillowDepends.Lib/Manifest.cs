using Microsoft.VisualStudio.Setup;
using Microsoft.VisualStudio.Setup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillowDepends.Lib
{
    public class Manifest
    {
        private readonly Dictionary<string, IPackage> dictionary;

        public Manifest(Dictionary<string, IPackage> dictionary)
        {
            this.dictionary = dictionary;
        }

        public IPackage this[string key] => dictionary[key.ToLower()];

        public static Manifest FromManifestFile(string pathToManifestFile)
        {
            var services = new ServiceProvider();
            services.AddService(FileSystem.Default); // Add as extended for our code
            var catalog = Catalog.Parse(services, pathToManifestFile);
            var dictionary = new Dictionary<string, IPackage>();
            foreach (var package in catalog.Packages)
            {
                if (!dictionary.ContainsKey(package.Id.ToLower()))
                {
                    dictionary.Add(package.Id.ToLower(), package);
                }
            }

            return new Manifest(dictionary);
        }

        public IEnumerable<IPackage> Workloads =>
            dictionary.Values.Where(x => x.Type == PackageType.Workload);
    }
}
