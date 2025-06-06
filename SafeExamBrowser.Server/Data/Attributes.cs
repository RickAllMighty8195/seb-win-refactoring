﻿/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using SafeExamBrowser.Server.Contracts.Events.Proctoring;

namespace SafeExamBrowser.Server.Data
{
	internal class Attributes
	{
		internal bool AllowChat { get; set; }
		internal int Id { get; set; }
		internal InstructionEventArgs Instruction { get; set; }
		internal string Message { get; set; }
		internal bool ReceiveAudio { get; set; }
		internal bool ReceiveVideo { get; set; }
		internal AttributeType Type { get; set; }
	}
}
