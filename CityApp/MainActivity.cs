using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace CityApp
{
    [Activity(Label = "CityApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
            Finish();
            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.task);
        }
    }
}

