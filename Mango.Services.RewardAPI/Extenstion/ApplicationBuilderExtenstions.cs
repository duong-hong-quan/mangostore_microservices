﻿using Mango.Services.RewardAPI.Messaging;
using Microsoft.Identity.Client;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Mango.Services.RewardAPI.Extenstion
{
    public static class ApplicationBuilderExtenstions
    {

        private static IAzureServiceBusConsumer ServiceBusConsumer { get; set; }


        public static IApplicationBuilder UserAzureServiceBusConsumer(this IApplicationBuilder app)
        {
            ServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLife.ApplicationStarted.Register(OnStart);
            hostApplicationLife.ApplicationStopping.Register(OnStop);

            return app;
        }

        private static void OnStart()
        {
            ServiceBusConsumer.Start();

        }

        private static void OnStop()
        {
            ServiceBusConsumer.Stop();

        }
    }
}
