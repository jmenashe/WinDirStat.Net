using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDirStat.Core.UI.Utils {
	public static class DelayedGC {

		public static DelayedAction CollectIn(TimeSpan delay) {
			return new DelayedAction(delay, GC.Collect);
		}
	}
}
