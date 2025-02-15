// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Enums;
using Axe.Windows.Rules.PropertyConditions;
using Axe.Windows.Rules.Resources;
using System;
using static Axe.Windows.Rules.PropertyConditions.ControlType;
using static Axe.Windows.Rules.PropertyConditions.Relationships;

namespace Axe.Windows.Rules.Library
{
    [RuleInfo(ID = RuleId.ControlShouldSupportExpandCollapsePattern)]
    class ControlShouldSupportExpandCollapsePattern : Rule
    {
        public ControlShouldSupportExpandCollapsePattern()
        {
            this.Info.Description = Descriptions.ControlShouldSupportExpandCollapsePattern;
            this.Info.HowToFix = HowToFix.ControlShouldSupportExpandCollapsePattern;
            this.Info.Standard = A11yCriteriaId.AvailableActions;
            this.Info.ErrorCode = EvaluationCode.Error;
        }

        public override bool PassesTest(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            return Patterns.ExpandCollapse.Matches(e);
        }

        protected override Condition CreateCondition()
        {
            var elligbleSplitButton = SplitButton & ~Parent(SplitButton);
            var complexComboBox = ComboBox & ~PlatformProperties.SimpleStyle;
            return AppBar | complexComboBox | elligbleSplitButton | (TreeItem & TreeItemChildrenExist);
        }
    } // class
} // namespace
