﻿/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using CefSharp;
using CefSharp.Enums;
using CefSharp.WinForms;
using SafeExamBrowser.Browser.Handlers;
using SafeExamBrowser.Browser.Wrapper.Events;
using SafeExamBrowser.Browser.Wrapper.Handlers;

namespace SafeExamBrowser.Browser.Wrapper
{
	internal class CefSharpBrowserControl : ChromiumWebBrowser, ICefSharpControl
	{
		public event AuthCredentialsEventHandler AuthCredentialsRequired;
		public event BeforeBrowseEventHandler BeforeBrowse;
		public event BeforeDownloadEventHandler BeforeDownload;
		public event BeforeUnloadDialogEventHandler BeforeUnloadDialog;
		public event CanDownloadEventHandler CanDownload;
		public event ContextCreatedEventHandler ContextCreated;
		public event ContextReleasedEventHandler ContextReleased;
		public event DialogClosedEventHandler DialogClosed;
		public event DownloadUpdatedEventHandler DownloadUpdated;
		public event DragEnterEventHandler DragEnterCefSharp;
		public event DraggableRegionsChangedEventHandler DraggableRegionsChanged;
		public event FaviconUrlChangedEventHandler FaviconUrlChanged;
		public event FileDialogRequestedEventHandler FileDialogRequested;
		public event FocusedNodeChangedEventHandler FocusedNodeChanged;
		public event GotFocusEventHandler GotFocusCefSharp;
		public event JavaScriptDialogEventHandler JavaScriptDialog;
		public event KeyEventHandler KeyEvent;
		public event LoadingProgressChangedEventHandler LoadingProgressChanged;
		public event OpenUrlFromTabEventHandler OpenUrlFromTab;
		public event PreKeyEventHandler PreKeyEvent;
		public event ResetDialogStateEventHandler ResetDialogState;
		public event ResourceRequestEventHandler ResourceRequestHandlerRequired;
		public event SetFocusEventHandler SetFocus;
		public event TakeFocusEventHandler TakeFocus;
		public event UncaughtExceptionEventHandler UncaughtExceptionEvent;

		public CefSharpBrowserControl(ILifeSpanHandler lifeSpanHandler, string url) : base(url)
		{
			DialogHandler = new DialogHandlerSwitch();
			DisplayHandler = new DisplayHandlerSwitch();
			DownloadHandler = new DownloadHandlerSwitch();
			DragHandler = new DragHandlerSwitch();
			FocusHandler = new FocusHandlerSwitch();
			JsDialogHandler = new JavaScriptDialogHandlerSwitch();
			KeyboardHandler = new KeyboardHandlerSwitch();
			LifeSpanHandler = lifeSpanHandler;
			MenuHandler = new ContextMenuHandler();
			RenderProcessMessageHandler = new RenderProcessMessageHandlerSwitch();
			RequestHandler = new RequestHandlerSwitch();
		}

		void ICefSharpControl.Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		public void GetAuthCredentials(IWebBrowser webBrowser, IBrowser browser, string originUrl, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback, GenericEventArgs args)
		{
			AuthCredentialsRequired?.Invoke(webBrowser, browser, originUrl, isProxy, host, port, realm, scheme, callback, args);
		}

		public void GetResourceRequestHandler(IWebBrowser webBrowser, IBrowser browser, IFrame frame, IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling, ResourceRequestEventArgs args)
		{
			ResourceRequestHandlerRequired?.Invoke(webBrowser, browser, frame, request, isNavigation, isDownload, requestInitiator, ref disableDefaultHandling, args);
		}

		public void OnBeforeBrowse(IWebBrowser webBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect, GenericEventArgs args)
		{
			BeforeBrowse?.Invoke(webBrowser, browser, frame, request, userGesture, isRedirect, args);
		}

		public void OnBeforeDownload(IWebBrowser webBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback, GenericEventArgs args)
		{
			BeforeDownload?.Invoke(webBrowser, browser, downloadItem, callback, args);
		}

		public void OnBeforeUnloadDialog(IWebBrowser webBrowser, IBrowser browser, string message, bool isReload, IJsDialogCallback callback, GenericEventArgs args)
		{
			BeforeUnloadDialog?.Invoke(webBrowser, browser, message, isReload, callback, args);
		}

		public void OnCanDownload(IWebBrowser webBrowser, IBrowser browser, string url, string requestMethod, GenericEventArgs args)
		{
			CanDownload?.Invoke(webBrowser, browser, url, requestMethod, args);
		}

		public void OnContextCreated(IWebBrowser webBrowser, IBrowser browser, IFrame frame)
		{
			ContextCreated?.Invoke(webBrowser, browser, frame);
		}

		public void OnContextReleased(IWebBrowser webBrowser, IBrowser browser, IFrame frame)
		{
			ContextReleased?.Invoke(webBrowser, browser, frame);
		}

		public void OnDialogClosed(IWebBrowser webBrowser, IBrowser browser)
		{
			DialogClosed?.Invoke(webBrowser, browser);
		}

		public void OnDownloadUpdated(IWebBrowser webBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
		{
			DownloadUpdated?.Invoke(webBrowser, browser, downloadItem, callback);
		}

		public void OnDragEnter(IWebBrowser webBrowser, IBrowser browser, IDragData dragData, DragOperationsMask mask, GenericEventArgs args)
		{
			DragEnterCefSharp?.Invoke(webBrowser, browser, dragData, mask, args);
		}

		public void OnDraggableRegionsChanged(IWebBrowser webBrowser, IBrowser browser, IFrame frame, IList<DraggableRegion> regions)
		{
			DraggableRegionsChanged?.Invoke(webBrowser, browser, frame, regions);
		}

		public void OnFaviconUrlChange(IWebBrowser webBrowser, IBrowser browser, IList<string> urls)
		{
			FaviconUrlChanged?.Invoke(webBrowser, browser, urls);
		}

		public void OnFileDialog(IWebBrowser webBrowser, IBrowser browser, CefFileDialogMode mode, string title, string defaultFilePath, IReadOnlyCollection<string> acceptFilters, IReadOnlyCollection<string> acceptExtensions, IReadOnlyCollection<string> acceptDescriptions, IFileDialogCallback callback)
		{
			FileDialogRequested?.Invoke(webBrowser, browser, mode, title, defaultFilePath, acceptFilters, acceptExtensions, acceptDescriptions, callback);
		}

		public void OnFocusedNodeChanged(IWebBrowser webBrowser, IBrowser browser, IFrame frame, IDomNode node)
		{
			FocusedNodeChanged?.Invoke(webBrowser, browser, frame, node);
		}

		public void OnGotFocus(IWebBrowser webBrowser, IBrowser browser)
		{
			GotFocusCefSharp?.Invoke(webBrowser, browser);
		}

		public void OnJavaScriptDialog(IWebBrowser webBrowser, IBrowser browser, string originUrl, CefJsDialogType type, string message, string promptText, IJsDialogCallback callback, ref bool suppress, GenericEventArgs args)
		{
			JavaScriptDialog?.Invoke(webBrowser, browser, originUrl, type, message, promptText, callback, ref suppress, args);
		}

		public void OnKeyEvent(IWebBrowser webBrowser, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
		{
			KeyEvent?.Invoke(webBrowser, browser, type, windowsKeyCode, nativeKeyCode, modifiers, isSystemKey);
		}

		public void OnLoadingProgressChange(IWebBrowser webBrowser, IBrowser browser, double progress)
		{
			LoadingProgressChanged?.Invoke(webBrowser, browser, progress);
		}

		public void OnOpenUrlFromTab(IWebBrowser webBrowser, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture, GenericEventArgs args)
		{
			OpenUrlFromTab?.Invoke(webBrowser, browser, frame, targetUrl, targetDisposition, userGesture, args);
		}

		public void OnPreKeyEvent(IWebBrowser webBrowser, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut, GenericEventArgs args)
		{
			PreKeyEvent?.Invoke(webBrowser, browser, type, windowsKeyCode, nativeKeyCode, modifiers, isSystemKey, ref isKeyboardShortcut, args);
		}

		public void OnResetDialogState(IWebBrowser webBrowser, IBrowser browser)
		{
			ResetDialogState?.Invoke(webBrowser, browser);
		}

		public void OnSetFocus(IWebBrowser webBrowser, IBrowser browser, CefFocusSource source, GenericEventArgs args)
		{
			SetFocus?.Invoke(webBrowser, browser, source, args);
		}

		public void OnTakeFocus(IWebBrowser webBrowser, IBrowser browser, bool next)
		{
			TakeFocus?.Invoke(webBrowser, browser, next);
		}

		public void OnUncaughtException(IWebBrowser webBrowser, IBrowser browser, IFrame frame, JavascriptException exception)
		{
			UncaughtExceptionEvent?.Invoke(webBrowser, browser, frame, exception);
		}
	}
}
