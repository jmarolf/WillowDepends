using Microsoft.VisualStudio.Setup;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillowDepends.Lib;

namespace WillowDepends.UI.ViewModels
{
    public class WorkloadViewModel : ReactiveObject, IWorkloadViewModel
    {
        public WorkloadViewModel(string manifestPath)
        {
            var manifest = Manifest.FromManifestFile(manifestPath);
            Workloads = manifest.Workloads.CreateDerivedCollection(x => new WillowPackage(x, manifest));
        }

        public IReactiveDerivedList<WillowPackage> Workloads { get; }
    }
}
