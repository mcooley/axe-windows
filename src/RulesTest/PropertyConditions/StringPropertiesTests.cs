// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Axe.Windows.Rules.PropertyConditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Axe.Windows.Rules.PropertyConditions.StringProperties;

namespace Axe.Windows.RulesTests.PropertyConditions
{
    [TestClass]
    public class StringPropertiesTests
    {
        private static StringProperty Property = new StringProperty(e => e.Name);

        [TestMethod]
        public void TestStringPropertyNullTrue()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = null;
                Assert.IsTrue(Property.Null.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyNullFalse()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "";
                Assert.IsFalse(Property.Null.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyNotNullTrue()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "";
                Assert.IsTrue(Property.NotNull.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyNotNullFalse()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = null;
                Assert.IsFalse(Property.NotNull.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyEmptyTrue()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "";
                Assert.IsTrue(Property.Empty.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyEmptyFalse()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "Hello world!";
                Assert.IsFalse(Property.Empty.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyNotEmptyTrue()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "MyStringProperty";
                Assert.IsTrue(Property.NotEmpty.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyNotEmptyFalse()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "";
                Assert.IsFalse(Property.NotEmpty.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyNullOrEmptyTrue()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "";
                Assert.IsTrue(Property.NullOrEmpty.Matches(e));

                e.Name = null;
                Assert.IsTrue(Property.NullOrEmpty.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyNullOrEmptyFalse()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "MyStringProperty";
                Assert.IsFalse(Property.NullOrEmpty.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyNotNullOrEmptyTrue()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "MyStringProperty";
                Assert.IsTrue(Property.NotNullOrEmpty.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyNotNullOrEmptyFalse()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "";
                Assert.IsFalse(Property.NotNullOrEmpty.Matches(e));

                e.Name = null;
                Assert.IsFalse(Property.NotNullOrEmpty.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyWhiteSpaceTrue()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = " ";
                Assert.IsTrue(Property.WhiteSpace.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyWhiteSpaceFalse()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "MyStringProperty";
                Assert.IsFalse(Property.WhiteSpace.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyNotWhiteSpaceTrue()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "MyStringProperty";
                Assert.IsTrue(Property.NotWhiteSpace.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyNotWhiteSpaceFalse()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "";
                Assert.IsFalse(Property.NotWhiteSpace.Matches(e));
            } // using
        }

        [TestMethod]
        public void NullOrWhiteSpace_Null_True()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = null;
                Assert.IsTrue(Property.NullOrWhiteSpace.Matches(e));
                Assert.IsFalse(Property.NotNullOrWhiteSpace.Matches(e));
            } // using
        }

        [TestMethod]
        public void NullOrWhiteSpace_Empty_True()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = string.Empty;
                Assert.IsTrue(Property.NullOrWhiteSpace.Matches(e));
                Assert.IsFalse(Property.NotNullOrWhiteSpace.Matches(e));
            } // using
        }

        [TestMethod]
        public void NullOrWhiteSpace_WhiteSpace_True()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = " \t";
                Assert.IsTrue(Property.NullOrWhiteSpace.Matches(e));
                Assert.IsFalse(Property.NotNullOrWhiteSpace.Matches(e));
            } // using
        }

        [TestMethod]
        public void NullOrWhiteSpace_False()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "Hello";
                Assert.IsFalse(Property.NullOrWhiteSpace.Matches(e));
                Assert.IsTrue(Property.NotNullOrWhiteSpace.Matches(e));
            } // using
        }

        [TestMethod]
        public void IncludesPrivateUnicodeCharacters_LowerBound_True()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "\uE000";
                Assert.IsTrue(Property.IncludesPrivateUnicodeCharacters.Matches(e));
                Assert.IsFalse(Property.ExcludesPrivateUnicodeCharacters.Matches(e));
            } // using
        }

        [TestMethod]
        public void IncludesPrivateUnicodeCharacters_LowerBound_False()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "\uDFFF";
                Assert.IsFalse(Property.IncludesPrivateUnicodeCharacters.Matches(e));
                Assert.IsTrue(Property.ExcludesPrivateUnicodeCharacters.Matches(e));
            } // using
        }

        [TestMethod]
        public void IncludesPrivateUnicodeCharacters_UpperBound_True()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "\uF8FF";
                Assert.IsTrue(Property.IncludesPrivateUnicodeCharacters.Matches(e));
                Assert.IsFalse(Property.ExcludesPrivateUnicodeCharacters.Matches(e));
            } // using
        }

        [TestMethod]
        public void IncludesPrivateUnicodeCharacters_UpperBound_False()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "\uF900";
                Assert.IsFalse(Property.IncludesPrivateUnicodeCharacters.Matches(e));
                Assert.IsTrue(Property.ExcludesPrivateUnicodeCharacters.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyIsTrue()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "Bob";
                Assert.IsTrue(Property.Is("Bob").Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyIsFalse()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "Bob";
                Assert.IsFalse(Property.Is("bob").Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyIgnoreCaseIsTrue()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "Bob";
                Assert.IsTrue(Property.IsNoCase("bob").Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyIsNullFalse()
        {
            using (var e = new MockA11yElement())
            {
                Assert.IsFalse(Property.Is("Bob").Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyIsEqualTrue()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "Frank";
                e.ClassName = "Frank";

                var condition = Name.IsEqualTo(ClassName);
                Assert.IsTrue(condition.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyIsEqualFalse()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "Frank";
                e.ClassName = "Carl";

                var condition = Name.IsEqualTo(ClassName);
                Assert.IsFalse(condition.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyIsEqualBothEmptyFalse()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "";
                e.ClassName = "";

                var condition = Name.IsEqualTo(ClassName);
                Assert.IsFalse(condition.Matches(e));
            } // using
        }

        [TestMethod]
        public void TestStringPropertyIsEqualBothNullFalse()
        {
            using (var e = new MockA11yElement())
            {
                var condition = Name.IsEqualTo(ClassName);
                Assert.IsFalse(condition.Matches(e));
            } // using
        }

        [TestMethod]
        public void MatchesRegEx_CaseMatches_True()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "Eat your food";
                var condition = Name.MatchesRegEx(@"fo+");
                Assert.IsTrue(condition.Matches(e));
            } // using
        }

        [TestMethod]
        public void MatchesRegEx_CaseDoesNotMatch_False()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "food";
                var condition = Name.MatchesRegEx(@"Fo+");
                Assert.IsFalse(condition.Matches(e));
            } // using
        }

        [TestMethod]
        public void MatchesRegExNoCase_CaseDoesNotMatch_True()
        {
            using (var e = new MockA11yElement())
            {
                e.Name = "Eat your Food";
                var condition = Name.MatchesRegEx(@"foo", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Assert.IsTrue(condition.Matches(e));
            } // using
        }

        [TestMethod]
        public void MatchesRegEx_NullProperty_False()
        {
            using (var e = new MockA11yElement())
            {
                var condition = Name.MatchesRegEx(@"foo");
                Assert.IsFalse(condition.Matches(e));
            } // using
        }

        [TestMethod]
        public void MatchesRegEx_WithOptions_NullProperty_False()
        {
            using (var e = new MockA11yElement())
            {
                var condition = Name.MatchesRegEx(@"foo", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Assert.IsFalse(condition.Matches(e));
            } // using
        }
    } // class
} // namespace
