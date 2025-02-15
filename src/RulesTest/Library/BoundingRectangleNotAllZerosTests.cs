// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace Axe.Windows.RulesTests.Library
{
    [TestClass]
    public class BoundingRectangleNotAllZerosTests
    {
        private static Axe.Windows.Rules.IRule Rule = new Axe.Windows.Rules.Library.BoundingRectangleNotAllZeros();

        [TestMethod]
        public void BoundingRectangleNotAllZeros_NotAllZeros()
        {
            var e = new MockA11yElement();
            e.BoundingRectangle = new Rectangle(0, 0, 0, 1);
            Assert.IsTrue(Rule.PassesTest(e));
        }

        [TestMethod]
        public void TestBoundingRectangleNotAllZeros_AllZeros()
        {
            var e = new MockA11yElement();
            e.BoundingRectangle = new Rectangle(0, 0, 0, 0);
            Assert.IsFalse(Rule.PassesTest(e));
        }

        [TestMethod]
        public void TestBoundingRectangleNotAllZeros_NullProperty()
        {
            var e = new MockA11yElement();
            e.IsOffScreen = false;
            Assert.IsFalse(Rule.Condition.Matches(e));
        }

        [TestMethod]
        public void TestBoundingRectangleNotAllZeros_IsOffScreenTrue()
        {
            var e = new MockA11yElement();
            e.BoundingRectangle = Rectangle.Empty;
            e.IsOffScreen = true;
            Assert.IsFalse(Rule.Condition.Matches(e));
        }

        [TestMethod]
        public void TestBoundingRectangleNotAllZeros_ConditionMatches()
        {
            var e = new MockA11yElement();
            e.BoundingRectangle = Rectangle.Empty;
            e.IsOffScreen = false;
            Assert.IsTrue(Rule.Condition.Matches(e));
        }
    } // class
} // namespace
