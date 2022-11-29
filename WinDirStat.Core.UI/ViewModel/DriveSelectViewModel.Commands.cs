using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinDirStat.Core.UI.Model.Drives;
using WinDirStat.Core.UI.ViewModel.Commands;

namespace WinDirStat.Core.UI.ViewModel {
	partial class DriveSelectViewModel {
		
		public IRelayCommand OK => GetCommand(OnOK);

		public IRelayCommand SelectFolder => GetCommand(OnSelectFolder);

		private void OnOK() {
			// Apply the settings for future use
			Settings.DriveSelectMode = mode;
			Settings.SelectedDrives = SelectedDrives.Select(d => d.Name).ToArray();
			Settings.SelectedFolderPath = folderPath;
			Result = new DriveSelectResult(Scanning,
										   Settings.DriveSelectMode,
										   Settings.SelectedDrives,
										   Settings.SelectedFolderPath);
		}

		private void OnSelectFolder() {
			string newFolder = Dialogs.ShowFolderBrowser(WindowOwner, "WinDirStat.Core.UI - Select Folder", false);
			if (newFolder != null) {
				FolderPath = newFolder;
			}
		}

	}
}
