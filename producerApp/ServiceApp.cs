using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace producerApp {
    public class ServiceApp: IServiceApp {
        private readonly ITopicClient _topicClient;
        public ServiceApp(ITopicClient topicClient)
        {
            _topicClient = topicClient;
        }

        public async Task Publish<T>(T obj) {
            var message=JsonConvert.SerializeObject(obj);
            var messageObj= new Message(Encoding.UTF8.GetBytes(message));
            messageObj.UserProperties["messageType"] = typeof(T).Name;
            await _topicClient.SendAsync(messageObj);
        }
        public async Task Publish(string str) {
            var message = new Message(Encoding.UTF8.GetBytes(str));
            await _topicClient.SendAsync(message);
        }
    }
}
