﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Types;
using Axe.Windows.Rules.Extensions;
using Axe.Windows.Rules.Resources;
using System;

namespace Axe.Windows.Rules.PropertyConditions
{
    /// <summary>
    /// Contains commonly used conditions for testing against the BoundingRectangle property of an IA11yElement.
    /// </summary>
    static class BoundingRectangle
    {
        private const int MinimumArea = 25;

        /// <summary>
        /// margin of error for comparing rectangles.
        /// Using a margin allows small boundary cases not to be marked as errors.
        /// increased margin from 30 to 35 since there is HDPI impact.
        /// </summary>
        public const int OverlapMargin = 35;

        public static Condition Null = Condition.Create(IsBoundingRectangleNull);
        public static Condition NotNull = ~Null;
        public static Condition Empty = Condition.Create(IsBoundingRectangleEmpty);
        public static Condition NotEmpty = ~Empty;
        public static Condition Valid = Condition.Create(HasValidArea);
        public static Condition NotValid = ~Valid;
        public static Condition CorrectDataFormat = Condition.Create(HasCorrectDataFormat);
        public static Condition NotCorrectDataFormat = ~CorrectDataFormat;
        public static Condition CompletelyObscuresContainer = Condition.Create(ElementCompletelyObscuresContainer, ConditionDescriptions.BoundingRectangleCompletelyObscuresContainer);
        public static Condition TelerikSparklineItemstatusContext = Condition.Create(ElementItemStatusContainsTelerikSparklineContext);

        private static bool IsBoundingRectangleNull(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));
            return !e.TryGetPropertyValue(PropertyType.UIA_BoundingRectanglePropertyId, out double[] _);
        }

        private static bool HasCorrectDataFormat(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            if (!e.TryGetPropertyValue(PropertyType.UIA_BoundingRectanglePropertyId, out double[] array)) return false;
            if (array == null) return false;

            return array.Length == 4;
        }

        private static bool IsBoundingRectangleEmpty(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            // Because IA11yElement.BoundingRectangle will return an empty rectangle when the property value is null,
            // we are expecting that this function is not called unless the nullity of the property has already been checked.

            return e.BoundingRectangle.IsEmpty;
        }

        private static bool HasValidArea(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            return (e.BoundingRectangle.Width * e.BoundingRectangle.Height) >= MinimumArea;
        }

        private static bool ElementCompletelyObscuresContainer(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            var container = e.FindContainerElement();
            if (container == null) throw new InvalidProgramException(ErrorMessages.ExpectedValidAncestor);

            return e.BoundingRectangle.CompletelyObscures(container.BoundingRectangle);
        }

        private static bool ElementItemStatusContainsTelerikSparklineContext(IA11yElement e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            return e.ItemStatus?.Contains("<Property Name=\"DataContext\" Value=\"Telerik.Windows.Controls.Sparklines.SparklineColumnDataPoint\" />") == true;
        }
    } // class
} // namespace
