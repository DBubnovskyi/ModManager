using ModsProcessor.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ModManager.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ModControl : UserControl
    {
        private readonly Mod _mod;
        private readonly FolderControl _folderControl;

        public string TargetPath { 
            get => _folderControl.TargetPath.Text;
        }

        public ModControl(bool isTitle = false)
        {
            InitializeComponent();

            if (isTitle)
            {
                Name.Text = "Ім'я";
                SelfId.Text = "ID";
                Version.Text = "Версія";
                State.Text = "Стан";
                Info.Text = "Інформація";
                Dev.Text = "Розробник";
                OpenButton.Visibility = Visibility.Collapsed;
                InstallButton.Visibility = Visibility.Collapsed;
            }
        }

        public ModControl(Mod mod, FolderControl folderControl)
        {
            InitializeComponent();

            _folderControl = folderControl;

            _mod = mod;
            Name.Text = _mod.ModInfo.DisplayName;
            SelfId.Text = _mod.ModInfo.SelfId;
            Version.Text = _mod.ModInfo.Version;
            State.Text = _mod.ModInfo.DisplayName;
            Info.Text = _mod.ModInfo.Info;
            Dev.Text = _mod.ModInfo.DevName;
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(_mod.DirPath);
        }

        private void InstallButton_Click(object sender, RoutedEventArgs e)
        {
            _mod.Install(TargetPath);
        }
    }
}
