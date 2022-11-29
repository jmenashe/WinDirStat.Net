﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using TriggersTools.SharpUtils.Collections;
using WinDirStat.Core.UI.Model.Files;
using WinDirStat.Core.UI.Services;
using WinDirStat.Core.UI.Structures;

#if DOUBLE
using Number = System.Double;
#else
using Number = System.Single;
#endif

namespace WinDirStat.Core.UI.Rendering {
	unsafe partial class TreemapRenderer {

		public void HighlightItems(WriteableBitmap bitmap, Rectangle2I rc, Rgba32Color color, IEnumerable<ITreemapItem> items) {
			if (rc.Width <= 0 || rc.Height <= 0)
				return;

			renderArea = rc;

			InitPixels(rc, Rgba32Color.Transparent);

			fixed (Rgba32Color* pBitmapBits = pixels) {

				foreach (FileItemBase item in items)
					HighlightRectangle(pBitmapBits, item.Rectangle, color);

				IntPtr bitmapBitsPtr = (IntPtr) pBitmapBits;
				
				ui.Invoke(() => {
					bitmap.WritePixels((Int32Rect) rc, bitmapBitsPtr, rc.Width * rc.Height * 4, bitmap.BackBufferStride);
				});
			}
		}

		public void HighlightExtensions(WriteableBitmap bitmap, Rectangle2I rc, FileItemBase root, Rgba32Color color, string extension) {
			if (rc.Width <= 0 || rc.Height <= 0)
				return;

			renderArea = rc;
			
			InitPixels(rc, Rgba32Color.Transparent);

			fixed (Rgba32Color* pBitmapBits = pixels) {
				
				RecurseHighlightExtensions(pBitmapBits, root, color, extension);

				IntPtr bitmapBitsPtr = (IntPtr) pBitmapBits;

				ui.Invoke(() => {
					bitmap.WritePixels((Int32Rect) rc, bitmapBitsPtr, rc.Width * rc.Height * 4, bitmap.BackBufferStride);
				});
			}
		}

		private void RecurseHighlightExtensions(Rgba32Color* bitmap, FileItemBase parent, Rgba32Color color, string extension) {
			List<FileItemBase> children = parent.Children;
			int count = children.Count;
			for (int i = 0; i < count; i++) {
				FileItemBase child = children[i];
				Rectangle2S rc = child.Rectangle;
				if (rc.Width > 0 && rc.Height > 0) {
					if (child.IsLeaf) {
						if (child.Extension == extension)
							HighlightRectangle(bitmap, rc, color);
					}
					else {
						RecurseHighlightExtensions(bitmap, child, color, extension);
					}
				}
			}
		}
		
		private void HighlightRectangle(Rgba32Color* bitmap, Rectangle2I rc, Rgba32Color color) {
			if (rc.Width >= 7 && rc.Height >= 7) {
				FillRectangle(bitmap, Rectangle2I.FromLTRB(rc.Left, rc.Top, rc.Right, rc.Top + 3), color);
				FillRectangle(bitmap, Rectangle2I.FromLTRB(rc.Left, rc.Bottom - 3, rc.Right, rc.Bottom), color);
				FillRectangle(bitmap, Rectangle2I.FromLTRB(rc.Left, rc.Top + 3, rc.Left + 3, rc.Bottom - 3), color);
				FillRectangle(bitmap, Rectangle2I.FromLTRB(rc.Right - 3, rc.Top + 3, rc.Right, rc.Bottom - 3), color);
			}
			else if (rc.Width == 1 && rc.Height == 1) {
				bitmap[rc.Left + rc.Top * renderArea.Width] = color;
			}
			else if (rc.Width > 0 && rc.Height > 0) {
				FillRectangle(bitmap, rc, color);
			}
		}

		private void FillRectangle(Rgba32Color* bitmap, Rectangle2I rc, Rgba32Color color) {
			int bottom = rc.Bottom;
			int right = rc.Right;
			if (rc.Width >= rc.Height) {
				for (int iy = rc.Top; iy < bottom; iy++) {
					for (int ix = rc.Left; ix < right; ix++) {
						bitmap[ix + iy * renderArea.Width] = color;
					}
				}
			}
			else {
				for (int ix = rc.Left; ix < right; ix++) {
					for (int iy = rc.Top; iy < bottom; iy++) {
						bitmap[ix + iy * renderArea.Width] = color;
					}
				}
			}
		}
	}
}
