// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Enums;
using Axe.Windows.Rules.PropertyConditions;
using Axe.Windows.Rules.Resources;
using System;
using static Axe.Windows.Rules.PropertyConditions.Relationships;

namespace Axe.Windows.Rules.Library
{
    [RuleInfo(ID = RuleId.LandmarkBannerIsTopLevel)]
    class LandmarkBannerIsTopLevel : Rule
    {
        public LandmarkBannerIsTopLevel()
        {
            this.Info.Description = Descriptions.LandmarkBannerIsTopLevel;
            this.Info.HowToFix = HowToFix.LandmarkBannerIsTopLevel;
            this.Info.Standard = A11yCriteriaId.InfoAndRelationships;
            this.Info.ErrorCode = EvaluationCode.Error;
        }

        public override bool PassesTest(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            var stopCondition = Condition.False;

            return !AnyAncestor(Landmarks.Any, stopCondition).Matches(e);
        }

        protected override Condition CreateCondition()
        {
            return Landmarks.Banner;
        }
    } // class
} // namespace
