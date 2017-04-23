using System;
using Xamarin.Auth;

namespace TaskMaster.iOS
{
    public class OAuthProviderSetting
    {
        public OAuth2Authenticator LoginWithProvider(string Provider)
        {
            OAuth2Authenticator auth = null;
            switch (Provider)
            {
                case "Google":
                {
                    auth = new OAuth2Authenticator(
                        "723494873981-qsnnp5vsa72f4d74bo8m8kqfsrbo25cq.apps.googleusercontent.com",
                        "taskmaster-163310",
                        "https://www.googleapis.com/auth/userinfo.email",
                        new Uri("https://accounts.google.com/o/oauth2/auth"),
                        new Uri("http://localhost"),
                        new Uri("https://accounts.google.com/o/oauth2/token"),
                        isUsingNativeUI: true
                    );
                    break;
                }
                case "FaceBook":
                {
                    auth = new OAuth2Authenticator(
                        "647866935403018",
                        "email",
                        new Uri("https://m.facebook.com/dialog/oauth/"),
                        new Uri("http://www.facebook.com/connect/login_success.html"),
                        isUsingNativeUI: true
                    );
                    break;
                }
            }
            return auth;
        }
    }
}

