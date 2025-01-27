// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Enums;
using Axe.Windows.Core.Types;
using Axe.Windows.Rules.PropertyConditions;
using Axe.Windows.Rules.Resources;
using static Axe.Windows.Rules.PropertyConditions.StringProperties;

namespace Axe.Windows.Rules.Library
{
    [RuleInfo(ID = RuleId.NameOnOptionalType)]
    class NameOnOptionalType : Rule
    {
        public NameOnOptionalType()
        {
            this.Info.Description = Descriptions.NameOnOptionalType;
            this.Info.HowToFix = HowToFix.NameOnOptionalType;
            this.Info.Standard = A11yCriteriaId.ObjectInformation;
            this.Info.PropertyID = PropertyType.UIA_NamePropertyId;
            this.Info.ErrorCode = EvaluationCode.NeedsReview;
        }

        public override bool PassesTest(IA11yElement e)
        {
            return Name.NotNullOrEmpty.Matches(e);
        }

        protected override Condition CreateCondition()
        {
            return ElementGroups.NameOptional;
        }
    } // class
} // namespace
