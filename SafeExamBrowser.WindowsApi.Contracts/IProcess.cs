﻿/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using SafeExamBrowser.WindowsApi.Contracts.Events;

namespace SafeExamBrowser.WindowsApi.Contracts
{
	/// <summary>
	/// Represents a process and defines its functionality.
	/// </summary>
	public interface IProcess
	{
		/// <summary>
		/// Indicates whether the process has been terminated.
		/// </summary>
		bool HasTerminated { get; }

		/// <summary>
		/// The process identifier.
		/// </summary>
		int Id { get; }

		/// <summary>
		/// The file name of the process executable.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// The original file name of the process executable, if available.
		/// </summary>
		string OriginalName { get; }

		/// <summary>
		/// The full path of the process executable.
		/// </summary>
		string Path { get; }

		/// <summary>
		/// The thumbprint of the certificate used to sign the process executable, or <c>default(string)</c> if the executable isn't signed.
		/// </summary>
		string Signature { get; }

		/// <summary>
		/// Event fired when the process has terminated.
		/// </summary>
		event ProcessTerminatedEventHandler Terminated;

		/// <summary>
		/// Returns a string with the most important additional information about the process (not already contained in <c>ToString()</c>).
		/// </summary>
		string GetAdditionalInfo();

		/// <summary>
		/// Attempts to gracefully terminate the process by closing its main window. This will only work for interactive processes which have a main
		/// window. Optionally waits the specified amount of time for the process to terminate. Returns <c>true</c> if the process has terminated,
		/// otherwise <c>false</c>.
		/// </summary>
		bool TryClose(int timeout_ms = 0);

		/// <summary>
		/// Attempts to immediately kill the process. Optionally waits the specified amount of time for the process to terminate. Returns <c>true</c>
		/// if the process has terminated, otherwise <c>false</c>.
		/// </summary>
		bool TryKill(int timeout_ms = 0);
	}
}
