using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Setup;
using WillowDepends.Lib;
using static System.Console;

namespace WillowDepends.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var manifest = Manifest.FromManifestFile(@"\\cpvsbuild\Drops\VS\d15.6\products\27412.00\VisualStudio.vsman");
            var workload = manifest["Microsoft.VisualStudio.Workload.NetWeb"];
            PrintPackageDependencies(manifest, workload);
        }

        static void PrintPackageDependencies(Manifest manifest, IPackage workload)
        {
            var alreadySeenPackages = new List<string>();
            void PringPackage(IPackage package, int v)
            {
                if (alreadySeenPackages.Contains(package.Id.ToLower()))
                {
                    return;
                }

                alreadySeenPackages.Add(package.Id.ToLower());
                for (int i = 0; i < v; i++)
                {
                    Write(" ");
                }
                Write(package.Id);
                Write(Environment.NewLine);
                foreach (var dependency in package.Dependencies)
                {
                    PringPackage(manifest[dependency.Id], v + 2);
                }
            }

            alreadySeenPackages.Add(workload.Id.ToLower());
            WriteLine(workload.Id);
            foreach (var dependency in workload.Dependencies)
            {
                PringPackage(manifest[dependency.Id], 4);
            }
        }
    }
}
