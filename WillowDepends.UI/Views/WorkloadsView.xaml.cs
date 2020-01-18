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
using Microsoft.Win32;
using ReactiveUI;
using WillowDepends.Lib;
using WillowDepends.UI.ViewModels;

namespace WillowDepends.UI.Views {
    /// <summary>
    /// Interaction logic for WorkloadsView.xaml
    /// </summary>
    public partial class WorkloadsView : Page, IViewFor<IWorkloadViewModel> {
        public WorkloadsView() {
            InitializeComponent();
            string filePath = OpenManifestFile();
            ViewModel = new WorkloadViewModel(filePath);
            this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext);
        }

        private string OpenManifestFile() {
            var openFileDialog = new OpenFileDialog {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "All files (*.*)|*.*|manifest files (*.vsman)|*.vsman",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == true) {
                return openFileDialog.FileName;
            }

            return null;
        }

        public IWorkloadViewModel ViewModel { get; set; }

        object IViewFor.ViewModel {
            get { return ViewModel; }
            set { ViewModel = value as IWorkloadViewModel; }
        }

        private void CopyItem(object sender, ExecutedRoutedEventArgs e) {
            // Set text to clip board 
            TreeView tvi = (TreeView)sender;
            Clipboard.SetText(((WillowPackage)tvi.SelectedItem).ToString());
        }

        private void CanCopyItem(object sender, CanExecuteRoutedEventArgs e) {
            if (sender is TreeView treeView && treeView.SelectedItem is WillowPackage) {
                e.CanExecute = true;
            }
        }
    }
}
