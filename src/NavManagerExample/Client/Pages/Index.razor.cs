using Microsoft.AspNetCore.Components;

namespace NavManagerExample.Client.Pages
{
    public partial class Index
    {
        [Inject]
        NavManagerExample.Client.Services.CancelableNavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            NavigationManager.BeforeLocationChange += NavigationManager_BeforeLocationChange;
        }

        private void NavigationManager_BeforeLocationChange(object sender, Models.Navigation e)
        {
            if (e.NewLocation.Contains("counter"))
            {
                e.IsCanceled = true;
            }
        }
    }
}
