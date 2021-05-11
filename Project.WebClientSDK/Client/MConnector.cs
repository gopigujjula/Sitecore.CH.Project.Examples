using Stylelabs.M.Sdk.WebClient;
using Stylelabs.M.Sdk.WebClient.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sitecore.CH.Project.WebClientSDK.Examples.Client
{
    internal static class MConnector
    {
        internal static IWebMClient Client()
        {
            // Enter your credentials here
            OAuthPasswordGrant oauth = new OAuthPasswordGrant
            {
                ClientId = AppSettings.ClientId,
                ClientSecret = AppSettings.ClientSecret,
                UserName = AppSettings.Username,
                Password = AppSettings.Password
            };

            // Create the Web SDK client
            return MClientFactory.CreateMClient(AppSettings.Host, oauth);
        }
    }
}
