﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Enums;
using Axe.Windows.Core.Types;
using Axe.Windows.Rules.PropertyConditions;
using Axe.Windows.Rules.Resources;
using static Axe.Windows.Rules.PropertyConditions.BoolProperties;
using static Axe.Windows.Rules.PropertyConditions.ControlType;
using static Axe.Windows.Rules.PropertyConditions.Relationships;
using static Axe.Windows.Rules.PropertyConditions.StringProperties;
using static Axe.Windows.Rules.PropertyConditions.Framework;

namespace Axe.Windows.Rules.Library
{
    [RuleInfo(ID = RuleId.BoundingRectangleSizeReasonable)]
    class BoundingRectangleSizeReasonable : Rule
    {
        public BoundingRectangleSizeReasonable()
        {
            this.Info.Description = Descriptions.BoundingRectangleSizeReasonable;
            this.Info.HowToFix = HowToFix.BoundingRectangleSizeReasonable;
            this.Info.Standard = A11yCriteriaId.ObjectInformation;
            this.Info.PropertyID = PropertyType.UIA_BoundingRectanglePropertyId;
            this.Info.ErrorCode = EvaluationCode.Error;
        }

        public override bool PassesTest(IA11yElement e)
        {
            return BoundingRectangle.Valid.Matches(e);
        }

        protected override Condition CreateCondition()
        {
            var ignoreableText = (Text | Separator) & ~IsKeyboardFocusable & Name.NullOrEmpty & ~ChildrenExist;

            return IsNotOffScreen
                & BoundingRectangle.NotNull
                & BoundingRectangle.CorrectDataFormat
                & ~ignoreableText
                & ~(WPF & BoundingRectangle.TelerikSparklineItemstatusContext);
        }
    } // class
} // namespace
