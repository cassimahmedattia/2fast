// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
#if WINDOWS_UWP
using Project2FA.UWP;
using Windows.UI.Xaml.Data;
#else
using Microsoft.UI.Xaml.Data;
#endif

namespace Project2FA.Controls
{
    /// <summary>
    /// A collection view implementation that supports filtering, grouping, sorting and incremental loading
    /// </summary>
    public partial class AdvancedCollectionView
    {
        /// <summary>
        /// Currently selected item changing event
        /// </summary>
        /// <param name="e">event args</param>
        private void OnCurrentChanging(CurrentChangingEventArgs e)
        {
            if (_deferCounter > 0)
            {
                return;
            }

            CurrentChanging?.Invoke(this, e);
        }

        /// <summary>
        /// Currently selected item changed event
        /// </summary>
        /// <param name="e">event args</param>
        private void OnCurrentChanged(object e)
        {
            if (_deferCounter > 0)
            {
                return;
            }
            try
            {
                CurrentChanged?.Invoke(this, e);
            }
            catch (Exception exc)
            {
                //TODO should be ignored?
#if WINDOWS_UWP
                TrackingManager.TrackExceptionCatched("ACV " + nameof(OnCurrentChanged), exc);
#endif
            }
            

            // ReSharper disable once ExplicitCallerInfoArgument
            OnPropertyChanged(nameof(CurrentItem));
        }

        /// <summary>
        /// Vector changed event
        /// </summary>
        /// <param name="e">event args</param>
        private void OnVectorChanged(IVectorChangedEventArgs e)
        {
            if (_deferCounter > 0)
            {
                return;
            }

            VectorChanged?.Invoke(this, e);

            // ReSharper disable once ExplicitCallerInfoArgument
            OnPropertyChanged(nameof(Count));
        }
    }
}