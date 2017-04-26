using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Auth;

namespace TaskMaster.Droid
{
    [Activity(Label = "ActivityCustomUrlSchemeInterceptor")]
    [
        // App Linking - custom url schemes
        IntentFilter
        (
            new[] { Intent.ActionView },
            Categories = new[]
            {
                Intent.CategoryDefault,
                Intent.CategoryBrowsable
            },
            DataSchemes = new[]
            {
                "http"
            },
            DataHost = "localhost"
        )
    ]
    public class ActivityCustomUrlSchemeInterceptor : Activity
    {
        string message;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            Android.Net.Uri uri_android = Intent.Data;
            
            Uri uri = new Uri(uri_android.ToString());
            IDictionary<string, string> fragment = Xamarin.Utilities.WebEx.FormDecode(uri.Fragment);

            Account account = new Account
            (
                "username",
                new Dictionary<string, string>(fragment)
            );

            AuthenticatorCompletedEventArgs args_completed = new AuthenticatorCompletedEventArgs(account);

            if (LoginRenderer.Auth != null)
            {
                // call OnSucceeded to trigger OnCompleted event
                LoginRenderer.Auth.OnSucceeded(account);
            }
            
            Finish();
        }
    }
}