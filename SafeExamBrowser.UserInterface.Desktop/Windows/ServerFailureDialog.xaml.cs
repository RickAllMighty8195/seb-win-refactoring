﻿/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Windows;
using SafeExamBrowser.I18n.Contracts;
using SafeExamBrowser.UserInterface.Contracts.Windows;
using SafeExamBrowser.UserInterface.Contracts.Windows.Data;

namespace SafeExamBrowser.UserInterface.Desktop.Windows
{
	public partial class ServerFailureDialog : Window, IServerFailureDialog
	{
		private readonly IText text;

		public ServerFailureDialog(string info, bool showFallback, IText text)
		{
			this.text = text;

			InitializeComponent();
			InitializeDialog(info, showFallback);
		}

		public ServerFailureDialogResult Show(IWindow parent = null)
		{
			return Dispatcher.Invoke(() =>
			{
				var result = new ServerFailureDialogResult { Success = false };

				if (parent is Window)
				{
					Owner = parent as Window;
					WindowStartupLocation = WindowStartupLocation.CenterOwner;
				}

				if (ShowDialog() is true)
				{
					result.Abort = Tag as string == nameof(AbortButton);
					result.Fallback = Tag as string == nameof(FallbackButton);
					result.Retry = Tag as string == nameof(RetryButton);
					result.Success = true;
				}
				else
				{
					result.Abort = true;
				}

				return result;
			});
		}

		private void InitializeDialog(string info, bool showFallback)
		{
			Info.Text = info;
			Message.Text = text.Get(TextKey.ServerFailureDialog_Message);
			Title = text.Get(TextKey.ServerFailureDialog_Title);

			AbortButton.Click += AbortButton_Click;
			AbortButton.Content = text.Get(TextKey.ServerFailureDialog_Abort);

			FallbackButton.Click += FallbackButton_Click;
			FallbackButton.Content = text.Get(TextKey.ServerFailureDialog_Fallback);
			FallbackButton.Visibility = showFallback ? Visibility.Visible : Visibility.Collapsed;

			Loaded += (o, args) => Activate();

			RetryButton.Click += RetryButton_Click;
			RetryButton.Content = text.Get(TextKey.ServerFailureDialog_Retry);
		}

		private void AbortButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Tag = nameof(AbortButton);
			Close();
		}

		private void FallbackButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Tag = nameof(FallbackButton);
			Close();
		}

		private void RetryButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Tag = nameof(RetryButton);
			Close();
		}
	}
}
