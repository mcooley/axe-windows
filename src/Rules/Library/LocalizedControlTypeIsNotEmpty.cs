// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Enums;
using Axe.Windows.Core.Types;
using Axe.Windows.Rules.Resources;
using System;
using static Axe.Windows.Rules.PropertyConditions.BoolProperties;
using static Axe.Windows.Rules.PropertyConditions.StringProperties;

namespace Axe.Windows.Rules.Library
{
    [RuleInfo(ID = RuleId.LocalizedControlTypeNotEmpty)]
    class LocalizedControlTypeIsNotEmpty : Rule
    {
        public LocalizedControlTypeIsNotEmpty()
        {
            this.Info.Description = Descriptions.LocalizedControlTypeNotEmpty;
            this.Info.HowToFix = HowToFix.LocalizedControlTypeNotEmpty;
            this.Info.Standard = A11yCriteriaId.ObjectInformation;
            this.Info.PropertyID = PropertyType.UIA_LocalizedControlTypePropertyId;
            this.Info.ErrorCode = EvaluationCode.Error;
        }

        public override bool PassesTest(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            return LocalizedControlType.NotEmpty.Matches(e);
        }

        protected override Condition CreateCondition()
        {
            return IsKeyboardFocusable & LocalizedControlType.NotNull;
        }
    } // class
} // namespace
