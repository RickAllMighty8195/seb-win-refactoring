﻿/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using SafeExamBrowser.Core.Contracts.OperationModel;
using SafeExamBrowser.Core.Contracts.OperationModel.Events;

namespace SafeExamBrowser.Core.Operations
{
	/// <summary>
	/// A wrapper operation to allow for a lazy (just-in-time) instantiation of an operation, initialized on <see cref="Perform"/>.
	/// Is useful when e.g. dependencies for a certain operation are not available during execution of the composition root, but rather
	/// only after a preceding operation within an <see cref="IOperationSequence"/> has finished.
	/// </summary>
	public class LazyInitializationOperation : IOperation
	{
		private readonly Func<IOperation> initialize;
		private IOperation operation;

		private event StatusChangedEventHandler StatusChangedImpl;

		public event StatusChangedEventHandler StatusChanged
		{
			add
			{
				StatusChangedImpl += value;

				if (operation != null)
				{
					operation.StatusChanged += value;
				}
			}
			remove
			{
				StatusChangedImpl -= value;

				if (operation != null)
				{
					operation.StatusChanged -= value;
				}
			}
		}

		public LazyInitializationOperation(Func<IOperation> initialize)
		{
			this.initialize = initialize;
		}

		public OperationResult Perform()
		{
			operation = initialize.Invoke();
			operation.StatusChanged += StatusChangedImpl;

			return operation.Perform();
		}

		public OperationResult Revert()
		{
			return operation.Revert();
		}
	}
}
