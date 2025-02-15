﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Axe.Windows.Core.Misc;
using Axe.Windows.Rules.Resources;

namespace Axe.Windows.Rules.PropertyConditions
{
    static class StringProperties
    {
        public static StringProperty AutomationID = new StringProperty(e => e.AutomationId);
        public static StringProperty ClassName = new StringProperty(e => e.ClassName);
        public static StringProperty Framework = new StringProperty(e => e.GetUIFramework());
        public static StringProperty HelpText = new StringProperty(e => e.HelpText, ConditionDescriptions.HelpText);
        public static StringProperty ItemStatus = new StringProperty(e => e.ItemStatus);
        public static StringProperty LocalizedControlType = new StringProperty(e => e.LocalizedControlType, ConditionDescriptions.LocalizedControlType);
        public static StringProperty LocalizedLandmarkType = new StringProperty(e => e.LocalizedLandmarkType, ConditionDescriptions.LocalizedControlType);
        public static StringProperty Name = new StringProperty(e => e.Name, ConditionDescriptions.Name);
        public static StringProperty ProcessName = new StringProperty(e => e.ProcessName);
    } // class
} // namespace
