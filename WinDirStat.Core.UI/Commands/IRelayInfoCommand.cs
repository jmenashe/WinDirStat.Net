using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using WinDirStat.Core.UI.ViewModel;
using WinDirStat.Core.UI.ViewModel.Commands;
using WinDirStat.Core.UI.Wpf.Input;

namespace WinDirStat.Core.UI.Wpf.Commands {
	public interface IRelayInfoCommand : IRelayCommandBase, INotifyPropertyChanged {

		#region Properties

		/// <summary>Gets or sets the UI specific info for the command.</summary>
		RelayInfo Info { get; set; }
		/// <summary>Gets the display text for the command.</summary>
		string Text { get; }
		/// <summary>Gets the display icon for the command.</summary>
		ImageSource Icon { get; }
		/// <summary>Gets the input gesture for the command.</summary>
		AnyKeyGesture InputGesture { get; }

		#endregion
	}
}
