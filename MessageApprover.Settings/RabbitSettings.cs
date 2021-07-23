namespace MessageApprover.Settings
{
    public static class RabbitSettings
    {     
        public const string RabbitMqUri = "rabbitmq://messageapprover_rabbitmq/";
        public const string UserName = "admin";
        public const string Password = "admin";
        public const string SagaQueue = "MessageApprover.Messages";
    }
}
