﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Axe.Windows.Actions.Attributes;
using Axe.Windows.Actions.Contexts;
using Axe.Windows.Actions.Enums;
using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Enums;
using Axe.Windows.Core.Misc;
using Axe.Windows.Desktop.UIAutomation.TreeWalkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Axe.Windows.Actions
{
    /// <summary>
    /// Class for CaptureLiveAction
    /// Capture hierarchy tree for Live Mode data
    /// it doesn't run test.
    /// </summary>
    [InteractionLevel(UxInteractionLevel.NoUxInteraction)]
    public static class CaptureAction
    {
        // Test hooks; making this non-static and enabling constructor injection
        // would be nicer, but this was the minimal set of changes to remove the
        // need to use Fakes to test this class.
        internal static Func<ITreeWalkerForLive> NewTreeWalkerForLive = () => new TreeWalkerForLive();
        internal static Func<A11yElement, BoundedCounter, ITreeWalkerForTest> NewTreeWalkerForTest = (A11yElement e, BoundedCounter bc) => new TreeWalkerForTest(e, bc);

        // This defines the maximum number of elements that we will support in a file.
        // We added this at the suggestion of the security team as a defense against
        // malicious files. Since we never want to write a file that we can't open,
        // we enforce this limit on file saves.
        private const int MaxElements = 20000;

        #region Live mode
        /// <summary>
        /// Update Data context in given ElementContext for Live Mode
        /// if Data Context is already up to date based on parameters, just skip.
        /// </summary>
        /// <param name="ecId">Element Context Id</param>
        /// <param name="mode"></param>
        /// <param name="force">Force the update</param>
        /// <returns></returns>
        public static void SetLiveModeDataContext(Guid ecId, TreeViewMode mode, bool force = false)
        {
            SetLiveModeDataContext(ecId, mode, DefaultActionContext.GetDefaultInstance(), force);
        }

        internal static void SetLiveModeDataContext(Guid ecId, TreeViewMode mode, IActionContext actionContext, bool force = false)
        {
            var ec = actionContext.DataManager.GetElementContext(ecId);

            if (NeedNewDataContext(ec.DataContext, DataContextMode.Live, mode) || force)
            {
                var dc = new ElementDataContext(ec.Element, MaxElements);
                PopulateDataContextForLiveMode(dc, mode, actionContext);

                ec.DataContext = dc;
            }
        }

        /// <summary>
        /// Populate Data Context for Live Mode.
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="mode"></param>
        static void PopulateDataContextForLiveMode(ElementDataContext dc, TreeViewMode mode, IActionContext actionContext)
        {
            dc.TreeMode = mode;
            dc.Mode = DataContextMode.Live;
            var ltw = NewTreeWalkerForLive();
            ltw.GetTreeHierarchy(dc.Element, mode, actionContext.DesktopDataContext);
            dc.RootElment = ltw.RootElement;
            dc.Elements = ltw.Elements.ToDictionary(i => i.UniqueId);
            ltw.Elements.Clear();
        }
        #endregion

        #region Test Mode
        /// <summary>
        /// Update Data context in given ElementContext for Test Mode
        /// if Data Context is already up to date based on parameters, just skip.
        /// </summary>
        /// <param name="ecId">ElementContext Id</param>
        /// <param name="dm"></param>
        /// <param name="tvm"></param>
        /// <param name="force">Force the update</param>
        /// <returns>boolean</returns>
        public static bool SetTestModeDataContext(Guid ecId, DataContextMode dm, TreeViewMode tvm, bool force = false)
        {
            return SetTestModeDataContext(ecId, dm, tvm, DefaultActionContext.GetDefaultInstance(), force);
        }

        internal static bool SetTestModeDataContext(Guid ecId, DataContextMode dm, TreeViewMode tvm, IActionContext actionContext, bool force = false)
        {
            var ec = actionContext.DataManager.GetElementContext(ecId);
            // if Data context is set via Live Mode, set it to null.
            if (ec.DataContext != null && ec.DataContext.Mode == DataContextMode.Live)
            {
                ec.DataContext = null;
                // Re-register user-configured custom UIA data
                actionContext.Registrar.RestoreCustomPropertyRegistrations();
            }

            if (NeedNewDataContext(ec.DataContext, dm, tvm) || force)
            {
                ec.DataContext = new ElementDataContext(ec.Element, MaxElements);
                PopulateData(ec.DataContext, dm, tvm, actionContext);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Populate Data
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="dcMode"></param>
        /// <param name="tm"></param>
        internal static void PopulateData(ElementDataContext dc, DataContextMode dcMode, TreeViewMode tm, IActionContext actionContext)
        {
            dc.TreeMode = tm;
            dc.Mode = dcMode;

            switch (dcMode)
            {
                case DataContextMode.Test:
                    var stw = NewTreeWalkerForTest(dc.Element, dc.ElementCounter);
                    stw.RefreshTreeData(tm, actionContext.DesktopDataContext);
                    dc.Elements = stw.Elements.ToDictionary(l => l.UniqueId);
                    dc.RootElment = stw.TopMostElement;
                    break;
                case DataContextMode.Load:
                    dc.RootElment = dc.Element.GetOriginAncestor();
                    PopulateElementAndDescendentsListFromLoadedData(dc);
                    break;
            }
        }

        /// <summary>
        /// Get List of A11yElements from loaded data
        /// </summary>
        static void PopulateElementAndDescendentsListFromLoadedData(ElementDataContext dc)
        {
            dc.Elements = new Dictionary<int, A11yElement>();
            AddElementAndChildrenIntoList(dc.RootElment, dc.Elements, dc.ElementCounter);
        }

        /// <summary>
        /// Add element and children into the list.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="dic"></param>
        /// <param name="elementCounter">Provides an upper bound on the number of elements we'll allow to be loaded</param>
        internal static void AddElementAndChildrenIntoList(A11yElement e, Dictionary<int, A11yElement> dic, BoundedCounter elementCounter)
        {
            if (!elementCounter.TryIncrement())
                return;

            dic.Add(e.UniqueId, e);

            if (e.Children != null && e.Children.Count != 0)
            {
                foreach (var c in e.Children)
                {
                    AddElementAndChildrenIntoList(c, dic, elementCounter);
                }
            }
        }

        private static bool NeedNewDataContext(ElementDataContext dc, DataContextMode sm, TreeViewMode tm)
        {
            return dc == null || (dc.Mode != DataContextMode.Load && (dc.Mode != sm || dc.TreeMode != tm));
        }
        #endregion
    }
}
