﻿using System;
using System.Configuration;
using BlaiseCaseAutoComplete.Interfaces.Providers;

namespace BlaiseCaseAutoComplete.Providers
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public string ProjectId => ConfigurationManager.AppSettings["ProjectId"];

        public string SubscriptionTopicId => ConfigurationManager.AppSettings["SubscriptionTopicId"];

        public string SubscriptionId => ConfigurationManager.AppSettings["SubscriptionId"];

        public string VmName => Environment.MachineName;
    }
}
