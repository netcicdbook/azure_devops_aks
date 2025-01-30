namespace Components.Communication.MessageBroker.Configuration
{
    public class RabbitMqHostOptions
    {
        public string Host { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
