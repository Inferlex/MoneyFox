namespace MoneyFox.Ui;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Microsoft.Identity.Client;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;
using MoneyFox.Infrastructure.DbBackup.Legacy;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        ParentActivityWrapper.ParentActivity = this;
        base.OnCreate(savedInstanceState);
        Platform.Init(activity: this, bundle: savedInstanceState);
    }

    protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data)
    {
        base.OnActivityResult(requestCode: requestCode, resultCode: resultCode, data: data);
        AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode: requestCode, resultCode: resultCode, data: data);
    }

    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
    {
        Platform.OnRequestPermissionsResult(requestCode: requestCode, permissions: permissions, grantResults: grantResults);
        base.OnRequestPermissionsResult(requestCode: requestCode, permissions: permissions, grantResults: grantResults);
    }
}
