﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Misc;
using Axe.Windows.Rules.Resources;
using System;
using System.Globalization;

namespace Axe.Windows.Rules
{
    class OrCondition : Condition
    {
        private readonly Condition A;
        private readonly Condition B;

        public OrCondition(Condition a, Condition b)
        {
            this.A = a ?? throw new ArgumentNullException(nameof(a));
            this.B = b ?? throw new ArgumentNullException(nameof(b));
        }

        public override bool Matches(IA11yElement element)
        {
            return this.A.Matches(element)
                || this.B.Matches(element);
        }

        public override string ToString()
        {
            return ConditionDescriptions.Or.WithParameters(this.A.ToString(), this.B.ToString());
        }
    } // class
} // namespace
