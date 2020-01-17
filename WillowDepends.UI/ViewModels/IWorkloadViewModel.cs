using Microsoft.VisualStudio.Setup;
using ReactiveUI;

namespace WillowDepends.UI.ViewModels
{
    public interface IWorkloadViewModel
    {
        IReactiveDerivedList<WillowPackage> Workloads { get; }
    }
}