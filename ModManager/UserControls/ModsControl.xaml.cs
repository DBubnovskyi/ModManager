using ModsProcessor.Models;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ModManager.UserControls
{
    /// <summary>
    /// Interaction logic for ModsControl.xaml
    /// </summary>
    public partial class ModsControl : UserControl
    {
        private FolderControl _folderControl;
        public ModsControl()
        {
            InitializeComponent();
        }

        public void AddMods(List<Mod> mods, FolderControl folderControl)
        {
            _folderControl = folderControl;
            Container.RowDefinitions.Add(new RowDefinition());

            ModControl control = new ModControl(true);
            Container.Children.Add(control);
            Grid.SetRow(control, Container.RowDefinitions.Count - 1);

            foreach (Mod mod in mods) 
            {
                Container.RowDefinitions.Add(new RowDefinition());

                control = new ModControl(mod, _folderControl);
                Container.Children.Add(control);
                Grid.SetRow(control, Container.RowDefinitions.Count - 1);
            }
        }
    }
}
