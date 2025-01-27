// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;

namespace Axe.Windows.RulesTests.Library
{
    [TestClass]
    public class ClickablePointOnScreenWPFTests
    {
        private static Axe.Windows.Rules.IRule Rule = new Axe.Windows.Rules.Library.ClickablePointOnScreenWPF();
        private Mock<IA11yElement> mockElement = new Mock<IA11yElement>(MockBehavior.Strict);
        private delegate void TryGetDelegate(int propertyId, out Point value);

        [TestCleanup]
        public void TestCleanup()
        {
            mockElement.Reset();
        }

        private void SetupTryGetProperty(Point outVal)
        {
            mockElement.Setup(m => m.TryGetPropertyValue<Point>(PropertyType.UIA_ClickablePointPropertyId, out It.Ref<Point>.IsAny))
                .Callback(new TryGetDelegate((int _, out Point p) =>
                {
                    p = outVal;
                }))
                .Returns(true);
        }

        [TestMethod]
        public void FrameworkIssueLink_IsNotNull()
        {
            Assert.IsNotNull(Rule.Info.FrameworkIssueLink);
        }

        [TestMethod]
        public void ClickablePointOnScreen_OffScreen_Error()
        {
            mockElement.Setup(m => m.IsOffScreen).Returns(true);
            Assert.IsFalse(Rule.PassesTest(mockElement.Object));
            mockElement.VerifyAll();
        }

        [TestMethod]
        public void ClickablePointOnScreen_OnScreen_Pass()
        {
            mockElement.Setup(m => m.IsOffScreen).Returns(false);
            Assert.IsTrue(Rule.PassesTest(mockElement.Object));
            mockElement.VerifyAll();
        }

        [TestMethod]
        public void ClickablePointOnScreen_FocusableClickable_FrameworkIsNotWPF_NoMatch()
        {
            SetupTryGetProperty(new Point(100, 100));
            mockElement.Setup(m => m.IsKeyboardFocusable).Returns(true);
            mockElement.Setup(m => m.Framework).Returns("Anything but WPF");
            Assert.IsFalse(Rule.Condition.Matches(mockElement.Object));
            mockElement.VerifyAll();
        }

        [TestMethod]
        public void ClickablePointOnScreen_FocusableClickable_FrameworkIsWPF_Matche()
        {
            SetupTryGetProperty(new Point(100, 100));
            mockElement.Setup(m => m.IsKeyboardFocusable).Returns(true);
            mockElement.Setup(m => m.Framework).Returns("WPF");
            Assert.IsTrue(Rule.Condition.Matches(mockElement.Object));
            mockElement.VerifyAll();
        }

        [TestMethod]
        public void ClickablePointOnScreen_IsNotFocusable_NoMatch()
        {
            mockElement.Setup(m => m.IsKeyboardFocusable).Returns(false);
            Assert.IsFalse(Rule.Condition.Matches(mockElement.Object));
            mockElement.VerifyAll();
        }
    } // class
} // namespace
