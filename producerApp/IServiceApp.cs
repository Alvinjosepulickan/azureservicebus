namespace producerApp {
    public interface IServiceApp {
        Task Publish<T>(T obj);
        Task Publish(string str);
    }
}
