// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Enums;
using Axe.Windows.Core.Types;
using Axe.Windows.Rules.PropertyConditions;
using Axe.Windows.Rules.Resources;
using System;
using static Axe.Windows.Rules.PropertyConditions.BoolProperties;
using static Axe.Windows.Rules.PropertyConditions.ControlType;

namespace Axe.Windows.Rules.Library
{
    [RuleInfo(ID = RuleId.NameNullButElementNotKeyboardFocusable)]
    class NameIsNullButElementIsNotKeyboardFocusable : Rule
    {
        public NameIsNullButElementIsNotKeyboardFocusable()
        {
            this.Info.Description = Descriptions.NameNullButElementNotKeyboardFocusable;
            this.Info.HowToFix = HowToFix.NameNullButElementNotKeyboardFocusable;
            this.Info.Standard = A11yCriteriaId.ObjectInformation;
            this.Info.PropertyID = PropertyType.UIA_NamePropertyId;
            this.Info.ErrorCode = EvaluationCode.NeedsReview;
        }

        public override bool PassesTest(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            return e.Name != null;
        }

        protected override Condition CreateCondition()
        {
            // Regardless if it is focusable, ProgressBar should be reported as an error
            // So it is handled in NameIsNotNull

            return IsNotKeyboardFocusable
                & ~ProgressBar
                & BoundingRectangle.Valid
                & ElementGroups.NameRequired;
        }
    } // class
} // namespace
