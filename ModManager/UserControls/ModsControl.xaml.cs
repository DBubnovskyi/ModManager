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
        public ModsControl()
        {
            InitializeComponent();
        }

        public void AddMods(List<Mod> mods)
        {
            Container.RowDefinitions.Add(new RowDefinition());

            ModControl control = new ModControl(true);
            Container.Children.Add(control);
            Grid.SetRow(control, Container.RowDefinitions.Count - 1);

            foreach (Mod mod in mods) 
            {
                Container.RowDefinitions.Add(new RowDefinition());

                control = new ModControl(mod);
                Container.Children.Add(control);
                Grid.SetRow(control, Container.RowDefinitions.Count - 1);
            }
        }
    }
}
