﻿/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

namespace SafeExamBrowser.SystemComponents.Contracts.Network
{
	/// <summary>
	/// Defines a wireless network which can be connected to by the application.
	/// </summary>
	public interface IWirelessNetwork
	{
		/// <summary>
		/// The network name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// The signal strength of this network, from <c>0</c> (worst) to <c>100</c> (best).
		/// </summary>
		int SignalStrength { get; }

		/// <summary>
		/// The connection status of this network.
		/// </summary>
		ConnectionStatus Status { get; }
	}
}
