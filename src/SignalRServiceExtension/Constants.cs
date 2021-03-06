﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Azure.WebJobs.Extensions.SignalRService
{
    internal static class Constants
    {
        public const string AzureSignalRConnectionStringName = "AzureSignalRConnectionString";
        public const string ServiceTransportTypeName = "AzureSignalRServiceTransportType";
        public const string AsrsHeaderPrefix = "X-ASRS-";
        public const string AsrsConnectionIdHeader = AsrsHeaderPrefix + "Connection-Id";
        public const string AsrsUserClaims = AsrsHeaderPrefix + "User-Claims";
        public const string AsrsUserId = AsrsHeaderPrefix + "User-Id";
        public const string AsrsHubNameHeader = AsrsHeaderPrefix + "Hub";
        public const string AsrsCategory = AsrsHeaderPrefix + "Category";
        public const string AsrsEvent = AsrsHeaderPrefix + "Event";
        public const string AsrsClientQueryString = AsrsHeaderPrefix + "Client-Query";
        public const string AsrsSignature = AsrsHeaderPrefix + "Signature";
        public const string JsonContentType = "application/json";
        public const string MessagePackContentType = "application/x-msgpack";
    }

    public static class Category
    {
        public const string Connections = "connections";
        public const string Messages = "messages";
    }

    public static class Event
    {
        public const string Connect = "connect";
        public const string Disconnect = "disconnect";
    }
}
