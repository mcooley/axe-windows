// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Axe.Windows.Core.Attributes;
using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Types;
using System;
using UIAutomationClient;

namespace Axe.Windows.Desktop.UIAutomation.Patterns
{
    /// <summary>
    /// Control pattern wrapper for Scroll Control Pattern
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ee872087(v=vs.85).aspx
    /// </summary>
    public class ScrollPattern : A11yPattern
    {
        IUIAutomationScrollPattern Pattern;

        public ScrollPattern(A11yElement e, IUIAutomationScrollPattern p) : base(e, PatternType.UIA_ScrollPatternId)
        {
            Pattern = p;

            PopulateProperties();
        }

        private void PopulateProperties()
        {
#pragma warning disable CA2000 // Properties are disposed in A11yPattern.Dispose()
            this.Properties.Add(new A11yPatternProperty() { Name = "HorizontallyScrollable", Value = Convert.ToBoolean(this.Pattern.CurrentHorizontallyScrollable) });
            this.Properties.Add(new A11yPatternProperty() { Name = "HorizontalScrollPercent", Value = this.Pattern.CurrentHorizontalScrollPercent });
            this.Properties.Add(new A11yPatternProperty() { Name = "HorizontalViewSize", Value = this.Pattern.CurrentHorizontalViewSize });
            this.Properties.Add(new A11yPatternProperty() { Name = "VerticallyScrollable", Value = Convert.ToBoolean(this.Pattern.CurrentVerticallyScrollable) });
            this.Properties.Add(new A11yPatternProperty() { Name = "VerticalScrollPercent", Value = this.Pattern.CurrentVerticalScrollPercent });
            this.Properties.Add(new A11yPatternProperty() { Name = "VerticalViewSize", Value = this.Pattern.CurrentVerticalViewSize });
#pragma warning restore CA2000 // Properties are disposed in A11yPattern.Dispose()
        }

        //Scroll() means that it has scroll functionality. but doesn't mean that item should be focused since scroll actually happens by scrollitem
        [PatternMethod]
        public void Scroll(ScrollAmount ha, ScrollAmount va)
        {
            this.Pattern.Scroll(ha, va);
        }

        protected override void Dispose(bool disposing)
        {
            if (Pattern != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(Pattern);
                this.Pattern = null;
            }

            base.Dispose(disposing);
        }
    }
}
