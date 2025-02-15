// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using static Axe.Windows.RulesTests.ControlType;

namespace Axe.Windows.RulesTests.Library
{
    [TestClass]
    public class BoundingRectangleNotNullTests
    {
        private static Axe.Windows.Rules.IRule Rule = new Axe.Windows.Rules.Library.BoundingRectangleNotNull();

        [TestMethod]
        public void TestBoundingRectangleNotNullPass()
        {
            using (var e = new MockA11yElement())
            {
                e.BoundingRectangle = Rectangle.Empty;
                Assert.IsTrue(Rule.PassesTest(e));
            } // using
        }

        [TestMethod]
        public void TestBoundingRectangleNotNullFail()
        {
            using (var e = new MockA11yElement())
            {
                Assert.IsFalse(Rule.PassesTest(e));
            } // using
        }

        [TestMethod]
        public void BoundingRectangleNotNull_WPFScrollbarPageUpButton_NotApplicable()
        {
            using (var e = new MockA11yElement())
            using (var parent = new MockA11yElement())
            {
                parent.ControlTypeId = ControlType.ScrollBar;
                e.IsOffScreen = false;
                e.ControlTypeId = ControlType.Button;
                e.Framework = "WPF";
                e.AutomationId = "PageUp";
                parent.Children.Add(e);
                e.Parent = parent;

                Assert.IsFalse(Rule.Condition.Matches(e));
            } // using
        }

        [TestMethod]
        public void BoundingRectangleNotNull_WPFScrollbarPageDownButton_NotApplicable()
        {
            using (var e = new MockA11yElement())
            using (var parent = new MockA11yElement())
            {
                parent.ControlTypeId = ControlType.ScrollBar;
                e.IsOffScreen = false;
                e.ControlTypeId = ControlType.Button;
                e.Framework = "WPF";
                e.AutomationId = "PageDown";
                parent.Children.Add(e);
                e.Parent = parent;

                Assert.IsFalse(Rule.Condition.Matches(e));
            } // using
        }

        [TestMethod]
        public void BoundingRectangleNotNull_WPFScrollbarPageLeftButton_NotApplicable()
        {
            using (var e = new MockA11yElement())
            using (var parent = new MockA11yElement())
            {
                parent.ControlTypeId = ControlType.ScrollBar;
                e.IsOffScreen = false;
                e.ControlTypeId = ControlType.Button;
                e.Framework = "WPF";
                e.AutomationId = "PageLeft";
                parent.Children.Add(e);
                e.Parent = parent;

                Assert.IsFalse(Rule.Condition.Matches(e));
            } // using
        }

        [TestMethod]
        public void BoundingRectangleNotNull_NonFocusableSliderButton_NotApplicable()
        {
            using (var e = new MockA11yElement())
            using (var parent = new MockA11yElement())
            {
                parent.ControlTypeId = ControlType.Slider;
                e.IsKeyboardFocusable = false;
                e.ControlTypeId = ControlType.Button;
                parent.Children.Add(e);
                e.Parent = parent;

                Assert.IsFalse(Rule.Condition.Matches(e));
            } // using
        }

        [TestMethod]
        public void BoundingRectangleNotNull_SystemMenu_NotApplicable()
        {
            using (var e = new MockA11yElement())
            using (var parent = new MockA11yElement())
            {
                parent.ControlTypeId = ControlType.MenuBar;
                parent.AutomationId = "SystemMenuBar";
                e.ControlTypeId = ControlType.MenuItem;
                parent.Children.Add(e);
                e.Parent = parent;

                Assert.IsFalse(Rule.Condition.Matches(e));
            } // using
        }

        [TestMethod]
        public void BoundingRectangleNotNull_SystemMenuBar_NotApplicable()
        {
            using (var e = new MockA11yElement())
            {
                e.ControlTypeId = ControlType.MenuBar;
                e.AutomationId = "SystemMenuBar";

                Assert.IsFalse(Rule.Condition.Matches(e));
            } // using
        }

        [TestMethod]
        public void BoundingRectangleNotNull_ListViewXAML_NotApplicable()
        {
            using (var e = new MockA11yElement())
            {
                e.IsOffScreen = true;
                e.Framework = "XAML";
                e.ControlTypeId = List;

                Assert.IsFalse(Rule.Condition.Matches(e));
            } // using
        }

        [TestMethod]
        public void BoundingRectangleNotNull_HyperlinkInTextBlockXAML_NotApplicable()
        {
            using (var e = new MockA11yElement())
            using (var parent = new MockA11yElement())
            {
                parent.ControlTypeId = Text;
                e.Parent = parent;
                e.IsOffScreen = true;
                e.Framework = "XAML";
                e.ControlTypeId = Hyperlink;

                Assert.IsFalse(Rule.Condition.Matches(e));
            } // using
        }
    } // class
} // namespace
