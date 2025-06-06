﻿/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SafeExamBrowser.Communication.Contracts.Hosts;
using SafeExamBrowser.Configuration.Contracts;
using SafeExamBrowser.Core.Contracts.OperationModel;
using SafeExamBrowser.I18n.Contracts;
using SafeExamBrowser.Logging.Contracts;
using SafeExamBrowser.Runtime.Communication;
using SafeExamBrowser.Runtime.Operations.Session;
using SafeExamBrowser.Settings;
using SafeExamBrowser.UserInterface.Contracts.MessageBox;
using SafeExamBrowser.UserInterface.Contracts.Windows;

namespace SafeExamBrowser.Runtime.UnitTests.Operations
{
	[TestClass]
	public class DisclaimerOperationTests
	{
		private RuntimeContext context;
		private Mock<IMessageBox> messageBox;
		private AppSettings settings;

		private DisclaimerOperation sut;

		[TestInitialize]
		public void Initialize()
		{
			context = new RuntimeContext();
			messageBox = new Mock<IMessageBox>();
			settings = new AppSettings();

			context.Next = new SessionConfiguration();
			context.Next.Settings = settings;

			var dependencies = new Dependencies(
				new ClientBridge(Mock.Of<IRuntimeHost>(), context),
				Mock.Of<ILogger>(),
				messageBox.Object,
				Mock.Of<IRuntimeWindow>(),
				context,
				Mock.Of<IText>());

			sut = new DisclaimerOperation(dependencies);
		}

		[TestMethod]
		public void Perform_MustShowDisclaimerWhenProctoringEnabled()
		{
			var count = 0;

			settings.Proctoring.ScreenProctoring.Enabled = true;
			messageBox
				.Setup(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxAction>(), It.IsAny<MessageBoxIcon>(), It.IsAny<IWindow>()))
				.Callback(() => count++)
				.Returns(MessageBoxResult.Ok);

			var result = sut.Perform();

			Assert.AreEqual(1, count);
			Assert.AreEqual(OperationResult.Success, result);
		}

		[TestMethod]
		public void Perform_MustAbortIfDisclaimerNotConfirmed()
		{
			var disclaimerShown = false;

			settings.Proctoring.ScreenProctoring.Enabled = true;
			messageBox
				.Setup(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxAction>(), It.IsAny<MessageBoxIcon>(), It.IsAny<IWindow>()))
				.Callback(() => disclaimerShown = true)
				.Returns(MessageBoxResult.Cancel);

			var result = sut.Repeat();

			Assert.IsTrue(disclaimerShown);
			Assert.AreEqual(OperationResult.Aborted, result);
		}

		[TestMethod]
		public void Perform_MustDoNothingIfProctoringNotEnabled()
		{
			var disclaimerShown = false;

			messageBox
				.Setup(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxAction>(), It.IsAny<MessageBoxIcon>(), It.IsAny<IWindow>()))
				.Callback(() => disclaimerShown = true)
				.Returns(MessageBoxResult.Cancel);

			var result = sut.Perform();

			Assert.IsFalse(disclaimerShown);
			Assert.AreEqual(OperationResult.Success, result);
		}

		[TestMethod]
		public void Repeat_MustShowDisclaimerWhenProctoringEnabled()
		{
			var count = 0;

			settings.Proctoring.ScreenProctoring.Enabled = true;
			messageBox
				.Setup(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxAction>(), It.IsAny<MessageBoxIcon>(), It.IsAny<IWindow>()))
				.Callback(() => count++)
				.Returns(MessageBoxResult.Ok);

			var result = sut.Perform();

			Assert.AreEqual(1, count);
			Assert.AreEqual(OperationResult.Success, result);
		}

		[TestMethod]
		public void Repeat_MustAbortIfDisclaimerNotConfirmed()
		{
			var disclaimerShown = false;

			settings.Proctoring.ScreenProctoring.Enabled = true;
			messageBox
				.Setup(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxAction>(), It.IsAny<MessageBoxIcon>(), It.IsAny<IWindow>()))
				.Callback(() => disclaimerShown = true)
				.Returns(MessageBoxResult.Cancel);

			var result = sut.Repeat();

			Assert.IsTrue(disclaimerShown);
			Assert.AreEqual(OperationResult.Aborted, result);
		}

		[TestMethod]
		public void Repeat_MustDoNothingIfProctoringNotEnabled()
		{
			var disclaimerShown = false;

			messageBox
				.Setup(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxAction>(), It.IsAny<MessageBoxIcon>(), It.IsAny<IWindow>()))
				.Callback(() => disclaimerShown = true)
				.Returns(MessageBoxResult.Cancel);

			var result = sut.Repeat();

			Assert.IsFalse(disclaimerShown);
			Assert.AreEqual(OperationResult.Success, result);
		}

		[TestMethod]
		public void Revert_MustDoNothing()
		{
			var disclaimerShown = false;

			messageBox
				.Setup(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxAction>(), It.IsAny<MessageBoxIcon>(), It.IsAny<IWindow>()))
				.Callback(() => disclaimerShown = true)
				.Returns(MessageBoxResult.Cancel);

			var result = sut.Revert();

			Assert.IsFalse(disclaimerShown);
			Assert.AreEqual(OperationResult.Success, result);
		}
	}
}
