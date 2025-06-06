﻿/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using SafeExamBrowser.Core.Contracts.OperationModel;
using SafeExamBrowser.Core.Contracts.OperationModel.Events;
using SafeExamBrowser.Logging.Contracts;

namespace SafeExamBrowser.Service.Operations
{
	internal class ServiceEventCleanupOperation : IOperation
	{
		private readonly ILogger logger;
		private readonly SessionContext sessionContext;

		public event StatusChangedEventHandler StatusChanged { add { } remove { } }

		public ServiceEventCleanupOperation(ILogger logger, SessionContext sessionContext)
		{
			this.logger = logger;
			this.sessionContext = sessionContext;
		}

		public OperationResult Perform()
		{
			return OperationResult.Success;
		}

		public OperationResult Revert()
		{
			if (sessionContext.ServiceEvent != null)
			{
				logger.Info("Closing service event...");
				sessionContext.ServiceEvent.Close();
				logger.Info("Service event successfully closed.");
			}

			return OperationResult.Success;
		}
	}
}
