﻿/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

namespace SafeExamBrowser.Monitoring.Contracts
{
	/// <summary>
	/// Provides functionality related to virtual machine detection.
	/// </summary>
	public interface IVirtualMachineDetector
	{
		/// <summary>
		/// Indicates whether the computer is in fact a virtual machine.
		/// </summary>
		bool IsVirtualMachine();
	}
}
