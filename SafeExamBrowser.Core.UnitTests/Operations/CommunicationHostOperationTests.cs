﻿/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SafeExamBrowser.Communication.Contracts;
using SafeExamBrowser.Core.Contracts.OperationModel;
using SafeExamBrowser.Core.Operations;
using SafeExamBrowser.Logging.Contracts;

namespace SafeExamBrowser.Core.UnitTests.Operations
{
	[TestClass]
	public class CommunicationHostOperationTests
	{
		private Mock<ICommunicationHost> hostMock;
		private Mock<ILogger> loggerMock;
		private CommunicationHostOperation sut;

		[TestInitialize]
		public void Initialize()
		{
			hostMock = new Mock<ICommunicationHost>();
			loggerMock = new Mock<ILogger>();

			sut = new CommunicationHostOperation(hostMock.Object, loggerMock.Object);
		}

		[TestMethod]
		public void MustRestartHostOnRepeat()
		{
			var order = 0;
			var stop = 0;
			var start = 0;

			hostMock.Setup(h => h.Stop()).Callback(() => stop = ++order);
			hostMock.Setup(h => h.Start()).Callback(() => start = ++order);

			var result = sut.Repeat();

			hostMock.Verify(h => h.Stop(), Times.Once);
			hostMock.Verify(h => h.Start(), Times.Once);

			Assert.AreEqual(1, stop);
			Assert.AreEqual(2, start);
			Assert.AreEqual(OperationResult.Success, result);
		}

		[TestMethod]
		public void MustOnlyRestartHostOnRepeatIfNotRunning()
		{
			hostMock.SetupGet(h => h.IsRunning).Returns(true);

			var result = sut.Repeat();

			hostMock.Verify(h => h.Start(), Times.Never);
			hostMock.Verify(h => h.Stop(), Times.Never);

			Assert.AreEqual(OperationResult.Success, result);
		}

		[TestMethod]
		public void MustStartHostOnPerform()
		{
			var result = sut.Perform();

			hostMock.Verify(h => h.Start(), Times.Once);
			hostMock.Verify(h => h.Stop(), Times.Never);

			Assert.AreEqual(OperationResult.Success, result);
		}

		[TestMethod]
		public void MustStopHostOnRevert()
		{
			sut.Revert();

			hostMock.Verify(h => h.Stop(), Times.Once);
			hostMock.Verify(h => h.Start(), Times.Never);
		}

		[TestMethod]
		public void MustFireStatusChangedEvent()
		{
			var fired = 0;

			sut.StatusChanged += (_) => fired++;

			sut.Perform();
			sut.Repeat();
			sut.Revert();

			Assert.AreEqual(3, fired);
		}
	}
}
