using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WillowDepends.UI.ViewModels;

namespace WillowDepends.UI.Views
{
    /// <summary>
    /// Interaction logic for WorkloadsView.xaml
    /// </summary>
    public partial class WorkloadsView : Page, IViewFor<IWorkloadViewModel>
    {
        public WorkloadsView()
        {
            InitializeComponent();
            ViewModel = new WorkloadViewModel(@"\\cpvsbuild\drops\VS\d15.9\products\28107.00\VisualStudio.vsman");
            this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext);
        }

        public IWorkloadViewModel ViewModel { get; set; }

        object IViewFor.ViewModel {
            get { return ViewModel; }
            set { ViewModel = value as IWorkloadViewModel; }
        }
    }
}
