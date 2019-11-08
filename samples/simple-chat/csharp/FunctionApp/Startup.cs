﻿using System;
using System.Security.Claims;
using FunctionApp;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.IdentityModel.Tokens;

[assembly: FunctionsStartup(typeof(Startup))]
namespace FunctionApp
{
    /// <summary>
    /// Runs when the Azure Functions host starts. Microsoft.NET.Sdk.Functions package version 1.0.28 or later
    /// </summary>
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Get the configuration files for the OAuth token issuer
            //var issuerToken = Environment.GetEnvironmentVariable("IssuerToken");

            // only for sample
            var issuerToken = "bXlmdW5jdGlvbmF1dGh0ZXN0"; // base64 encoded for "myfunctionauthtest";

            // Register the access token provider as a singleton, customer can register one's own
            // builder.AddAuth(new AccessTokenProvider());
            builder.AddAuth(parameters =>
            {
                parameters.IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(issuerToken));
                // for sample only
                parameters.RequireSignedTokens = false;
                parameters.ValidateAudience = false;
                parameters.ValidateIssuer = false;
                parameters.ValidateIssuerSigningKey = false;
                parameters.ValidateLifetime = false;
            }, (accessTokenResult, httpRequest, signalRConnectionDetail) =>
            {
                if (accessTokenResult.Status == AccessTokenStatus.Valid)
                {
                    // resolve the identity
                    var identity = accessTokenResult.Principal.Identity.Name;

                    // generate custom claim
                    var myHeader = httpRequest.Headers["myheader"];
                    var customClaim = new Claim("myheader", myHeader);

                    // update connection info detail
                    signalRConnectionDetail.UserId = identity;
                    signalRConnectionDetail.Claims?.Add(customClaim);

                    // binding will generate ASRS negotiate response inside with this new signalRsignalRConnectionDetail,
                    // now you can keep your negotiate function clean
                    return signalRConnectionDetail;
                }

                // todo: improve the interface
                signalRConnectionDetail.Error = "Error while validating negotiate function token";
                return signalRConnectionDetail;
            });
        }
    }
}