﻿/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;

namespace SafeExamBrowser.WindowsApi.Delegates
{
	/// <remarks>
	/// See https://msdn.microsoft.com/en-us/library/windows/desktop/ms633498(v=vs.85).aspx
	/// </remarks>
	internal delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lParam);
}
