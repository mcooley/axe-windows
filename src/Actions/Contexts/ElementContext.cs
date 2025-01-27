// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Axe.Windows.Actions.Enums;
using Axe.Windows.Core.Bases;
using Axe.Windows.Desktop.Utility;
using System;

namespace Axe.Windows.Actions.Contexts
{
    /// <summary>
    /// Class ElementContext
    /// Contain ElementContext information
    /// </summary>
    public class ElementContext : IDisposable
    {
        /// <summary>
        /// Process name of the selected element
        /// </summary>
        public string ProcessName { get; private set; }

        /// <summary>
        /// Element information
        /// </summary>
        public A11yElement Element { get; private set; }

        /// <summary>
        /// Indicate the Select Type (Live or Loaded)
        /// </summary>
        public SelectType SelectType { get; private set; }

        /// <summary>
        /// Element Context Id
        /// </summary>
        public Guid Id { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element"></param>
        public ElementContext(A11yElement element)
        {
            this.Element = element ?? throw new ArgumentNullException(nameof(element));

            if (this.Element.PlatformObject == null)
            {
                this.SelectType = SelectType.Loaded;
                this.ProcessName = "Unknown";
            }
            else
            {
                this.SelectType = SelectType.Live;
                this.ProcessName = this.Element.GetProcessName();
            }

            this.Id = Guid.NewGuid();
        }

        // Backing object for DataContext - is disposed via DataContext property
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_dataContext")]
        ElementDataContext _dataContext = null;

        /// <summary>
        /// Data context for this element context
        /// </summary>
        public ElementDataContext DataContext
        {
            get
            {
                return _dataContext;
            }

            set
            {
                _dataContext?.Dispose();
                _dataContext = value;
            }
        }

        #region IDisposable Support
        private bool disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.ProcessName = null;
                    if (this.DataContext != null)
                    {
                        this.DataContext = null;
                    }
                    else
                    {
                        this.Element.Dispose();
                    }
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
