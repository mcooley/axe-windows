// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Enums;
using Axe.Windows.Core.Types;
using Axe.Windows.Rules.Extensions;
using Axe.Windows.Rules.PropertyConditions;
using Axe.Windows.Rules.Resources;
using System;
using System.Text.RegularExpressions;
using static Axe.Windows.Rules.PropertyConditions.BoolProperties;
using static Axe.Windows.Rules.PropertyConditions.ControlType;
using static Axe.Windows.Rules.PropertyConditions.Relationships;

namespace Axe.Windows.Rules.Library
{
    [RuleInfo(ID = RuleId.BoundingRectangleContainedInParent)]
    class BoundingRectangleContainedInParent : Rule
    {
        const int Margin = BoundingRectangle.OverlapMargin;

        public BoundingRectangleContainedInParent()
        {
            this.Info.Description = Descriptions.BoundingRectangleContainedInParent;
            this.Info.HowToFix = HowToFix.BoundingRectangleContainedInParent;
            this.Info.Standard = A11yCriteriaId.ObjectInformation;
            this.Info.PropertyID = PropertyType.UIA_BoundingRectanglePropertyId;
            this.Info.ErrorCode = EvaluationCode.Warning;
        }

        public override bool PassesTest(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            if (!IsBoundingRectangleContained(e.Parent, e))
            {
                // if the element is not contained in the parent element, go further
                var container = e.FindContainerElement();

                return IsBoundingRectangleContained(container, e);
            }

            return true;
        }

        private static bool IsBoundingRectangleContained(IA11yElement container, IA11yElement containee)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (containee == null) throw new ArgumentNullException(nameof(containee));

            if (ScrollPattern.NotHorizontallyScrollable.Matches(container))
            {
                if (container.BoundingRectangle.Left - Margin > containee.BoundingRectangle.Left)
                    return false;

                if (container.BoundingRectangle.Right + Margin < containee.BoundingRectangle.Right)
                    return false;
            }

            if (ScrollPattern.NotVerticallyScrollable.Matches(container))
            {
                if (container.BoundingRectangle.Top - Margin > containee.BoundingRectangle.Top)
                    return false;

                if (container.BoundingRectangle.Bottom + Margin < containee.BoundingRectangle.Bottom)
                    return false;
            }

            return true;
        }

        protected override Condition CreateCondition()
        {
            // Windows can be any size, regardless of their parents
            // Certain chrome panes are treated as windows because that's how they behave.
            var chromePane = Pane
                & IntProperties.NativeWindowHandle != 0
                & StringProperties.ClassName.MatchesRegEx(@"Chrome_WidgetWin_\d+$", RegexOptions.IgnoreCase);

            return ~Window
                & IsNotOffScreen
                & BoundingRectangle.Valid
                & ~chromePane
                & ParentExists
                & Parent(IsNotDesktop)
                & Parent(BoundingRectangle.Valid)
                & ~BoundingRectangle.CompletelyObscuresContainer;
        }
    } // class
} // namespace
