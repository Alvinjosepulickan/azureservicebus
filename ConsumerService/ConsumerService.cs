using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace ConsumerService {
    public class ConsumerService:BackgroundService
                                {
        private readonly ISubscriptionClient _subscriptionClient;
        public ConsumerService(ISubscriptionClient subscriptionClient) {
            _subscriptionClient = subscriptionClient;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) {
            _subscriptionClient.RegisterMessageHandler((message, token) => {
                var custObjString = Encoding.UTF8.GetString(message.Body);
                var customer = JsonConvert.DeserializeObject<Customer>(custObjString);
                return _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
            },
            new MessageHandlerOptions(args => Task.CompletedTask));
            return Task.CompletedTask;
        }
        public  Task ExecuteAsync() {
            var p = string.Empty;
            _subscriptionClient.RegisterMessageHandler((message, token) => {
                var custObjString = Encoding.UTF8.GetString(message.Body);
                var customer = JsonConvert.DeserializeObject<Customer>(custObjString);
                p = customer.FullName;
                Console.WriteLine(customer.FullName);
                return _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
            },
            new MessageHandlerOptions(args => Task.CompletedTask));
            return Task.CompletedTask;
        }
    }
}
