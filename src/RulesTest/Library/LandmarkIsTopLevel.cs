// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Axe.Windows.Core.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Axe.Windows.RulesTests.Library
{
    public class LandmarkIsTopLevel
    {
        private readonly Axe.Windows.Rules.IRule Rule = null;
        private readonly int LandmarkType = 0;
        private readonly string LocalizedLandmarkType = null;

        protected LandmarkIsTopLevel(object rule, int landmarkType, string localizedLandmarkType)
        {
            // we must pass in an object because the IRule type is not exposed publicly and it causes a compiler error
            this.Rule = (Axe.Windows.Rules.IRule)rule;
            this.LandmarkType = landmarkType;
            this.LocalizedLandmarkType = localizedLandmarkType;
        }

        [TestMethod]
        public void LandmarkIsTopLevel_Pass()
        {
            var e = new MockA11yElement();
            var parent = new MockA11yElement();
            e.LandmarkType = this.LandmarkType;
            e.LocalizedLandmarkType = this.LocalizedLandmarkType;
            e.Parent = parent;

            Assert.IsTrue(Rule.PassesTest(e));
        }

        [TestMethod]
        public void LandmarkIsTopLevel_Error()
        {
            var e = new MockA11yElement();
            var parent = new MockA11yElement();
            e.LandmarkType = this.LandmarkType;
            e.LocalizedLandmarkType = this.LocalizedLandmarkType;
            e.Parent = parent;
            parent.LandmarkType = this.LandmarkType;
            parent.LocalizedLandmarkType = this.LocalizedLandmarkType;

            Assert.IsFalse(Rule.PassesTest(e));
        }
    } // class

    [TestClass]
    public class LandmarkMainIsTopLevel : LandmarkIsTopLevel
    {
        public LandmarkMainIsTopLevel()
            : base(new Axe.Windows.Rules.Library.LandmarkMainIsTopLevel(), LandmarkType.UIA_MainLandmarkTypeId, null)
        { }
    } // class

    [TestClass]
    public class LandmarkBannerIsTopLevel : LandmarkIsTopLevel
    {
        public LandmarkBannerIsTopLevel()
            : base(new Axe.Windows.Rules.Library.LandmarkBannerIsTopLevel(), LandmarkType.UIA_CustomLandmarkTypeId, "banner")
        { }
    } // class

    [TestClass]
    public class LandmarkContentInfoIsTopLevel : LandmarkIsTopLevel
    {
        public LandmarkContentInfoIsTopLevel()
            : base(new Axe.Windows.Rules.Library.LandmarkContentInfoIsTopLevel(), LandmarkType.UIA_CustomLandmarkTypeId, "contentinfo")
        { }
    } // class

    [TestClass]
    public class LandmarkComplementaryIsTopLevel : LandmarkIsTopLevel
    {
        public LandmarkComplementaryIsTopLevel()
            : base(new Axe.Windows.Rules.Library.LandmarkComplementaryIsTopLevel(), LandmarkType.UIA_CustomLandmarkTypeId, "complementary")
        { }
    } // class
} // namespace
