using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Setup;
using WillowDepends.Lib;
using static System.Console;

namespace WillowDepends.Console {
    class Program {
        static void Main(string[] args) {
            PrintPackageDependencies(args[0]);
        }

        static void PrintPackageDependencies(string pathToManifest, string workloadId = null) {
            var manifest = Manifest.FromManifestFile(pathToManifest);

            if (workloadId != null) {
                var workload = manifest[workloadId];
                PrintWorkload(manifest, workload);
            } else {
                foreach (var workload in manifest.Workloads) {
                    PrintWorkload(manifest, workload.Value);
                }
            }
        }

        private static void PrintWorkload(Manifest manifest, IPackage workload) {
            var alreadySeenPackages = new List<IPackage> { workload };
            WriteLine(workload.Id);
            foreach (var dependency in workload.Dependencies) {
                PrintPackage(manifest[dependency.Id], indentLevel: 1);
            }

            void PrintPackage(IPackage package, int indentLevel) {
                if (alreadySeenPackages.Contains(package)) {
                    return;
                }

                alreadySeenPackages.Add(package);
                for (int i = 0; i < (indentLevel * 2); i++) {
                    Write(" ");
                }
                Write(package.Id);
                Write(Environment.NewLine);
                foreach (var dependency in package.Dependencies) {
                    PrintPackage(manifest[dependency.Id], indentLevel + 1);
                }
            }
        }
    }
}
