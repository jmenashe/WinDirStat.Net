﻿// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using System.Windows;
using WinDirStat.Core.UI.ViewModel.Files;

namespace WinDirStat.Core.UI.Wpf.Controls.FileList {
	class EditTextBox : TextBox {
		static EditTextBox() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(EditTextBox),
				new FrameworkPropertyMetadata(typeof(EditTextBox)));
		}

		public EditTextBox() {
			Loaded += delegate { Init(); };
		}

		public FileTreeViewItem Item { get; set; }

		public FileItemViewModel Node {
			get { return Item.Node; }
		}

		void Init() {
			Text = Node.LoadEditText();
			Focus();
			SelectAll();
		}

		protected override void OnKeyDown(KeyEventArgs e) {
			if (e.Key == Key.Enter) {
				Commit();
			}
			else if (e.Key == Key.Escape) {
				Node.IsEditing = false;
			}
		}

		protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e) {
			if (Node.IsEditing) {
				Commit();
			}
		}

		bool commiting;

		void Commit() {
			if (!commiting) {
				commiting = true;

				Node.IsEditing = false;
				if (!Node.SaveEditText(Text)) {
					Item.Focus();
				}
				//Node.RaisePropertyChanged("Text");

				//if (Node.SaveEditText(Text)) {
				//    Node.IsEditing = false;
				//    Node.RaisePropertyChanged("Text");
				//}
				//else {
				//    Init();
				//}

				commiting = false;
			}
		}
	}
}
