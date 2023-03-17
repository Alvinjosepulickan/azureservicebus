using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer {
    public class ConsumerService:BackgroundService {
        private readonly ISubscriptionClient _subscriptionClient;
        public ConsumerService(ISubscriptionClient subscriptionClient)
        {
            _subscriptionClient = subscriptionClient;
        }

        protected override  Task ExecuteAsync(CancellationToken stoppingToken) {
            _subscriptionClient.RegisterMessageHandler((message, token) => {
                var custObjString = Encoding.UTF8.GetString(message.Body);
                var customer = JsonConvert.DeserializeObject<Customer>(custObjString);
                return _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
            },
            new MessageHandlerOptions(args =>Task.CompletedTask));
            return Task.CompletedTask;
        }
    }
}
