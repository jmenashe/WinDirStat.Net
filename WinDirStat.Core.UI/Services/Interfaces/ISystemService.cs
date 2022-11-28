using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDirStat.Net.Services {
	public interface ISystemService {
		
		/// <summary>Opens the explorer to display the Computer.</summary>
		void ExploreComputer();
		/// <summary>Opens a folder in explorer.</summary>
		void ExploreFolder(string folderPath);
		/// <summary>Opens and selects a file in explorer.</summary>
		void ExploreFile(string filePath);
		/// <summary>Opens the properties window of a file.</summary>
		void OpenProperties(string filePath);
		/// <summary>Opens the command prompt or terminal.</summary>
		void OpenCommandLine(string directory);
		/// <summary>Opens PowerShell.</summary>
		void OpenPowerShell(string directory);
		
		/// <summary>Recycles the specified files.</summary>
		bool RecycleFiles(params string[] files);
		/// <summary>Recycles the specified directories.</summary>
		bool RecycleDirectories(params string[] directories);
		/// <summary>Empties the Recycle Bin.</summary>
		bool EmptyRecycleBin(string owner = null);
		/// <summary>Gets the Recycle Bin info.</summary>
		RecycleBinInfo GetRecycleBinInfo(string owner = null);
	}
}
