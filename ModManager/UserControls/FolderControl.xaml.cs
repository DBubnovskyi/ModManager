using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ModManager.UserControls
{
    /// <summary>
    /// Interaction logic for FolderControl.xaml
    /// </summary>
    public partial class FolderControl : UserControl
    {
        public FolderControl()
        {
            InitializeComponent();
            ModPath.Text = @"D:\Projects\Pet\ModManager\Mods";
        }

        //#fbb840
        //#f59e0f
    }
}
