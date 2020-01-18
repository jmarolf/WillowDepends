using Microsoft.VisualStudio.Setup;
using ReactiveUI;
using WillowDepends.Lib;

namespace WillowDepends.UI.ViewModels
{
    public interface IWorkloadViewModel
    {
        IReactiveDerivedList<WillowPackage> Workloads { get; }
    }
}
