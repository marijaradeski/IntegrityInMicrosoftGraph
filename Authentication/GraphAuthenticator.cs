using Microsoft.Identity.Client;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.Authentication
{
    public class GraphAuthenticator : IAuthenticationProvider 
    {

        private const string ClientId = "635e161e-dd62-4fad-bd53-82d1b831c150";

        private readonly string[] _scopes =
        {
        "Files.ReadWrite.All",
        "User.Read"
    };

        private readonly IPublicClientApplication _app;

        public GraphAuthenticator()
        {
            _app = PublicClientApplicationBuilder
                .Create(ClientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, "consumers")
                .WithDefaultRedirectUri()
                .Build();
        }

        private async Task<string> GetTokenAsync()
        {
            var result = await _app.AcquireTokenWithDeviceCode(_scopes, callback =>
            {
                Console.WriteLine(callback.Message);
                return Task.CompletedTask;
            }).ExecuteAsync();

            return result.AccessToken;
        }

        public async Task AuthenticateRequestAsync(
            RequestInformation request,
            Dictionary<string, object>? additionalAuthenticationContext,
            CancellationToken cancellationToken)
        {
            var token = await GetTokenAsync();
            request.Headers.Add("Authorization", $"Bearer {token}");
        }
    }
}
