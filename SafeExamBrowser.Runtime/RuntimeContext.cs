﻿/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using SafeExamBrowser.Communication.Contracts.Proxies;
using SafeExamBrowser.Configuration.Contracts;
using SafeExamBrowser.Core.Contracts.ResponsibilityModel;
using SafeExamBrowser.Runtime.Responsibilities;
using SafeExamBrowser.WindowsApi.Contracts;

namespace SafeExamBrowser.Runtime
{
	/// <summary>
	/// Holds all configuration and session data for the runtime.
	/// </summary>
	internal class RuntimeContext
	{
		/// <summary>
		/// The currently running client process.
		/// </summary>
		internal IProcess ClientProcess { get; set; }

		/// <summary>
		/// The communication proxy for the currently running client process.
		/// </summary>
		internal IClientProxy ClientProxy { get; set; }

		/// <summary>
		/// The configuration of the currently active session.
		/// </summary>
		internal SessionConfiguration Current { get; set; }

		/// <summary>
		/// The configuration of the next session to be activated.
		/// </summary>
		internal SessionConfiguration Next { get; set; }

		/// <summary>
		/// The path of the configuration file to be used for reconfiguration.
		/// </summary>
		internal string ReconfigurationFilePath { get; set; }

		/// <summary>
		/// The original URL from where the configuration file was downloaded.
		/// </summary>
		internal string ReconfigurationUrl { get; set; }

		/// <summary>
		/// The runtime responsibilities.
		/// </summary>
		internal IResponsibilityCollection<RuntimeTask> Responsibilities { get; set; }
	}
}
