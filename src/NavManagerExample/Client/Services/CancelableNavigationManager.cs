using Microsoft.AspNetCore.Components.Routing;
using NavManagerExample.Client.Models;
using System;

namespace NavManagerExample.Client.Services
{
    public class CancelableNavigationManager : Microsoft.AspNetCore.Components.NavigationManager
    {
        private Microsoft.AspNetCore.Components.NavigationManager _UnderlyingNavigationManager;

        public event EventHandler<Navigation> BeforeLocationChange;

        public CancelableNavigationManager(Microsoft.AspNetCore.Components.NavigationManager underlyingNavigationManager)
        {
            _UnderlyingNavigationManager = underlyingNavigationManager;

            base.Initialize(underlyingNavigationManager.BaseUri, underlyingNavigationManager.Uri);

            _UnderlyingNavigationManager.LocationChanged += OnUnderlyingNavigationManagerLocationChanged;
        }

        protected override void EnsureInitialized()
        {
            base.Initialize(_UnderlyingNavigationManager.BaseUri, _UnderlyingNavigationManager.Uri);
        }

        protected override void NavigateToCore(string uri, bool forceLoad)
        {
            // Call the underlying navigation manager.
            _UnderlyingNavigationManager.NavigateTo(uri, forceLoad);
        }


        private Navigation NotifyBeforeLocationChange(LocationChangedEventArgs e)
        {
            var navigation = new Navigation()
            {
                CurrentLocation = this.Uri,
                NewLocation = e.Location,
                IsNavigationIntercepted = e.IsNavigationIntercepted,
                IsCanceled = false
            };

            BeforeLocationChange?.Invoke(this, navigation);

            return navigation;
        }

        private void OnUnderlyingNavigationManagerLocationChanged(object sender, LocationChangedEventArgs e)
        {
            var navigation = NotifyBeforeLocationChange(e);

            if (navigation.IsCanceled)
            {
                // Puts the correct link back - else it will change, but the page will not navigate.
                _UnderlyingNavigationManager.NavigateTo(this.Uri, false);
                return;
            }

            // NOTE: Must be set before calling notify location changed, as it will use this uri property in its args.
            this.Uri = e.Location;

            this.NotifyLocationChanged(e.IsNavigationIntercepted);
        }


    }
}
